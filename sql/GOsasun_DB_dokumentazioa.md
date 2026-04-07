# GOsasun Datu-basearen Dokumentazioa (GOsasun_DB)

Dokumentu honek `GOsasun_DB` datu-basearen egitura teknikoa, erlazioak eta optimizazioak zehazten ditu. Proiektuaren datu-osotasuna eta errendimendua bermatzeko diseinatuta dago.

---

## 1. PK eta FK Definizioak

Datu-basearen erlazioak kudeatzeko funtsezko bi kontzeptu erabili dira:

*   **Primary Key (PK)**: Taula bateko erregistrorik bakoitza modu esklusiboan identifikatzen duen zutabea edo zutabe multzoa da. Gure sisteman gehienak `id` izenekoak dira eta `AUTO_INCREMENT` propietatea dute (salbu eta 1:1 lotura duten tauletan, adibidez `pazienteak`).
*   **Foreign Key (FK)**: Taula bat beste batekin lotzen duen zutabea da. Guraso-taula bateko PK-ari egiten dio erreferentzia, datuen arteko koherentzia ziurtatuz. Adibidez, `erabiltzaileak` taulako `rol_id` eremua `rolak` taulako `id`-ari lotuta dago.

---

## 2. Taulen Laburpen Taula

Honako taula honek datu-baseko entitate nagusiak, haien PK-ak, FK-ak eta aplikatutako murrizketak erakusten ditu:

| Taula | PK | Foreign Key (FK) | Guraso Taula | Constraints / Murrizketak |
| :--- | :--- | :--- | :--- | :--- |
| **rolak** | `id` | - | - | `izena` UNIQUE |
| **erabiltzaileak** | `id` | `rol_id` | `rolak` | `email` UNIQUE, RESTRICT/CASCADE |
| **pazienteak** | `id` | `id` | `erabiltzaileak` | `nan` UNIQUE, RESTRICT/CASCADE |
| **medikuak** | `id` | `id` | `erabiltzaileak` | `elkargokide_zenbakia` UNIQUE, RESTRICT/CASCADE |
| **harrerako_langileak** | `id` | `id` | `erabiltzaileak` | RESTRICT/CASCADE |
| **mediku_paziente** | `id` | `mediku_id`, `paziente_id` | `medikuak`, `pazienteak` | UNIQUE(`mediku_id`, `paziente_id`) |
| **neurketak** | `id` | `paziente_id` | `pazienteak` | RESTRICT/CASCADE |
| **mezuak** | `id` | `bidaltzaile_id`, `hartzaile_id` | `erabiltzaileak` | RESTRICT/CASCADE |
| **dokumentuak** | `id` | `paziente_id`, `igotzaile_id` | `pazienteak`, `erabiltzaileak` | RESTRICT/CASCADE |
| **hitzorduak** | `id` | `paziente_id`, `mediku_id` | `pazienteak`, `medikuak` | RESTRICT/CASCADE |
| **errezetak** | `id` | `hitzordu_id`, `mediku_id`, `paziente_id` | `hitzorduak`, `medikuak`, `pazienteak` | `hitzordu_id` SET NULL/CASCADE |
| **abisuak** | `id` | `paziente_id` | `pazienteak` | RESTRICT/CASCADE |
| **botikak** | `id` | - | - | `izena` UNIQUE |
| **errezeta_botikak** | `id` | `errezeta_id`, `botika_id` | `errezetak`, `botikak` | UNIQUE(`errezeta_id`, `botika_id`) |

---

## 3. Erreferentzia-Osotasuna (ON DELETE / ON UPDATE)

Datuen osotasuna mantentzeko, DBak automatikoki kudeatzen ditu erregistroen arteko loturak:

1.  **`ON UPDATE CASCADE`**: Erreferentziatutako taularen (gurasoaren) PK-a aldatzen bada, gailuko menpeko erregistro guztietan informazio hori automatikoki eguneratuko da.
2.  **`ON DELETE RESTRICT`**: Guraso-taula bateko erregistrorik ezin da ezabatu baldin eta menpeko taularen batean erabiltzen ari bada. Segurtasun neurri bat da, ustekabeko datu galderak ekiditeko.
3.  **`ON DELETE SET NULL`**: Erabilia `errezetak` taulan. Hitzordu bat ezabatzen bada, hari lotutako errezeta ez da ezabatzen; lotura `NULL` geratzen da, historikoa mantendu ahal izateko.

---

## 4. Indizeak (Indexes)

Bilaketak eta iragazketak azkartzeko, honako indizeak definitu dira:

*   **`idx_pazienteak_abizenak_izena`**: Pazienteak izen-abizenen arabera bilatzea azkartzen du.
*   **`idx_hitzorduak_data`**: Agendako hitzorduak dataren arabera lortzean errendimendua hobetzen du.
*   **`idx_neurketak_paziente_data`**: Paziente baten neurketak dataren arabera ordenatuta azkarrago kargatzea lortzen du.
*   **`idx_mezuak_hartzaile_data`**: Mezuak hartzailearen eta dataren arabera kargatzea optimizatzen du.

---

## 5. Bistak (Views)

Bistak taula konplexuen arteko `JOIN`ak sinplifikatzeko erabiltzen dira. Hona hemen definizioak eta PHP kodean non erabiltzen diren:

| Bista Izena | Deskribapena | Non erabiltzen da? (PHP) |
| :--- | :--- | :--- |
| **`V_Login`** | Erabiltzailearen kredentzialak eta rola bateratzen ditu. | `php_hasiera/login.php` |
| **`V_Pazientea`** | Pazientearen datuak eta erabiltzaile kontua batzen ditu. | `php_pazientea/index.php`, `paziente_info.php` |
| **`V_Medikua`** | Mediku profesionalaren eta kontuaren datu guztiak. | `php_medikua/index.php` |
| **`V_Mediku_Pazienteak`** | Mediku bati esleitutako pazienteen zerrenda osoa. | `php_medikua/pazienteak.php` |
| **`V_Hitzorduak_Osoa`** | Hitzorduak, pazienteen eta medikuen izenekin batera. | `php_harrera/hitzorduak.php`, `hitzordu_xehetasuna.php` |
| **`V_Harrera`** | Harrerako langileen eta haien kontuen informazioa. | `php_harrera/index.php` |
| **`V_Abisuak_Osoa`** | Pazienteei zuzendutako abisu klinikoak eta pertsonalak. | `php_medikua/abisuak.php` |

---

> [!NOTE]
> Dokumentazio hau SQL eskema aldaketaren baten ondorioz eguneratu behar bada, ziurtatu `sql/` direktorioan dauden `.sql` fitxategiak (eskema, indizeak eta bistak) sinkronizatuta daudela.
