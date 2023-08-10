param(
    [string] $Project
)

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

DotNetTest $Project