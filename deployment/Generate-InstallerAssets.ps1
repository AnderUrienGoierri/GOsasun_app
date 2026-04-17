param(
    [string]$SourcePng = "",
    [string]$OutputDir = ""
)

$ErrorActionPreference = "Stop"

Add-Type -AssemblyName System.Drawing
Add-Type @"
using System;
using System.Runtime.InteropServices;

public static class NativeMethods
{
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool DestroyIcon(IntPtr hIcon);
}
"@

$repoRoot = Split-Path -Parent $PSScriptRoot

if ([string]::IsNullOrWhiteSpace($SourcePng)) {
    $SourcePng = Join-Path $repoRoot "GOsasun_app\img\png\logoak\GOsasun_logo_whatsap.png"
}

if ([string]::IsNullOrWhiteSpace($OutputDir)) {
    $OutputDir = Join-Path $PSScriptRoot "installer_assets"
}

$projectIconPath = Join-Path $repoRoot "GOsasun_app\img\png\logoak\GOsasun_logo_whatsap.ico"
$setupIconPath = Join-Path $OutputDir "GOsasun_logo_whatsap.ico"
$wizardLargePath = Join-Path $OutputDir "GOsasun_logo_whatsap_wizard.bmp"
$wizardSmallPath = Join-Path $OutputDir "GOsasun_logo_whatsap_wizard_small.bmp"

if (-not (Test-Path $SourcePng)) {
    throw "Ez da logo PNG fitxategia aurkitu: $SourcePng"
}

New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null

$image = [System.Drawing.Image]::FromFile($SourcePng)

function New-FittedBitmap {
    param(
        [System.Drawing.Image]$SourceImage,
        [int]$CanvasWidth,
        [int]$CanvasHeight,
        [System.Drawing.Color]$BackgroundColor,
        [bool]$TransparentBackground = $false
    )

    $bitmap = New-Object System.Drawing.Bitmap($CanvasWidth, $CanvasHeight)
    if ($TransparentBackground) {
        $bitmap.MakeTransparent()
    }

    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::HighQuality
    $graphics.InterpolationMode = [System.Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic
    $graphics.PixelOffsetMode = [System.Drawing.Drawing2D.PixelOffsetMode]::HighQuality
    $graphics.CompositingQuality = [System.Drawing.Drawing2D.CompositingQuality]::HighQuality
    $graphics.Clear($BackgroundColor)

    $scale = [Math]::Min($CanvasWidth / $SourceImage.Width, $CanvasHeight / $SourceImage.Height)
    $drawWidth = [int][Math]::Round($SourceImage.Width * $scale)
    $drawHeight = [int][Math]::Round($SourceImage.Height * $scale)
    $offsetX = [int][Math]::Floor(($CanvasWidth - $drawWidth) / 2)
    $offsetY = [int][Math]::Floor(($CanvasHeight - $drawHeight) / 2)

    $graphics.DrawImage($SourceImage, $offsetX, $offsetY, $drawWidth, $drawHeight)
    $graphics.Dispose()

    return $bitmap
}

function Save-IcoFile {
    param(
        [System.Drawing.Image]$SourceImage,
        [string]$DestinationPath
    )

    $bitmap = New-FittedBitmap -SourceImage $SourceImage -CanvasWidth 256 -CanvasHeight 256 -BackgroundColor ([System.Drawing.Color]::Transparent) -TransparentBackground $true
    $iconHandle = [IntPtr]::Zero
    $icon = $null
    $fileStream = $null
    try {
        $iconHandle = $bitmap.GetHicon()
        $icon = [System.Drawing.Icon]::FromHandle($iconHandle)
        $fileStream = [System.IO.File]::Open($DestinationPath, [System.IO.FileMode]::Create, [System.IO.FileAccess]::Write)
        $icon.Save($fileStream)
    }
    finally {
        if ($fileStream -ne $null) {
            $fileStream.Dispose()
        }
        if ($icon -ne $null) {
            $icon.Dispose()
        }
        if ($iconHandle -ne [IntPtr]::Zero) {
            [NativeMethods]::DestroyIcon($iconHandle) | Out-Null
        }
        $bitmap.Dispose()
    }
}

function Save-BmpFile {
    param(
        [System.Drawing.Image]$SourceImage,
        [string]$DestinationPath,
        [int]$Width,
        [int]$Height
    )

    $bitmap = New-FittedBitmap -SourceImage $SourceImage -CanvasWidth $Width -CanvasHeight $Height -BackgroundColor ([System.Drawing.Color]::White)
    try {
        $bitmap.Save($DestinationPath, [System.Drawing.Imaging.ImageFormat]::Bmp)
    }
    finally {
        $bitmap.Dispose()
    }
}

try {
    Save-IcoFile -SourceImage $image -DestinationPath $setupIconPath
    Copy-Item $setupIconPath $projectIconPath -Force
    Save-BmpFile -SourceImage $image -DestinationPath $wizardLargePath -Width 164 -Height 314
    Save-BmpFile -SourceImage $image -DestinationPath $wizardSmallPath -Width 55 -Height 55
}
finally {
    $image.Dispose()
}

Write-Host "Installer asset-ak sortu dira:" -ForegroundColor Green
Write-Host "  $setupIconPath"
Write-Host "  $wizardLargePath"
Write-Host "  $wizardSmallPath"
Write-Host "  $projectIconPath"