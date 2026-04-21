# 3.3.1 GOsasun App: Sekuentzia Diagramen Azalpena

Aplikazioaren diseinu arkitektonikoa sakonago ulertzeko, objektuen arteko interakzioak eta denborazko exekuzioak aztertu behar dira. `diagramak/metodo_sekuentzia_diagramak` direktorioan definitu diren sekuentzia diagramak (UML) pausoz pauso esplikatzen dira jarraian, modu profesionalean eta ulergarrian.

Hau ezinbestekoa da MVC ereduak datu-basearekin nola hitz egiten duen defendatzeko.

## 1. Osasun Langilea Saioa Hasi (`1_osasun_langilea_saioa_hasi`)
Funtzio honen helburua sistemaren segurtasuna bermatzea da.
- **1. Pausoa**: Osasun langileak (Medikua/Erizaina) bere emaila eta pasahitza sartzen ditu UI-an (`SaioaHasi` formularioan) eta "Sartu" botoia sakatzen du.
- **2. Pausoa**: UI-ak datu horiek jasotzen ditu eta `ErabiltzaileKontrolatzailea`-ko `Login()` metodoari deitzen dio.
- **3. Pausoa**: Kontrolatzaileak `LoginBlokeoZerbitzua`-ri galdetzen dio ea erabiltzailea aldi baterako blokeatuta dagoen segurtasun arrazoiengatik.
- **4. Pausoa**: Dena ondo badago, Kontrolatzaileak `ErabiltzaileDB` (Repositorioa) galdekatzen du datu-basean kointzidentzia bilatzeko.
- **5. Pausoa**: Datu-baseak baieztapena (eta erabiltzailearen rola) itzultzen du. Kontrolatzaileak erantzun hori bildu (`LoginEmaitza` objektua) eta UI-ari pasatzen dio, saioa irekiz.

## 2. Harrerakoak Pazientea Sortu (`2_harrerakoa_pazientea_sortu`)
Harrerako langile baten eginkizun nagusia da.
- **1. Pausoa**: Harrerakoak pazientearen datu pertsonalak betetzen ditu interfazeko testu-koadroetan.
- **2. Pausoa**: UI-ak `Pazientea` objektu hutsa instantziatzen du datu horiekin, eta `PazienteKontrolatzailea`-ko `SortuPazientea()` funtzioari pasatzen dio.
- **3. Pausoa**: Kontrolatzaileak datuen baliozkotzea egiten du (NAN-ak formatu egokia duela, etab.).
- **4. Pausoa**: Baliozkoa bada, `PazienteaDB`-ri bidaltzen dio SQL `INSERT` sententzia bidez exekutatzeko datu-basean. UI-ak mezu berde bat erakusten du.

## 3. Pazienteak Tentsiometro Jarraipena Sortu (`3_pazientea_tentsiometro_jarraipena_sortu`)
Hardware (BM58) eta Softwarearen arteko zubi garrantzitsuena.
- **1. Pausoa**: Osasun langileak edo pazienteak berak "Tentsiometroa Irakurri" eskatzen du aplikazioan gailua USB bidez lotuta dagoenean.
- **2. Pausoa**: UI-ak `BM58Driver` klaseari deitzen dio, eta honek hardware konexioa egiaztatzen du (`EgiaztatuHardwareKonexioa`).
- **3. Pausoa**: `BM58Driver`-ak datu gordinak (`RawRecords`) irakurtzen ditu memoriaren barrutik eta batezbesteko kalkulu matematikoak egiten ditu.
- **4. Pausoa**: Driver-ak `Jarraipena` (edo `Neurketa`) objektu baliozko bat itzultzen du.
- **5. Pausoa**: Objektu hori `JarraipenaKontrolatzailea`-ra bidaltzen da, eta honek datu-baseko `neurketak` taulan gordetzen du `JarraipenaDB` klasearen bitartez.

## 4. Osasun Langileak Paziente Zerrenda Ikusi (`4_osasun_langilea_paziente_zerrenda_ikusi`)
Datuen karga masiboen optimizazioa erakusten duen prozesua.
- **1. Pausoa**: Medikuak dagokion atala irekitzen duenean, UI-ak bere mediku-identifikatzailea (ID) pasatzen dio `PazienteKontrolatzailea`-ri.
- **2. Pausoa**: Kontrolatzaileak `PazienteaDB`-ko `LortuLangilearenPazienteak()` funtzioari deitzen dio.
- **3. Pausoa**: DBak `mediku_paziente` taulako JOIN kontsulta konplexu bat egiten du mediku horri bakarrik lotutako pazienteak eskuratzeko.
- **4. Pausoa**: Datu-baseak List<Pazientea> egitura bat itzultzen dio kontrolatzaileari.
- **5. Pausoa**: UI-ak zerrenda hori iteratu eta `DataGridView` taula batean marrazten du pantailan (paginazioa aplikatuz errendimendua ez erortzeko).

## 5. Osasun Langileak Errezeta Sortu (`5_osasun_langilea_errezeta_sortu`)
Taula anitz ukitzen dituen SQL Transakzio bat defendatzen du diagrama honek.
- **1. Pausoa**: Medikuak paziente bat hautatu eta "Errezeta Sortu" ematen du, diagnosia eta botikak aukeratuz.
- **2. Pausoa**: UI-ak `Errezeta` eta `ErrezetaBotika` objektuak instanziatzen ditu eta `ErrezetaKontrolatzailea`-ri bidaltzen dizkio.
- **3. Pausoa**: Kontrolatzaileak `ErrezetaDB`-ra deitzen du. Lehenik errezeta nagusia gordetzen da (`INSERT INTO errezetak`) eta honek ID berria bueltatzen du.
- **4. Pausoa**: Ekoitzitako ID berri horrekin, botika bakoitzeko `INSERT INTO errezeta_botikak` exekutatzen da.
- **5. Pausoa**: Dena ondo gorde dela ziurtatu ondoren, UI-ari berresten zaio prozesua.

## 6. Osasun Langileak Dokumentua Gehitu (`6_osasun_langilea_dokumentua_gehitu_jarraipenari`)
Fitxategien sistema eta datu-basea uztartzen dituen fluxua.
- **1. Pausoa**: Medikuak pazientearen PDF edo irudi txosten bat ordenagailutik hautatzen du.
- **2. Pausoa**: UI-ak `DokumentuaKontrolatzailea`-ko `GordeDokumentua()` eskatzen du.
- **3. Pausoa**: Kontrolatzaileak lehenik fitxategi fisikoa zerbitzariaren biltegi segurura (`\Dokumentuak\` karpetara) kopiatzen du, izen esklusibo bat (adibidez denbora-zigiluarekin) emanez.
- **4. Pausoa**: Behin fitxategia gorde dela, Kontrolatzaileak fitxategiaren ibilbidea (`Path`), izena eta jarraipenaren ID-a biltzen dituen `Dokumentua` objektua sortzen du.
- **5. Pausoa**: `DokumentuaDB`-k bide hori datu-baseko `dokumentuak` taulan txertatzen du.
