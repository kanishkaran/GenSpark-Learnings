CREATE EXTENSION pgcrypto;

-- THE STORED PROCEDURE VERSION OF QUESTION 1, 3, 4 IS IN Task_with_procedures FILE. DO CHECKOUT THAT :)

--  Q1. Create a stored procedure to encrypt a given text - FUNCTION
CREATE OR REPLACE FUNCTION encrypt_text(p_data TEXT, p_key TEXT)
RETURNS BYTEA
AS $$
BEGIN
	RETURN pgp_sym_encrypt(p_data, p_key);
END;
$$
LANGUAGE plpgsql;

SELECT encrypt_text('password', 'sha');


-- Q2. Create a stored procedure to compare two encrypted texts
CREATE OR REPLACE PROCEDURE proc_compare_encrypted(p_hash_1 BYTEA, p_hash_2 BYTEA, p_key TEXT)
AS $$
DECLARE 
	decrypt_1 TEXT;
	decrypt_2 TEXT;
BEGIN
	SELECT pgp_sym_decrypt(p_hash_1, p_key) INTO decrypt_1;
	SELECT pgp_sym_decrypt(p_hash_2, p_key) INTO decrypt_2;

	IF decrypt_1 = decrypt_2 THEN
		RAISE NOTICE 'They Match';
	ELSE
		RAISE NOTICE 'They Do not Match';
	END IF;
END;
$$
LANGUAGE plpgsql;


-- Q3. Create a stored procedure to partially mask a given text - FUNCTION

CREATE OR REPLACE FUNCTION mask_text(p_data TEXT)
RETURNS TEXT
AS $$
BEGIN
	IF LENGTH(p_data) < 2 THEN
		RAISE EXCEPTION 'The Text must be atleast 3 Characters :/';
	ELSIF LENGTH(p_data) <= 4 THEN
		RETURN LEFT(p_data, 1) || REPEAT('*', LENGTH(p_data) - 2) || RIGHT(p_data, 1);
	ELSE
		RETURN LEFT(p_data, 2) || REPEAT('*', LENGTH(p_data) - 4) || RIGHT(p_data, 2);
	END IF;
END;
$$
LANGUAGE plpgsql;


SELECT mask_text('person@example.com')					-- Even 3 or 4 letter words can be masked
SELECT mask_text('Mark')
SELECT mask_text('Bob')



-- Q4. Create a procedure to insert into customer with encrypted email and masked name 


CREATE TABLE customers(
customer_id SERIAL PRIMARY KEY,
store_id INT,
first_name TEXT,
last_name TEXT,
email BYTEA,											-- Directly Storing it as binary data
address_id INT,
FOREIGN KEY (store_id) REFERENCES store(store_id),		-- Used Store Table & Address Table From Dvd Rental Database :)
FOREIGN KEY (address_id) REFERENCES address(address_id)
);

DROP TABLE customers

CREATE OR REPLACE PROCEDURE proc_insert_customer(p_store_id INT, p_first_name TEXT, p_last_name TEXT, p_email TEXT, p_address_id INT, p_key TEXT)
AS $$
BEGIN
	INSERT INTO customers(store_id, first_name, last_name, email, address_id)
	VALUES(p_store_id, mask_text(p_first_name), p_last_name, encrypt_text(p_email, p_key), p_address_id);		-- This line would Change for accessing the stored procedure version of the mask_text and encryt_text
END;
$$
LANGUAGE plpgsql;

CALL proc_insert_customer(1, 'Emmanuel', 'Brown', 'ebrown@gmail.com', 10, 'password');
CALL proc_insert_customer(2, 'Micheal', 'Cane', 'micane@gmail.com', 11, 'password');
CALL proc_insert_customer(1, 'Andreson', 'Green', 'andy@gmail.com', 12, 'password');
CALL proc_insert_customer(2, 'Thompson', 'Harley', 'TommyH@gmail.com', 13, 'password');
CALL proc_insert_customer(1, 'Marcus', 'Bob', 'mb@gmail.com', 14, 'password');
CALL proc_insert_customer(5, 'Marcus', 'Bob', 'mb@gmail.com', 14, 'password');			-- Store Id fkey Violation :(
CALL proc_insert_customer(1, 'Marcus', 'Bob', 'mb@gmail.com', 99999, 'password');			-- Address Id fkey Violation :(

SELECT * FROM customers;

-- Dynamic Keys w/ Names - Using the first 2 letters and last 2 letters of firstname and lastname as key.

CREATE TABLE customers_without_keys(
customer_id SERIAL PRIMARY KEY,
store_id INT,
first_name TEXT,
last_name TEXT,
email BYTEA,											-- Directly Storing it as binary data
address_id INT,
FOREIGN KEY (store_id) REFERENCES store(store_id),		-- Used Store Table & Address Table From Dvd Rental Database :)
FOREIGN KEY (address_id) REFERENCES address(address_id)
);

CREATE OR REPLACE PROCEDURE proc_insert_customer_without_key(p_store_id INT, p_first_name TEXT, p_last_name TEXT, p_email TEXT, p_address_id INT)
AS $$
BEGIN
	INSERT INTO customers_without_keys(store_id, first_name, last_name, email, address_id)
	VALUES(p_store_id, mask_text(p_first_name), p_last_name, encrypt_text(p_email, LEFT(p_first_name, 2) || RIGHT(p_last_name, 2)), p_address_id);  -- Using the first 2 letters and last 2 letters of firstname and lastname.
END;
$$
LANGUAGE plpgsql;

CALL proc_insert_customer_without_key(2, 'Micheal', 'Cane', 'micane@gmail.com', 11);
CALL proc_insert_customer_without_key(1, 'Andreson', 'Green', 'andy@gmail.com', 12);
CALL proc_insert_customer_without_key(2, 'Thompson', 'Harley', 'TommyH@gmail.com', 13);
CALL proc_insert_customer_without_key(1, 'Marcus', 'Bob', 'mb@gmail.com', 14);

SELECT * FROM customers_without_keys;

-- Q5. Create a procedure to fetch and display masked first_name and decrypted email for all customers


CREATE OR REPLACE PROCEDURE proc_read_customer_masked()
AS $$
DECLARE
	customer_cursor CURSOR FOR
	SELECT * FROM customers;
	rec RECORD;
BEGIN
	OPEN customer_cursor;

	LOOP
		FETCH customer_cursor INTO rec;
		EXIT WHEN NOT FOUND;

		RAISE NOTICE 'Customer Id: %, Masked First Name: %, Email: % ', rec.customer_id, rec.first_name, pgp_sym_decrypt(rec.email, 'password');
	END LOOP;
	CLOSE customer_cursor;
END;
$$
LANGUAGE plpgsql;

CALL proc_read_customer_masked();


-- For Second Table - Customers Without Keys

CREATE OR REPLACE PROCEDURE proc_read_customer_masked_keyless()
AS $$
DECLARE
	customer_cursor CURSOR FOR
	SELECT * FROM customers_without_keys;
	rec RECORD;
BEGIN
	OPEN customer_cursor;

	LOOP
		FETCH customer_cursor INTO rec;
		EXIT WHEN NOT FOUND;

		RAISE NOTICE 'Customer Id: %, Masked First Name: %, Email: % ', rec.customer_id, rec.first_name, pgp_sym_decrypt(rec.email, LEFT(rec.first_name, 2) || RIGHT(rec.last_name, 2));
	END LOOP;
	CLOSE customer_cursor;
END;
$$
LANGUAGE plpgsql;

CALL proc_read_customer_masked_keyless();









	
	
	