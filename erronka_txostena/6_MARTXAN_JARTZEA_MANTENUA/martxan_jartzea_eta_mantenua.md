# 6. Martxan Jartzea eta Mantentze Lanak

Proiektuaren azken txanpan, inplementazioa eta probak gainditu ondoren, softwareak bizitza erreala hasten du. Fase honek berebiziko garrantzia dauka, bezeroarengan produktuarekiko konfiantza finkatzen baitu.

## Martxan Jartzea (Deployment)
Martxan jartzea softwarea probatu eta (aurkitutako) akatsak konpondu ondoren, dagokion ingurune erreal edo produkziokoan (Production Environment) hedatzea da. GOsasun proiektuaren kasuan:
- **Datu-basea eta Weba**: Apache eta MySQL zerbitzari zentral batean (adibidez, osasun-zentroko zerbitzari nagusian) ostatatu dira. Karpeten baimenak konfiguratu dira webguneak kanpotik sarbidea izan dezan segurtasunez.
- **Aplikazioa**: Osasun langileen eta harrerakoen Windows ordenagailuetan instalagarri baten bidez banatu da, zerbitzari nagusiaren IParantz seinalatzen duten konexio-parametroekin (`appsettings.json` bidez).

## Mantentze Lanak
Softwarea bizirik dagoen entitate bat da. Martxan jarri ondoren, ezinbestekoa da mantentze-lanak etengabe egitea sistema osasuntsu mantentzeko. Funtsean, hiru mantentze-mota esanguratsu ditugu gure aplikazioarentzat:

### 1. Mantentze Zuzentzailea (Corrective Maintenance)
Frogak egin arren, ingurune errealean bezeroak erroreak (bug-ak) aurkitu ohi ditu beti (erabiltzailearen ekintza ezustekoen ondorioz). Errore hauek konpondu egin behar dira sistema leheneratzeko.
- **Adibidea gurean**: Paziente batek apostrofe bat (') zeukan abizen arraro bat sartzean datu-baseak errore bat botatzen bazuen, kodea zuzendu eta adabaki bat (patch) askatzea da gure zeregina.

### 2. Eguneraketa Mantentzea (Adaptive Maintenance)
Teknologia eta ingurunea denborarekin aldatzen dira, eta aplikazioak ezinbesteko du horretara egokitzea, nahiz eta errorerik egon ez. Zerbitzu bat eskaintzen digun zerbitzariak (edo kanpoko API batek) datuak bidaltzeko modua aldatu badu, aldaketa horiek inplementatu beharko dira gure kodean softwareak bateragarri izaten jarraitu dezan.
- **Adibidea gurean**: Zentro medikuak sistema eragilea Windows 10-etik Windows 11-ra edo hurrengo bertsioetara eguneratzen badu, gure mahaigaineko .NET aplikazioak arazo barik funtzionatzen jarraituko duela ziurtatu eta menpekotasunak eguneratu behar dira. Edo zerbitzariak PHP 8.1-etik PHP 9.0-ra salto egiten badu, desagertutako funtzioak eguneratu.

### 3. Perfekzionatzeko Mantentzea (Perfective Maintenance)
Denboraren poderioz normala izaten da gure bezeroak (zentro medikoak) funtzionalitate berriak eskatzea behar dituelako edo horrela nahi dutelako. Kasu horretan, softwareari mantentze lanak egiten zaizkio atal berriak txertatzeko edo daudenak errendimenduz hobetzeko.
- **Adibidea gurean**: Zentroak, hilabete batzuk pasa ondoren, gure webgunean pacienteek medikuen aurpegiak ikusteaz gain, "Bideo-dei bidezko kontsulta" botoi bat sartzea eskatzen digu. Guk webgunea perfekzionatu egingo genuke funtzio berri hori gehituz, hasierako analisitik igaroz berriz ere.
