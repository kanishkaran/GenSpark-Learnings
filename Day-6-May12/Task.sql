-- 12 May 2025: Transactions and Concurrency

-- 1.Question:
-- In a transaction, if I perform multiple updates and an error happens in the third statement, but I have not used SAVEPOINT, what will happen if I issue a ROLLBACK?
-- Will my first two updates persist?

	No, The first 2 updates will not persist and the table goes to last commited state/ you can say original state before transaction.

-- 2.Question:
-- Suppose Transaction A updates Alice’s balance but does not commit. Can Transaction B read the new balance if the isolation level is set to READ COMMITTED?

	No, Transaction B cant read the new-updated data or new balance under READ COMMITED isolation Level. It only allows commited data to be read

-- 3.Question:
-- What will happen if two concurrent transactions both execute:
-- UPDATE tbl_bank_accounts SET balance = balance - 100 WHERE account_name = 'Alice';
-- at the same time? Will one overwrite the other?

	It depends on the isolation Level

	If there is low isolation level like READ UNCOMMITED 
		- Then the value would be overwritten, which leads to `LOST UPDATES`

	Else if the isolation Levels are higher
		- Then Transaction B must wait until Transaction A gets commited
		- Thus Only Commited/ New value is READ by Transaction B


-- 4.Question:
-- If I issue ROLLBACK TO SAVEPOINT after_alice;, will it only undo changes made after the savepoint or everything?
	Yes, It will undo the only changes made after the savepoint. [not everything]

	Additionally, If there are any other updates after the "ROLLBACK TO SAVEPOINT after_alice;"
	Those changes will be reflected too,if commited.

-- 5.Question:
-- Which isolation level in PostgreSQL prevents phantom reads?

	Only Serializable Isolation Level Prevents the phantom reads, As it does not allow update (Here inserts) which raises conflicts

-- 6.Question:
-- Can Postgres perform a dirty read (reading uncommitted data from another transaction)?

	No, Postgres cannot perform any dirty read.
	by default, READ COMMITED is the isolation Level in postgre.
	

-- 7.Question:
-- If autocommit is ON (default in Postgres), and I execute an UPDATE, is it safe to assume the change is immediately committed?
	Yes. I can be called auto-commit or Implicit Commits.
	If the update is NOT contained within a Transaction Block, auto commit is on. Thus changes are immediately commited.

--  8.Question:
-- If I do this:

-- BEGIN;
-- UPDATE accounts SET balance = balance - 500 WHERE id = 1;
-- -- (No COMMIT yet)
-- And from another session, I run:

-- SELECT balance FROM accounts WHERE id = 1;
-- Will the second session see the deducted balance?

	In Postgre, Due to MVCC or Multi-Version Concurrency Control, Only the last Commited data will be fetched. 	--This enables Multi-user read/write without conflict.
	

	Thus there will be NO deduction of balance

