# Klase-diagrametarako metodo estatikoak

Dokumentu honek klase-diagrametan `{static}` notazioarekin markatu beharreko `public static` metodoak jasotzen ditu, baina soilik zerbitzu edo utilitate mailan zentzua dutenak.

## Irizpidea

- Sartu dira zerbitzu edo utilitate izaera argia duten `public static` metodoak.
- Ez dira sartu `private static` helper lokalak.
- Ez dira sartu `internal static` klase/metodoak.
- Ez dira sartu UI elkarrizketa edo formulario laguntzaile puntualak.
- Ez dira sartu DTO edo emaitza-objektuen factory metodo estatiko txikiak, diagraman zarata gehiegi sortzen dutelako.

## UMLn markatu beharrekoak

### DatuBaseKonexioa

Fitxategia: `GOsasun_app/Repositorioa/DatuBaseKonexioa.cs`

- `+ LortuDatuBaseIzena(): string {static}`
- `+ LortuKonexioa(datuBasearekin: bool = true): MySqlConnection {static}`
- `+ ItxiKonexioa(konexioa: MySqlConnection?): void {static}`
- `+ ProbatuKonexioa(out erroreMezua: string): bool {static}`

Oharra: klase hau bera `public static class` da; hortaz, metodo publiko guztiak estatikoak dira eta diagraman hala agertu behar dute.

### AplikazioKonfigurazioaHornitzailea

Fitxategia: `GOsasun_app/Kontrola/Zerbitzuak/AplikazioKonfigurazioa.cs`

- `+ LortuKonfigurazioa(): AplikazioKonfigurazioa {static}`
- `+ GordeKonfigurazioa(konfigurazioa: AplikazioKonfigurazioa): void {static}`
- `+ BerrizKargatu(): void {static}`

Oharra: `KonfigurazioFitxategiBidea` propietatea ere estatikoa da, baina metodoen zerrenda soilik jaso da hemen.

### AplikazioBideak

Fitxategia: `GOsasun_app/Kontrola/Zerbitzuak/AplikazioKonfigurazioa.cs`

- `+ LortuWebErroa(): string {static}`
- `+ LortuDokumentuKarpeta(): string {static}`
- `+ LortuPazienteDokumentuKarpeta(): string {static}`
- `+ LortuXmlKarpeta(): string {static}`
- `+ LortuIrudiKarpeta(): string {static}`
- `+ LortuAplikazioIrudiKarpeta(): string {static}`
- `+ LortuIrudiErroak(): IEnumerable<string> {static}`
- `+ LortuIrudiHelmugaBidea(irudiErlatiboa: string): string {static}`
- `+ ZiurtatuBiltegiratzeKarpetak(): void {static}`

Oharra: hau ere `public static class` da; beraz, bere metodo publikoak diagraman estatiko gisa adierazi behar dira.

### HasierakoPrestaketaZerbitzua

Fitxategia: `GOsasun_app/Kontrola/Zerbitzuak/HasierakoPrestaketaZerbitzua.cs`

- `+ Exekutatu(): HasierakoPrestaketaEmaitza {static}`

Oharra: klase hau hasierako bootstrapping zerbitzu estatiko gisa modelatzea zentzuzkoa da diagraman.

### DokumentuPdfZerbitzua

Fitxategia: `GOsasun_app/Kontrola/Zerbitzuak/DokumentuPdfZerbitzua.cs`

- `+ SortuHelmugaBidea(fitxategiIzena: string): string {static}`

Oharra: klase osoa ez da estatikoa, baina metodo hau bai; beraz, metodo mailan markatu behar da `{static}` bidez.

### TxostenGrafikaZerbitzua

Fitxategia: `GOsasun_app/Kontrola/Zerbitzuak/TxostenGrafikaZerbitzua.cs`

- `+ LortuGrafikaTestua(mota: TxostenGrafikaMota): string {static}`

Oharra: klase osoa ez da estatikoa, baina metodo hau utilitarioa da eta diagraman estatiko gisa markatzea egokia da.

## Kanpoan utzitakoak

Hauek badaude kodean `static` gisa, baina ez dira gomendatzen klase-diagrama nagusietan nabarmentzeko:

- `HasierakoPrestaketaEmaitza.Arrakasta(...)` eta `Errorea(...)`: emaitza-objektu baten factory metodoak dira.
- `DokumentuaEzabatuLaguntzailea.Baieztatu(...)` eta `JarraipenOharLaguntzailea.*`: UI laguntzaile puntualak dira.
- `DatuBaseTestua.*`, `MySqlReaderExtensions.HasColumn(...)`, `IrudiCachea.LortuBitmapa(...)`: `internal` mailako utilitateak dira.
- Formulario eta pantailen barruko `private static` helper guztiak: lokalak dira eta ez dute balio arkitektura-diagrama mailan.

## Laburpena

Klase-diagrama mailan markatzeko multzo txikia hau da:

- `DatuBaseKonexioa` klase publikoko metodo publiko estatiko guztiak
- `AplikazioKonfigurazioaHornitzailea` klase publikoko metodo publiko estatiko guztiak
- `AplikazioBideak` klase publikoko metodo publiko estatiko guztiak
- `HasierakoPrestaketaZerbitzua.Exekutatu()`
- `DokumentuPdfZerbitzua.SortuHelmugaBidea()`
- `TxostenGrafikaZerbitzua.LortuGrafikaTestua()`
