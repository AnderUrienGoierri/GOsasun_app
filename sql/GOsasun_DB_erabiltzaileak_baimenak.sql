-- SEGURTASUNA ETA BAIMENAK - GOsasun_DB
USE gosasun_db;
-- 1. ADMIN ERABILTZAILEA
CREATE USER 'adminRoot' @'%' IDENTIFIED BY 'pasahitzZaila1234';
GRANT ALL PRIVILEGES ON GOsasun_DB.* TO 'adminRoot' @'%' WITH
GRANT OPTION;
-- 2. ROLAK SORTU
CREATE ROLE IF NOT EXISTS 'db_admin',
'db_osasun_langilea',
'db_pazientea',
'db_harrera';
-- 3. ADMIN BAIMENAK
GRANT ALL PRIVILEGES ON GOsasun_DB.* TO 'db_admin';
-- 4. OSASUN LANGILEAREN BAIMENAK (db_osasun_langilea)
-- Bistak (Irakurketa bakarrik)
GRANT SELECT ON GOsasun_DB.V_Login TO 'db_osasun_langilea';
GRANT SELECT ON GOsasun_DB.V_Pazientea TO 'db_osasun_langilea';
GRANT SELECT ON GOsasun_DB.V_Osasun_Langilea TO 'db_osasun_langilea';
GRANT SELECT ON GOsasun_DB.V_Langile_Pazienteak TO 'db_osasun_langilea';
GRANT SELECT ON GOsasun_DB.V_Hitzorduak_Osoa TO 'db_osasun_langilea';
GRANT SELECT ON GOsasun_DB.V_Harrera TO 'db_osasun_langilea';
-- Taula komunak eta kudeaketa
GRANT SELECT ON GOsasun_DB.Rolak TO 'db_osasun_langilea';
GRANT SELECT,
    INSERT,
    UPDATE ON GOsasun_DB.Erabiltzaileak TO 'db_osasun_langilea';
GRANT SELECT,
    INSERT,
    UPDATE ON GOsasun_DB.Pazienteak TO 'db_osasun_langilea';
GRANT SELECT,
    INSERT,
    UPDATE ON GOsasun_DB.Osasun_Langileak TO 'db_osasun_langilea';
GRANT SELECT ON GOsasun_DB.Pazientek_Langileak TO 'db_osasun_langilea';
GRANT SELECT,
    UPDATE,
    DELETE ON GOsasun_DB.Botikak TO 'db_osasun_langilea';
-- Jarraipenak, Dokumentuak eta Errezetak (Kudeaketa osoa)
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Jarraipenak TO 'db_osasun_langilea';
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Dokumentuak TO 'db_osasun_langilea';
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Errezetak TO 'db_osasun_langilea';
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Errezeta_Botikak TO 'db_osasun_langilea';
-- Hitzorduak (Irakurketa eta aldaketa)
GRANT SELECT,
    UPDATE ON GOsasun_DB.Hitzorduak TO 'db_osasun_langilea';
-- 5. PAZIENTEAREN BAIMENAK (db_pazientea)
-- Bistak
GRANT SELECT ON GOsasun_DB.V_Login TO 'db_pazientea';
GRANT SELECT ON GOsasun_DB.V_Pazientea TO 'db_pazientea';
GRANT SELECT ON GOsasun_DB.V_Osasun_Langilea TO 'db_pazientea';
GRANT SELECT ON GOsasun_DB.V_Langile_Pazienteak TO 'db_pazientea';
GRANT SELECT ON GOsasun_DB.V_Hitzorduak_Osoa TO 'db_pazientea';
-- Datuak eta kudeaketa propioa
GRANT SELECT,
    UPDATE ON GOsasun_DB.Erabiltzaileak TO 'db_pazientea';
GRANT SELECT,
    UPDATE ON GOsasun_DB.Pazienteak TO 'db_pazientea';
GRANT SELECT ON GOsasun_DB.Pazientek_Langileak TO 'db_pazientea';
GRANT SELECT ON GOsasun_DB.Rolak TO 'db_pazientea';
-- Jarraipenak eta Dokumentuak (Irakurri eta gehitu bereak)
GRANT SELECT,
    INSERT ON GOsasun_DB.Jarraipenak TO 'db_pazientea';
GRANT SELECT,
    INSERT ON GOsasun_DB.Dokumentuak TO 'db_pazientea';
-- Hitzorduak (Eskatu eta kudeatu)
GRANT SELECT,
    INSERT,
    UPDATE ON GOsasun_DB.Hitzorduak TO 'db_pazientea';
-- Errezetak eta Botikak (Irakurri bakarrik)
GRANT SELECT ON GOsasun_DB.Errezetak TO 'db_pazientea';
GRANT SELECT ON GOsasun_DB.Errezeta_Botikak TO 'db_pazientea';
GRANT SELECT ON GOsasun_DB.Botikak TO 'db_pazientea';
-- 6. HARRERAREN BAIMENAK (db_harrera)
-- Datu nagusien kudeaketa (Zentralizatua)
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Erabiltzaileak TO 'db_harrera';
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Pazienteak TO 'db_harrera';
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Osasun_Langileak TO 'db_harrera';
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Harrerako_Langileak TO 'db_harrera';
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Pazientek_Langileak TO 'db_harrera';
GRANT SELECT,
    INSERT,
    UPDATE,
    DELETE ON GOsasun_DB.Hitzorduak TO 'db_harrera';
-- Irakurketa eta kudeaketa laguntzailea
GRANT SELECT ON GOsasun_DB.Rolak TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.Botikak TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.Jarraipenak TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.Dokumentuak TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.Errezetak TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.Errezeta_Botikak TO 'db_harrera';
-- Bistak
GRANT SELECT ON GOsasun_DB.V_Login TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.V_Pazientea TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.V_Osasun_Langilea TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.V_Harrera TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.V_Hitzorduak_Osoa TO 'db_harrera';
GRANT SELECT ON GOsasun_DB.V_Langile_Pazienteak TO 'db_harrera';
-- 7. APLIKAZIO ERABILTZAILEAK
-- Pazientearen App-a (PHP)
CREATE USER IF NOT EXISTS 'app_php_pazienteak' @'localhost' IDENTIFIED BY 'AppPaziente123!';
GRANT 'db_pazientea' TO 'app_php_pazienteak' @'localhost';
SET DEFAULT ROLE 'db_pazientea' TO 'app_php_pazienteak' @'localhost';
-- Medikuaren App-a (C# Desktop)
CREATE USER IF NOT EXISTS 'app_csharp_medikua' @'%' IDENTIFIED BY 'AppMediku123!';
GRANT 'db_osasun_langilea' TO 'app_csharp_medikua' @'%';
SET DEFAULT ROLE 'db_osasun_langilea' TO 'app_csharp_medikua' @'%';
-- Harreraren App-a (PHP)
CREATE USER IF NOT EXISTS 'app_php_harrera_langileak' @'localhost' IDENTIFIED BY 'AppHarrera123!';
GRANT 'db_harrera' TO 'app_php_harrera_langileak' @'localhost';
SET DEFAULT ROLE 'db_harrera' TO 'app_php_harrera_langileak' @'localhost';
FLUSH PRIVILEGES;