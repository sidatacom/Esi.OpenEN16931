[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High')]
param(
	[int]$OlderThanMonths = 12,
	[switch]$Delete
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

$repoRoot = (Invoke-Git -Arguments @('rev-parse', '--show-toplevel')).Trim()
if (-not $repoRoot) {
	throw 'Could not determine repository root.'
}

$cutoff = (Get-Date).AddMonths(-$OlderThanMonths)
$entries = Invoke-Git -Arguments @('config', '--file', '.gitmodules', '--get-regexp', '^submodule\..*\.path$') -WorkingDirectory $repoRoot
$paths = foreach ($line in $entries) {
	if ($line -match '^submodule\.(.+?)\.path\s+(.*)$') {
		[pscustomobject]@{
			Name = $Matches[1]
			Path = $Matches[2]
			Url  = (Invoke-Git -Arguments @('config', '--file', '.gitmodules', '--get', "submodule.$($Matches[1]).url") -WorkingDirectory $repoRoot).Trim()
		}
	}
}

$stalePaths = New-Object System.Collections.Generic.List[object]
foreach ($entry in $paths) {
	$repoPath = Join-Path $repoRoot $entry.Path
	if (-not (Test-Path $repoPath)) {
		continue
	}

	$dateText = & git -c core.longpaths=true -C $repoPath log -1 --format=%ci 2>$null
	if ($LASTEXITCODE -ne 0 -or -not $dateText) {
		continue
	}

	$lastCommit = [datetime]::Parse($dateText)
	if ($lastCommit -lt $cutoff) {
		$stalePaths.Add([pscustomobject]@{
			Name = $entry.Name
			Path = $entry.Path
			LastCommit = $lastCommit
		})
	}
}

if ($stalePaths.Count -eq 0) {
	Write-Host 'No stale submodule folders found.'
	return
}

Write-Host "Found $($stalePaths.Count) stale submodule folder(s) older than $OlderThanMonths months."
foreach ($item in $stalePaths) {
	Write-Host ("{0} | {1:yyyy-MM-dd} | {2}" -f $item.Path, $item.LastCommit, $item.Name)
}

if (-not $Delete) {
	Write-Host 'Dry run only. Re-run with -Delete to delete the folders.'
	return
}

foreach ($item in $stalePaths) {
	$folder = Join-Path $repoRoot $item.Path
	if (Test-Path $folder) {
		if ($PSCmdlet.ShouldProcess($folder, 'Remove stale submodule folder')) {
			Remove-Item -Path $folder -Recurse -Force
			Write-Host "Removed $($item.Path)"
		}
	}
}
