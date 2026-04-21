# 2.3. GOsasun App-aren Analisia

GOsasun aplikazioa (C# eta .NET teknologietan oinarritutako Desktop bertsioa) kudeaketa kliniko integralerako garatu den tresna pisu-tsua (Thick Client) da. Aplikazio honen azterketak erakusten du beharrezkoa dela interfaze azkar, fidagarri eta gailu medikoekin zuzenean komunikatzeko gai den sistema bat.

## Nori zuzendua (Intraneta) eta Arkitektura
Aplikazioa **Intranet** moduan funtzionatzeko diseinatuta dago osasun-zentroaren barruan. Ez da Internet bidezko publikoarentzat; osasun-zentroko langileek (medikuak, erizainak, harrerakoak) eta zentrora bertaratzen diren pazienteek soilik erabiliko dute zentroko barne sarean edo terminal espezifikoetan.

Arkitekturaren aldetik, aplikazioak **MVC (Model-View-Controller)** eredua zorrozki jarraitzen du. Honek aplikazioaren logika (Datu-basearekiko konexioa eta kalkuluak) eta ikuspegia (Windows Forms interfazea) bereiztea bermatzen du, etorkizuneko mantenu-lanak asko erraztuz.

## Erabilpen Kasuen Analisi Sakona
Aplikazioak bete beharreko funtzionalitate nagusiak honako erabilpen kasuetan banatzen dira:

1. **Saio-hasiera Segurua (Login)**
   - *Funtzioa*: Erabiltzaile bakoitza bere rolaren arabera autentifikatu (Harrera, Osasun Langilea, Pazientea).
   - *Analisi teknikoa*: Kredentzialen egiaztapena enkriptazio bidez egingo da, eta blokeo-sistemak ezarriko dira saiakera okerren aurrean.

2. **Pazienteen eta Erabiltzaileen Kudeaketa (CRUD)**
   - *Funtzioa*: Harrerakoek zein osasun langileek paziente berriak erregistratu, editatu eta bilatu ahal izatea.
   - *Analisi teknikoa*: Bilaketa-barra dinamikoak eta iragazkiak (Altan/Bajan) beharko dira datu-multzo handietan nabigazio azkarra bermatzeko.

3. **Neurketen Inportazioa eta Jarraipenen Erregistroa (Aplikazioaren Erdigunea)**
   - *Funtzioa*: Pazientearen osasun egoera kontrolatzea da sistemaren xede nagusia.
   - *Analisi teknikoa*: **Beurer BM58 tentsiometroarekin** konexio zuzena ezartzea (USB-HID protokoloa edo Serie Portua erabiliz) neurketak (tentsio sistolikoa, diastolikoa, pultsua) automatikoki eta akatsik gabe jasotzeko. Jarraipen hauen erregistro automatikoa sistemaren bihotza da. Eskuzko sarrerak ekidinez, fidagarritasuna %100ekoa da.

4. **Jarraipen Txostenak Sortu (PDF Esportazioa)**
   - *Funtzioa*: Pazienteen jarraipen datuekin osasun-txosten profesionalak modu automatikoan sortzea.
   - *Analisi teknikoa*: QuestPDF bezalako liburutegiak erabiliko dira datu taulak eta bilakaera-grafikoak biltzen dituzten dokumentu bisualki erakargarriak eta inprimatzeko prest daudenak sortzeko. Txosten hauek ere datu-basean gordeko dira jarraipen bakoitzari lotuta.

5. **Errezeten Kudeaketa eta Interoperabilitatea**
   - *Funtzioa*: Medikuek botikak esleitu eta errezetak digitalki sortzea.
   - *Analisi teknikoa*: Errezeta horiek XML formatuan esportatzeko ahalmena izango du sistemak, beste osasun-sistema batzuekin (kanpoko farmaziekin, adibidez) interoperabilitatea bermatzeko.

## Erabilpen Kasu Diagrama (UML Ikuspegia)
- **Aktoreak**: Osasun Langilea, Harrerako Langilea, Pazientea, Tentsiometroa (Hardware Aktorea).
- **Eszenatoki Ohikoena**: 
  1. Pazientea kontsultara sartu.
  2. Osasun Langileak tentsiometroa konektatu.
  3. Sistemak datuak inportatu eta *Jarraipen* berri bat sortzen du DBan.
  4. Langileak PDF txosten bat sortzen du emaitzekin eta pazienteari inprimatzen dio.
