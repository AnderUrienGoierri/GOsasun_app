param(
    [string]$DiagramRoot = $PSScriptRoot
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Add-Type -AssemblyName System.Drawing

$targets = @(
    'klase_diagrama/modeloa_klase_diagrama.md',
    'klase_diagrama/kontrola_repositorioa_klase_diagrama.md',
    'metodo_sekuentzia_diagramak/1_osasun_langilea_saioa_hasi.md',
    'metodo_sekuentzia_diagramak/2_harrerakoa_pazientea_sortu.md',
    'metodo_sekuentzia_diagramak/3_pazientea_tentsiometro_jarraipena_sortu.md',
    'metodo_sekuentzia_diagramak/4_osasun_langilea_paziente_zerrenda_ikusi.md',
    'metodo_sekuentzia_diagramak/5_osasun_langilea_errezeta_sortu.md',
    'metodo_sekuentzia_diagramak/6_osasun_langilea_dokumentua_gehitu_jarraipenari.md'
)

function Remove-Diacritics {
    param([string]$Value)

    $normalized = $Value.Normalize([Text.NormalizationForm]::FormD)
    $builder = New-Object System.Text.StringBuilder

    foreach ($char in $normalized.ToCharArray()) {
        if ([Globalization.CharUnicodeInfo]::GetUnicodeCategory($char) -ne [Globalization.UnicodeCategory]::NonSpacingMark) {
            [void]$builder.Append($char)
        }
    }

    return $builder.ToString().Normalize([Text.NormalizationForm]::FormC)
}

function New-Slug {
    param([string]$Value)

    $ascii = Remove-Diacritics $Value
    $ascii = $ascii -replace '`', ''
    $ascii = $ascii -replace '[^A-Za-z0-9]+', '-'
    $ascii = $ascii.Trim('-').ToLowerInvariant()

    if ([string]::IsNullOrWhiteSpace($ascii)) {
        return 'pantailazoa'
    }

    return $ascii
}

function Measure-MaxWidth {
    param(
        [System.Drawing.Graphics]$Graphics,
        [System.Drawing.Font]$Font,
        [string[]]$Lines
    )

    $maxWidth = 0.0
    $format = [System.Drawing.StringFormat]::GenericTypographic
    foreach ($line in $Lines) {
        $size = $Graphics.MeasureString($line, $Font, 4000, $format)
        if ($size.Width -gt $maxWidth) {
            $maxWidth = $size.Width
        }
    }

    return [Math]::Ceiling($maxWidth)
}

function Render-CodeScreenshot {
    param(
        [string]$Code,
        [string]$Title,
        [string]$OutputPath
    )

    $cleanCode = $Code -replace "`r", ''
    $lines = @($cleanCode -split "`n")
    if ($lines.Count -gt 0 -and $lines[-1] -eq '') {
        $lines = $lines[0..($lines.Count - 2)]
    }

    if ($lines.Count -eq 0) {
        $lines = @(' ')
    }

    $fontName = 'Consolas'
    $fontSizePx = 18.0
    $font = New-Object System.Drawing.Font($fontName, $fontSizePx, [System.Drawing.FontStyle]::Regular, [System.Drawing.GraphicsUnit]::Pixel)
    $headerFont = New-Object System.Drawing.Font('Segoe UI', 15.0, [System.Drawing.FontStyle]::Bold, [System.Drawing.GraphicsUnit]::Pixel)
    $numberFont = New-Object System.Drawing.Font($fontName, 16.0, [System.Drawing.FontStyle]::Regular, [System.Drawing.GraphicsUnit]::Pixel)

    $measureBitmap = New-Object System.Drawing.Bitmap 1, 1
    $measureGraphics = [System.Drawing.Graphics]::FromImage($measureBitmap)
    $measureGraphics.TextRenderingHint = [System.Drawing.Text.TextRenderingHint]::ClearTypeGridFit
    $measureGraphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias

    $lineHeight = [Math]::Ceiling($font.GetHeight($measureGraphics) + 8)
    $lineDigits = [Math]::Max(2, $lines.Count.ToString().Length)
    $numberWidth = [Math]::Ceiling($measureGraphics.MeasureString(('9' * $lineDigits), $numberFont).Width + 18)
    $codeWidth = Measure-MaxWidth -Graphics $measureGraphics -Font $font -Lines $lines
    $titleWidth = [Math]::Ceiling($measureGraphics.MeasureString($Title, $headerFont).Width)

    $padding = 26
    $headerHeight = 54
    $bodyTop = $headerHeight + $padding
    $bodyBottom = $padding

    $width = [Math]::Max(820, $padding + $numberWidth + 24 + $codeWidth + $padding)
    $width = [Math]::Max($width, $padding + $titleWidth + 140)
    $height = $bodyTop + ($lines.Count * $lineHeight) + $bodyBottom

    $bitmap = New-Object System.Drawing.Bitmap $width, $height
    $graphics = [System.Drawing.Graphics]::FromImage($bitmap)
    $graphics.TextRenderingHint = [System.Drawing.Text.TextRenderingHint]::ClearTypeGridFit
    $graphics.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
    $graphics.Clear([System.Drawing.ColorTranslator]::FromHtml('#0f172a'))

    $headerBrush = New-Object System.Drawing.SolidBrush ([System.Drawing.ColorTranslator]::FromHtml('#111827'))
    $panelBrush = New-Object System.Drawing.SolidBrush ([System.Drawing.ColorTranslator]::FromHtml('#111827'))
    $lineBrush = New-Object System.Drawing.SolidBrush ([System.Drawing.ColorTranslator]::FromHtml('#e5e7eb'))
    $numberBrush = New-Object System.Drawing.SolidBrush ([System.Drawing.ColorTranslator]::FromHtml('#64748b'))
    $titleBrush = New-Object System.Drawing.SolidBrush ([System.Drawing.ColorTranslator]::FromHtml('#f8fafc'))
    $separatorPen = New-Object System.Drawing.Pen ([System.Drawing.ColorTranslator]::FromHtml('#1f2937'))

    $graphics.FillRectangle($panelBrush, 0, 0, $width, $height)
    $graphics.FillRectangle($headerBrush, 0, 0, $width, $headerHeight)
    $graphics.DrawLine($separatorPen, 0, $headerHeight, $width, $headerHeight)

    $circleY = 18
    $graphics.FillEllipse([System.Drawing.Brushes]::IndianRed, 20, $circleY, 12, 12)
    $graphics.FillEllipse([System.Drawing.Brushes]::Goldenrod, 40, $circleY, 12, 12)
    $graphics.FillEllipse([System.Drawing.Brushes]::MediumSeaGreen, 60, $circleY, 12, 12)
    $graphics.DrawString($Title, $headerFont, $titleBrush, 88, 14)

    $numberX = $padding
    $codeX = $padding + $numberWidth + 24
    $y = $bodyTop

    for ($index = 0; $index -lt $lines.Count; $index++) {
        $lineNumber = ($index + 1).ToString().PadLeft($lineDigits, ' ')
        $graphics.DrawString($lineNumber, $numberFont, $numberBrush, $numberX, $y + 1)
        $graphics.DrawString($lines[$index], $font, $lineBrush, $codeX, $y)
        $y += $lineHeight
    }

    [System.IO.Directory]::CreateDirectory([System.IO.Path]::GetDirectoryName($OutputPath)) | Out-Null
    $encoder = [System.Drawing.Imaging.ImageFormat]::Png
    $bitmap.Save($OutputPath, $encoder)

    $graphics.Dispose()
    $bitmap.Dispose()
    $measureGraphics.Dispose()
    $measureBitmap.Dispose()
    $font.Dispose()
    $headerFont.Dispose()
    $numberFont.Dispose()
    $headerBrush.Dispose()
    $panelBrush.Dispose()
    $lineBrush.Dispose()
    $numberBrush.Dispose()
    $titleBrush.Dispose()
    $separatorPen.Dispose()
}

function Convert-MarkdownToScreenshots {
    param([string]$MarkdownPath)

    $content = Get-Content -Path $MarkdownPath -Raw -Encoding UTF8
    $lines = [System.Collections.Generic.List[string]]::new()
    foreach ($item in ($content -split "`r?`n", 0)) {
        $lines.Add($item)
    }

    $docName = [System.IO.Path]::GetFileNameWithoutExtension($MarkdownPath)
    $docSlug = New-Slug $docName
    $relativeImageRoot = "../kode_pantailazoak/$docSlug"
    $imageRoot = Join-Path $DiagramRoot "kode_pantailazoak/$docSlug"
    [System.IO.Directory]::CreateDirectory($imageRoot) | Out-Null

    $newLines = New-Object System.Collections.Generic.List[string]
    $insideScreenshots = $false
    $currentHeading = 'Kode pantailazoa'
    $imageIndex = 1

    for ($i = 0; $i -lt $lines.Count; $i++) {
        $line = $lines[$i]

        if ($line -eq '## Kode pantailazoak') {
            $insideScreenshots = $true
            $newLines.Add($line)
            continue
        }

        if ($insideScreenshots -and $line -match '^##\s+' -and $line -ne '## Kode pantailazoak') {
            $insideScreenshots = $false
        }

        if ($insideScreenshots -and $line -match '^###\s+(.+)$') {
            $currentHeading = $Matches[1]
            $newLines.Add($line)
            continue
        }

        if ($insideScreenshots -and $line -eq '```csharp') {
            $codeLines = New-Object System.Collections.Generic.List[string]
            $i++
            while ($i -lt $lines.Count -and $lines[$i] -ne '```') {
                $codeLines.Add($lines[$i])
                $i++
            }

            $sourceLine = $null
            while (($i + 1) -lt $lines.Count -and [string]::IsNullOrWhiteSpace($lines[$i + 1])) {
                $i++
            }

            if (($i + 1) -lt $lines.Count -and $lines[$i + 1] -match '^Iturria:\s+`(.+)`$') {
                $i++
                $sourceLine = $lines[$i]
            }

            $imageName = '{0:D2}_{1}.png' -f $imageIndex, (New-Slug $currentHeading)
            $imagePath = Join-Path $imageRoot $imageName
            $imageRelativePath = "$relativeImageRoot/$imageName" -replace '\\', '/'
            $altText = ("$currentHeading - kode pantailazoa" -replace '`', '')
            Render-CodeScreenshot -Code ($codeLines -join "`n") -Title $currentHeading -OutputPath $imagePath

            if ($newLines.Count -gt 0 -and $newLines[$newLines.Count - 1] -ne '') {
                $newLines.Add('')
            }

            $newLines.Add("![$altText]($imageRelativePath)")

            if ($sourceLine) {
                $newLines.Add('')
                $newLines.Add($sourceLine)
            }

            $imageIndex++
            continue
        }

        $newLines.Add($line)
    }

    [System.IO.File]::WriteAllText($MarkdownPath, ($newLines -join [Environment]::NewLine), [System.Text.UTF8Encoding]::new($false))
    return ($imageIndex - 1)
}

$total = 0
foreach ($relativeTarget in $targets) {
    $markdownPath = Join-Path $DiagramRoot $relativeTarget
    if (-not (Test-Path $markdownPath)) {
        throw "Ez da aurkitu: $markdownPath"
    }

    $total += Convert-MarkdownToScreenshots -MarkdownPath $markdownPath
}

Write-Host "Sortutako screenshot kopurua: $total"