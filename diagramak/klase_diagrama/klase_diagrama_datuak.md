# GOsasun App - Klase Diagramarako Datuak

Dokumentu honek klase diagrama prestatzeko behar diren oinarrizko datuak biltzen ditu. Fokua `Modeloa` karpetako klaseetan dago, eta bigarren atalean aplikazioan benetan erabiltzen diren metodo publiko nagusiak zerrendatzen dira, klase-diagraman eragiketa gisa sartu nahi badira.

## 1. Modeloko klaseak eta beraien loturak

### 1.1 Klase guztiak

Erabili diren modeloko klaseak hauek dira:

1. `Erabiltzailea`.
1. `Pazientea`.
1. `OsasunLangilea`.
1. `HarrerakoLangilea`.
1. `Rola`.
1. `Hitzordua`.
1. `Jarraipena`.
1. `Dokumentua`.
1. `Errezeta`.
1. `ErrezetaBotika`.
1. `Botika`.

### 1.2 Klase bakoitzaren laburpena

#### `Erabiltzailea` (abstraktua)

- Mota: oinarrizko klase abstraktua.
- Helburua: sistemako erabiltzaile guztien datu komunak biltzea.
- Propietate nagusiak:
  `Id`, `Emaila`, `Pasahitza`, `RolId`, `Nan`, `Izena`, `Abizenak`, `JaiotzeData`, `Telefonoa`, `Helbidea`, `Herria`, `PostaKodea`, `Irudia`, `Aktibo`, `SortzeData`, `Hizkuntza`.
- Propietate/portaera eratorriak:
  `IzenOsoa`, `Rola`, `DaPazientea()`, `DaOsasunLangilea()`.

#### `Pazientea : Erabiltzailea`

- Helburua: paziente baten datu kliniko eta pertsonalak modelatzea.
- Propietate osagarriak:
  `Sexua`, `OdolTaldea`, `AzkenAltuera`, `AzkenPisua`, `EgoeraKlinikoa`, `OsasunLangileak`.
- Oharra: `OsasunLangileak` atributuak paziente bati lotutako osasun langileen zerrenda adierazten du.

#### `OsasunLangilea : Erabiltzailea`

- Helburua: mediku edo osasun profesional baten datuak modelatzea.
- Propietate osagarriak:
  `ElkargokideZenbakia`, `Espezialitatea`, `Kontsulta`, `Lanaldia`, `Pazienteak`.
- Oharra: `Pazienteak` atributuak profesional horri lotutako pazienteak adierazten ditu.

#### `HarrerakoLangilea : Erabiltzailea`

- Helburua: harrerako langile baten datuak modelatzea.
- Propietate osagarria:
  `Txanda`.

#### `Rola`

- Helburua: erabiltzailearen rol katalogoa modelatzea.
- Propietateak:
  `RolId`, `Izena`.

#### `Hitzordua`

- Helburua: paziente baten eta osasun langile baten arteko hitzordu bat modelatzea.
- Propietate nagusiak:
  `HitzorduId`, `PazienteId`, `OsasunLangileId`, `Data`, `HasieraOrdua`, `BukaeraOrdua`, `Arrazoia`, `Egoera`, `SortzeData`.
- UI/Join bidezko propietate osagarriak:
  `PazienteNan`, `PazienteIzena`, `PazienteAbizenak`, `OsasunLangileIzena`, `OsasunLangileAbizenak`, `PazienteIzenOsoa`, `OsasunLangileIzenOsoa`.

#### `Jarraipena`

- Helburua: pazientearen osasun neurketa edo jarraipen-erregistro bat modelatzea.
- Propietate nagusiak:
  `Id`, `PazienteId`, `OsasunLangileId`, `TentsioSistolikoa`, `TentsioDiastolikoa`, `PisuaKg`, `Altuera`, `PultsuaPpm`, `Oharrak`, `BideaZerbitzarian`, `ErregistroData`, `DokumentuKopurua`.
- UI/Join propietateak:
  `PazienteNan`, `PazienteIzena`, `PazienteAbizenak`, `PazienteIzenOsoa`, `EkintzakTestua`.

#### `Dokumentua`

