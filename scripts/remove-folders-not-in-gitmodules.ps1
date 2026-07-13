[CmdletBinding(SupportsShouldProcess = $true, ConfirmImpact = 'High')]
param(
	[switch]$Delete,
	[string]$RepositoryRoot
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

function Test-IsAncestorPath {
	param(
		[Parameter(Mandatory = $true)]
		[string]$Candidate,
		[Parameter(Mandatory = $true)]
		[string]$Path
	)

	$normalizedCandidate = $Candidate.TrimEnd('/')
	$normalizedPath = $Path.TrimEnd('/')

	return $normalizedPath.StartsWith($normalizedCandidate + '/', [System.StringComparison]::OrdinalIgnoreCase)
}

if (-not $RepositoryRoot) {
	$RepositoryRoot = (Invoke-Git -Arguments @('rev-parse', '--show-toplevel')).Trim()
}

if (-not $RepositoryRoot) {
	throw 'Could not determine repository root.'
}

$gitmodulesPath = Join-Path $RepositoryRoot '.gitmodules'
if (-not (Test-Path $gitmodulesPath)) {
	throw '.gitmodules was not found in the repository root.'
}

$lines = Invoke-Git -Arguments @('config', '--file', '.gitmodules', '--get-regexp', '^submodule\..*\.path$') -WorkingDirectory $RepositoryRoot
$referencedPaths = New-Object 'System.Collections.Generic.HashSet[string]' ([System.StringComparer]::OrdinalIgnoreCase)
foreach ($line in $lines) {
	if ($line -match '^submodule\.(.+?)\.path\s+(.*)$') {
		$path = $Matches[2].Trim().Replace('\', '/')
		[void]$referencedPaths.Add($path)
	}
}

if ($referencedPaths.Count -eq 0) {
	Write-Host 'No submodule paths were found in .gitmodules.'
	return
}

$directories = Get-ChildItem -Path $RepositoryRoot -Directory -Recurse | Sort-Object { $_.FullName.Length }
$toRemove = New-Object 'System.Collections.Generic.List[object]'
$selectedForRemoval = New-Object 'System.Collections.Generic.HashSet[string]' ([System.StringComparer]::OrdinalIgnoreCase)

foreach ($directory in $directories) {
	$relativePath = $directory.FullName.Substring($RepositoryRoot.Length).TrimStart('\').Replace('\', '/')
	if (-not $relativePath) {
		continue
	}

	$referenced = $false
	foreach ($referencedPath in $referencedPaths) {
		if ($referencedPath -ieq $relativePath -or (Test-IsAncestorPath -Candidate $relativePath -Path $referencedPath)) {
			$referenced = $true
			break
		}
	}

	if ($referenced) {
		continue
	}

	$hasUnreferencedAncestor = $false
	foreach ($selectedPath in $selectedForRemoval) {
		if (Test-IsAncestorPath -Candidate $selectedPath -Path $relativePath) {
			$hasUnreferencedAncestor = $true
			break
		}
	}

	if ($hasUnreferencedAncestor) {
		continue
	}

	[void]$selectedForRemoval.Add($relativePath)
	$toRemove.Add([pscustomobject]@{
		Path = $relativePath
		FullName = $directory.FullName
	})
}

if ($toRemove.Count -eq 0) {
	Write-Host 'No unreferenced folders were found.'
	return
}

Write-Host "Found $($toRemove.Count) unreferenced folder(s)."
foreach ($item in $toRemove) {
	Write-Host $item.Path
}

if (-not $Delete) {
	Write-Host 'Dry run only. Re-run with -Delete to remove the folders.'
	return
}

foreach ($item in $toRemove) {
	if ($PSCmdlet.ShouldProcess($item.FullName, 'Remove unreferenced folder')) {
		Remove-Item -LiteralPath $item.FullName -Recurse -Force
		Write-Host "Removed $($item.Path)"
	}
}
