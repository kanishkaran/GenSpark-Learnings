CREATE TABLE tbl_bank
(
account_id SERIAL CONSTRAINT pk_accId PRIMARY KEY,
account_name VARCHAR(40),
balance NUMERIC(10, 2)
);	


INSERT INTO tbl_bank
(account_name, balance)
VALUES
('Mark', 400.00),
('Cuban', 300.00);

SELECT * FROM tbl_bank;


-- Transaction - Either All are Successful and gets commited or fails and roll backed

-- BEFORE
-- Mark - 400
-- Cuban - 300
BEGIN;  -- or START TRANSACTION

UPDATE tbl_bank 
SET balance = 300
WHERE account_name = 'Mark';  -- Assuming Distinct Names


UPDATE tbl_bank
SET balance = 400
WHERE account_name = 'Cuban';

COMMIT;

SELECT * FROM tbl_bank;

-- AFTER
-- Mark - 300
-- Cuban - 400

-- ROLL BACK
START TRANSACTION;

UPDATE tbl_bank 
SET balance = 300
WHERE account_name = 'Mark';  -- Assuming Distinct Names


UPDATE tbl_bank
SET balance = 400
WHERE account_name = 'Cuban';

ROLLBACK;  -- Above Changes are undone
END; 		

-- SAVEPOINT
START TRANSACTION;

UPDATE tbl_bank
SET account_name = 'Mark Jones'
WHERE account_id = 1;

SAVEPOINT s1;

UPDATE tbl_bank
SET account_name = 'Cuban'
WHERE account_id = 2;

ROLLBACK TO s1;
END;
SELECT * FROM tbl_bank;

/*  O/P
	2	"Cuban"			400.00
	1	"Mark Jones"	300.00 
*/

-- ROLLBACKS OUTSIDE TRANSACTION

START TRANSACTION;

UPDATE tbl_bank
SET account_name = 'Cuban Zane'
WHERE account_id = 2;

SAVEPOINT s1;

UPDATE tbl_bank
SET account_name = 'Mark Jones'
WHERE account_id = 1;

COMMIT;


ROLLBACK TO s1;            -- CANT ROLLBACK OUTSIDE TRANSACTION:- ERROR:  ROLLBACK TO SAVEPOINT can only be used in transaction blocks 
SELECT * FROM tbl_bank;



-- IFs and ELSE
DO $$
DECLARE
  current_balance NUMERIC;
BEGIN
SELECT balance INTO current_balance
FROM tbl_bank
WHERE account_name = 'Mark Jones';

IF current_balance >= 300 THEN
   UPDATE tbl_bank SET balance = balance - 50 WHERE account_name = 'Mark Jones';
   UPDATE tbl_bank SET balance = balance + 50 WHERE account_name = 'Cuban Zane';
ELSE
   RAISE NOTICE 'Insufficient Funds!';
   ROLLBACK;                       -- ROLLBACK IS NOT NECESSARY HERE 
END IF;
END;
$$;




-- CONCURRENCY CONTROL

/* 1. DIRTY READ:
	When some Transaction B reads Data from Tran A, but transaction A is uncommited.
	If transaction A rolls back the value. The data read by transaction B becomes dirty
	Hence dirty read

	Dirty Read is not possible in postgre.

	2. LOST UPDATE:
	When Two Transactions Take in same value / data and updates it. This overrides the update of other transaction
	on commits.

	Lost Update is not Possible in plsql
*/


START TRANSACTION;

UPDATE tbl_bank
SET account_name = 'Cuban Zane'
WHERE account_id = 2;

COMMIT;


/*

Solutions to Avoid Lost Updates:

1. Pessimistic Locking (Explicit Locks)
	Lock the record when someone reads it, so no one else can read or write until the lock is released.
	Example: SELECT ... FOR UPDATE in SQL.
	Prevents concurrency but can reduce performance due to blocking.
	
2. Optimistic Locking (Versioning)
	Common and scalable solution.
	Each record has a version number or timestamp.
	When updating, you check that the version hasn’t changed since you read it.
	If it changed, you reject the update (user must retry).
	Example:
	UPDATE products
	SET price = 100, version = version + 1
	WHERE id = 1 AND version = 3; --3
	
3. Serializable Isolation Level
	In database transactions, using the highest isolation level (SERIALIZABLE) can prevent lost updates.
	But it's heavier and can cause performance issues (due to more locks or transaction retries).
	
Which Solution is Best?
	For web apps and APIs: Optimistic locking is often the best balance (fast reads + safe writes).
	For critical financial systems: Pessimistic locking may be safer.
*/



-- 2. Isolation Levels :
--    1. READ UNCOMMITTED -> PSQL not supported
--    2. READ COMMITTED -> 
--    3. Repeatable Read -> Ensures repeatabe reads
--    4. Serializable -> Full isolation (safe but slow, no dirty reads, no lost updates, no repeatable reads, no phantom reads)


-- PLPGSQL is limited to SET TRANSACTION ISOLATION LEVEL - Serializable and Read Uncommited (By Default)


-- Non-Repeatable Read
Transaction A reads a row, -- 100
Transaction B updates and commits the row, then --90
Transaction A reads the row again and sees different data.


-- Phatom Read
-- SELECT * FROM orders WHERE amount > 1000; returns 5 rows.
-- Another transaction inserts a new order with amount 1200 and commits — now re-running the
-- query returns 6 rows.

BEGIN;
	SELECT * FROM tbl_bank
	WHERE account_id > 0;

END;



-- PostgreSQL handles concurrency using:
	MVCC (Multi-Version Concurrency Control):
		MVCC allows multiple versions of the same data (rows) to exist temporarily,
		so readers and writers don't block each other.

Readers don't block writers and Writers don't block readers.