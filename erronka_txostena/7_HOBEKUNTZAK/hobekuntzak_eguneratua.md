# Hobekuntzak – Eguneratutako Zerrenda

## 1️⃣ Datu‑basea
| # | Hobekuntza | Deskribapena | Prioritatea |
|---|------------|--------------|------------|
| 1 | **Partizionamendua** | `InnoDB` taulei `pacienteak`, `jarraipenak` eta `errezetak` partizioak gehitu (range‑partition per `azken_alta_data`). Horrela kontsulta handien errendimendua hobetuko da. | Altua |
| 2 | **Replikazioa Master‑Slave** | Master‑a (`192.168.115.163`) eta Slave (`192.168.115.164`) artean binario log‑aren bidez errepikatu, irakurketa eskala handitzeko. | Handia |
| 3 | **Audit‑log plugin** | `mysql-audit` edo `MariaDB Audit Plugin` integratu, aldaketa guztiak (`INSERT/UPDATE/DELETE`) jasotzeko. | Ertaina |
| 4 | **JSON/Document Store** | `Jarraipenak` taulan **JSON** eremu bat (`neurketa_detalleak`) gehitu, horrela neurketa gehigarriak (HR‑ekintzak) erraz gehitu ahal izango dira. | Ertaina |
| 5 | **Indexazio dinamikoa** | `ANALYZE TABLE` eta `OPTIMIZE TABLE` planifikatu `cron`-en bidez, indizeak beti eguneratuta mantentzeko. | Ertaina |
| 6 | **Zero‑Downtime Migration** | `gh‑ost` edo `pt‑online‑schema‑change` erabiliz aldaketa eskematikoko migrazioak zerbait aldiz aldiz aplikatu (esaterako, `ON DELETE SET NULL` → `CASCADE`). | Altua |

## 2️⃣ GOsasun App (Desktop, C#)
| # | Hobekuntza | Deskribapena | Prioritatea |
|---|------------|--------------|------------|
| 1 | **MVVM + ReactiveUI** | Presentazioko logika MVVM patroian birrezarri, `ReactiveUI` liburutegiarekin. Hobetu testegindarritasuna eta mantentzea. | Altua |
| 2 | **Dependency Injection** | `Microsoft.Extensions.DependencyInjection` (edo `Autofac`) integratu, konexioak (`DbContext`, `BM58Driver`) centralizatzeko. | Ertaina |
| 3 | **Unit‑Test eta Integration Test** | `xUnit` + `Moq` erabiliz kontroller, repositorio eta `BM58Driver`‑aren testak implementatu. CI‑ean (`GitHub Actions`) exekutatu. | Handia |
| 4 | **Dark‑Mode / Theming** | `MaterialSkin` edo `FluentDesign` liburutegia erabiliz, erabiltzaileak gai hauta dezake (Light / Dark). | Ertaina |
| 5 | **USB‑Driver sinplea** | `WinUSB` bidez driver independenteago bat sortu, *plug‑and‑play* 100 % garbi. | Ertaina |
| 6 | **Logging strukturala** | `Serilog` konfiguratu `JSON` log‑fitxategiekin, `Kibana`‑n ikus daiteke. | Ertaina |
| 7 | **Auto‑Update mekanismoa** | `Squirrel.Windows` edo `ClickOnce` erabiliz aplikazioaren auto‑update-a gehitu. | Ertaina |

## 3️⃣ GOsasun Web (PHP / Apache)
| # | Hobekuntza | Deskribapena | Prioritatea |
|---|------------|--------------|------------|
| 1 | **HTTPS + HSTS** | Letzakez `Let's Encrypt` ziurtagiriak. `Header always set Strict-Transport-Security "max-age=31536000; includeSubDomains"`. | Altua |
| 2 | **API‑RESTful** | Front‑end (JS) eta mobil aplikazioentzat **Laravel Lumen** edo **Slim** erabiliz API‑ak sortu. | Handia |
| 3 | **CSRF / XSS** | `OWASP CSRFGuard` eta `Content‑Security‑Policy` gehitu. | Handia |
| 4 | **Docker‑Compose** | `docker-compose.yml` beharrezko zerbitzuak (php, mysql, nginx) barne du, instalazioa errazteko. | Ertaina |
| 5 | **Caching** | `Redis` edo `OPcache` gehitu, kontsulta errepikatuen karga murrizteko. | Ertaina |
| 6 | **CI/CD pipeline** | `GitHub Actions`‑en pipeline, unit‑test (`PHPUnit`), lint (`PHP_CodeSniffer`), eta **deployment** `scp`‑rekin 192.168.115.163 zerbitzarira. | Ertaina |
| 7 | **Internationalization (i18n)** | `gettext` edo `Laravel Localization` integratu, hizkuntza ezezaguna (esaterako, Euskal‑Euskara). | Ertaina |

## 4️⃣ Multiplataforma (Web + Mobile)
| # | Hobekuntza | Deskribapena | Prioritatea |
|---|------------|--------------|------------|
| 1 | **React Native / Flutter Front‑end** | Web‑API‑rekin konektatu, Android / iOS aplikazioak sortu, tentsiometroaren Bluetooth‑LE (BLE) integratzeko aukera. | Altua |
| 2 | **Progressive Web App (PWA)** | `service‑worker`, `manifest.json` eta offline‑cachea, erabiltzaileek web gunea instalatu dezaten telefonoetan. | Handia |
| 3 | **GraphQL Layer** | `Apollo Server` gehitu, bezeroen datu‑eskaerak optimizatzeko. | Ertaina |
| 4 | **Cross‑Platform Testing** | `Appium` edo `Detox` erabiliz UI testak exekutatu Android/iOS/Windows-en. | Ertaina |
| 5 | **CI/CD multiplataforma** | `GitHub Actions`‑en matrix build (Windows, macOS, Linux) egokitu, artefaktu guztiak (`apk`, `ipa`, `zip`) artefaktu karpetara igotzeko. | Ertaina |

> **Oharra**: Prioritateak **Altua**, **Handia** eta **Ertaina** bezalako balioak hartzen dira, **biztanearen errealitatearekin** (root‑ekin instalazioa, 192.168.115.163 IP eta USB‑ instalazioa) bat etortzeko.
