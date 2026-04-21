# 5.1. FROGAK ETA ONDORIOAK – Datu‑Basea

## 5.1.1 Helburua
Testatu **MySQL** datu‑basearen instalazioa, eskema, indizeak, bistak eta trigger‑ak, baita erabiltzaile‑baimenak ere, **lokalekoa (root baimenak)** eta **urruneko zerbitzarian** (IP: `192.168.115.163`).

## 5.1.2 Pre‑Eskaerak
- **MySQL Server** 8.x instalatua. 
- `root` erabiltzailearen sarbidea (lokalean) edo `sudo` baimenak.
- `ssh` sarbidea `192.168.115.163`-ra (root baimenak).
- `GOsasun_DB.sql`, `GOsasun_DB_indizeak.sql`, `GOsasun_DB_trigger.sql`, `GOsasun_DB_bistak.sql` fitxategiak `C:\Apache24-64\htdocs\GOsasun_web\sql` karpetan.

## 5.1.3 Frogak (Test‑Caseak)
| # | Test‑Case izena | Deskribapena | Komandoa | Espero den emaitza | Oharrak/Erroreak |
|---|----------------|--------------|----------|-------------------|-----------------|
| 1 | **Instalazioaren egiaztapena (lokalean)** | MySQL zerbitzaria martxan dagoela konprobatzea. | `sudo systemctl status mysql` | `active (running)` | `systemctl: command not found` → `sudo apt-get install mysql-server` (Linux) edo `services.msc` (Windows).
| 2 | **Datubasea inportatzea (lokalean)** | `GOsasun_DB.sql` kargatu. | `mysql -u root -p < GOsasun_DB.sql` | `Database created successfully` | `ERROR 1049 (42000): Unknown database` → `CREATE DATABASE GOsasun_DB;` lehenetsia.
| 3 | **Indizeak egiaztapena** | `SHOW INDEX FROM jarraipenak;` | `mysql -u root -p -e "USE GOsasun_DB; SHOW INDEX FROM jarraipenak;"` | Indize izenak (`idx_paziente_id`, `idx_langile_id`…) agertzen dira. | `ERROR 1146 (42S02): Table 'jarraipenak' doesn't exist` → `source GOsasun_DB.sql`.
| 4 | **Trigger‑en probak** | `INSERT INTO jarraipenak (paziente_id, tentsio_sistolikoa) VALUES (1,120);` | `mysql -u root -p -e "USE GOsasun_DB; INSERT ...;"` | `trg_eguneratu_neurri_fisikoak` aktibatzen da eta `pazienteak` taulako `azken_pisua` balioa eguneratzen da. | `ERROR 1452 (23000): Cannot add or update a child row` → `INSERT pazienteak` lehenengo.
| 5 | **Bistak (Views) probak** | `SELECT * FROM v_paziente_jarraipenak WHERE paziente_id=1;` | `mysql -u root -p -e "USE GOsasun_DB; SELECT * FROM v_paziente_jarraipenak LIMIT 5;"` | Zehaztutako zutabeak (`izena`, `tentsio_sistolikoa`, `azken_pisua`) itzultzen dira. | `ERROR 1356 (HY000): View 'v_paziente_jarraipenak' references invalid table` → `source GOsasun_DB_bistak.sql`.
| 6 | **Baimen‑azterketa (lokalean)** | `SELECT * FROM jarraipenak;` root‑ek ondo duela. | `mysql -u root -p -e "USE GOsasun_DB; SELECT * FROM jarraipenak;"` | Datu guztiak ikusten dira. | `ERROR 1045 (28000): Access denied` → `GRANT SELECT, INSERT, UPDATE, DELETE ON GOsasun_DB.* TO 'db_osasun_langilea'@'localhost';`
| 7 | **Urruneko konexioaren frogak** | MySQL zerbitzaria IP‑etan zuzenean onartzen duela. | `mysql -h 192.168.115.163 -u db_osasun_langilea -p` | Konexioa ondo lortzen da (prompt `mysql>`). | `ERROR 2003 (HY000): Can't connect to MySQL server` → `firewall‑en port 3306 ireki, my.cnf‑n `bind-address = 0.0.0.0` aldatu.
| 8 | **Urruneko DB inportazioa** | `GOsasun_DB.sql` urruneko zerbitzarian kargatu. | `ssh root@192.168.115.163 "mysql -u root -p < /tmp/GOsasun_DB.sql"` | Datu-basea urrunetik sortua. | `ERROR 1044 (42000): Access denied for user 'root'@'%'` → `GRANT ALL PRIVILEGES ON *.* TO 'root'@'%' WITH GRANT OPTION;`

## 5.1.4 Error‑Handling aurreratua
- **Log‑fitxategiak**: MySQL‑ren erroreak `/var/log/mysql/error.log` (Linux) edo `C:\ProgramData\MySQL\MySQL Server 8.0\Data\MySQL.err` (Windows). 
- **Automatizazioa**: Bash skript (`run_frogak_db.sh`) erabiliz elkartu test‑case guztiak. Adibidez:
```bash
#!/bin/bash
set -e
mysql -u root -p -e "source GOsasun_DB.sql"
# Indizeak
mysql -u root -p -e "source GOsasun_DB_indizeak.sql"
# Trigger‑ak
mysql -u root -p -e "source GOsasun_DB_trigger.sql"
# Bistak
mysql -u root -p -e "source GOsasun_DB_bistak.sql"
# Test‑caseak exekutatu
./test_cases_db.sh
```
- **Errore‑berreskurapena**: `ROLLBACK;` komandoa erabiliz transakzioak huts eginez gero datu-basea aurreko egoerara itzultzeko.

---
*Frogak datu‑basea ondo ezarri dutenaren froga dira. Seguruen arte `root`‑ek baimen guztiak du, baina prod‑etan baimenak mugatu (principio de menor privilegio).*
