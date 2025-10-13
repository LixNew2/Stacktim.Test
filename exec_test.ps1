# Chemin vers votre projet/test project
$projectPath = ".\Stacktim.Test.csproj"

# Dossier pour stocker les rapports de couverture
$coverageDir = "$PSScriptRoot\Coverage"
$coverageFile = "$coverageDir\coverage.xml"
$reportDir = "$coverageDir\Report"

# Crée les dossiers si nécessaire
if (-Not (Test-Path $coverageDir)) {
    New-Item -ItemType Directory -Path $coverageDir | Out-Null
}
if (-Not (Test-Path $reportDir)) {
    New-Item -ItemType Directory -Path $reportDir | Out-Null
}

# Exécution des tests xUnit avec couverture de code
Write-Host "Exécution des tests et collecte de la couverture..."
dotnet test $projectPath `
    --collect:"XPlat Code Coverage" `
    --results-directory $coverageDir `
    --logger "trx;LogFileName=TestResults.trx"

# Vérification que le fichier de couverture a été généré
$coverageReport = Get-ChildItem -Path $coverageDir -Filter "coverage.cobertura.xml" -Recurse | Select-Object -First 1
if (-Not $coverageReport) {
    Write-Error "Le fichier de couverture n'a pas été trouvé !"
    exit 1
}

# Génération du rapport HTML avec ReportGenerator
Write-Host "Génération du rapport HTML..."
reportgenerator `
    "-reports:$($coverageReport.FullName)" `
    "-targetdir:$reportDir" `
    "-reporttypes:HtmlInline_AzurePipelines" `
    "-verbosity:Info"

# Ouvre le rapport HTML dans le navigateur
$htmlReport = Join-Path $reportDir "index.html"
if (Test-Path $htmlReport) {
    Write-Host "Ouverture du rapport dans le navigateur..."
    Start-Process $htmlReport
} else {
    Write-Error "Le rapport HTML n'a pas été trouvé !"
}
