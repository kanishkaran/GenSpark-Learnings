-- Q1. Write a cursor that loops through all films and prints titles longer than 120 minutes.

DO
$$
DECLARE film_cursor CURSOR
FOR SELECT title, length FROM film;
record RECORD;
BEGIN
	OPEN film_cursor;
	LOOP
		FETCH film_cursor INTO record;
		EXIT WHEN NOT FOUND;
		IF record.length > 120 THEN
		RAISE NOTICE 'Movie Name is % of length % mins' , record.title, record.length;
		END IF;
	END LOOP;
	CLOSE film_cursor;
END
$$;


-- Q2.Create a cursor that iterates through all customers and counts how many rentals each made.


DO $$
DECLARE customer_cursor CURSOR
FOR SELECT customer_id, first_name, last_name FROM customer;
each_customer RECORD;
totalCount INT;
BEGIN
	OPEN customer_cursor;
	LOOP
		FETCH customer_cursor INTO each_customer;
		EXIT WHEN NOT FOUND;
		SELECT COUNT(*) INTO totalCount 
		FROM rental
		WHERE customer_id = each_customer.customer_id;
		RAISE NOTICE 'id: % Name: % Total_Rental: %', each_customer.customer_id, each_customer.first_name || ' ' || each_customer.last_name, totalCount;
	END LOOP;
END;
$$


-- Q3.Using a cursor, update rental rates: Increase rental rate by $1 for films with less than 5 rentals.


DO $$
DECLARE rental_counts CURSOR
FOR SELECT f.film_id, title, COUNT(rental_id) rental_count
FROM film f
JOIN inventory i
	ON f.film_id = i.film_id
JOIN rental r
	ON i.inventory_id = r.inventory_id
GROUP BY f.film_id, title;
rental_record RECORD;
BEGIN
	OPEN rental_counts;
	LOOP
		FETCH rental_counts INTO rental_record;
		EXIT WHEN NOT FOUND;
		IF (rental_record.rental_count < 5) THEN
			UPDATE film SET rental_rate = rental_rate + 1
			WHERE film_id = rental_record.film_id;
		RAISE NOTICE 'Rental Rate for the film % with id % is Updated', rental_record.title, rental_record.film_id;
		END IF;
	END LOOP;
	CLOSE rental_record;
END;
$$

-- To view films with rental count less than 5
WITH cte_rental_counts
AS
(SELECT f.film_id, title, COUNT(rental_id) rental_count
FROM film f
JOIN inventory i
	ON f.film_id = i.film_id
JOIN rental r
	ON i.inventory_id = r.inventory_id
GROUP BY f.film_id, title)

SELECT * from cte_rental_counts WHERE rental_count < 5;


-- Q4.Create a function using a cursor that collects titles of all films from a particular category.


CREATE OR REPLACE FUNCTION get_films_by_category(cat TEXT)
RETURNS VOID 
AS
$$
DECLARE movie_cursor CURSOR
FOR
	SELECT title 
	FROM film f
		JOIN film_category fc
			ON f.film_id = fc.film_id
		JOIN category c
			ON fc.category_id = c.category_id
	WHERE c.name = cat;
movie RECORD;
BEGIN
	OPEN movie_cursor;
	LOOP
		FETCH movie_cursor INTO movie;
		EXIT WHEN NOT FOUND;

		RAISE NOTICE 'Movie % ', movie.title;
	END LOOP;
	CLOSE movie_cursor;
END;
$$
LANGUAGE plpgsql;


SELECT get_films_by_category('Action')

-- Q5.Loop through all stores and count how many distinct films are available in each store using a cursor.

DO $$
DECLARE 
	store_record RECORD;
	film_count INT;
	store_cursor CURSOR FOR
		SELECT store_id FROM store;
BEGIN
	OPEN store_cursor;

	LOOP 
		FETCH store_cursor INTO store_record;
		EXIT WHEN NOT FOUND;

		SELECT COUNT(DISTINCT i.film_id) INTO film_count
		FROM inventory i
		WHERE i.store_id = store_record.store_id;

		RAISE NOTICE 'Store id: % films: %', store_record.store_id, film_count;
	END LOOP;

	CLOSE store_cursor;
END;
$$


-- Triggers

-- Q1.Write a trigger that logs whenever a new customer is inserted.

CREATE TABLE customer_logs(
c_id INT PRIMARY KEY,
name TEXT)


CREATE FUNCTION log_customer()
RETURNS TRIGGER
AS
$$
BEGIN
	INSERT INTO customer_logs(c_id, name)
	VALUES(NEW.customer_id, NEW.first_name|| ' ' || NEW.last_name);

	RETURN NEW;
END;
$$
LANGUAGE plpgsql;


CREATE TRIGGER trg_log_customer_insert
AFTER INSERT ON customer
FOR EACH ROW 
EXECUTE FUNCTION log_customer();

select * from customer;

INSERT INTO customer(store_id, first_name, last_name, email, address_id, create_date, active)
VALUES(1, 'Aurthur', 'Morgan', 'am@example.com', 2, CURRENT_DATE, 1)

SELECT * FROM customer_logs


-- Q2. Create a trigger that prevents inserting a payment of amount 0.

CREATE FUNCTION prevent_zero()
RETURNS TRIGGER
AS
$$
BEGIN
	IF NEW.amount = 0 THEN
		RAISE EXCEPTION 'Payment cannot be zero';
	END IF;
END;
$$
LANGUAGE plpgsql;

CREATE TRIGGER trg_prevent_zero_payment
BEFORE INSERT ON payment
FOR EACH ROW
EXECUTE FUNCTION prevent_zero();


