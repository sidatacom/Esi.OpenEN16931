[CmdletBinding()]
param(
	[switch]$FetchOnly,
	[bool]$CloneMissing = $true,
	[string]$Remote = 'origin'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Invoke-Git {
	param(
		[Parameter(Mandatory = $true)]
		[string[]]$Arguments,
		[string]$WorkingDirectory = (Get-Location).Path
	)

	$stdoutFile = [System.IO.Path]::GetTempFileName()
	$stderrFile = [System.IO.Path]::GetTempFileName()
	try {
		$argumentList = @('-c', 'core.longpaths=true', '-C', $WorkingDirectory) + $Arguments
		$process = Start-Process -FilePath git -ArgumentList $argumentList -NoNewWindow -Wait -PassThru -RedirectStandardOutput $stdoutFile -RedirectStandardError $stderrFile
		$stdout = if (Test-Path $stdoutFile) { Get-Content -Path $stdoutFile -Raw } else { '' }
		$stderr = if (Test-Path $stderrFile) { Get-Content -Path $stderrFile -Raw } else { '' }

		if ($process.ExitCode -ne 0) {
			throw "git $($Arguments -join ' ') failed in '$WorkingDirectory': $stderr"
		}

		if ([string]::IsNullOrWhiteSpace($stdout)) {
			return @()
		}

		return ($stdout -split '\r?\n' | Where-Object { -not [string]::IsNullOrWhiteSpace($_) })
	}
	finally {
		Remove-Item -Path $stdoutFile, $stderrFile -ErrorAction SilentlyContinue
	}
}

function Get-RemoteDefaultBranch {
	param(
		[Parameter(Mandatory = $true)]
		[string]$RepositoryUrl,
		[string]$RemoteName = 'origin'
	)

	$headInfo = & git -c core.longpaths=true ls-remote --symref $RepositoryUrl HEAD 2>$null
	if ($LASTEXITCODE -eq 0) {
		foreach ($line in $headInfo) {
			if ($line -match '^ref:\s+refs/heads/(.+?)\s+HEAD$') {
				return $Matches[1]
			}
		}
	}

	foreach ($candidate in @('master', 'main')) {
		$heads = & git -c core.longpaths=true ls-remote --heads $RepositoryUrl $candidate 2>$null
		if ($LASTEXITCODE -eq 0 -and $heads) {
			return $candidate
		}
	}

	throw "Could not determine a default branch for '$RepositoryUrl'."
}

function Get-SubmoduleEntries {
	param(
		[Parameter(Mandatory = $true)]
		[string]$RepositoryPath
	)

	$lines = Invoke-Git -Arguments @('config', '--file', '.gitmodules', '--get-regexp', '^submodule\..*\.path$') -WorkingDirectory $RepositoryPath
	foreach ($line in $lines) {
		if ($line -match '^submodule\.(.+?)\.path\s+(.*)$') {
			$name = $Matches[1]
			$path = $Matches[2]
			$url = Invoke-Git -Arguments @('config', '--file', '.gitmodules', '--get', "submodule.$name.url") -WorkingDirectory $RepositoryPath
			[pscustomobject]@{
				Name = $name
				Path = $path
				Url  = $url.Trim()
			}
		}
	}
}

function Get-DefaultBranch {
	param(
		[Parameter(Mandatory = $true)]
		[string]$RepositoryPath,
		[string]$RemoteName = 'origin'
	)

	$remoteInfo = Invoke-Git -Arguments @('remote', 'show', $RemoteName) -WorkingDirectory $RepositoryPath
	foreach ($line in $remoteInfo) {
		if ($line -match '^\s*HEAD branch:\s*(.+)$') {
			$branch = $Matches[1].Trim()
			if ($branch -and $branch -ne '(unknown)') {
				return $branch
			}
		}
	}

	foreach ($candidate in @('master', 'main')) {
		$heads = & git -C $RepositoryPath ls-remote --heads $RemoteName $candidate 2>$null
		if ($LASTEXITCODE -eq 0 -and $heads) {
			return $candidate
		}
	}

	throw "Could not determine a default branch for '$RepositoryPath'."
}

function Ensure-Repository {
	param(
		[Parameter(Mandatory = $true)]
		[pscustomobject]$Entry,
		[Parameter(Mandatory = $true)]
		[string]$RepositoryRoot
	)

	$submodulePath = Join-Path $RepositoryRoot $Entry.Path
	if (Test-Path (Join-Path $submodulePath '.git')) {
		return $submodulePath
	}

	if (-not (Test-Path $submodulePath)) {
		if ($CloneMissing) {
			$defaultBranch = Get-RemoteDefaultBranch -RepositoryUrl $Entry.Url
			Write-Host "Cloning $($Entry.Name) to $($Entry.Path)"
			Invoke-Git -Arguments @('clone', '--recursive', '--branch', $defaultBranch, '--single-branch', $Entry.Url, $Entry.Path) -WorkingDirectory $RepositoryRoot | Out-Null
			return $submodulePath
		}

		Write-Warning "Skipping missing submodule path without local repository: $($Entry.Path)"
		return $null
	}

	throw "Path exists but is not a git repository: $submodulePath"
}
$repoRoot = (Invoke-Git -Arguments @('rev-parse', '--show-toplevel')).Trim()
if (-not $repoRoot) {
	throw 'Could not determine repository root.'
}

Write-Host "Repository root: $repoRoot"
Write-Host 'Synchronizing all submodules...'
Invoke-Git -Arguments @('submodule', 'sync', '--recursive') -WorkingDirectory $repoRoot | Out-Null

$submoduleEntries = Get-SubmoduleEntries -RepositoryPath $repoRoot

if (-not $submoduleEntries) {
	Write-Host 'No submodules found.'
	return
}

$totalSubmodules = $submoduleEntries.Count
Write-Host "Found $totalSubmodules submodule(s)."

$entryIndex = 0
foreach ($entry in $submoduleEntries) {
	$entryIndex++
	Write-Host "[$entryIndex/$totalSubmodules] Processing $($entry.Path)"
	Write-Progress -Activity 'Processing submodules' -Status "$entryIndex / $totalSubmodules : $($entry.Path)" -PercentComplete ([math]::Round(($entryIndex / [math]::Max(1, $totalSubmodules)) * 100, 0))
	$submodulePath = Ensure-Repository -Entry $entry -RepositoryRoot $repoRoot
	if (-not $submodulePath) {
		continue
	}

	Write-Host "  Fetching $($entry.Path)"
	Invoke-Git -Arguments @('fetch', '--prune', $Remote) -WorkingDirectory $submodulePath | Out-Null

	if ($FetchOnly) {
		continue
	}

	try {
		$defaultBranch = Get-DefaultBranch -RepositoryPath $submodulePath -RemoteName $Remote
		Write-Host "  Resetting $defaultBranch in $($entry.Path)"
		Invoke-Git -Arguments @('checkout', $defaultBranch) -WorkingDirectory $submodulePath | Out-Null
		Invoke-Git -Arguments @('reset', '--hard', "$Remote/$defaultBranch") -WorkingDirectory $submodulePath | Out-Null
		Invoke-Git -Arguments @('clean', '-fdx') -WorkingDirectory $submodulePath | Out-Null
	}
	catch {
		Write-Warning "Skipping update for $($entry.Path): $($_.Exception.Message)"
	}
}

Write-Progress -Activity 'Processing submodules' -Completed

Write-Host 'Done.'
