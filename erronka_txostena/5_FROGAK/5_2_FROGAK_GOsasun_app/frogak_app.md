# 5.2. FROGAK – GOsasun App (Desktop)

## 5.2.1 Helburua
Testatu **C# Windows‑Forms** aplikazioaren instalazioa, konexioa Beurer BM58‑rekin, datu‑basea (lokaleko MySQL), baimenak eta erroreen kudeaketa. Instalazio‑prozesua **USB‑driblotik** egin behar da, beraz ".exe" fitxategia kopiatu eta `C:\Program Files\GOsasun\` karpeta sortu.

## 5.2.2 Pre‑eskaerak
- Windows 10/11 (Admin baimenak).
- .NET 10 SDK instalatuta.
- MySQL Server 8.x (lokalean `root` baimenak).
- **BM58Driver.dll** liburutegia ".exe"‑aren "+\libs" azpidirektorioan.
- `GOsasun_DB.sql` datu‑basea lokalki `GOsasun_DB` izeneko datu‑basea sortuta.

## 5.2.3 Frogak (Test‑Caseak)
| # | Izena | Deskribapena | Pausa | Esperotako emaitza | Oharrak / Erroreko konponketak |
|---|-------|--------------|-------|--------------------|-------------------------------|
| 1 | **Instalazio USB‑ bidez** | `.exe` kopiatu, `C:\Program Files\GOsasun\GOsasun.exe` exekutatu. | 1. `setup.exe /S` (silent) 2. `C:\Program Files\GOsasun\GOsasun.exe` | Aplikazioa abiarazita, logoa agertzen da. | `ERROR: Access denied` → `Run as Administrator`.
| 2 | **DB konexioa** | Aplikazioak `appsettings.json`‑en "ConnectionString" erabiltzen du. | `SELECT 1 FROM pazienteak LIMIT 1;` | Datu‑baseko konexioa ondo da. | `MySqlException: Unable to connect` → `MySQL zerbitzaria martxan` edo `firewall` portu 3306.
| 3 | **Erabiltzaile Saioa** | `ErabiltzaileKontrolatzailea.Login(email, pass)` . | Saioa ireki. | `LoginEmaitza.Egoera` = `LoginSegurtasunEgoera` = `ez dago blokeatua`. | Okerreko pasahitzak → `LoginBlokeoZerbitzua` blokeoa.
| 4 | **BM58 konektatzea** | Plug‑and‑play USB. | `BM58Driver.EgiaztatuHardwareKonexioa()` | `true` (gailua entzuten da). | `false` → `USB drivera instalatu` (`devcon.exe` erabiliz).
| 5 | **Tentsio‑neurketa irakurketa** | `BM58Driver.IrakurriErrekordGuztiak()` → `Jarraipena` objektua. | `JarraipenaKontrolatzailea.GordeJarraipena(j)` | Datuak `jarraipenak` taulan sartzen dira, `trg_eguneratu_neurri_fisikoak` aktibo. | `ERROR 1452` → `pazientea lehenik sortu`.
| 6 | **PDF Txostena** | `DokumentuPdfZerbitzua.SortuPazientearenTxostena(pazienteId)` | Fitxategi sortua `C:\Dokumentuak\` karpetan. | PDF baliozkoa, izenak formatua `txostena_{id}.pdf`. | `System.UnauthorizedAccessException` → `Grant write permission to folder`.
| 7 | **Error‑Handling** | Simulatu SQL‑eko `INSERT` hutsegitea (`DROP TABLE jarraipenak` aurretik). | `GordeJarraipena` exekutatu. | `try/catch` blokeak `MessageBox` errorea erakusten du. | Konfiguratu `AppDomain.CurrentDomain.UnhandledException` log fitxategira.

## 5.2.4 Automatizazioa (PowerShell)
```powershell
# Test‑case 1 – Instalazioa (silent)
Start-Process -FilePath "C:\Setup\GOsasunSetup.exe" -ArgumentList "/S" -Wait -Verb RunAs
# Test‑case 2 – DB konexioa
& "C:\Program Files\GOsasun\GOsasun.exe" -testConnection
# Test‑case 4 – BM58 konektatzea
& "C:\Program Files\GOsasun\BM58Driver.exe" -check
```
Log fitxategi (`C:\Program Files\GOsasun\logs\frogak.log`) sortu eta `try/catch` bloke guztietan idatzi.

---
*Frogak aplikazioaren guztiak ondo instalatu, konexioak egiaztatu eta erroreak kudeatu direla bermatzen dute, bai lokaleko root baimenekin bai produksio‑ingurunean.*
