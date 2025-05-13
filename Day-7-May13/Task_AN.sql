--CURSORS


-- Q1.Write a cursor to list all customers and how many rentals each made. Insert these into a summary table.
CREATE TABLE customer_rental_summary(
id INT,
name TEXT,
total_rental NUMERIC
)

DO
$$
DECLARE 
	rec RECORD;
	summary_cur CURSOR FOR
	SELECT c.customer_id, CONCAT(first_name, ' ', last_name) AS Name, COUNT(rental_id) AS Total_Rentals
	FROM customer c
	JOIN rental r
		ON c.customer_id = r.customer_id
	GROUP BY c.customer_id, c.first_name, c.last_name
	ORDER BY 3 DESC;
BEGIN
	OPEN summary_cur;
	
	LOOP
		FETCH summary_cur INTO rec;
		EXIT WHEN NOT FOUND;

		INSERT INTO customer_rental_summary VALUES(
		rec.customer_id,
		rec.name,
		rec.total_rentals
		);
	END LOOP;
	CLOSE summary_cur;
END;
$$

SELECT * FROM customer_rental_summary;

-- Q2.Using a cursor, print the titles of films in the 'Comedy' category rented more than 10 times.


DO $$
DECLARE
	rec RECORD;
	film_cursor CURSOR FOR
		SELECT title AS film_name, name AS category, COUNT(rental_id) rental_count
		FROM film f
		JOIN film_category fc
			ON f.film_id = fc.film_id
		JOIN category c
			ON fc.category_id = c.category_id
		JOIN inventory i
			 ON f.film_id = i.film_id
		JOIN rental r
			ON i.inventory_id = r.inventory_id
		WHERE c.name = 'Comedy'
			GROUP BY f.title, name;
BEGIN
	OPEN film_cursor;

	LOOP
		FETCH film_cursor INTO rec;
		EXIT WHEN NOT FOUND;

		IF rec.rental_count > 10 THEN
			RAISE NOTICE 'Film Name: %  has rental count of %', rec.film_name, rec.rental_count;
		END IF;
	END LOOP;
	CLOSE film_cursor;
END;
$$


-- Q3.Create a cursor to go through each store and count the number of distinct films available, and insert results into a report table.


CREATE TABLE tbl_report(
store_id INT,
film_count INT
);
	

DO $$
DECLARE 
	rec RECORD;
	st_id INT;
	film_counts INT;
	cur CURSOR FOR
	SELECT * FROM store;
BEGIN
	OPEN cur;
	
	LOOP
		FETCH cur INTO rec;
		EXIT WHEN NOT FOUND;
		
		SELECT COUNT(DISTINCT title) INTO Film_Counts
		FROM film f
		JOIN inventory i
			ON f.film_id = i.film_id
		WHERE i.store_id = rec.store_id;
	
		INSERT INTO tbl_report VALUES(rec.store_id, film_counts);

	END LOOP;
	CLOSE cur;
END;
$$

SELECT * FROM tbl_report;


-- Q4.Loop through all customers who haven't rented in the last 6 months and insert their details into an inactive_customers table.

-- LATEST ENTRY OF LAST_UPDATE '2006-02-23 09:12:08'
-- ASSUMING IT AS TODAY/ CURRENT DATE

-- OLDEST RENTAL "2005-05-24 22:53:30"
-- EARLIEST RENTAL "2006-02-14 15:16:03"

CREATE TABLE tbl_inactive_customers(
id INT,
fname TEXT,
lname TEXT
);


DO $$
DECLARE 
	rec RECORD;
	cur CURSOR FOR
        SELECT c.customer_id, c.first_name, c.last_name, MAX(r.rental_date) AS last_rental
        FROM customer c
        LEFT JOIN rental r ON c.customer_id = r.customer_id
        GROUP BY c.customer_id, c.first_name, c.last_name;
BEGIN
	OPEN cur;

	LOOP
		FETCH cur INTO rec;
		EXIT WHEN NOT FOUND;

		IF rec.last_rental IS NULL OR rec.last_rental < TIMESTAMP '2006-02-23 09:12:08' - INTERVAL '6 months' THEN
			INSERT INTO tbl_inactive_customers  VALUES(rec.customer_id, rec.first_name, rec.last_name);
		END IF;
	END LOOP;
	CLOSE cur;
END;
$$

SELECT * FROM tbl_inactive_customers;




-- TRANSACTIONS

-- Q1.Write a transaction that inserts a new customer, adds their rental, and logs the payment – all atomically.



BEGIN;

WITH cte_cust
	AS (INSERT INTO customer(store_id, first_name, last_name, email, address_id, active, create_date)
	VALUES (1, 'Kevin', 'Anderson', 'ka@example.com', 2, 1, NOW())
	RETURNING customer_id),
cte_rental AS (
INSERT INTO rental(rental_date, inventory_id, customer_id, staff_id)
SELECT
        CURRENT_TIMESTAMP,
        1,                     
        customer_id,
        1                      
    FROM cte_cust
	RETURNING rental_id, customer_id)

INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
SELECT
    customer_id,
    1,                      
    rental_id,
    4.99,
    CURRENT_TIMESTAMP
FROM cte_rental;


COMMIT;

select * from customer;
select * from rental;
select * from payment;


--  Q2.Simulate a transaction where one update fails (e.g., invalid rental ID), and ensure the entire transaction rolls back.

