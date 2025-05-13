-- SESSION 2


-- Q1
-- TRANSACTION B
BEGIN;

UPDATE tbl_bank
SET balance = balance + 50
WHERE account_id = 1;		--Writing on same record

-- Waited Until Transaction A from session 1 is commited or Rolled Back

COMMIT;

-- Q2. SELECT .... FOR UPDATE / ROW LOCK

-- TRANSACTION B
BEGIN;

UPDATE tbl_bank
SET balance = balance + 50
WHERE account_id = 1;		--Writing on Locked record

-- Waited Until Transaction A from session 1 is commited or Rolled Back
COMMIT;


-- Q3. Simulate Deadlock

BEGIN;
-- Lock second row
UPDATE items SET name = 'Item B updated by T2' WHERE id = 2;
-- Wait a bit before locking the first row

-- Try to lock the row already locked by Session 1
UPDATE items SET name = 'Item A updated by T2' WHERE id = 1;
ROLLBACK;
--OUTPUT
-- ERROR:  deadlock detected
-- Process 15268 waits for ShareLock on transaction 1139; blocked by process 18680.
-- Process 18680 waits for ShareLock on transaction 1140; blocked by process 15268. 


-- Q5. LOCK MODES

BEGIN;

UPDATE items SET name = 'Session 2 Update' WHERE id = 1;

-- advisory lock is used in session 1
COMMIT;


-- EXCLUSIVE MODE
BEGIN;

UPDATE items SET name = 'exclusive mode test' WHERE id = 1;

-- advisory lock is used in session 1
COMMIT;

-- ACCESS EXCLUSIVE MODE;
BEGIN;

SELECT * FROM items;

-- Had to wait until session 1 commits

COMMIT;


