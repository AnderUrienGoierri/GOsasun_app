# 2.2. Datu-basearen Analisia

Datu-basearen analisiak sistemak kudeatuko dituen datuen bolumena, izakera, osotasuna eta entitateen arteko erlazioak sakonki definitzen ditu. Analisi hau funtsezkoa da datuen sendotasuna eta informazioaren eskuragarritasuna bermatzeko, datu-base erlazionalen normalizazio printzipioak (3NF) jarraituz.

## Erabiltzaile Motak eta Datu-Sarbideak
GOsasun sistemak rol ezberdinak kudeatzen ditu, eta rol bakoitzak datu-baseko taula eta bista zehatzetara sarbide desberdina izango du:
- **Osasun Langilea (Medikuak/Erizainak)**: Baimen zabalak dituzte. Pazienteen datu klinikoak irakurri eta idatzi ditzakete. Jarraipenak, txostenak eta errezetak sortzea da beraien pisu nagusia datu-basean.
- **Pazientea**: Baimen mugatuak dituzte. Euren datu propioak (neurketak, hitzorduak, errezetak) soilik irakurri ditzakete (Irakurketa eskubidea / SELECT).
- **Harrerako Langilea**: Kudeaketa administratiboa egiten dute. Erabiltzaileak (pazienteak eta langileak) kudeatu eta hitzorduak antolatzen dituzte. Ez dute eskuragarritasun zuzenik pazienteen datu sentikor klinikoetarako (adibidez, jarraipeneko ohar medikoak).

## Eredu Kontzeptuala: Entitateak eta Beraien Ezaugarriak
Datuak egituratzeko honako entitateak eta beraien ezaugarri nagusiak identifikatu dira, etorkizuneko Eredu Erlazionalaren oinarri izango direnak:

1. **Erabiltzaileak (Super-entitatea)**
   - Sistema erabiltzen duten pertsona guztien datu komunak biltzen ditu (Herentzia eredu bat simulatuz).
   - **Atributu nagusiak**: `ID` (PK), `Email` (Unique), `Pasahitza` (Enkriptatua), `Rol_ID` (FK), `NAN` (Unique), `Izena`, `Abizenak`, `Jaiotze_data`, `Telefonoa`, `Hizkuntza`, etab.

2. **Pazienteak (Azpi-entitatea)**
   - Pazienteen jarraipen fisikorako datu estatikoak edo administratiboak.
   - **Atributu nagusiak**: `ID` (PK/FK Erabiltzaileak), `Sexua`, `Odol_taldea`, `Azken_altuera`, `Azken_pisua`, `Egoera_klinikoa` (Alta/Baja).

3. **Osasun Langileak (Azpi-entitatea)**
   - Pertsonal medikuaren lan-informazioa.
   - **Atributu nagusiak**: `ID` (PK/FK), `Elkargokide_zenbakia` (Unique), `Espezialitatea`, `Kontsulta`, `Lanaldia`.

4. **Harrerako Langileak (Azpi-entitatea)**
   - Kudeaketa administratiboko pertsonala.
   - **Atributu nagusiak**: `ID` (PK/FK), `Txanda` (Goizez/Arratsaldez).

5. **Hitzorduak**
   - Paziente eta osasun-langileen arteko topaketak planifikatzeko entitatea.
   - **Atributu nagusiak**: `ID` (PK), `Paziente_ID` (FK), `Osasun_Langile_ID` (FK), `Data`, `Hasiera_ordua`, `Bukaera_ordua`, `Egoera` (Zain, Bukatuta, Ezeztatuta).

6. **Jarraipenak (Sistemaren Ardatza)**
   - Neurketa kliniko puntualak. Hau da datu-baseko entitate garrantzitsuena eta erabiliena.
   - **Atributu nagusiak**: `ID` (PK), `Paziente_ID` (FK), `Tentsio_sistolikoa`, `Tentsio_diastolikoa`, `Pultsua`, `Oharrak`, `Erregistro_data`.

7. **Dokumentuak**
   - Jarraipenei lotutako fitxategi digitalak (PDF txostenak, esaterako).
   - **Atributu nagusiak**: `ID` (PK), `Jarraipen_ID` (FK), `Bidea_zerbitzarian`, `Fitxategi_izena`.

8. **Errezetak eta Botikak**
   - **Errezetak**: Medikuek pazienteentzat sortutako preskripzioak. `Igorpen_data`, `Iraungitze_data`, `Diagnostikoa`.
   - **Botikak**: Katalogo orokorra. `Izena`, `Izen_kimikoa`, `Eragin_fokoa`.
   - **Errezeta-Botikak (N:M erlazioa)**: Errezeta batek botika asko izan ditzake, eta botika bat errezeta askotan egon daiteke. Tartekako taula bat beharko da Dosia eta Maiztasuna gordetzeko.

## Datuen Osotasuna eta Segurtasuna
Analisi fase honetan argi finkatzen da datuen segurtasuna ezinbestekoa dela. Horretarako, pasahitzak modu seguruan gordeko dira, kanpotarren erasoak ekiditeko injekzioen aurkako babesak (Prepared Statements) erabiliko dira, eta taulen arteko osotasun erreferentziala (Foreign Keys ON DELETE CASCADE / RESTRICT) zorrotz aplikatuko da datu umezurtzak saihesteko.
