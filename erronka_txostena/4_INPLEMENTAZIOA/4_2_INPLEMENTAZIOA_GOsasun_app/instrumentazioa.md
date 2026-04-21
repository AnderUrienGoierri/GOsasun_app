# Instrumentazioaren Analisi – Beurer BM58 Tentsiometroa

Aplikazioan integratutako **Beurer BM58** tentsiometroaren funtzionamendua, hardware‑konexioa eta neurketa‑prozesua, dokumentazio osagarri gisa argazki batean erakusten da.

> **Irudia**: Beurer BM58 tentsiometroa (handi‑handik‑egokituta, USB‑HID interfazearekin)

![Beurer BM58 tentsiometroa](/absolute/path/to/Eskuliburuak/eskuliburu_irudiak/tentsiometroa.png)

### Nola erabiltzen da aplikazioan?
1. **Konektatzea** – Osasun‑langileak gailua USB bidez ordenagailura lotzen du, `BM58Driver` klaseak (`EgiaztatuHardwareKonexioa()`) konexioa balioztatzen du.
2. **Irakurketa** – `BM58Driver`‑k `IrakurriErrekordGuztiak()` metodoa deitzen du, gailutik **Raw Records** guztiak erauzten ditu.
3. **Kalkuluak** – `KalkulatuBatezbestekoa()` zehazten du tentsio‑sistolikoa, tentsio‑diastolikoa eta pultsua `Jarraipena` objektuetan.
4. **Gordetzea** – `JarraipenaKontrolatzailea.GordeJarraipena()` metodoren bidez balio horiek datu‑baseko `jarraipenak` taulan gordetzen dira.

### Aldagai teknikoak (PDF‑an aurkitu)
| Parametroa            | Deskribapena                               |
|----------------------|--------------------------------------------|
| **Irudia**           | 📷 Beurer BM58 (USB‑HID)                   |
| **Neurketak**         | Tentsio (mmHg) – Sist. / Diast. – Pultsua (ppm) |
| **Denbora**           | 1‑2 seg. irakurketa‑bitarra                |
| **Komunikazioa**      | HID/Serial (USB)                           |
| **Power**            | 5 V (USB)                                  |

**Azken oharra**: Neurketa‑datuak **XML** eredu batera esportatzeko `ExportatuXML()` funtzioak erabiliko ditu, horrela bestelako sistemek (esatreko, osasun‑informazio‑plataformak) integratu ahal izango dituzte.
