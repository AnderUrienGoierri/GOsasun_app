-- GOsasun_DB_indizeak.sql
-- Datu-basearen bilaketak bizkortzeko indizeak (Indexes)
USE GOsasun_DB;

-- PAZIENTEAK
-- 1. Pazienteen bilaketa azkartzeko (abizenak eta izenaren arabera)
CREATE INDEX idx_pazienteak_abizenak_izena ON pazienteak (abizenak, izena);

-- HITZORDUAK
-- 2. Hitzorduen datak iragazteko (adibidez: gaurko hitzorduak edo aste honetakoak)
CREATE INDEX idx_hitzorduak_data ON hitzorduak (data);

-- NEURKETAK
-- 3. Neurketen historia paziente bakoitzeko dataren arabera ordenatuta azkartzeko
CREATE INDEX idx_neurketak_paziente_data ON neurketak (paziente_id, erregistro_data);

-- MEZUAK
-- 4. Mezularitza sarrera-ontzia (hartzailearen arabera eta dataren arabera)
CREATE INDEX idx_mezuak_hartzaile_data ON mezuak (hartzaile_id, bidalketa_data);
