# 4.3. GOsasun Web-aren Inplementazioa

Web plataforma garatzeko orduan, diseinuko erabakiak (Mobile First, AJAX bidezko karga asinkronoa, Arkitektura garbia) kode exekutagarri bihurtu ditugu. PHP 8, CSS Grid/Flexbox eta JavaScript Vanilla + jQuery teknologien bateratzea egin dugu.

## Mobile First Diseinua Inplementatzea
Diseinuan aipatu bezala, lehenengo pentsamendua mugikorrak izan behar zirela erabaki genuen. CSS kodea inplementatzerakoan, oinarrizko arauak gailu txikientzat egiten dira, eta `min-width` (ez soilik `max-width`) kontsultak erabiltzen dira pantaila handietarako.

**Kode pantailazoa: CSS Inplementazio erantzunkorra (`pazienteak.css`)**
```css
/* Oinarrizko inplementazioa: Mugikorrentzat pentsatua automatikoki */
.fitxa-edukiontzia {
    display: grid;
    grid-template-columns: 1fr; /* Zutabe bakar bat, elementuak gainean/azpian */
    gap: 20px;
}

/* Pantaila tableta edo ordenagailua bada (768px baino zabalagoa) */
@media (min-width: 768px) {
    .fitxa-edukiontzia {
        grid-template-columns: 1fr 2fr; /* Bi zutabetan banatu espazioa */
        gap: 30px;
    }
}
```
**Azalpena**: Inplementazio honek bermatzen du mugikor bat datuak kargatzen saiatzean ez duela prozesatze-ahalmen handirik behar, zuzenean egitura lineala onartzen duelako. CSS-ak bakarrik pantaila handiagoa detektatzean hartzen du zutabeak banatzeko lana.

## Arkitektura Dinamikoaren Inplementazioa (AJAX)
Erabiltzailearen esperientzia moderno eta arin bat inplementatzeko, diseinu-fasean aplikazio izaera (SPA - Single Page Application sentsazioa) ematea erabaki genuen. Hau lortzeko, orrialdeen birkargatzea %80an saihestu dugu inplementazioan.

**Kode pantailazoa: Txosten dinamikoen inplementazioa (`txostenak.js`)**
```javascript
// Filtro bat aldatzean (adibidez, urteko edo hilabeteko grafikoa ikusi nahi dugunean)
$('#denbora-filtroa').on('change', function() {
    const filtroa = $(this).val();
    
    // AJAX bidez PHP backend-ari deitu
    fetch(`grafikak.php?filtroa=${filtroa}&paziente_id=${unekoPazienteId}`)
        .then(response => response.json())
        .then(data => {
            // Chart.js objektua eguneratu PHPk itzulitako datu berriekin
            unekoGrafikoa.data.labels = data.datak;
            unekoGrafikoa.data.datasets[0].data = data.tentsioSistolikoa;
            unekoGrafikoa.data.datasets[1].data = data.tentsioDiastolikoa;
            unekoGrafikoa.update(); // Grafikoa momentuan aldatzen da, dirdirarik gabe
        });
});
```
**Azalpena**: Inplementazio zati honek erakusten du zein ondo sinkronizatu ditugun Backend-a (PHP) eta Frontend-a (JS). PHP script batek datu baseari kontsulta egiten dio eta datu gordinak itzultzen ditu JSON formatuan. Ondoren, Javascript-ak zuzenean DOM-eko grafikoa (Chart.js) aldatzen du modu fluidoan.
