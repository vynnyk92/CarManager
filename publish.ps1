param(
    [string] $Project
)

function DotNetPublish {
    param(
        [string] $Project
    )
    $solutionPath = Split-Path $MyInvocation.MyCommand.Definition
    $publishPath = (Join-Path $solutionPath "publish")

    Write-Output "Before DotNetPublish project: $Project"
    Write-Output $publishPath
        
    & dotnet publish `
        $Project `
        --configuration "Release" `
        --output $publishPath

    Write-Output "After DotNetPublish project: $Project"

    if ($LASTEXITCODE -ne 0) {
        throw "dotnet test failed with exit code $LASTEXITCODE"
    }
}


DotNetPublish $Project