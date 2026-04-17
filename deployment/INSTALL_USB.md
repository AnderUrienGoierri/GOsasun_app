# GOsasun App USB instalazioa

## Ohar garrantzitsua

`Launch4j` ez da egokia proiektu honetarako. Tresna hori Java `.jar` aplikazioetarako da, eta zure aplikazioa `.NET WinForms` da.

## 1. Exekutagarria prestatu

PowerShell-etik exekutatu:

```powershell
pwsh -ExecutionPolicy Bypass -File .\deployment\Publish-GOsasun.ps1
```

Honek `publish\win-x64` karpetan autoedukitutako bertsioa sortuko du.

## 2. Instaladorea sortu

1. Instalatu `Inno Setup`.
2. Ireki `deployment\GOsasun_app.iss`.
3. Sakatu `Build`.
4. Sortutako instalatzailea hemen agertuko da:

```text
deployment\output\GOsasun_app_Setup.exe
```

## 3. USB-an gorde

USB-ra kopiatu gutxienez hau:

```text
deployment\output\GOsasun_app_Setup.exe
```

Nahi baduzu, `sql` fitxategiak ere kopiatu azalpen edo mantentze lanetarako.

## 4. Beste ordenagailuan instalatu

1. USB sartu.
2. `GOsasun_app_Setup.exe` administratzaile gisa exekutatu.
3. Instalazio lehenetsia hemen egingo da:

```text
C:\GOsasun_app
```

Instalatzaileak karpeta hauek ere sortzen ditu, aplikazioak kodean bide absolutu horiek erabiltzen dituelako:

```text
C:\Apache24-64\htdocs\GOsasun_web\dokumentuak
C:\Apache24-64\htdocs\GOsasun_web\paziente_dokumentuak
C:\Apache24-64\htdocs\GOsasun_web\xml_paziente_neurketak
C:\Apache24-64\htdocs\GOsasun_web\img\png
```

## 5. Instalazioak funtzionatzeko bete beharreko baldintzak

### MySQL

Aplikazioak une honetan konexio hauek hardcodeatuta ditu [GOsasun_app/Repositorioa/DatuBaseKonexioa.cs](GOsasun_app/Repositorioa/DatuBaseKonexioa.cs#L18):

- Zerbitzaria: `localhost`
- Portua: `3306`
- Datu-basea: `GOsasun_DB`
- Erabiltzailea: `root`
- Pasahitza: `1MG32025`

Horrek esan nahi du helburuko ordenagailuan MySQL instalatuta egon behar dela eta datu-base hori sortuta egon behar dela.

### SQL inportazioa

Gutxienez hauek inportatu behar dira:

- `sql\GOsasun_DB.sql`
- `sql\GOsasun_DB_data.sql`

### Administratzaile baimenak

`C:\` erroan instalatzeko eta `C:\Apache24-64\...` azpikarpetak sortzeko, administratzaile baimenak behar dira.

## Gomendioa

Egonkor hedatu nahi baduzu, hurrengo hobekuntza teknikoa egitea komeni da:

1. DB konexioa `appsettings.json` edo antzeko konfigurazio-fitxategi batera ateratzea.
2. `C:\Apache24-64\...` bide absolutuak konfiguragarri bihurtzea.
3. Orduan instalatzailea askoz eramangarriagoa izango da.