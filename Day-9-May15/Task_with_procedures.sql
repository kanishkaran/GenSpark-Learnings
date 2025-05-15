
--  Q1. Create a stored procedure to encrypt a given text

CREATE OR REPLACE PROCEDURE proc_encrypt_text(p_data TEXT, p_key TEXT, OUT hash BYTEA)
AS $$
BEGIN
	SELECT pgp_sym_encrypt(p_data, p_key) INTO hash;
END;
$$
LANGUAGE plpgsql;


DO $$
DECLARE 
	hashed_content BYTEA;
BEGIN
	CALL proc_encrypt_text('message', 'key', hashed_content);
	RAISE NOTICE '%', hashed_content;
END;
$$




-- Q3. Create a stored procedure to partially mask a given text

CREATE OR REPLACE PROCEDURE proc_mask_text(p_data TEXT, OUT p_masked_content TEXT)
AS $$
BEGIN
	IF LENGTH(p_data) < 2 THEN
		RAISE EXCEPTION 'The Text Must be atleast 3 Characters';
	ELSIF LENGTH(p_data) <= 4 THEN
		SELECT (LEFT(p_data, 1) || REPEAT('*', LENGTH(p_data) - 2) || RIGHT(p_data, 1)) INTO p_masked_content;
	ELSE
		SELECT (LEFT(p_data, 2) || REPEAT('*', LENGTH(p_data) - 4) || RIGHT(p_data, 2)) INTO p_masked_content;
	END IF;
END;
$$
LANGUAGE plpgsql;

DO $$
DECLARE
	masked_content TEXT;
BEGIN
	CALL proc_mask_text('mark', masked_content);		-- Even 3 or 4 letter words can be masked
	RAISE NOTICE '%', masked_content;
END;
$$


-- Q4. Create a procedure to insert into customer with encrypted email and masked name 

CREATE OR REPLACE PROCEDURE proc_insert_customer_proc(p_store_id INT, p_first_name TEXT, p_last_name TEXT, p_email TEXT, p_address_id INT, p_key TEXT)
AS $$
DECLARE
	masked_name TEXT;
	encrypted_email BYTEA;
BEGIN
	CALL proc_mask_text(p_first_name, masked_name);	
	CALL  proc_encrypt_text(p_email, LEFT(p_first_name, 2) || RIGHT(p_last_name, 2), encrypted_email);
	INSERT INTO customers_without_keys(store_id, first_name, last_name, email, address_id)
	VALUES(p_store_id, masked_name, p_last_name, encrypted_email, p_address_id);
END;
$$
LANGUAGE plpgsql;


CALL proc_insert_customer_without_key(2, 'Harley', 'Bob', 'bob@gmail.com', 13);
CALL proc_insert_customer_without_key(1, 'Thompson', 'Mark', 'tom@gmail.com', 14);

SELECT * FROM customers_without_keys;