- Helburua: paziente edo jarraipen bati lotutako dokumentu baten metadata modelatzea.
- Propietate nagusiak:
  `Id`, `JarraipenaId`, `PazienteId`, `FitxategiIzena`, `BideaZerbitzarian`, `DokumentuIzena`, `Deskribapena`, `IgotzeData`, `JarraipenData`.
- UI/Join propietateak:
  `PazienteNan`, `PazienteIzena`, `PazienteAbizenak`, `PazienteIzenOsoa`.

#### `Errezeta`

- Helburua: paziente bati emandako errezeta edo diagnostiko-erregistro bat modelatzea.
- Propietate nagusiak:
  `ErrezetaId`, `HitzorduId`, `OsasunLangileId`, `PazienteId`, `IgorpenData`, `IraungitzeData`, `XmlBidea`, `Diagnostikoa`, `Aktibo`.
- Lotura-propietatea:
  `Botikak : List<ErrezetaBotika>`.
- UI/Join propietateak:
  `PazienteIzenOsoa`, `PazienteNan`, `HitzorduData`.

#### `ErrezetaBotika`

- Helburua: errezeta baten eta botika baten arteko lotura modelatzea.
- Propietateak:
  `LoturaId`, `ErrezetaId`, `BotikaId`, `Dosia`, `Maiztasuna`, `BotikaIzena`.

#### `Botika`

- Helburua: botika katalogoko elementu bat modelatzea.
- Propietateak:
  `BotikaId`, `Izena`, `IzenKimikoa`, `NomenklaturaKimikoa`, `EraginFokoa`, `Aktibitatea`.

### 1.3 Lotura nagusiak klase diagramarako

#### Herentzia

1. `Pazientea` -> `Erabiltzailea`.
1. `OsasunLangilea` -> `Erabiltzailea`.
1. `HarrerakoLangilea` -> `Erabiltzailea`.

#### Elkarteak eta mendekotasunak

1. `Erabiltzailea` -> `Rola`.
   `RolId` atributuaren bidez rol bat dauka.

1. `Pazientea` <-> `OsasunLangilea`.
   Erlazio asko-asko motakoa da kontzeptualki.
   `Pazientea.OsasunLangileak` eta `OsasunLangilea.Pazienteak` atributuek adierazten dute.

1. `Hitzordua` -> `Pazientea`.
   `PazienteId` bidez lotzen da.

1. `Hitzordua` -> `OsasunLangilea`.
   `OsasunLangileId` bidez lotzen da.

1. `Jarraipena` -> `Pazientea`.
   `PazienteId` bidez lotzen da.

1. `Jarraipena` -> `OsasunLangilea`.
   `OsasunLangileId` atributua aukerakoa da (`nullable`).

1. `Dokumentua` -> `Jarraipena`.
   `JarraipenaId` bidez lotzen da.

1. `Dokumentua` -> `Pazientea`.
   `PazienteId` bidez lotzen da.

1. `Errezeta` -> `Pazientea`.
   `PazienteId` bidez lotzen da.

1. `Errezeta` -> `OsasunLangilea`.
   `OsasunLangileId` bidez lotzen da.

1. `Errezeta` -> `Hitzordua`.
   `HitzorduId` atributua aukerakoa da (`nullable`).

1. `Errezeta` -> `ErrezetaBotika`.
   Konposizio edo agregazio moduan marraztu daiteke, `Botikak` zerrenda duelako.

1. `ErrezetaBotika` -> `Botika`.
   `BotikaId` bidez lotzen da.

1. `ErrezetaBotika` -> `Errezeta`.
   `ErrezetaId` bidez lotzen da.

### 1.4 Diagrama marrazteko gomendatutako kardinalitateak

1. `Erabiltzailea` - `Rola`: `*` -> `1`.
1. `Pazientea` - `OsasunLangilea`: `*` <-> `*`.
1. `Pazientea` - `Hitzordua`: `1` -> `*`.
1. `OsasunLangilea` - `Hitzordua`: `1` -> `*`.
1. `Pazientea` - `Jarraipena`: `1` -> `*`.
1. `OsasunLangilea` - `Jarraipena`: `1` -> `0..*`.
1. `Jarraipena` - `Dokumentua`: `1` -> `0..*`.
1. `Pazientea` - `Dokumentua`: `1` -> `0..*`.
1. `Pazientea` - `Errezeta`: `1` -> `0..*`.
1. `OsasunLangilea` - `Errezeta`: `1` -> `0..*`.
1. `Hitzordua` - `Errezeta`: `1` -> `0..*` edo `0..1` -> `0..*`, egin nahi duzun xehetasun mailaren arabera.
1. `Errezeta` - `ErrezetaBotika`: `1` -> `1..*`.
1. `Botika` - `ErrezetaBotika`: `1` -> `0..*`.

