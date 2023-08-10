function DotNetTest {
    param(
        [string] $Project
    )
    
    Write-Output "Before test project: $Project"
        
    & dotnet test `
        $Project `
        -- RunConfiguration.TestSessionTimeout=900000

    Write-Output "After test project: $Project"

    if ($LASTEXITCODE -ne 0) {
        throw "dotnet test failed with exit code $LASTEXITCODE"
    }
}

DotNetTest $project