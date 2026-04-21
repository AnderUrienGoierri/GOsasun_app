# 2.1. Analisi Orokorra: Garapen Prozesua eta Metodologia

Softwarearen garapen-prozesu sendo eta antolatu bat bermatzeko, proiektu hau fase logikoetan banatu da, garapen-ziklo klasikoan oinarrituta. Metodologia honek aukera ematen du fase bakoitza balioztatzeko hurrengora igaro aurretik, prozesuaren kalitatea ziurtatuz. Hala ere, prozesu honen barruan ikuspegi ebolutiboa eta Agile metodologiaren printzipioak txertatu ditugu, bertsioak pixkanaka hobetzeko eta bezeroaren zein erabiltzaileen beharretara azkarrago egokitzeko.

## Garapen Prozesuaren Faseak
Proiektua erabat menperatzeko eta emaitza ezin hobea lortzeko, jarraian azaltzen diren 5 fase nagusietan oinarritu dugu gure lan-fluxua:

1. **Analisia**: Fase honetan proiektuaren oinarriak finkatzen dira.
   - *Helburuak*: Arazoa ulertu eta konponbidea proposatu.
   - *Eskakizunak*: Eskakizun funtzionalak (sistemak zer egin behar duen) eta ez-funtzionalak (errendimendua, segurtasuna, etab.) zehaztu.
   - *Plangintza*: Datu-basea (DB), aplikazioa (C#) eta webgunea nola garatuko diren zehaztu.
2. **Diseinua**: Analisian jasotako eskakizunak eredu teknikoetara itzuli.
   - *Ereduak*: Eredu Erlazionalak datu-baserako, UML diagramak klaseetarako.
   - *Interfazeak*: GUI (Erabiltzaile Interfaze Grafikoa) blokeak asmatu eta webgunerako nabigazio-mapak osatu.
3. **Programazioa (Inplementazioa)**: Diseinua kode bihurtu.
   - *Kodeketa*: Datu-basea SQL bidez sortu, aplikazioa C# bidez garatu eta webgunea HTML/CSS/PHP/JS erabiliz programatu. Git eta GitHub erabiliko dira bertsio-kontrola mantentzeko.
4. **Frogak (Testing)**: Garatutakoa balioztatu.
   - *Helburua*: Sistemak ondo funtzionatzen duela eta hasierako eskakizun guztiak betetzen dituela frogatu, akatsak (bug-ak) aurkituz.
5. **Martxan Jartzea eta Mantenua**: Produktua askatu.
   - *Hedapena*: Ingurune errealean (zerbitzarian eta bezeroen ekipoetan) hedatu. Gerora, hobekuntzak eta akatsen zuzenketak inplementatuko dira.

## Prozesu Ebolutiboa eta Agile Printzipioak
Garapen-ziklo klasiko hori ez da guztiz zurruna (Ur-jauzi edo Waterfall eredua bezala). Prozesu ebolutiboa jarraituko dugu:
- **Komunikazioa eta Plangintza (Quick Design)**: Oinarrizko diseinu azkar bat egin.
- **Modelaketa eta Eraikuntza (Prototyping)**: Prototipo funtzional bat sortu ahalik eta azkarren.
- **Feedback-a**: Bertsio hori probatu eta erabiltzaileen iritzia jaso hurrengo iterazioan aplikatzeko.

## Eskakizun Funtzionalak eta Ez-funtzionalak
- **Funtzionalak**: Erabiltzaileen erregistroa, rol-kudeaketa, tentsiometro bidezko datu-bilketa automatikoa, errezeten sorrera, hitzorduen kudeaketa.
- **Ez-funtzionalak**: Segurtasuna (pasahitzen enkriptazioa), irisgarritasuna (eleaniztasuna: Euskara, Gaztelania, Ingelesa, Nederlandera), errendimendua (kontsulta azkarrak), eta diseinu *Responsive* bat webgunean.

## Gantt Diagrama eta Plangintza Ziklikoa
Proiektuaren faseak denboran kokatzeko Gantt diagrama bat erabiliko dugu. Honek fase bakoitzaren iraupena, mugarriak (milestones) eta atazen arteko dependentziak ikusteko aukera emango digu, taldearen esfortzua optimizatuz.

## Sistemaren Ardatz Nagusia: Pazienteak eta Jarraipenak
Azterketa sakon honen ondorio garrantzitsuena hau da: proiektu honen (bai webgunean zein C# aplikazioan) **ardatz nagusia eta erdigunea pazienteak eta beraien jarraipen klinikoak dira**. Edozein funtzionalitate, diseinu, edo datu-base egituraren azken helburua pazientearen jarraipena modu fidagarri, azkar eta eskuragarrian kudeatzea da. Pazienteen datu fisikoak, tentsiometro bidezko neurketak eta eboluzio klinikoa (jarraipenak) sistemaren muina osatzen dute, beste modulu guztiak (errezetak, hitzorduak, txostenak) jarraipen horren inguruan orbitatzen dutelarik.