INSERT INTO payment(customer_id, staff_id, rental_id, amount, payment_date)
VALUES(600, 1, 1, 0, CURRENT_DATE)


-- Q3.Set up a trigger to automatically set last_update on the film table before update.

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

UPDATE film SET rental_rate = 5 WHERE film_id = 55;
SELECT * FROM film WHERE film_id = 55;

-- Q4.Create a trigger to log changes in the inventory table (insert/delete).

CREATE TABLE inventory_log(
id INT PRIMARY KEY,
film_id INT,
store_id INT,
last_update DATE
)

CREATE OR REPLACE FUNCTION log_inventory_changes()
RETURNS TRIGGER
AS
$$
BEGIN
	IF TG_OP = 'INSERT' THEN
		INSERT INTO inventory_log(id, film_id, store_id, last_update)
		VALUES(NEW.inventory_id, NEW.film_id, NEW.store_id, NEW.last_update);
		RETURN NEW;
	ELSIF TG_OP = 'DELETE' THEN
		INSERT INTO inventory_log(id, film_id, store_id, last_update)
		VALUES(old.inventory_id, old.film_id, old.store_id, old.last_update);
		RETURN NEW;
	END IF;
END;
$$
LANGUAGE 'plpgsql';


CREATE OR REPLACE TRIGGER trg_log_change_on_inventory
BEFORE INSERT OR DELETE ON inventory
FOR EACH ROW
EXECUTE FUNCTION log_inventory_changes()

DELETE FROM inventory WHERE inventory_id = 4581;


INSERT INTO inventory( film_id, store_id, last_update)
VALUES(28, 2, CURRENT_DATE);
SELECT * FROM inventory_log

-- Q5.Write a trigger that ensures a rental can’t be made for a customer who owes more than $50.

CREATE OR REPLACE FUNCTION check_customer_balance()
RETURNS TRIGGER AS $$
DECLARE
    unpaid_balance NUMERIC := 0;
BEGIN
    -- Calculate the total rental cost for rentals that have no payment
    SELECT COALESCE(SUM(f.rental_rate), 0) INTO unpaid_balance
    FROM rental r
    JOIN inventory i ON r.inventory_id = i.inventory_id
    JOIN film f ON i.film_id = f.film_id
    LEFT JOIN payment p ON r.rental_id = p.rental_id
    WHERE r.customer_id = NEW.customer_id
      AND p.payment_id IS NULL;

    IF unpaid_balance >= 50 THEN
        RAISE EXCEPTION 'Customer % has unpaid rentals of $%. Cannot rent more.', NEW.customer_id, unpaid_balance;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER block_high_balance_rental
BEFORE INSERT ON rental
FOR EACH ROW
EXECUTE FUNCTION check_customer_balance();



-- TRANSACTIONS

-- Q1.Write a transaction that inserts a customer and an initial rental in one atomic operation.

START TRANSACTION;


WITH new_customer AS (
    INSERT INTO customer (
        store_id, first_name, last_name, email, address_id,
        activebool, create_date, last_update, active
    )
    VALUES (
        1, 'John', 'Berger', 'johnB@example.com', 5,
        true, NOW(), NOW(), 1
    )
    RETURNING customer_id
)
INSERT INTO rental (
    rental_date, inventory_id, customer_id, staff_id, last_update
)
SELECT
    NOW(), 100, customer_id, 2, NOW()
FROM new_customer;

COMMIT;

-- Q2.Simulate a failure in a multi-step transaction (update film + insert into inventory) and roll back.

DO $$
BEGIN
    START TRANSACTION;
	UPDATE film SET rental_rate = rental_rate + 1 WHERE film_id = 10;
 
    INSERT INTO inventory (film_id, store_id, last_update)
    VALUES (99999, 1, NOW());

	-- INSERT INTO inventory (film_id, store_id, last_update)    - valid entry
	-- VALUES (10, 1, NOW());
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        ROLLBACK;
        RAISE NOTICE 'Transaction failed and rolled back';
END;
$$ LANGUAGE plpgsql;


-- Q3.Create a transaction that transfers an inventory item from one store to another.



START TRANSACTION;

UPDATE inventory
SET store_id = 2,
    last_update = NOW()
WHERE inventory_id = 100;

COMMIT;

select * from inventory WHERE inventory_id = 100;


-- Q4. Demonstrate SAVEPOINT and ROLLBACK TO SAVEPOINT by updating payment amounts, then undoing one.

--BEFORE
select amount from payment where payment_id = 17503; -- 7.99
select amount from payment where payment_id = 17504; -- 1.99

START TRANSACTION;
	UPDATE payment SET amount = amount + 1 WHERE payment_id = 17503;
	
	SAVEPOINT after_first_update;
	
	-- Second update
	UPDATE payment SET amount = amount + 1 WHERE payment_id = 17504;
	
	-- Now decide to undo second update
	ROLLBACK TO SAVEPOINT after_first_update;
COMMIT;

--AFTER
select amount from payment where payment_id = 17503; -- 8.99
select amount from payment where payment_id = 17504; -- 1.99

-- Q5.Write a transaction that deletes a customer and all associated rentals and payments, ensuring atomicity.

START TRANSACTION;

-- Delete payments
DELETE FROM payment WHERE customer_id = 10;

-- Delete rentals
DELETE FROM rental WHERE customer_id = 10;

-- Delete customer
DELETE FROM customer WHERE customer_id = 10;

COMMIT;

SELECT * FROM payment WHERE customer_id = 10; -- none displayed
SELECT * FROM rental WHERE customer_id = 10; -- none displayed
SELECT * FROM customer WHERE customer_id = 10; -- none displayed
