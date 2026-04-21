# 3.2. Datu-basearen Diseinu Aurreratua

Datu-basearen diseinua informazioaren osotasuna, segurtasuna eta abiadura bermatzen dituen arkitektura konplexu bat da. Gure aplikazioaren `sql/` direktorioan egituratutako dokumentazio ofizialean oinarrituta (`GOsasun_DB_dokumentazioa.md`), jarraian arkitektura erlazional zehatza esplikatzen eta defendatzen da.

## Datuen Normalizazioa eta Taulen Egitura (3NF)
Datu-basea modu profesionalean diseinatzeko erredundantzia minimizatu da. Identifikatzaile nagusiak (`PK`) eta atzerritarrak (`FK`) zehatz finkatu dira 14 taula ezberdinetan:
- **Erabiltzaileen herentzia**: `erabiltzaileak` da guraso-taula (`email` UNIQUE murrizketarekin). Bere azpian, 1:1 loturaz, `pazienteak` (`nan` UNIQUE), `medikuak` (`elkargokide_zenbakia` UNIQUE) eta `harrerako_langileak` taulek osatzen dute sistema.
- **Lotura Taulak (N:M)**: Mediku batek paziente asko dituenez eta alderantziz, `mediku_paziente` taula sortu dugu erdian. Era berean, errezeten eta botiken arteko lotura konplexua konpontzeko, `errezeta_botikak` taula diseinatu da (biak bakoitzaren FK-ekin lotuta).
- **Taula Medikoak**: `neurketak` (pazienteen jarraipen fisikoetarako), `hitzorduak`, `dokumentuak`, `errezetak` eta `abisuak`.

## Osotasun Erreferentziala: ON DELETE eta ON UPDATE Erabakiak
`GOsasun_DB` diseinuaren alderdi garrantzitsuena erlazioen kudeaketa automatikoa da (datu zaborra saihesteko).
- **`ON UPDATE CASCADE`**: Erreferentziatutako taularen (gurasoaren) PK-a aldatzen bada, menpeko erregistro guztietan informazio hori automatikoki eguneratzen da. Taula guztietan aplikatu da.
- **`ON DELETE RESTRICT`**: Guraso-taula bateko erregistrorik ezin da ezabatu menpeko taularen batean erabiltzen ari bada. Segurtasun neurri bat da, ustekabeko ezabatzeak ekiditeko. Adibidez, `erabiltzaileak` taulan ezarri da. Paziente bat ez bada garbitu, bere oinarrizko erabiltzailea ezin da zuzenean suntsitu.
- **`ON DELETE SET NULL`**: Bereziki `errezetak` taulan erabilia: Hitzordu bat ezabatzen bada, hari lotutako errezeta ez da ezabatzen; lotura `NULL` geratzen da (`hitzordu_id`), datu mediko historikoa mantendu ahal izateko. Horrelako erabakiek profesionaltasun maila handia ematen diote datu-baseari.

## Segurtasuna: Erabiltzaileak eta Rolen Baimenak
Aplikazioa eta Weba bereizteko, "Principle of Least Privilege" jarraituz DCL (Data Control Language) bidez baimen zorrotzak sortu dira:
- **`db_harrerakoa`**: `erabiltzaileak` eta `hitzorduak` tauletan `SELECT`, `INSERT`, `UPDATE` edo `DELETE` baimenak ditu administraziorako. Ezin du `neurketak` edo `errezetak` kudeatu.
- Beste rol batzuk beren baimen murriztuekin sortu dira pribatutasuna bermatzeko (RGPD).

## Bistak (Views)
Kontsulta konplexuak eta etengabeak sinplifikatzeko (bereziki PHP aplikazioan erabili ahal izateko), hainbat bista zehaztu dira:
- **`V_Login`**: Kredentzialak eta rolak bateratzen ditu azkar erabiltzeko.
- **`V_Pazientea`** / **`V_Medikua`** / **`V_Harrera`**: Kontuaren oinarrizko datuak bere azpi-taulekin batzen dituzte.
- **`V_Mediku_Pazienteak`**: Mediku bati esleitutako pazienteen zerrenda azkar bat lortzeko (N:M taulak saihestuz exekuzioan).
- **`V_Hitzorduak_Osoa`**: Agendako datak eta langile/pazienteen izen guztiak JOIN bidez ebatzita itzultzen ditu.
- **`V_Abisuak_Osoa`**: Abisu kliniko zein pertsonalen bateratzea.

## Indizeak (Indexes) eta Errendimenduaren Optimizazioa
Datu-baseko kontsultak optimizatzeko `CREATE INDEX` sententziak definitu dira `GOsasun_DB_indizeak.sql` fitxategian. Hauek erabaki ditugu:
- **`idx_pazienteak_abizenak_izena`**: Pazienteak abizenen eta izenaren arabera askotan bilatzen direlako Harreran. Honek *Table Scan* saihesten du.
- **`idx_hitzorduak_data`**: Agendan egun espezifikoak kargatzea oso ohikoa denez.
- **`idx_neurketak_paziente_data`**: Tentsiometroen neurketak marraztean (adibidez grafikoetan), paziente zehatz baten neurketak denboran ordenatuta eskatzen direlako. Errendimendua %80 arte hobe daiteke bolumen handietan.

## Trigger-ak (Disparadoreak)
`GOsasun_DB_trigger.sql`-n negozio-logikaren zati bat Datu-Base mailan automatizatu da aurreratuagoak izateko. Ekintza (INSERT/UPDATE) bakoitzak beste atal bateko datuak automatikoki aldatzea dakar, adibidez pazientearen estatus orokorra aldatzea jarraipen espezifiko baten emaitzen arabera.
