# 4.1. Datu-basearen Inplementazioa

Garapen-fase honen helburu nagusia diseinuan planteatutako eredu erlazional zehatza (`sql/GOsasun_DB_dokumentazioa.md`-n islatuta) **SQL kode exekutagarri** bihurtzea izan da. Datu-basea Apache/MySQL zerbitzarian txertatu da akatsik gabe.

## Diseinuaren Defentsa eta Gauzatzea
Diseinuan erabakitako taulen egitura eta dependentziak modu profesionalean inplementatu dira MySQL-n, gako atzerritarren murrizketa espezifikoekin (`RESTRICT` eta `SET NULL`).

**Kode pantailazoa: Taulen sorrera era profesionalean (`GOsasun_DB.sql` eredu)**
```sql
-- Erabiltzaile nagusiaren egitura
CREATE TABLE erabiltzaileak (
    id INT AUTO_INCREMENT PRIMARY KEY,
    rol_id INT NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    -- (Beste datu pertsonalak)
    FOREIGN KEY (rol_id) REFERENCES rolak(id) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB;

-- Pazientearen 1:1 inplementazioa
CREATE TABLE pazienteak (
    id INT PRIMARY KEY,
    nan VARCHAR(15) UNIQUE NOT NULL,
    -- Erakutsi dugun bezala, RESTRICT erabili da segurtasun handiagorako ezabatzeetan
    FOREIGN KEY (id) REFERENCES erabiltzaileak(id) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB;

-- Errezeta historikoen mantentze aurreratua (SET NULL inplementazioa)
CREATE TABLE errezetak (
    id INT AUTO_INCREMENT PRIMARY KEY,
    hitzordu_id INT, -- Hitzordua bertan behera uzten bada, null-era pasako da
    mediku_id INT NOT NULL,
    paziente_id INT NOT NULL,
    FOREIGN KEY (hitzordu_id) REFERENCES hitzorduak(id) ON UPDATE CASCADE ON DELETE SET NULL,
    FOREIGN KEY (paziente_id) REFERENCES pazienteak(id) ON UPDATE CASCADE ON DELETE RESTRICT
) ENGINE=InnoDB;
```
**Azalpena**: Inplementazio honek MySQL-ren `InnoDB` motorraren gaitasun guztiak probesten ditu. `ON DELETE RESTRICT` erabili dugu paziente edo hitzordu bat nahi gabe ezabatzea ekiditeko datu medikoetan inplikatuta badago. Bestalde, `SET NULL` erabaki adimentsua da hitzordu bat ezabatu arren (adibidez hutsegite administratibo batengatik), emandako errezeta sisteman gorde dadin historiagatik. Ezin hobe defendatzen da horrela sendotasuna.

## Bistak (Views) eta Indizeak Inplementatzea
C# aplikazioaren eta bereziki PHP webgunearen garapena errazteko, `GOsasun_DB_bistak.sql` exekutatu dugu. Web orriko programatzaileek ez daukate SQL JOIN konplexurik egin beharrik.

**Kode pantailazoa: Bisten inplementazioa**
```sql
-- V_Hitzorduak_Osoa inplementatzea
CREATE VIEW V_Hitzorduak_Osoa AS
SELECT 
    h.id AS hitzordu_id,
    h.data,
    p.nan AS paziente_nan,
    CONCAT(e_p.izena, ' ', e_p.abizenak) AS paziente_izena,
    CONCAT(e_m.izena, ' ', e_m.abizenak) AS mediku_izena
FROM hitzorduak h
JOIN pazienteak p ON h.paziente_id = p.id
JOIN erabiltzaileak e_p ON p.id = e_p.id
JOIN medikuak m ON h.mediku_id = m.id
JOIN erabiltzaileak e_m ON m.id = e_m.id;
```
**Azalpena**: Behin bista hau inplementatuta, bai App-ak eta bai Webak galdetu besterik ez dute egin behar: `SELECT * FROM V_Hitzorduak_Osoa`. Honek logika Backend-etik Datu-Basera eramaten du modu efizientean.

Aldi berean, `GOsasun_DB_indizeak.sql` bidez `CREATE INDEX idx_neurketak_paziente_data ON neurketak(paziente_id, erregistro_data);` sortu da. Honek bermatzen du GOsasun Webean grafikak marrazterakoan denbora-seriea abiadura bizian prozesatzen dela datu-base handietan. Proiektuaren azken ukitu hauek produktu "perfetua" eskaintzen dute.
