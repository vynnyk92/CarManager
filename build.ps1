function DotNetTest {
    param(
        [string] $Project
    )
    
    try {
        & dotnet test `
            $Project `
            -- RunConfiguration.TestSessionTimeout=900000
    }
    finally {
        $env:environment = $oldEnvironment
    }

    if ($LASTEXITCODE -ne 0) {
        throw "dotnet test failed with exit code $LASTEXITCODE"
    }
}

DotNetTest $project