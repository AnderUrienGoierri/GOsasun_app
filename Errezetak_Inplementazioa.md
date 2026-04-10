# Errezeten Inplementazioa - GOSasun App

Medikuaren moduluan errezeten kudeaketa egonkorra izateko emandako pausoak:

1. **Datu-basearen Diseinua**: 
   - `errezetak` taula sortu da hitzorduekin eta pazienteekin lotzeko.
   - `errezeta_botikak` taula sortu da errezetei botika anitz lotzeko (N:M erlazioa).
   - Test datuak txertatu dira (`500_errezetak_test.sql`) probetarako.

2. **Datu-Aksesu Geruza (Repositories)**:
   - `ErrezetaDB` klasea: Errezetak sortu, irakurri, eguneratu eta ezabatzeko (CRUD) metodoak.
   - `BotikaDB` klasea: Medikamentuen zerrenda kudeatzeko.

3. **Interfazearen Birmoldaketa (UI Refactoring)**:
   - Pantaila guztiak subfolder-etan antolatu dira: `Medikua/`, `Pazientea/`, `Harrerakoa/`.
   - `OinarriPantaila` klasea sortu da goiburua eta nabigazioa bateratzeko.

4. **Errezeta Pantaila Berriak**:
   - `ErrezetakMenua`: Aukerak (Sortu/Ikusi) hautatzeko.
   - `ErrezetakIkusi`: Iragazki dinamikoak eta botiken bistaratzea beheko taulan.
   - `ErrezetaSortu`: Errezetak sortzeko eta lehendik daudenak editatzeko bertsio bateratua.

5. **Egonkortasuna eta Segurtasuna**:
   - `BindingSource` eta DTO kudeatzaileak inplementatu dira `DataGridView` kontrolen erroreak (Index -1) saihesteko.
   - Errore-kudeatzaile globala ezarri da `Program.cs`-n.
