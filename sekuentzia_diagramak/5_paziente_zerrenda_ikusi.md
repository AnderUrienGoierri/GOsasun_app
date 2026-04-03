# 5. Paziente Zerrenda Ikusi - Sekuentzia Diagrama

Medikuak saioa hasita duela, bere kargura dauden paziente guztiak kontsultatzen dituen prozesua.

## Draw.io-n marrazteko elementuak (Zutabeak):
*   **Aktorea:** Medikua
*   **Muga / Interfazea:** PazienteenZerrenda (Interfazea)
*   **Kontrola:** ErabiltzaileKontrolatzailea (Kontrola)
*   **DatuBasea:** ErabiltzaileDB (DatuBasea)
*   **Klasea:** Pazientea (Modeloa)

## Urratsak (Geziak) Draw.io-n irudikatzeko:
1.  **Medikua -> Interfazea:** Medikuak menutik "Pazienteak ikusi" sakatzen du. Gezi testua: `PazienteenZerrenda(mediku)`
2.  **Interfazea -> Kontrola:** Kontrolatzaileari deitzen dio medikuaren IDarekin. Gezi testua: `LortuMedikuarenPazienteak(medikuId, bilatzailea)`
3.  **Kontrola -> DatuBasea:** SQL kudeaketarako Datu-Base geruzari deitzen dio. Gezi testua: `LortuMedikuarenPazienteak(medikuId, bilatzailea)`
4.  **DatuBasea -> Modeloa:** SQL SELECT bidezko datuak `Pazientea` objektuetara mapatzen ditu.
5.  **DatuBasea -> Kontrola (Zatikakoa):** `List<Pazientea>` itzuli.
6.  **Kontrola -> Interfazea (Zatikakoa):** Paziente zerrenda bueltatzen du.
7.  **Interfazea -> Medikua (Zatikakoa):** Grid-a eguneratu eta zerrenda erakutsi. Testua: `dgvPazienteak.DataSource = lista`

---

## Ikuspegia (Mermaid bidez)

```mermaid
sequenceDiagram
    participant M as Medikua
    participant I as PazienteenZerrenda
    participant K as ErabiltzaileKontrolatzailea
    participant DB as ErabiltzaileDB
    participant KL as Modeloa

    M->>I: PazienteenZerrenda(mediku)
    I->>K: LortuMedikuarenPazienteak(medikuId, bilatzailea)
    K->>DB: LortuMedikuarenPazienteak(medikuId, bilatzailea)
    
    DB->>KL: SQL Select + Mapaketa (Pazientea)
    KL-->>DB: Objektu zerrenda
    DB-->>K: List<Pazientea>
    K-->>I: List<Pazientea>
    I-->>M: dgvPazienteak.DataSource = lista
```