## 2. App-ean erabili diren metodo publiko nagusiak

Atal honek modeloko klaseekin lan egiten duten metodo publiko nagusiak zerrendatzen ditu. Klase-diagraman metodo guzti-guztiak sartu beharrean, hemen daudenak dira erabilgarrienak eragiketa edo dependentzia gisa marrazteko.

### 2.1 Modeloko metodoak

#### `Erabiltzailea`

1. `DaPazientea() : bool`.
2. `DaOsasunLangilea() : bool`.

#### `Pazientea`

1. `DaPazientea() : bool` override.

#### `OsasunLangilea`

1. `DaOsasunLangilea() : bool` override.

### 2.2 Erabiltzaileen kudeaketa

#### `ErabiltzaileKontrolatzailea`

1. `Login(string emaila, string pasahitza) : LoginEmaitza`.
2. `LortuLoginBlokeoEgoera() : LoginSegurtasunEgoera`.
3. `LortuLangilearenPazienteak(int langileId, string? bilatzailea = null, string? egoeraFiltroa = null) : List<Pazientea>`.
4. `LortuGuztiakPazienteak(string? bilatzailea = null, string? egoeraFiltroa = null) : List<Pazientea>`.
5. `LortuGuztiakOsasunLangileak(string? bilatzailea = null) : List<OsasunLangilea>`.
6. `LortuPazientea(int pazienteId) : Pazientea?`.
7. `LortuOsasunLangilea(int osasunLangileId) : OsasunLangilea?`.
8. `LortuGuztiakHarrerakoak(string? bilatzailea = null) : List<HarrerakoLangilea>`.
9. `LortuHarrerakoa(int harrerakoaId) : HarrerakoLangilea?`.
10. `LortuPazientearenOsasunLangileak(int pazienteId) : List<OsasunLangilea>`.
11. `SortuPazientea(Pazientea p) : bool`.
12. `SortuPazientea(Pazientea p, IEnumerable<int> osasunLangileIds, string? irudiBidea) : bool`.
13. `SortuOsasunLangilea(OsasunLangilea m) : bool`.
14. `SortuOsasunLangilea(OsasunLangilea m, string? irudiBidea) : bool`.
15. `SortuHarrerakoa(HarrerakoLangilea h) : bool`.
16. `SortuHarrerakoa(HarrerakoLangilea h, string? irudiBidea) : bool`.
17. `EsleituOsasunLangileakPazienteari(int pazienteId, IEnumerable<int> osasunLangileIds) : bool`.
18. `EzabatuPazientea(int id) : bool`.
19. `EzabatuOsasunLangilea(int id) : bool`.
20. `EzabatuHarrerakoa(int id) : bool`.
21. `EguneratuPazientea(Pazientea p) : bool`.
22. `EguneratuOsasunLangilea(OsasunLangilea m) : bool`.
23. `EguneratuHarrerakoa(HarrerakoLangilea h) : bool`.
24. `AldatuPazientearenEgoera(int pazienteId, string egoeraBerria) : bool`.

#### `ErabiltzaileDB`

1. `Login(string emaila, string pasahitza) : Erabiltzailea?`.

#### `PazienteaDB`

1. `LortuLangilearenPazienteak(int langileId, string? bilatzailea = null, string? egoeraFiltroa = null) : List<Pazientea>`.
2. `LortuGuztiakPazienteak(string? bilatzailea = null, string? egoeraFiltroa = null) : List<Pazientea>`.
3. `LortuPazientea(int pazienteId) : Pazientea?`.
4. `LortuPazientearenOsasunLangileak(int pazienteId) : List<OsasunLangilea>`.
5. `SortuPazientea(Pazientea p) : bool`.
6. `SortuPazientea(Pazientea p, IEnumerable<int>? osasunLangileIds, string? irudiBidea) : bool`.
7. `EzabatuPazientea(int id) : bool`.
8. `EguneratuPazientea(Pazientea p) : bool`.
9. `AldatuPazientearenEgoera(int pazienteId, string egoeraBerria) : bool`.
10. `EsleituOsasunLangileakPazienteari(int pazienteId, IEnumerable<int> osasunLangileIds) : bool`.

