# 3.2. Datu-basearen Diseinu Aurreratua

Datu-basearen diseinua ez da taulak sortzera mugatzen; informazioaren osotasuna, segurtasuna eta abiadura bermatzen dituen arkitektura konplexu bat definitzea eskatzen du. MySQL erabiliz, egitura normalizatu (3NF) eta mardul bat osatu da.

## Datuen Normalizazioa (3NF)
Datu-basea modu profesionalean diseinatzeko, hiru forma normalak aplikatu dira erredundantzia minimizatzeko eta anomaliak saihesteko:
1. **1NF (Lehen Forma Normala)**: Taula guztiek gako nagusi bat dute (`id`). Ez dago balio anitzeko atributurik (adibidez, paziente batek bi telefono balitu, aparteko taula bat erabiliko litzateke, nahiz eta proiektu honetan telefono bakarra onartu dugun sinpletasunagatik). Atributu guztiak atomikoak dira.
2. **2NF (Bigarren Forma Normala)**: 1NF betetzen da eta gako partzialen mendekotasunik ez dago. Hau da, gako nagusia konposatua den tauletan (adibidez `Pazientek_Langileak`), atributu guztiak gako osoaren mende daude, eta ez zati baten mende.
3. **3NF (Hirugarren Forma Normala)**: 2NF betetzen da eta ez dago mendekotasun iragankorrik. Adibidez, `Pazienteak` taulan ez dugu medikuaren izena gordetzen, baizik eta erlazio taula bat erabiltzen dugu. Datu guztiak zuzenean beraien entitatearen gako nagusiaren mende daude soilik.

## Segurtasuna: Erabiltzaileak eta Rolen Baimenak
Sistemaren atal ezberdinek (Weba eta Aplikazioa) datu-basera sarbide ezberdina izan behar dute segurtasuna bermatzeko. Horretarako, "Principle of Least Privilege" (Pribilegio Txikienaren Printzipioa) jarraituz, datu-base mailan hiru erabiltzaile ezberdin sortu dira DCL (Data Control Language) bidez:

1. **`db_osasun_langilea`**: 
   - **Deskribapena**: C# mahaigaineko aplikaziotik konektatuko den erabiltzailea, sendagileentzat eta erizainentzat.
   - **Baimenak**: `SELECT`, `INSERT`, `UPDATE` baimenak ditu `pazienteak`, `jarraipenak`, `dokumentuak`, eta `errezetak` tauletan. Ezin du erabiltzaile berririk sortu edo ezabatu (hori harreraren lana da).
2. **`db_harrerakoa`**:
   - **Deskribapena**: Harrerako langileek aplikaziotik konektatzeko erabiliko duten rola.
   - **Baimenak**: `SELECT`, `INSERT`, `UPDATE` erabiltzaile administratiboen tauletan (`erabiltzaileak`, `pazienteak` oinarrizko datuak) eta `hitzorduak` taulan. Datu klinikoak (jarraipenak, txostenak) irakurtzeko debekua du (Ez du `SELECT` baimenik taula horietan) pribatutasun legeak (RGPD) betetzeko.
3. **`db_pazientea`**:
   - **Deskribapena**: PHP Backend-etik webgunean saioa hasten den bakoitzean erabiliko den datu-baseko erabiltzailea.
   - **Baimenak**: Muga oso zorrotzak. Soilik `SELECT` baimenak ditu bista (Views) espezifiko batzuetan, inola ere ezin ditu datuak manipulatu edo beste paziente batzuen datuak ikusi.

## Bistak (Views)
Datuen segurtasuna hobetzeko eta SQL kontsulta konplexuak aplikazioaren kodean barneratzea ekiditeko, bistak sortu dira:
- **`v_paziente_jarraipenak`**: Pazienteen oinarrizko datuak (`izena`, `abizenak`) eta haien jarraipen-datuak lotzen ditu `JOIN` bidez. Webgunean kontsulta garbiak egiteko aproposa.
- **`v_langile_hitzorduak`**: Osasun langilearen informazioa eta bere hitzorduen zerrenda uztartzen ditu, programazioan `SELECT * FROM v_langile_hitzorduak` bezalako sententzia sinpleak erabiltzeko.

