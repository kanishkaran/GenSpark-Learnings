

SELECT * FROM item

-- Dirty Reads
BEGIN TRANSACTION;
UPDATE item SET item_type = 'C'
WHERE item_name = 'Boots-snake proof';
ROLLBACK;



-- Non repeatable read
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
BEGIN TRANSACTION;
SELECT * FROM Item
WHERE item_name = 'Boots-snake proof';

WAITFOR DELAY '00:00:10';

SELECT * FROM Item
WHERE item_name = 'Boots-snake proof';

COMMIT;

-- Phanthom Reads
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
BEGIN TRANSACTION;
SELECT * FROM Item
WHERE item_type = 'N';

WAITFOR DELAY '00:00:10';

SELECT * FROM Item
WHERE item_type = 'N';

COMMIT;




