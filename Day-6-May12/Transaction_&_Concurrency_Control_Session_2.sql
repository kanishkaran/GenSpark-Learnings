-- SAMPLE FILE FOR CONCURRENT UPDATES

START TRANSACTION;


INSERT INTO tbl_bank(account_name, balance)       -- FOR Phantom Read
VALUES('Adam', 600)

UPDATE tbl_bank
SET account_name = 'Zane Cuban'
WHERE account_id = 2;

COMMIT;


-- FOR MVCC 
SELECT * FROM tbl_bank;