## Indizeak (Indexes) eta Errendimenduaren Optimizazioa
Datu-basea hazten denean errendimendua ez galtzeko, B-Tree indizeak sortu dira bilaketak azkartzeko. Zergatik indize hauek?
- **`idx_erabiltzaile_email` (Unique Index)**: `erabiltzaileak.email` zutabean. Saio-hasiera (Login) prozesuan bilaketa konstanteak egingo dira zutabe honen bidez. Indize honek "Table Scan" izugarri bat ekiditen du.
- **`idx_erabiltzaile_nan` (Unique Index)**: `erabiltzaileak.nan` zutabean. Harrerak bilaketak askotan NAN bidez egingo dituelako.
- **Gako Atzerritarren Indizeak (FK Indexes)**: InnoDB motorrak automatikoki sortzen baditu ere, esplizituki berrikusi dira `paziente_id` eta `osasun_langile_id` zutabeetako indizeak, ezinbestekoak direlako `JOIN` eragiketak optimizatzeko (adibidez, jarraipen guztietatik paziente jakin batenak bilatzean).

## Osotasun Erreferentziala: ON DELETE eta ON UPDATE Erabakiak
Taulen arteko erlazioetan jokabide automatikoak definitu dira datu zaborra ("Orphaned records") saihesteko:

- **Herentzia simulazioan (`pazienteak`, `osasun_langileak` -> `erabiltzaileak`)**:
  - `ON DELETE CASCADE`: `erabiltzaileak` taulan pertsona bat ezabatzen bada, automatikoki ezabatuko da `pazienteak` edo `osasun_langileak` taulan (datu guztiak suntsituz).
  - `ON UPDATE CASCADE`: PK/ID-a aldatuko balitz, azpi-tauletan islatuko litzateke.
- **`jarraipenak` taulan**:
  - FK `paziente_id`: `ON DELETE CASCADE`. Pazientea ezabatzen bada, bere neurketa eta jarraipen historiko osoa desagertuko da araudi legalen baitan (Ahaztua izateko eskubidea).
  - FK `osasun_langile_id`: `ON DELETE SET NULL`. Jarraipena egin zuen medikua sistematik ezabatzen bada, jarraipenaren datuak mantenduko dira (medikoki garrantzitsuak direlako), baina medikuaren erreferentzia balio nulu (NULL) batekin ordezkatuko da.
- **`hitzorduak` taulan**:
  - FK `osasun_langile_id`: `ON DELETE RESTRICT`. Ezin da mediku bat ezabatu etorkizunean hitzorduak esleituta baditu. Erabiltzailea ohartaraziko da lehenengo hitzordu horiek berriro esleitzeko beste mediku bati.

## Trigger-ak (Disparadoreak)
Negozio-logikaren zati bat datu-base mailan automatizatu da Trigger profesionalen bidez:
- **`trg_eguneratu_neurri_fisikoak` (AFTER INSERT ON jarraipenak)**:
  - **Azalpena**: Osasun langile batek C# aplikaziotik jarraipen berri bat (neurketa) txertatzen duen bakoitzean, trigger hau aktibatzen da.
  - **Funtzionamendua**: Jarraipen berrian `pisua` edo `altuera` eremuak bete badira, triggerrak `UPDATE` bat egiten du `pazienteak` taulako `azken_pisua` eta `azken_altuera` zutabeetan.
  - **Justifikazioa**: Honela, webgunean edo profileko pantaila nagusian pazientearen pisu eta altuera eguneratuena erakuts dezakegu kontsulta historiko pisutsu bat egin gabe. Automatikoki sinkronizatuta mantentzen da sistema.
