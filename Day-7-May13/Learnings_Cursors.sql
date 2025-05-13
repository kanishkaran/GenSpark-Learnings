-- NORMAL CURSORS

DO
$$
DECLARE
	film_record RECORD;
	film_cursor CURSOR FOR
	SELECT * FROM film
	ORDER BY film_id;
BEGIN
	OPEN film_cursor;

	LOOP
		FETCH film_cursor INTO film_record;
		EXIT WHEN NOT FOUND;

		RAISE NOTICE 'Film id: %, Film Name: %, Description: %, Release Year: %',
		film_record.film_id,
		film_record.title,
		film_record.description,
		film_record.release_year;
	END LOOP;
	CLOSE film_cursor;
END;
$$



-- TO INSERT INTO ANOTHER TABLE W/ SOME CALCULATIONS

CREATE TABLE rental_tax_log (
    rental_id INT ,
    customer_name TEXT,
    rental_date TIMESTAMP,
    amount NUMERIC,
    tax NUMERIC
);


DO $$
DECLARE
    rental_record record;
    cur CURSOR FOR
        SELECT r.rental_id, 
               c.first_name || ' ' || c.last_name AS customer_name,
               r.rental_date,
               p.amount
        FROM rental r
        	JOIN payment p 
				ON r.rental_id = p.rental_id
        	JOIN customer c 
				ON r.customer_id = c.customer_id;
BEGIN
    OPEN cur;

    LOOP
        FETCH cur into rental_record;
		EXIT WHEN NOT FOUND;

        INSERT INTO rental_tax_log (rental_id, customer_name, rental_date, amount, tax)
        VALUES (
            rental_record.rental_id,
            rental_record.customer_name,
            rental_record.rental_date,
            rental_record.amount,
            rental_record.amount * 0.10
        );
    END LOOP;

    CLOSE cur;
END;
$$;


SELECT * FROM rental_tax_log



-- CURSOR INSIDE PROCEDURES

CREATE TABLE rental_tax_log_proc (
    rental_id INT ,
    customer_name TEXT,
    rental_date TIMESTAMP,
    amount NUMERIC,
    tax NUMERIC
);

CREATE OR REPLACE PROCEDURE proc_create_log_tbl(tax NUMERIC(10, 2))
AS
$$
DECLARE
    rental_record record;
    cur CURSOR FOR
        SELECT r.rental_id, 
               c.first_name || ' ' || c.last_name AS customer_name,
               r.rental_date,
               p.amount
        FROM rental r
        	JOIN payment p 
				ON r.rental_id = p.rental_id
        	JOIN customer c 
				ON r.customer_id = c.customer_id;
BEGIN
    OPEN cur;

    LOOP
        FETCH cur into rental_record;
		EXIT WHEN NOT FOUND;

        INSERT INTO rental_tax_log_proc (rental_id, customer_name, rental_date, amount, tax)
        VALUES (
            rental_record.rental_id,
            rental_record.customer_name,
            rental_record.rental_date,
            rental_record.amount,
            rental_record.amount * tax
        );
    END LOOP;

    CLOSE cur;
END;
$$
LANGUAGE plpgsql;


CALL proc_create_log_tbl(0.20);

SELECT * FROM rental_tax_log_proc;

