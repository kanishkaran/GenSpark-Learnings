
-- Dirty Reads
BEGIN TRANSACTION;
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
select * from item	
COMMIT;

-- Non repeatable read
BEGIN TRANSACTION;
	UPDATE item SET item_color = 'Blue'
	WHERE item_name = 'Boots-snake proof';
COMMIT;

-- Phanthom Read
BEGIN TRANSACTION;
	INSERT INTO item(item_name, item_type, item_color)
	VALUES ('Item A', 'N', 'Orane');
COMMIT;