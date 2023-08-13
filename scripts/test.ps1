function DotNetTest {
    param(
        [string] $Project
    )
    
    Write-Output "Before test project: $Project"
        
    & dotnet test `
        $Project `

    Write-Output "After test project: $Project"

    if ($LASTEXITCODE -ne 0) {
        throw "dotnet test failed with exit code $LASTEXITCODE"
    }
}

$currentPath =$MyInvocation.MyCommand.Definition
$parentPath = (get-item $scriptPath ).parent.parent
$testPath = (Join-Path $parentPath "test/CarManager.UnitTests/CarManager.UnitTests.csproj")

Write-Output $currentPath
Write-Output $parentPath
Write-Output $testPath
DotNetTest -Project: $testPath 