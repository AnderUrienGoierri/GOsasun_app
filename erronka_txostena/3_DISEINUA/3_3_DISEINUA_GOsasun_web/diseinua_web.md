# 3.4. GOsasun Web-aren Diseinu Aurreratua eta Elementuak

GOsasun web ataria diseinatzerakoan, teknologia-pila (Tech Stack) oso bat uztartu dugu nabigatzailean esperientzia moderno, azkar eta seguru bat eskaintzeko. Atal honetan HTML, CSS, JS eta PHP fitxategietako kode adibide zehatzak aztertuko ditugu funtzio bakoitzaren arrazoia ulertzeko.

## 1. CSS3: Media Queries eta Mobile First Metodologia
Diseinu erantzunkorra (Responsive Design) ezinbestekoa da webgune modernotan. Gure diseinuan `@media` kontsultak erabili ditugu, bereziki *Mobile First* hurbilketa partzial bat erabiliz pantaila txikietan edukia berrantolatzeko.

**Kode pantailazoa: `pazienteak.css` (Aldagaiak eta Media Query)**
```css
/* 1. Aldagai korporatiboak (Custom Properties) */
:root {
    --bg-surface: #ffffff;
    --border-color: #e0e0e0;
    --box-shadow: 0 4px 20px rgba(0,0,0,0.1);
}

/* 2. Diseinu Nagusia (Desktop) Flexbox erabiliz */
.orri-goiburua {
    display: flex;
    justify-content: flex-start;
    align-items: center;
    gap: 30px;
}

/* 3. MOBILE FIRST / RESPONSIVE EGOKITZAPENA */
/* Pantaila 480px edo txikiagoa bada (Mugikorrak) */
@media (max-width: 480px) {
    .orri-goiburua {
        flex-direction: column; /* Elementuak bata bestearen azpian pilatu */
        align-items: flex-start;
        gap: 15px;
    }
    
    .talde-flex .botoia {
        width: 100%; /* Botoiak pantaila osoko zabalera hartzen du */
        margin: 0 !important;
        text-align: center;
        justify-content: center;
    }
}
```
**Azalpena**:
- **`:root` aldagaiak**: Proiektu osoan zehar koloreak eta itzalak estandarizatzen dituzte. Hau ezinbestekoa da koherentzia bisuala mantentzeko.
- **`display: flex`**: Goiburuko edukia (titulua, bilatzailea) lerro berean antolatzeko.
- **`@media (max-width: 480px)`**: Metodologia honen muina da. Pantaila ordenagailu batena bada, CSS arrunta irakurtzen du. Baina pantaila txikia bada (480px baino gutxiago), bloke honetako kodeak aurrekoa zapaltzen du. `flex-direction: column` jarriz, ondoan zeuden elementuak zutabetan pilatzen dira hatzarekin mugitzeko (scroll) prest, eta botoiei `%100` zabalera ematen zaie pantailan errazago sakatu ahal izateko.

## 2. HTML5: Etiketa Semantikoen Erabilera
Webaren hezurdura ez da bakarrik `<div>` etiketekin osatu, irisgarritasuna (SEO eta irakurgailuak) bermatzen duten HTML5 etiketa semantikoak erabili dira.

**Kode pantailazoa: HTML egitura klasikoa**
```html
<main class="eduki-nagusia">
    <!-- Goiburua eta Titulartasuna -->
    <header class="orri-goiburua">
        <h2>Pazienteen Jarraipenak</h2>
        <p>Hemen zure osasun historia kontsulta dezakezu.</p>
    </header>
    
    <!-- Datuen atal logiko bat -->
    <section class="kutxa-zuria txartel-atala">
        <!-- Taula semantikoa -->
        <table class="datu-taula" id="paziente-taula">
            <thead>
                <tr>
                    <th>Data</th>
                    <th>Tentsioa</th>
                    <th>Ekintzak</th>
                </tr>
            </thead>
            <tbody>
                <!-- JS-ak AJAX bidez dinamikoki beteko du hau -->
            </tbody>
        </table>
    </section>
</main>
```
**Azalpena**:
- `<main>`: Orriaren eduki garrantzitsuena mugatzen du. Bilatzaileek (Google) etiketa hau bilatzen dute lehenik.
- `<header>`: Tituluak biltzen ditu.
- `<section>`: Bloke tematiko bat banatzen du (adibidez, jarraipenen taula).
- `<table>`, `<thead>`, `<tbody>`: Datu historikoak zutabeetan zentzuarekin antolatzeko etiketak. Egitura honek estiloak ematea asko errazten du (adibidez CSSan `thead { background: grey }`).

