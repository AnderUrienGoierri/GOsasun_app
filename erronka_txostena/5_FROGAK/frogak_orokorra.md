# 5. Frogak eta Ondorioak (Testing)

Software ingeniaritzan, inplementazio fasea osatu ondoren ezinbestekoa da probak egitea. Gure proiektuan frogak ez dira amaierako pauso bat izan soilik, prozesu iteratiboaren parte bat baizik.

## Frogekiko Sarrera eta Helburuak
Proben helburu nagusia da, **bezeroari produktua entregatu aurretik ahalik eta errore (akats/bug) gehien aurkitzea**. Horretarako, garatutako softwarearen egiaztapena eta balioztatzea burutu behar dira:

1. **Egiaztatzea (Verificar)**: Softwareak momentu oro egin behar duena zuzen egiten duela begiratzea da. Hau da, kode mailan exekuzioan errorerik edo salbuespenik (Exceptions) ez dagoela ziurtatzea.
2. **Balioztatzea (Validar)**: Softwareak diseinu fasean bezeroarekin adostutako portaera ote daukan ziurtatzea da. Adibidez, bezeroak "tentsioa grafikoki ikusi nahi dut" eskatu badu, balioztatzeak hori bisualki horrela dela frogatzen du.

## Froga Motak
Gure proiektuan bi proba mota nagusiak konbinatu ditugu:
- **Kaxa Txuriko Frogak (White-Box Testing)**: Kodearen barruko egitura, logika, begiztak eta baldintzak probatzea. Normalean garatzaileok egiten dugu (Unit Testing, adibidez).
- **Kaxa Beltzeko Frogak (Black-Box Testing)**: Funtzionalitate hutsa probatzea, barruko kodea nolakoa den jakin gabe. Erabiltzailearen ikuspuntutik egiten da: botoi bat sakatu eta espero den emaitza agertzen den aztertzen da.

---

## 5.1 Datu-Baseko Frogak
- **Kaxa Txurikoak**: Kontsulta konplexuak (JOIN dutenak) zuzenean MySQL Workbench-en exekutatu dira, erantzun-denborak 100ms-tik beherakoak direla ziurtatzeko. Bisten (`VIEWS`) egitura egiaztatu da.
- **Kaxa Beltzekoak**: `ON DELETE CASCADE` arauak ondo funtzionatzen ote duten frogatu da. Paziente fitxa bati *DELETE* egiterakoan, datu-baseko pisu guztiak eta jarraipenak automatikoki desagertzen direla balioztatu da datu "umezurtzak" ez uzteko. Emaitza positiboa izan da.

## 5.2 GOsasun App-eko Frogak
- **Kaxa Txurikoak**: C# kodean `try-catch` blokeen portaera frogatu da. Bereziki saio-hasierako enkriptazio prozesua egiaztatu da, pasahitzak zuzen *hasheatu* direla ziurtatzeko.
- **Kaxa Beltzekoak**: Formularioetan daturen bat ahazten denean (adibidez NAN laukia hutsa), sistemak mezu gorri bat botatzen duen ala zuzenean pitzatzen (crash) den frogatu da. Era berean, BM58 gailua deskonektatuta dagoela inportatu botoia sakatzean, programak abisu bat ematen duela balioztatu dugu adostutakoaren arabera.

## 5.3 GOsasun Web-eko Frogak
- **Kaxa Txurikoak**: PHP bidez datu-baseari egiten zaizkion eskaerak `Prepared Statements` bidez babestuta daudela egiaztatu da. Nahita SQL Injekzioak egiten saiatu gara inprimakietatik, eta sistemak karaktereez ihes egiten duela berretsi dugu.
- **Kaxa Beltzekoak**: Orrialdearen *Responsive* gaitasuna frogatu da nabigatzailea behin eta berriz estutuz, pantaila 480px-etik behera jaistean edukiak modu argian erakusten direla balioztatzeko. Gainera, saioa hasi gabe helbide-barran URL babestu bat idaztean (adib: `/grafikak.php`), nabigatzaileak automatikoki login atarira itzularazten gaituela balioztatu dugu.
