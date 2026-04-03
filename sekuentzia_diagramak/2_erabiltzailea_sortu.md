# 2. Erabiltzailea Sortu - Sekuentzia Diagrama

Sistemako langile batek (normalean Harrerakoak) erabiltzaile berri bat sistema barruan erregistratzean ematen den prozesua.

## Draw.io-n marrazteko elementuak (Zutabeak):
*   **Aktorea:** Harrerakoa (edo Administradorea)
*   **Muga / Interfazea:** ErabiltzaileaSortu (Interfazea)
*   **Kontrola:** ErabiltzaileKontrolatzailea (Kontrola)
*   **DatuBasea:** ErabiltzaileDB (DatuBasea)
*   **Modeloa:** Pazientea / Medikua / HarrerakoLangilea (Modeloa)

## Urratsak (Geziak) Draw.io-n irudikatzeko:
1.  **Harrerakoa -> Interfazea:** Datuak bete eta "Gorde" botoia sakatzen du. Gezi testua: `btnGorde_Click()`
2.  **Interfazea -> Kontrola:** Kontrolatzaileari deitzen dio aukeratutako rolaren arabera. Gezi testua: `SortuPazientea(obj)`
3.  **Kontrola -> DatuBasea:** SQL transakzio bat irekitzen da DBan. Gezi testua: `SortuPazientea(obj)`
4.  **DatuBasea -> Modeloa:** Objektuaren datuak erabiliz SQL INSERT query-ak sortzen dira.
5.  **DatuBasea -> Kontrola (Zatikakoa):** `true / false` emaitza itzuli (Transakzioa Commit/Rollback).
6.  **Kontrola -> Interfazea (Zatikakoa):** Sorkuntza ondo joan den adierazi.
7.  **Interfazea -> Harrerakoa (Zatikakoa):** Mezua pantailan. Testua: `MessageBox("Erabiltzailea ondo gorde da")`

---

## Ikuspegia (Mermaid bidez)

```mermaid
sequenceDiagram
    participant H as Harrerakoa
    participant I as ErabiltzaileaSortu
    participant K as ErabiltzaileKontrolatzailea
    participant DB as ErabiltzaileDB
    participant KL as Modeloa

    H->>I: btnGorde_Click()
    I->>K: SortuPazientea(obj)
    
    alt datuak okerrak
        K-->>I: Ebaluazio errorea (validazioa)
        I-->>H: MessageBox("Datuak okerrak")
    else datuak ondo
        K->>DB: SortuPazientea(obj)
        DB->>KL: Datuen mapaketa
        KL-->>DB: SQL Query prest
        DB-->>K: true (gordeta)
        K-->>I: prozesua amaituta
        I-->>H: MessageBox("Erabiltzailea sortuta")
    end
```
