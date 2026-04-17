param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64"
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$projectPath = Join-Path $repoRoot "GOsasun_app\GOsasun_app.csproj"
$publishRoot = Join-Path $repoRoot "publish"
$publishDir = Join-Path $publishRoot $Runtime

if (-not (Test-Path $projectPath)) {
    throw "Ez da proiektua aurkitu: $projectPath"
}

if (Test-Path $publishDir) {
    Remove-Item $publishDir -Recurse -Force
}

New-Item -ItemType Directory -Path $publishDir -Force | Out-Null

dotnet publish $projectPath `
    -c $Configuration `
    -r $Runtime `
    --self-contained true `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -p:EnableCompressionInSingleFile=true `
    -o $publishDir

Write-Host ""
Write-Host "Publish-a osatu da:" -ForegroundColor Green
Write-Host "  $publishDir"
Write-Host ""
Write-Host "Hurrengo pausoa: deployment\GOsasun_app.iss fitxategia Inno Setup-rekin konpilatu." -ForegroundColor Yellow