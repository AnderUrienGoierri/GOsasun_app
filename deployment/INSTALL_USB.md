# GOsasun App USB instalazioa

## Ohar garrantzitsua

`Launch4j` ez da egokia proiektu honetarako. Tresna hori Java `.jar` aplikazioetarako da, eta zure aplikazioa `.NET WinForms` da.

## 1. Exekutagarria prestatu

PowerShell-etik exekutatu:

```powershell
pwsh -ExecutionPolicy Bypass -File .\deployment\Publish-GOsasun.ps1
```

Honek hauek egingo ditu automatikoki:

- `publish\win-x64` karpetan autoedukitutako bertsioa sortu
- `deployment\GOsasun_app.iss` konpilatu Inno Setup-rekin
- azken instalatzailea `deployment\output\GOsasun_app_Setup.exe` fitxategian eguneratu
- kopia bat `D:\Instalatzailea\GOsasun_app_Setup.exe` helmugan utzi

## 2. Instaladorea sortu

Publish script-a erabilita, urrats hau automatikoki egiten da. Eskuz egin nahi baduzu:

1. Instalatu `Inno Setup`.
2. Ireki `deployment\GOsasun_app.iss`.
3. Sakatu `Build`.
4. Sortutako instalatzailea hemen agertuko da:

```text
deployment\output\GOsasun_app_Setup.exe
```

## 3. USB-an gorde

USB helmuga lehenetsia hau da:

```text
D:\Instalatzailea\GOsasun_app_Setup.exe
```

Beste USB edo karpeta batera kopiatu nahi baduzu, gutxienez hau eraman:

```text
deployment\output\GOsasun_app_Setup.exe
```

Nahi baduzu, `sql` fitxategiak ere kopiatu azalpen edo mantentze lanetarako.

## 4. Beste ordenagailuan instalatu

1. USB sartu.
2. `GOsasun_app_Setup.exe` administratzaile gisa exekutatu.
3. Lehen pantailan irakurri azalpen laburra. Prest izan hurrengo datuak:

- web/Apache erroaren bidea
- SQL zerbitzariaren IP edo hostname-a
- SQL portua
- datu-basearen izena
- erabiltzailea eta pasahitza

4. Instalazioan zehar galdetuko zaizu, ordena honetan:

- aplikazioa non instalatu nahi duzun
- Apache/web erroaren bidea
- SQL zerbitzariaren IP edo hostname-a
- portua, datu-basea, erabiltzailea eta pasahitza
- lehen exekuzioan eskema eta seed datuak automatikoki prestatu nahi dituzun

5. Aukera gomendatua ingurune berrirako:

- web erroa: zure zerbitzarian erabiltzen duzun bidea
- SQL host/IP: eskolako zerbitzaria edo MySQL dagoen makina
- eskema sortu: `Bai`
- hasierako datuak kargatu: `Bai`

6. Instalazio lehenetsia hemen egingo da:

```text
C:\GOsasun_app
```

Instalatzaileak aukeratutako web erroaren barruan karpeta hauek sortzen saiatzen da:

```text
<webErroa>\dokumentuak
<webErroa>\paziente_dokumentuak
<webErroa>\xml_paziente_neurketak
<webErroa>\img\png
```

Oharra: `webErroa` lokala izan daiteke edo sareko UNC bide bat, adibidez `\\192.168.1.20\GOsasun_web`.

### Instalazioa amaitutakoan zer egin

1. Sakatu `Amaitu` eta nahi baduzu aplikazioa berehala abiarazi.
2. Lehen abioan, aplikazioak karpetak sortu eta DB konexioa egiaztatuko du.
3. Eskema eta seed datuak aktibatu badituzu, lehen abioak pixka bat gehiago iraun dezake.
4. Errore bat agertzen bada, berrikusi lehenengo `appsettings.json`-en sartutako SQL host/IP-a eta web erroaren bidea.

## 5. Instalazioak funtzionatzeko bete beharreko baldintzak

### MySQL

Aplikazioak hemendik aurrera `appsettings.json`-etik irakurtzen du DB konfigurazioa, eta instalatzaileak balio horiek sortzen ditu. Beraz, helburuko ordenagailuan edo sarean eskuragarri dagoen SQL zerbitzaria konfiguratu dezakezu, eskola zerbitzariko IP-a barne.

### SQL inportazioa

Instalazioan aukeratzen baduzu, aplikazioak lehen exekuzioan automatikoki exekutatuko ditu gutxienez hauek:

- `sql\GOsasun_DB.sql`
- `sql\GOsasun_DB_data.sql`

Eta baita hauek ere eskema osatzeko:

- `sql\GOsasun_DB_trigger.sql`
- `sql\GOsasun_DB_bistak.sql`
- `sql\GOsasun_DB_indizeak.sql`

Horrela, lehen abioaren ondoren aplikazioa martxan geratzeko beharrezko oinarria prestatuta uzten da.

### Lehen exekuzio automatikoa

Aplikazioa lehen aldiz irekitzean:

- konfigurazioa irakurtzen du
- web/Apache karpetak existitzen direla egiaztatzen du eta beharrezkoa bada sortzen ditu
- aukeratu baduzu, DB eskema eta hasierako datuak exekutatzen ditu
- DB konexioa probatzen du

Prestaketa hori huts egiten badu, aplikazioak errore argi bat erakusten du eta ez da login pantailara iristen, arazoa ezkutuan gera ez dadin.

### Administratzaile baimenak

`C:\` erroan instalatzeko edo bide lokal babestuetan karpetak sortzeko, administratzaile baimenak behar dira.

## Arazo ohikoena

### "Ez da publish karpeta aurkitu"

Mezu hori ez da helburuko ordenagailuan instalatzean agertu behar. Mezu hori agertzen bazen, instalatzaile zahar bat erabiltzen ari zinen. Instalatzeko erabili behar den fitxategia beti da aurrez konpilatutako hau:

```text
deployment\output\GOsasun_app_Setup.exe
```

Ez da beharrezkoa helburuko ordenagailuan `Publish-GOsasun.ps1` exekutatzea.

## Gomendioa

Une honetan DB eta Apache/web bide nagusiak konfiguragarri bihurtuta daude. Hurrengo hobekuntza gomendagarria da instalatzaileari konektibitate-proba gehitzea instalazioan bertan, SQL zerbitzaria eskuragarri dagoen unean bertan egiaztatzeko.