DO $$
BEGIN
START TRANSACTION;

	UPDATE rental SET inventory_id = 1
	WHERE rental_id = 99999;

	COMMIT;

EXCEPTION
	WHEN OTHERS THEN
		ROLLBACK;
		RAISE NOTICE 'Transaction failed and rolled back';

END
$$

-- Q3.Use SAVEPOINT to update multiple payment amounts. Roll back only one payment update using ROLLBACK TO SAVEPOINT.

START TRANSACTION;

	UPDATE payment SET amount = amount + 1		--1.99
	WHERE payment_id = 17504;
	SAVEPOINT s1;
		
	UPDATE payment SET amount = amount + 1		-- 7.99
	WHERE payment_id = 17505;
	SAVEPOINT s2;
	
	UPDATE payment SET amount = amount + 1		-- 2.99
	WHERE payment_id = 17506;
	SAVEPOINT s3;

	ROLLBACK TO SAVEPOINT s1;

COMMIT;

SELECT amount FROM payment
WHERE payment_id IN (17504, 17505, 17506); -- O/P: 2.99, 7.99, 2.99

-- Q4.Perform a transaction that transfers inventory from one store to another (delete + insert) safely.


BEGIN;

  DELETE FROM inventory
  WHERE inventory_id = 4584;

  INSERT INTO inventory(film_id, store_id, last_update)
  VALUES(1, 2, NOW());

COMMIT;

SELECT * FROM inventory
ORDER BY last_update DESC;


--  Q5.Create a transaction that deletes a customer and all associated records (rental, payment), ensuring referential integrity.	

START TRANSACTION;

-- Delete payments
DELETE FROM payment WHERE customer_id = 11;

-- Delete rentals
DELETE FROM rental WHERE customer_id = 11;

-- Delete customer
DELETE FROM customer WHERE customer_id = 11;

COMMIT;

SELECT * FROM payment WHERE customer_id = 11; -- none displayed
SELECT * FROM rental WHERE customer_id = 11; -- none displayed
SELECT * FROM customer WHERE customer_id = 11; -- none displayed


-- TRIGGERS

-- Q1.Create a trigger to prevent inserting payments of zero or negative amount.	
CREATE OR REPLACE FUNCTION prevent_zero()
RETURNS TRIGGER
AS
$$
BEGIN
	IF NEW.amount <= 0 THEN
		RAISE EXCEPTION 'Payment cannot be zero or negative';
	END IF;
	RETURN NEW;
END;
$$
LANGUAGE plpgsql;

CREATE TRIGGER trg_prevent_zero_payment
BEFORE INSERT ON payment
FOR EACH ROW
EXECUTE FUNCTION prevent_zero();


INSERT INTO payment(customer_id, staff_id, rental_id, amount, payment_date)
VALUES(601, 1, 1, 10, CURRENT_DATE)

INSERT INTO payment(customer_id, staff_id, rental_id, amount, payment_date)
VALUES(600, 1, 1, -1, CURRENT_DATE)

SELECT * FROM payment
ORDER BY payment_date DESC;


-- Q2.Set up a trigger that automatically updates last_update on the film table when the title or rental rate is changed.

CREATE FUNCTION update_last_update()
RETURNS TRIGGER
AS
$$
BEGIN
	NEW.last_update:= NOW();
	RETURN NEW;
END;
$$
LANGUAGE plpgsql;

CREATE TRIGGER trg_update_last_update
BEFORE UPDATE ON film
FOR EACH ROW
EXECUTE FUNCTION update_last_update();

UPDATE film SET rental_rate = 5 WHERE film_id = 98;
SELECT * FROM film WHERE film_id = 98;


-- Q3.Write a trigger that inserts a log into rental_log whenever a film is rented more than 3 times in a week.

CREATE TABLE rental_log (
    id SERIAL PRIMARY KEY,
    film_id INT,
    log_time TIMESTAMP DEFAULT now(),
    message TEXT
);


CREATE OR REPLACE FUNCTION log_high_rentals()
RETURNS TRIGGER AS $$
DECLARE
    v_film_id INT;
    v_rental_count INT;
BEGIN

    SELECT film_id INTO v_film_id
    FROM inventory
    WHERE inventory_id = NEW.inventory_id;

    SELECT COUNT(*) INTO v_rental_count
    FROM rental r
    JOIN inventory i ON r.inventory_id = i.inventory_id
    WHERE i.film_id = v_film_id
      AND r.rental_date >= (NOW() - INTERVAL '7 days');


    IF v_rental_count > 3 THEN
        INSERT INTO rental_log (film_id, message)
        VALUES (
            v_film_id,
            'Film ID ' || v_film_id || ' rented more than 3 times in the last week.'
        );
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;


CREATE TRIGGER trg_log_high_rentals
AFTER INSERT ON rental
FOR EACH ROW
EXECUTE FUNCTION log_high_rentals();


INSERT INTO rental(rental_date, inventory_id, customer_id, staff_id)
VALUES(NOW(), 2, 1, 1);

INSERT INTO rental(rental_date, inventory_id, customer_id, staff_id)
VALUES(NOW(), 2, 2, 1);

INSERT INTO rental(rental_date, inventory_id, customer_id, staff_id)
VALUES(NOW(), 2, 1, 1);

SELECT * FROM rental_log;

