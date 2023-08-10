$solutionPath = Split-Path $MyInvocation.MyCommand.Definition
$sdkFile      = Join-Path $solutionPath "global.json"

$dotnetVersion = (Get-Content $sdkFile | Out-String | ConvertFrom-Json).sdk.version

Write-Host $dotnetVersion