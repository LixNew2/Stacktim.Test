$projectPath = Join-Path $PSScriptRoot "Stacktim.Test.csproj"

$coverageDir = Join-Path $PSScriptRoot "TestDirResult"
$coverageFile = Join-Path $PSScriptRoot "coverage.xml"
$reportDir = Join-Path $PSScriptRoot "TestDirResult"

if (-Not (Test-Path $coverageDir)) {
    New-Item -ItemType Directory -Path $coverageDir | Out-Null
}
if (-Not (Test-Path $reportDir)) {
    New-Item -ItemType Directory -Path $reportDir | Out-Null
}

Write-Host "Running tests and collecting coverage..."
dotnet test $projectPath `
    --collect:"XPlat Code Coverage" `
    --results-directory $coverageDir `
    --logger "trx;LogFileName=TestResults.trx"

$coverageReport = Get-ChildItem -Path $coverageDir -Filter "coverage.cobertura.xml" -Recurse | Select-Object -First 1
if (-Not $coverageReport) {
    Write-Error "The cover file was not found !"
    exit 1
}

Write-Host "Generating HTML report..."
reportgenerator "-reports:$($coverageReport.FullName)" `
                 "-targetdir:$reportDir" `
                 "-classfilters:+MvcMovie.Controllers.*" `
                 "-classfilters:-*" `
                 


$htmlReport = Join-Path $reportDir "index.html"
if (Test-Path $htmlReport) {
    Write-Host "Opening the report in the browser..."
    Start-Process $htmlReport
} else {
    Write-Error "The HTML report was not found !"
}