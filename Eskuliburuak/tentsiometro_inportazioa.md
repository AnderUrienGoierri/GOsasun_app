# Beurer BM58 Tentsiometroaren Integrazioa eta Komunikazioa

Dokumentu honek zehatz-mehatz azaltzen du nola komunikatzen den GOsasun aplikazioa Beurer BM58 tentsiometroarekin. USB-HID zein Serie portu (COM) bidezko protokoloak, memoria-banaketen azterketa eta lortutako byte gordinek nola itzultzen diren giza-irakurketako datu izateko esplikatzen da.

## 1. Gailuarekin Konektatzea

### Erabiltzailearen Ikuspegia
Erabiltzaileak tentsiometroa ordenagailura konektatu behar du estandar USB kable batekin. Konektatu bezain laster, tentsiometroaren pantailan **"PC"** agertu behar da. Hori gertatzen ez bada, hasierako botoia sakatuta mantendu edo pilak berriro jarri behar dira prozesua berrabiarazteko.

### Ikuspegi Teknikoa (Handshake)
Aplikazioak `BM58Driver.cs` klasea erabiltzen du hardware bilaketa egiteko. Windows-en HID gisa (Human Interface Device) identifikatuko da (VID: `0x0C45`, PID: `0x7406`).

Behin gailua aurkituta, aplikazioak "Handshake"-a hasten du konexio segurua bermatzeko:
1. Ordenagailuak `0xAA` byte-a bidaltzen dio.
2. Tentsiometroak `0x55` byte-arekin erantzun behar du prest badago.
3. Behin sinkronizatuta, gailua `Init` egoeran jartzeko `0xA4` komandoa bidaltzen da.

```csharp
// Handshake exekuzioa (BM58Driver.cs)
hid.Write(new byte[] { 0xAA });
Thread.Sleep(200);
if (hid.ReadByte() == 0x55) { 
    ok = true; // Sinkronizatuta!
}
```

## 2. Memoria Banaketa (U1 eta U2)

Beurer BM58 gailuan memoria bitan banatzen da: lehenengoa erabiltzailearentzat (U1) eta bigarrena beste batentzat (U2). Memoria hauek 60 neurketako lekua daukate bakoitzak.

**Zein komando bidali behar da?**
* **U1 irakurtzeko**: `0xA6` komandoa.
* **U2 irakurtzeko**: `0xA7` komandoa.

Tentsiometro eredu desberdinen barruan ("Andon" jatorriko BM58 bertsioak), `0xA7` bidalita ere, maiz gailuak U1 eta U2ko neurketa guztiak elkarrekin bidaltzen ditu bat-batean. Hori dela eta, programa benetako erabiltzailea identifikatzeko *Marka berezi* (bit bat) batez baliatzen da.

### Markaren Detekzioa Byte-etan (U2 Bita)
Zortzi byteko egituran, `0x80` marka (hau da, 128 zenbaki hamartarra edo bit altuena) gaineratzen zaio hilabeteari (`data[4]`) edo urteari (`data[3]`) irakurketa hori U2 bati dagokiola argitzeko.

```csharp
// U2 bita data[4] (hilabetea) edo data[3] (urtea) barruan etorri daiteke
bool isU2Month = (data[4] & 0x80) != 0;
bool isU2Year = (data[3] & 0x80) != 0;
bool trueU2Bit = isU2Month || isU2Year;

// "Benetako" hilabetea lortzeko, 0x80 bita kentzen (garbitzen) zaio
int benetakoHila = data[4] & 0x7F; 

// Emaitzen sailkapena
int benetakoUserId = trueU2Bit ? 2 : 1; 
```

## 3. Datu Gordinen Konbertsioa (Payload)

Behin bankua aukeratuta, ezkutuko indize bat irakurtzen da (`0xA3, indizea` bidalita), eta 8 bytez osaturiko pakete bat itzultzen du gailuak. 

### Byte egituraren esanahia
(Adibidez: Tentsioa 141 sistoliko, 106 diastoliko, 70 pultsua denean) -> `74-51-46-01-09-04-40-08`

| Byte-a | Eduki Gordina | Aldaketaren formula | Azalpena | Adibidean |
|--------|---------------|-----------------------|----------|------------|
| `data[0]` | `0x74` (116) | **`+ 25`** | Sistolikoa (mmHg) | 116 + 25 = 141 |
| `data[1]` | `0x51` (81)  | **`+ 25`** | Diastolikoa (mmHg)| 81 + 25 = 106 |
| `data[2]` | `0x46` (70)  |  - (Bat ere ez) | Pultsua (ppm) | 70 |
| `data[3]` | `0x01` (1)   | U2 Bit (Batzuetan)| Urtea (Adib. 2000 + 1) | 01 |
| `data[4]` | `0x09` (9)   | U2 Bit (Batzuetan)| Hilabetea | Iraila |
| `data[5]` | `0x04` (4)   | - | Eguna | 4a |
| `data[6]` | `0x40` (64)| - | Ordua | 64 (?) |
| `data[7]` | `0x08` (8)   | - | Minutua | 8 |

Tentsioari dagokionez berezitasun garrantzitsu bat protokolo honetan: **25eko Offset-a** (Desplazamendua) dauka sistoliko eta diastoliko balioetan memoria aurrezteko. Beraz, uneko byteari +25 batu behar zaio tentsio erreala lortzeko.

```csharp
// Batezbestekoa kalkulatzeko prozesuan nola konbertitzen den
int si = r.Data[0] + 25; // Sistolikoa
int di = r.Data[1] + 25; // Diastolikoa
int pu = r.Data[2];      // Pultsua
```

## 4. Datuen Filtroketa eta Balioztapena
Aplikazioak, neurketetan sarrerako datu hondatuak saihesteko, babes mekanismoak ditu martxan:
1. Hasierako 2 byteak askotan *Andon / Beurer* fabrikatzaileen erregistro komertziala bidaltzen dute (testu formako hitzak byte gisa).
2. Horregatik baldintzak zorrotzak dira onarpena egiteko:
```csharp
bool datuaOn = (data[0] > 10 && data[0] < 250); // Errealzkoa al da tentsioa?
bool hilaOn = (benetakoHila >= 1 && benetakoHila <= 12); // Hilabete zuzena?
bool egunaOn = (data[5] >= 1 && data[5] <= 31); // Egun zuzena?

if (datuaOn && hilaOn && egunaOn) {
    // Baliozkoa da eta memoria zerrendari lotuko zaio
}
```

Behin deskarga osoa (edo hutsa erantzuten hasten den zikloa) eginda, `0xA5` komandoarekin gailuarekin konexioa formalki itxi egiten da. Ondoren batezbestekoa kalkulatu, datu-basean gorde eta XML esportazioak aurrera eramaten dira `C:\Apache24-64\htdocs\GOsasun_web\xml_paziente_neurketak` karpetan.