## 3. PHP: Backend Logika eta Datu-basearekiko Konexioa
PHP-k atzeko planoan lan egiten du datuak irakurtzeko. Datu-basearekiko elkarrekintza PDO bidez egingo da.

**Kode pantailazoa: `bilatu_pazienteak_ajax.php`**
```php
<?php
// 1. Saioa hasi aldagai globaletara sartzeko
session_start();
require_once 'DB_konexioa.php';

// JSON erantzungo dugula zehaztu
header('Content-Type: application/json');

if (!isset($_SESSION['erabiltzaile_id'])) {
    echo json_encode(['success' => false, 'error' => 'Saioa hasi gabe']);
    exit; // Exekuzioa gelditu segurtasunagatik
}

$testua = $_GET['q'] ?? '';
$langile_id = $_SESSION['erabiltzaile_id'];

try {
    // 2. PDO: Prepared Statement (SQL Injekzioen aurka)
    $bilaketa = "%$testua%";
    $stmt = $pdo->prepare("
        SELECT paziente_id, izena, abizenak, nan, irudia 
        FROM V_Langile_Pazienteak 
        WHERE langile_id = ? AND (izena LIKE ? OR abizenak LIKE ?)
        LIMIT 10
    ");
    
    // ? ikurren lekuan aldagaiak txertatu modu seguruan
    $stmt->execute([$langile_id, $bilaketa, $bilaketa]);
    
    // 3. Emaitzak Array asoziatibo gisa atera eta JSON-era pasatu
    $pazienteak = $stmt->fetchAll(PDO::FETCH_ASSOC);
    echo json_encode(['success' => true, 'pazienteak' => $pazienteak]);
} catch (PDOException $e) {
    echo json_encode(['success' => false, 'error' => $e->getMessage()]);
}
?>
```
**Azalpena**:
Funtzio honek argi erakusten du backend garapenaren kalitatea. Segurtasuna bermatzeko `$_SESSION` egiaztatzen da hasteko. Garrantzitsuena `pdo->prepare()` funtzioa da; honek bermatzen du erabiltzaileak bilaketa kutxan idazten duena ezingo dela kode exekutagarri bihurtu datu-basean (SQL Injection), dena testu gisa tratatzen delako.

## 4. JavaScript (jQuery): AJAX deietarako
Orrialde bat birkargatu gabe edukia aldatzea "Single Page Application" izaeraren oinarria da. Horretarako AJAX deiak (Asynchronous JavaScript and XML) erabili ditugu.

**Kode pantailazoa: AJAX bilaketa asinkronoa**
```javascript
// DOM kargatzean prest egon
$(document).ready(function() {
    
    // Pazientea bilatzeko laukian tekla bat altxatzean
    $('#bilaketa-input').on('keyup', function() {
        let testua = $(this).val(); // Erabiltzaileak idatzitakoa jaso
        
        if(testua.length >= 2) {
            // Eskaera asinkronoa zerbitzariari
            $.ajax({
                url: 'bilatu_pazienteak_ajax.php',
                type: 'GET',
                data: { q: testua },
                success: function(response) {
                    if(response.success) {
                        // 1. Aurreko datuak garbitu
                        $('#paziente-taula tbody').empty();
                        
                        // 2. Datu berriak begizta bidez sortu
                        response.pazienteak.forEach(p => {
                            let lerroa = `<tr>
                                <td>${p.izena} ${p.abizenak}</td>
                                <td>${p.nan}</td>
                                <td><button class="btn">Ikusi</button></td>
                            </tr>`;
                            $('#paziente-taula tbody').append(lerroa);
                        });
                    }
                }
            });
        }
    });
});
```
**Azalpena**:
`keyup` gertaerak tekla bat sakatu eta askatzean abiarazten du logika. `.ajax` metodoak bezeroaren nabigatzailetik PHP fitxategira hegan egiten du isilpean, lortutako JSON erantzuna `response` aldagaian jasoz. Gero, DOM manipulazioa egiten da: `.empty()` bidez taula garbitzen da, eta `forEach` begiztarekin HTML berria sortu eta `.append()` bidez sartzen da orrialdean. Zero birkargatze, abiadura maximoa.
