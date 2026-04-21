# GOsasun App - Klase Diagramarako Datu Xehatuak

Dokumentu honek klase diagrama prestatzeko behar diren oinarrizko datuak biltzen ditu, baina aldi berean, **aplikazioko klase, aldagai eta metodo guztien gida esplikatibo** gisa funtzionatzen du. Helburua da programazio-ezagutza sakonik gabe ere, elementu bakoitzak zergatik existitzen den eta zer egiten duen ulertzea.

## 1. Modeloko klaseak (Entitateak) eta beraien ezaugarriak

Klase hauek datu-baseko taulen errepresentazioa dira aplikazioaren barruan. Bakoitzak aldagai (propietate) zehatzak ditu bere informazioa gordetzeko.

### 1.1 `Erabiltzailea` (Klase Abstraktua)
- **Helburua**: Sistemako pertsona guztiek (Pazienteek zein Langileek) komunean dituzten datuak biltzea. "Abstraktua" izateak esan nahi du ezin dela "Erabiltzaile" huts bat sortu; beti izan behar du paziente edo langile bat.
- **Aldagai nagusiak**:
  - `Id`: Datu-baseko identifikatzaile esklusiboa.
  - `Emaila` / `Pasahitza`: Sistemara sartzeko kredentzialak.
  - `Nan`, `Izena`, `Abizenak`, `JaiotzeData`, `Telefonoa`: Oinarrizko datu pertsonalak.
  - `Aktibo`: Erabiltzailea sisteman bajan emanda dagoen kontrolatzeko (True/False).
- **Metodo espezifikoak**:
  - `DaPazientea()` / `DaOsasunLangilea()`: Momentuko erabiltzaile hori zein rol jokatzen ari den jakiteko funtzio azkarrak.

### 1.2 `Pazientea` (Erabiltzailearen oinordekoa)
- **Helburua**: Paziente baten datu kliniko eta pertsonal espezifikoak gordetzea.
- **Aldagai nagusiak**:
  - `Sexua`, `OdolTaldea`: Datu biologiko estatikoak.
  - `AzkenAltuera`, `AzkenPisua`: Uneko egoera fisikoa islatzen duten aldagaiak.
  - `EgoeraKlinikoa`: Ospitaleratuta, alta jasota, etab. dagoen adierazten du.
  - `OsasunLangileak`: Paziente honen kargu dauden mediku edo erizainen zerrenda (List).

### 1.3 `OsasunLangilea` (Erabiltzailearen oinordekoa)
- **Helburua**: Zentroko pertsonal medikuaren lan-datuak biltzea.
- **Aldagai nagusiak**:
  - `ElkargokideZenbakia`: Medikuaren lizentzia ofiziala.
  - `Espezialitatea`: Kardiologoa, Erizaina, etab.
  - `Kontsulta`: Zein gelatan egiten duen lan (adibidez: "A-102").
  - `Pazienteak`: Mediku honi esleituta dauden pazienteen zerrenda.

### 1.4 `HarrerakoLangilea` (Erabiltzailearen oinordekoa)
- **Helburua**: Zentroko kudeaketa administratiboa egiten duen langilea.
- **Aldagai nagusia**:
  - `Txanda`: Goizez edo arratsaldez lan egiten duen zehazteko.

### 1.5 `Hitzordua`
- **Helburua**: Paziente eta sendagile baten arteko topaketa planifikatzea.
- **Aldagai nagusiak**:
  - `Data`, `HasieraOrdua`, `BukaeraOrdua`: Topaketaren denbora zehaztapenak.
  - `Arrazoia`: Zergatik datorren pazientea (adib: "Urteko errebisioa").
  - `Egoera`: Zain dagoen, bukatuta dagoen edo ezeztatuta dagoen.
  - `PazienteNan`, `OsasunLangileIzenOsoa`: UI-an datuak errazago erakusteko aldagai lagungarriak.

### 1.6 `Jarraipena` (Sistemaren Ardatza)
- **Helburua**: Pazientearen osasun-neurketen (bereziki tentsiometroaren) erregistroa gordetzea.
- **Aldagai nagusiak**:
  - `TentsioSistolikoa`, `TentsioDiastolikoa`, `PultsuaPpm`: Tentsiometroak itzulitako benetako balio medikoak.
  - `PisuaKg`, `Altuera`: Neurketa momentuko datu fisikoak.
  - `Oharrak`: Medikuak idatzitako testu librea.
  - `BideaZerbitzarian`: Jarraipen honi lotuta sortutako PDF txostenaren bidea.

### 1.7 `Errezeta` eta `Botika`
- **Helburua**: Pazienteari agindutako medikazioa kudeatzea.
- **Aldagai nagusiak (`Errezeta`)**: `IgorpenData`, `IraungitzeData`, `Diagnostikoa`.
- **Aldagai nagusiak (`Botika`)**: `IzenKimikoa`, `EraginFokoa`.
- **Aldagai nagusiak (`ErrezetaBotika`)**: Bi aurrekoen arteko zubi-klasea da. Hemen `Dosia` (adib: "500mg") eta `Maiztasuna` (adib: "8 orduz behin") gordetzen dira.


