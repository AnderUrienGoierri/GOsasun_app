-- GOsasun_DB_collation_fix.sql
-- Helburua:
--   MySQL 8-n ager daitezkeen `illegal mix of collations` erroreak saihesteko
--   datu-basearen eta base table guztien kolazioa `utf8mb4_unicode_ci`-ra bateratzea.
--
-- Erabilera:
--   1. Aukeran, egin backup bat migrazioa exekutatu aurretik.
--   2. Exekutatu fitxategi hau `gosasun_db` instantzian.
--   3. Amaieran dauden egiaztapen-kontsultak exekutatu eta ziurtatu ez dela `utf8mb4_0900_ai_ci`
--      edo bestelako kolazio desberdinik geratu testu-zutabeetan.

USE gosasun_db;

ALTER DATABASE gosasun_db
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

SET @aurreko_foreign_key_checks = @@FOREIGN_KEY_CHECKS;
SET FOREIGN_KEY_CHECKS = 0;

DROP PROCEDURE IF EXISTS NormalizatuGOsasunDbKolazioa;

DELIMITER $$

CREATE PROCEDURE NormalizatuGOsasunDbKolazioa()
BEGIN
    DECLARE amaituta BOOLEAN DEFAULT FALSE;
    DECLARE taulaIzena VARCHAR(255);

    DECLARE taulaKurtsorea CURSOR FOR
        SELECT TABLE_NAME
        FROM information_schema.TABLES
        WHERE TABLE_SCHEMA = DATABASE()
            AND TABLE_TYPE = 'BASE TABLE'
        ORDER BY TABLE_NAME;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET amaituta = TRUE;

    OPEN taulaKurtsorea;

    taulaLoop: LOOP
        FETCH taulaKurtsorea INTO taulaIzena;

        IF amaituta THEN
            LEAVE taulaLoop;
        END IF;

        SET @sql = CONCAT(
            'ALTER TABLE `',
            REPLACE(taulaIzena, '`', '``'),
            '` CONVERT TO CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci'
        );

        PREPARE stmt FROM @sql;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
    END LOOP;

    CLOSE taulaKurtsorea;
END$$

DELIMITER ;

CALL NormalizatuGOsasunDbKolazioa();

DROP PROCEDURE IF EXISTS NormalizatuGOsasunDbKolazioa;

SET FOREIGN_KEY_CHECKS = @aurreko_foreign_key_checks;

-- Egiaztapena 1:
-- Datu-basearen default kolazioa.
SELECT SCHEMA_NAME, DEFAULT_CHARACTER_SET_NAME, DEFAULT_COLLATION_NAME
FROM information_schema.SCHEMATA
WHERE SCHEMA_NAME = 'gosasun_db';

-- Egiaztapena 2:
-- Taulen default kolazioa.
SELECT TABLE_NAME, TABLE_COLLATION
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = 'gosasun_db'
  AND TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;

-- Egiaztapena 3:
-- Testu-zutabeetan kolazio arrarorik geratzen den ikusteko.
SELECT TABLE_NAME, COLUMN_NAME, CHARACTER_SET_NAME, COLLATION_NAME, COLUMN_TYPE
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = 'gosasun_db'
  AND DATA_TYPE IN ('char', 'varchar', 'text', 'tinytext', 'mediumtext', 'longtext', 'enum', 'set')
ORDER BY TABLE_NAME, ORDINAL_POSITION;