-- SESSION 1


SELECT * FROM tbl_bank;

 -- Q1. Try two concurrent updates to same row → see lock in action.

 -- TRANSACTION A
BEGIN;

UPDATE tbl_bank
SET balance = balance + 50
WHERE account_id = 1;

SELECT * FROM tbl_bank;		-- Temp Update


COMMIT;

-- Q2. Write a query using SELECT...FOR UPDATE and check how it locks row.

 -- TRANSACTION A
BEGIN;

SELECT balance 
FROM tbl_bank
WHERE account_id = 1
FOR UPDATE; 		-- Lock Acquired


COMMIT;

-- Q3. Intentionally create a deadlock and observe PostgreSQL cancel one transaction.

CREATE TABLE items (
    id SERIAL PRIMARY KEY,
    name TEXT
);

INSERT INTO items (name) VALUES ('Item A'), ('Item B');

BEGIN;
-- Lock first row
UPDATE items SET name = 'Item A updated by T1' WHERE id = 1;

-- Try to lock the row already locked by Session 2
UPDATE items SET name = 'Item B updated by T1' WHERE id = 2;

COMMIT;

-- Q4.Use pg_locks query to monitor active locks.

BEGIN;
-- Lock first row
UPDATE items SET name = 'Item A updated by T1' WHERE id = 1;

SELECT * FROM pg_locks;

END;

-- Q5. Explore about Lock Modes.

BEGIN;

SELECT pg_advisory_lock(123);
	UPDATE items SET name = 'Custom lock' WHERE id = 1;

SELECT pg_advisory_unlock(123);

COMMIT;

-- EXCLUSIVE MODE

BEGIN;

LOCK TABLE items IN EXCLUSIVE MODE;


COMMIT;

-- ACCESS EXCLUSIVE MODE;
BEGIN;

LOCK TABLE items IN ACCESS EXCLUSIVE MODE;


COMMIT;