## 2. Aplikazioko Kontrolatzaileak eta Metodoak (Logika)

Klase hauek aplikazioaren "garuna" dira. Datu-basearekin hitz egiten dute eta botoiak sakatzean exekutatzen diren ekintzak definitzen dituzte.

### 2.1 `ErabiltzaileKontrolatzailea`
Erabiltzaileen saio-hasiera eta segurtasuna kudeatzen ditu.
- `Login(string emaila, string pasahitza) : LoginEmaitza`: Erabiltzailea existitzen den eta pasahitza zuzena den egiaztatzen du. Okerra bada, blokeo bat erregistratzen du.
- `LortuLoginBlokeoEgoera() : LoginSegurtasunEgoera`: Uneko IP-a edo erabiltzailea blokeatuta ote dagoen aztertzen du (erasoen aurkako defentsa).

### 2.2 `PazienteKontrolatzailea` / `OsasunLangileKontrolatzailea`
Pazienteen eta langileen CRUD (Sortu, Irakurri, Eguneratu, Ezabatu) ekintzak.
- `LortuGuztiakPazienteak(...)`: Datu-baseko paziente guztien zerrenda eskatzen du (iragazkiak onartzen ditu).
- `SortuPazientea(Pazientea p)`: Paziente berri baten datuak balioztatu eta datu-basean txertatzen ditu.
- `AldatuPazientearenEgoera(int id, string egoeraBerria)`: Paziente bati alta edo baja emateko modu azkarra, bere datu guztiak aldatu beharrik gabe.

### 2.3 `HitzorduKontrolatzailea`
Hitzorduen kudeaketa egutegian.
- `LortuPazientearenHitzorduak(int pazienteId)`: Paziente zehatz baten historia osoa (iragana eta etorkizuna) lortzen du.
- `GehituHitzordua(Hitzordua h)`: Ordua libre dagoela egiaztatu eta hitzordu berri bat gordetzen du.
- `EguneratuHitzordua(Hitzordua h)`: Hitzordu bat ezeztatzen edo egunez aldatzen denean datu-baseari abisatzen dio.

### 2.4 `JarraipenaKontrolatzailea`
Neurketen logika zentrala.
- `GordeJarraipena(Jarraipena j)`: Tentsiometroak lortutako datuak (edo eskuz sartutakoak) DBan txertatzen ditu.
- `LortuJarraipenGuztiak(...)`: Data edo paziente zehatz baten arabera jarraipenak filtratzen ditu.
- `EsportatuXML(Jarraipena j)`: Jarraipen baten datu mediko guztiak XML formatuko fitxategi batean idazten ditu, beste sistema batzuek irakurri ahal izateko.

### 2.5 `DokumentuaKontrolatzailea` (QuestPDF sorkuntza)
- `GehituTxostena(...)`: Metodo oso indartsua. Paziente baten datuak jasotzen ditu, atzealdean diseinu txantiloi bat aplikatzen du (QuestPDF liburutegia erabiliz) eta PDF dokumentu profesional bat sortzen du sistemaren disko gogorrean, ondoren bere bidea DBan gordez.

### 2.6 `BM58Driver` (Tentsiometroaren komunikazioa)
Hardwarearekin zuzenean hitz egiten duen klasea (USB edo Bluetooth/Serial bidez).
- `EgiaztatuHardwareKonexioa()`: Tentsiometroa PC-ari fisikoki konektatuta dagoen begiratzen du.
- `IrakurriErrekordGuztiak()`: Gailuaren memorian sartu eta han gordetako azken tentsio eta pultsu neurketa gordinak (Raw Records) deskargatzen ditu.
- `KalkulatuBatezbestekoa()`: Beurer BM58-tik hartutako hainbat neurketen batezbestekoa egiten du balio mediko fidagarri bat emateko, eta zuzenean `Jarraipena` objektu berri bat itzultzen du.


## 3. Erlazio eta Kardinalitate Gida (Diagramarako)
Klase diagrama marraztean, objektu hauen arteko erlazioak zehaztu behar dira:
- **`Erabiltzailea` eta Bere Oinordekoak**: Gezi zuriak `Pazientea`-tik, `OsasunLangilea`-tik eta `HarrerakoLangilea`-tik `Erabiltzailea` klasera joan behar du (Herentzia / Generalizazioa).
- **`Pazientea` - `Hitzordua` (1:N)**: Paziente batek hitzordu asko izan ditzake bere bizitzan zehar, baina hitzordu zehatz bat paziente bakar batena da.
- **`Pazientea` - `Jarraipena` (1:N)**: Sistema osoaren muina. Pazienteak jarraipen ugari ditu historial klinikoan.
- **`Pazientea` - `OsasunLangilea` (N:M)**: Paziente bat hainbat medikurekin artatu daiteke, eta mediku batek paziente ugari ditu bere ardurapean. Honek erlazio bi-direkzionala eskatzen du.
- **`Errezeta` - `Botika` (N:M bidez `ErrezetaBotika`)**: Errezeta batek Ibuprofenoa eta Parazetamola izan ditzake; Ibuprofenoa, aldi berean, milaka errezetatan agertzen da. `ErrezetaBotika` klaseak erdiko taula gisa jokatzen du hau konpontzeko.
