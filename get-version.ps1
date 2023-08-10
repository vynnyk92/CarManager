$solutionPath = Split-Path $MyInvocation.MyCommand.Definition
$sdkFile      = Join-Path $solutionPath "global.json"

$dotnetVersion = (Get-Content $sdkFile | Out-String | ConvertFrom-Json).sdk.version

Write-Output "::set-output name=dotnet-version::$dotnetVersion"