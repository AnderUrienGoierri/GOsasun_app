# Beurer BM58 USB Komunikazio Gida (Pausoz Pausoko Inplementazioa)

Dokumentu honek Beurer BM58 tentsiometroaren eta ordenagailuaren arteko USB komunikazio prozesua azaltzen du, `BM58Driver.cs` fitxategiko C# kode-adibideekin batera.

Gailuak **USB-HID (Human Interface Device)** interfazea erabiltzen du, baina barnean Microdia/Sonix txip bat dauka **UART protokoloa** (Serie komunikazioa) tunelatzen duena.

---

## 1. Urratsa: Konexioa eta Abiadura Konfiguratzea (Baud Rate)

HID gailu normalek ez dute komunikazio-abiadurarik behar, baina Beurer BM58-k Serie portu bat simulatzen duenez, `4800 bps`-ko abiaduran lan egiteko esan behar diogu **Feature Report** baten bidez.

```csharp
// Microdia/Sonix tunela konfiguratzen dugu (Feature Report ID=2 erabiliz)
byte[] baud = new byte[_maxFeature];
if (_maxFeature >= 4) {
    baud[0] = 0x02; // Report ID: 2
    baud[1] = 0x01; // Flags: 1 (Baud rate-a ezarri)

    // 0x12C0 balio hamaseitarra 4800 bps da hamartarretan
    baud[2] = 0xC0; // Baud rate-aren zati baxua (Low byte)
    baud[3] = 0x12; // Baud rate-aren zati altua (High byte)
}
_stream.SetFeature(baud); // Gailuari bidali
Thread.Sleep(800);        // Sinkronizatzeko denbora eman
```

---

## 2. Urratsa: "Handshake" edo Eskuz Datzea

Tentsiometroa datuak bidaltzeko prest egon dadin, lehenengo harremana edo "Agurra" egin behar dugu. Ordenagailuak `0xAA` bidaltzen du, eta tentsiometroak `0x55` erantzun behar du gailua prest dagoela baieztatzeko.

```csharp
// 1. "Kaixo, hor al zaude?" galdetu diogu gailuari
channel.Write(new byte[] { 0xAA }); // Shake Signal
Thread.Sleep(200);

// 2. Erantzuna irakurtzen dugu
byte resp = channel.ReadByte();

if (resp == 0x55) {
    Debug.WriteLine("Handshake arrakastatsua! Gailua prest dago.");
} else {
    throw new Exception("Handshake errorea.");
}
```

---

## 3. Urratsa: Identifikazioa eta "PC" modua aktibatzea

Handshake-a lortu ostean, gailuaren pantailan "PC" jartzen duela ziurtatu behar da. Horretarako, identifikazio-komando bat (`0xA4`) bidaltzen da **4 aldiz**. Honek gailuaren "PC Busy" (PC Lanpetuta) egoera blokeatua askatzen du.

```csharp
// 0xA4 komandoa 4 aldiz bidali blokeoak saihesteko
for (int j = 0; j < 4; j++) {
    channel.Write(new byte[] { 0xA4 }); // Identifikazio komandoa bidali
    Thread.Sleep(150);
    try {
        channel.ReadPayload(); // Tentsiometroak itzulitako zabor-datuak irentsi
    } catch { }
}
```

---

## 4. Urratsa: Neurketa Kopurua Eskatzea

Datuak irakurri aurretik, zenbat neurketa dauden galdetu behar diogu tentsiometroari, guk azkenengoa bakarrik irakurri ahal izateko. Horretarako `0xA2` komandoa erabiltzen da.

```csharp
// Zenbat neurketa daude gailuaren memorian?
channel.Write(new byte[] { 0xA2 }); // Eskari komandoa
Thread.Sleep(100);

int count = channel.ReadByte(); // Neurketa kopurua bueltatzen digu
Debug.WriteLine($"Gailuan {count} neurketa daude.");

if (count <= 0) {
    throw new Exception("Ez dago neurketarik gailuan.");
}
```

---

## 5. Urratsa: Azken Neurketa Irakurtzea

Badakigu zenbat egon dauden, goazen orain azkenengoa irakurtzera (Hemen adibidez, `count` balioa erabiliz indize gisa). `0xA3` komandoa eta eskatu nahi dugun neurketa zenbakia bidaltzen dugu.

```csharp
// Azken errekorra irakurtzea eskatzen diogu
channel.Write(new byte[] { 0xA3, (byte)count }); 
Thread.Sleep(200);

// Gailuak 8 byteko array bat itzuliko digu datu guztiekin
byte[] data = channel.ReadPayload(); 

// Datuen adibide bat hau izango litzateke: { 0x56, 0x2b, 0x41, 0x01, 0x02, 0x0b, 0x34, 0x08 }
```

---

## 6. Urratsa: Datuen Dekonpresioa (Parsing)

Tentsiometroak datuak formatu trinkotan (byte bidez) bidaltzen dituenez, C# kodean objektu bihurtu behar dugu. Formatu honen sekretu handiena da **Tentsioari 25eko balioa (offset)** gehitu behar zaiola.

Egin dezagun parsing-a lortu dugun `data` byte array-arekin:

```csharp
// Adibidez data = { 0x56, 0x2b, 0x41, 0x01, 0x02, 0x0b, 0x34, 0x08 }
// 0x56 hamartarrez -> 86
// 0x2b hamartarrez -> 43
// 0x41 hamartarrez -> 65

Neurketa neurketa = new Neurketa
{
    // TENTSIOA: (0 indizea Sistolikoa eta 1 indizea Diastolikoa)
    // Formula garrantzitsua: Jasotako balioa + 25 = Benetako tentsioa mmHg-tan
    TentsioSistolikoa = data[0] + 25,   // 86 + 25 = 111 mmHg
    TentsioDiastolikoa = data[1] + 25,  // 43 + 25 = 68 mmHg
    
    // PULTSUA: (2 indizea) Hemen ez da gehiketarik egiten
    PultsuaPpm = data[2],               // 65 ppm
    
    // DATA ETA ORDUA:
    ErregistroData = new DateTime(
        2000 + data[7], // Urtea     (Indizea 7): 2000 + 8 = 2008
        data[3],        // Hilabetea (Indizea 3): 1 = Urtarrila
        data[4],        // Eguna     (Indizea 4): 2
        data[5],        // Ordua     (Indizea 5): 11
        data[6],        // Minutua   (Indizea 6): 52
        0               // Segundoak (Tentsiometroak ez ditu ematen)
    )
};

// Hemendik aurrera 'neurketa' objektua aplikazioan erakutsi edo XMLan gorde dezakegu.
```

---

## 7. Urratsa: Komunikazioa Amaitzea

Datuak behar bezala ateratakoan, oso gomendagarria da komunikazioa ixtea gailua modu seguruan deskonektatzeko. Horretarako `0xA5` komandoa bidaltzen dugu.

```csharp
// Komunikazio saioa amaitu (End Communication signal)
channel.Write(new byte[] { 0xA5 }); 
Thread.Sleep(50);
try { 
    channel.ReadPayload(); // Jasotako baieztapena baztertu
} catch { } 
```