#### `OsasunLangileaDB`

1. `LortuGuztiakOsasunLangileak(string? bilatzailea = null) : List<OsasunLangilea>`.
2. `LortuOsasunLangilea(int osasunLangileId) : OsasunLangilea?`.
3. `SortuOsasunLangilea(OsasunLangilea m) : bool`.
4. `SortuOsasunLangilea(OsasunLangilea m, string? irudiBidea) : bool`.
5. `EzabatuOsasunLangilea(int id) : bool`.
6. `EguneratuOsasunLangilea(OsasunLangilea m) : bool`.

#### `HarrerakoLangileaDB`

1. `LortuGuztiakHarrerakoak(string? bilatzailea = null) : List<HarrerakoLangilea>`.
2. `LortuHarrerakoa(int harrerakoaId) : HarrerakoLangilea?`.
3. `SortuHarrerakoa(HarrerakoLangilea h) : bool`.
4. `SortuHarrerakoa(HarrerakoLangilea h, string? irudiBidea) : bool`.
5. `EzabatuHarrerakoa(int id) : bool`.
6. `EguneratuHarrerakoa(HarrerakoLangilea h) : bool`.

### 2.3 Hitzorduen kudeaketa

#### `HitzorduKontrolatzailea`

1. `LortuHitzorduGuztiak() : List<Hitzordua>`.
2. `LortuPazientearenHitzorduak(int pazienteId) : List<Hitzordua>`.
3. `LortuOsasunLangilearenHitzorduak(int osasunLangileId) : List<Hitzordua>`.
4. `GehituHitzordua(Hitzordua h) : void`.
5. `EguneratuHitzordua(Hitzordua h) : void`.
6. `EzabatuHitzordua(int hitzorduId) : void`.

#### `HitzorduDB`

1. `LortuHitzorduGuztiak() : List<Hitzordua>`.
2. `LortuPazientearenHitzorduak(int pazienteId) : List<Hitzordua>`.
3. `LortuOsasunLangilearenHitzorduak(int langileId) : List<Hitzordua>`.
4. `GehituHitzordua(Hitzordua h) : void`.
5. `EguneratuHitzordua(Hitzordua h) : void`.
6. `EzabatuHitzordua(int hitzorduId) : void`.

### 2.4 Jarraipenen kudeaketa

#### `JarraipenaKontrolatzailea`

1. `LortuPazientearenJarraipenak(int pazienteId) : List<Jarraipena>`.
2. `LortuJarraipenGuztiak(string? bilaketa = null, DateTime? hasieraData = null, DateTime? amaieraData = null, int? pazienteId = null) : List<Jarraipena>`.
3. `LortuJarraipena(int jarraipenaId) : Jarraipena?`.
4. `GordeJarraipena(Jarraipena jarraipena) : bool`.
5. `GordeJarraipenaEtaLortuId(Jarraipena jarraipena) : int?`.
6. `EzabatuJarraipena(int jarraipenaId) : bool`.
7. `EguneratuJarraipena(Jarraipena jarraipena) : bool`.
8. `LortuJarraipenarenDokumentuak(int jarraipenaId) : List<Dokumentua>`.
9. `GordeDokumentua(Dokumentua dokumentua) : bool`.
10. `EsportatuXML(Jarraipena n) : void`.

#### `JarraipenaDB`

1. `LortuJarraipenGuztiak(...) : List<Jarraipena>`.
2. `LortuPazientearenJarraipenak(int pazienteId) : List<Jarraipena>`.
3. `LortuJarraipena(int jarraipenaId) : Jarraipena?`.
4. `GordeJarraipenaEtaLortuId(Jarraipena jarraipena) : int?`.
5. `GordeJarraipena(Jarraipena jarraipena) : bool`.
6. `EguneratuJarraipena(Jarraipena jarraipena) : bool`.
7. `EzabatuJarraipena(int jarraipenaId) : bool`.

### 2.5 Dokumentuen kudeaketa

#### `DokumentuaKontrolatzailea`

