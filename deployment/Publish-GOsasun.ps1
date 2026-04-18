param(
    [string]$Configuration = "Release",
    [string]$Runtime = "win-x64",
    [string]$UsbDestination = "D:\Instalatzailea"
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$projectPath = Join-Path $repoRoot "GOsasun_app\GOsasun_app.csproj"
$publishRoot = Join-Path $repoRoot "publish"
$publishDir = Join-Path $publishRoot $Runtime
$installerAssetsScript = Join-Path $PSScriptRoot "Generate-InstallerAssets.ps1"
$issPath = Join-Path $PSScriptRoot "GOsasun_app.iss"
$installerOutput = Join-Path $PSScriptRoot "output\GOsasun_app_Setup.exe"

if (-not (Test-Path $projectPath)) {
    throw "Ez da proiektua aurkitu: $projectPath"
}

if (Test-Path $publishDir) {
    Remove-Item $publishDir -Recurse -Force
}

New-Item -ItemType Directory -Path $publishDir -Force | Out-Null

if (-not (Test-Path $installerAssetsScript)) {
    throw "Ez da installer asset-en script-a aurkitu: $installerAssetsScript"
}

if (-not (Test-Path $issPath)) {
    throw "Ez da Inno Setup script-a aurkitu: $issPath"
}

& $installerAssetsScript

dotnet publish $projectPath `
    -c $Configuration `
    -r $Runtime `
    --self-contained true `
    -o $publishDir

Write-Host ""
Write-Host "Publish-a osatu da:" -ForegroundColor Green
Write-Host "  $publishDir"
Write-Host ""

$isccCandidates = @(
    "C:\Users\anurt\AppData\Local\Programs\Inno Setup 6\ISCC.exe",
    "C:\Program Files (x86)\Inno Setup 6\ISCC.exe",
    "C:\Program Files\Inno Setup 6\ISCC.exe"
)

$isccPath = $isccCandidates | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $isccPath) {
    $isccCommand = Get-Command ISCC.exe -ErrorAction SilentlyContinue
    if ($isccCommand) {
        $isccPath = $isccCommand.Source
    }
}

if (-not $isccPath) {
    throw "Ez da Inno Setup-ren ISCC.exe aurkitu. Instalatu Inno Setup 6 lehenik."
}

& $isccPath $issPath | Out-Host

if (-not (Test-Path $installerOutput)) {
    throw "Ez da instalatzailea sortu: $installerOutput"
}

New-Item -ItemType Directory -Path $UsbDestination -Force | Out-Null
$usbInstallerOutput = Join-Path $UsbDestination "GOsasun_app_Setup.exe"
Copy-Item $installerOutput $usbInstallerOutput -Force

Write-Host "Instalatzailea eguneratu da:" -ForegroundColor Green
Write-Host "  $installerOutput"
Write-Host "USB helmuga:" -ForegroundColor Green
Write-Host "  $usbInstallerOutput"