# 2.4. GOsasun Web-aren Analisia

GOsasun webgunea aplikazio mahaigainekoaren (Desktop) ezinbesteko luzapena da. Aplikazioa zentro barruko kudeaketa librerako pentsatuta badago ere, webgunearen helburua **irisgarritasun unibertsala** bermatzea da. Pazienteek zein langileek euren informaziora sarbidea izango dute edonondik eta edozein gailutatik.

## Erabiltzaileak, Irisgarritasuna eta Responsive Diseinua
Web orria bi erabiltzaile profil nagusirentzat diseinatu da, erabilpen-helburu oso ezberdinekin:
- **Pazienteak**: Atari pertsonal honen bitartez, pazientea ahalduntzea da helburua. Bere osasun-datuak gardenki eta modu ulerterrazean erakutsi behar zaizkio.
- **Osasun Langileak eta Harrera**: Txandak, hitzorduak eta pazienteen oinarrizko datuak kontsultatzeko tresna arina, zentrotik kanpo daudenerako.

Erabiltzaile hauek askotan sakelako telefonoak erabiliko dituztela aurreikusita, sistemak nahitaez **Responsive (gailu anitzetara moldagarria)** izan behar du. Analisiak erakusten du diseinuak HTML5, CSS3 eta CSS Grid/Flexbox teknikak erabili behar dituela, edukia pantaila txikietara (Mobile-First ikuspegia) modu naturalean egokitzeko.

## Datu Klinikoak eta Jarraipenak (Webaren Erdigunea)
Sistemaren ardatza jarraipenak direnez, webgunean ere **Pazienteen Jarraipenak dira funtzio izarra**. Pazienteek euren osasun jarraipen guztiak kontsultatu ditzakete modu grafiko eta ulerterrazean. Tentsioaren, pultsuaren eta pisuaren bilakaera grafikoen bidez (JavaScript liburutegiak erabiliz, adib. Chart.js) erakustea eta sendagileak sortutako PDF txostenak deskargatu ahal izatea da web atariaren funtzionalitate kritikoena eta garrantzitsuena.

## Funtzionalitate Osagarrien Analisia
Jarraipenez gain, web orriak honako betebehar funtzionalak izango ditu:
1. **Ataria (Dashboard/Portal)**
   - *Funtzioa*: Saioa hasi bezain laster, erabiltzaileari zuzendutako panel pertsonalizatua. Azken hitzorduak, azken neurketak eta abisu garrantzitsuak bistaratzen ditu kolpe bakarrean.
2. **Hitzorduen Kudeaketa eta Kontsulta**
   - *Funtzioa*: Pazienteek beraien hurrengo mediku-bisitak ikustea, data eta ordu zehatzekin, eta behar izanez gero ezeztatzea.
3. **Errezeta Digitalen Atala**
   - *Funtzioa*: Pazienteak eskuragarri dituen botiken zerrenda, hartu beharreko dosifikazioa eta iraungitze-datak modu argian erakustea.
4. **Profilaren Pertsonalizazioa**
   - *Funtzioa*: Erabiltzaileak bere datu pertsonalak (telefonoa, hizkuntza hobespena) kudeatzea eta segurtasuna bermatzeko pasahitza eguneratzea. Era berean, profileko argazkiak igotzeko aukera eskaintzea.

## Azpiegitura eta Teknologia Web-a
Webgunea inplementatzeko azpiegitura klasiko bat aztertu da:
- **Backend-a**: PHP erabiliko da datu-basearekin (MySQL) komunikatzeko eta negozio-logika kudeatzeko (API deiak sortzeko).
- **Frontend-a**: JavaScript garbia (Vanilla JS) eta AJAX deiak erabiliko dira orrialdeak dinamikoki eguneratzeko (orrialde osoa birkargatu gabe), erabiltzaile-esperientzia askoz fluidoagoa lortuz.