1. `LortuDokumentuak(...) : List<Dokumentua>`.
2. `LortuJarraipenarenDokumentuak(int jarraipenaId) : List<Dokumentua>`.
3. `LortuPazientearenBesteDokumentuak(int pazienteId, int? baztertuJarraipenaId = null, string? bilaketa = null) : List<Dokumentua>`.
4. `LortuDokumentua(int dokumentuId) : Dokumentua?`.
5. `GordeDokumentua(Dokumentua dokumentua) : bool`.
6. `EguneratuDokumentua(Dokumentua dokumentua) : bool`.
7. `EzabatuDokumentua(int dokumentuId) : bool`.
8. `BerrlotuDokumentuaJarraipenera(int dokumentuId, int jarraipenaId) : bool`.
9. `ZiurtatuJarraipena(int pazienteId, int? jarraipenaId, int? osasunLangileId, string? oharrak = null) : int?`.
10. `GehituDokumentuGenerikoa(...) : bool`.
11. `GehituTxostena(...) : bool`.

#### `DokumentuaDB`

1. `LortuDokumentuGuztiak(...) : List<Dokumentua>`.
2. `LortuJarraipenarenDokumentuak(int jarraipenaId) : List<Dokumentua>`.
3. `LortuPazientearenBesteDokumentuak(...) : List<Dokumentua>`.
4. `LortuDokumentua(int dokumentuId) : Dokumentua?`.
5. `GordeDokumentua(Dokumentua dokumentua) : bool`.
6. `EguneratuDokumentua(Dokumentua dokumentua) : bool`.
7. `AldatuDokumentuarenJarraipena(int dokumentuId, int jarraipenaId) : bool`.
8. `EzabatuDokumentua(int dokumentuId) : bool`.

### 2.6 Errezeten eta botiken kudeaketa

#### `ErrezetaDB`

1. `LortuErrezetaGuztiak(bool soilikAktiboak = true) : List<Errezeta>`.
2. `SortuErrezeta(Errezeta errezeta) : bool`.
3. `LortuOsasunLangilearenErrezetak(int langileId, bool soilikAktiboak = true) : List<Errezeta>`.
4. `LortuPazientearenErrezetak(int pazienteId, bool soilikAktiboak = true) : List<Errezeta>`.
5. `EguneratuErrezeta(Errezeta errezeta) : bool`.
6. `EzabatuErrezeta(int errezetaId) : bool`.

#### `BotikaDB`

1. `LortuBotikaGuztiak() : List<Botika>`.

### 2.7 BM58 eta beste zerbitzu lagungarri batzuk

Klase-diagrama zabalago bat egin nahi baduzu, modeloei zuzenean eragiten dieten zerbitzu hauek ere kontuan hartu ditzakezu:

#### `BM58Driver`

1. `EgiaztatuHardwareKonexioa() : bool`.
2. `BilatuGailua(out bool isHid) : string?`.
3. `IrakurriErrekordGuztiak(string identifier, bool isHid) : List<BM58RawRecord>`.
4. `KalkulatuBatezbestekoa(List<BM58RawRecord> records, int pazienteId, int memoria) : Jarraipena?`.
5. `LortuAzkenNeurketa(List<BM58RawRecord> records, int pazienteId, int memoria) : Jarraipena?`.
6. `AnalizatuErrekordak(List<BM58RawRecord> records) : MemoriaInformazioa`.

#### `DokumentuPdfZerbitzua`

1. `SortuHelmugaBidea(string fitxategiIzena) : string`.
2. `SortuPazientearenTxostena(...) : string`.

## 3. Klase diagraman sartzeko gomendio praktikoa

Klase diagrama argi geratzeko, honako estrategia hau gomendatzen da:

1. Lehen mailako diagrama: `Modeloa` karpetako 11 klaseak bakarrik, haien atributu eta loturekin.
2. Bigarren mailako diagrama edo oharra: `Kontrola` eta `Repositorioa` geruzetako metodo publiko nagusiak, operazio edo dependentzia gisa.
3. Ez sartu UI klase guztiak diagrama berean, bestela gehiegi handituko da.
4. `Erabiltzailea` abstraktu gisa marraztu eta hiru azpiklaseak herentzia-geziekin lotu.
5. `Errezeta` -> `ErrezetaBotika` -> `Botika` zatia aparteko azpidiagrama moduan marraztea komeni da, oso argi ikusteko.
