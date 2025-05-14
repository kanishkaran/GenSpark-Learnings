CREATE TABLE rental_log (
    log_id SERIAL PRIMARY KEY,
    rental_time TIMESTAMP,
    customer_id INT,
    film_id INT,
    amount NUMERIC,
    logged_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);


CREATE OR REPLACE PROCEDURE sp_add_rental_log(
    p_customer_id INT,
    p_film_id INT,
    p_amount NUMERIC
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO rental_log (rental_time, customer_id, film_id, amount)
    VALUES (CURRENT_TIMESTAMP, p_customer_id, p_film_id, p_amount);
EXCEPTION WHEN OTHERS THEN
    RAISE NOTICE 'Error occurred: %', SQLERRM;
END;
$$;


CALL sp_add_rental_log(1, 100, 4.99);

SELECT * FROM rental_log;


CREATE TABLE rental_log_updates(
old_value TEXT,
new_value TEXT,
update_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP
)




CREATE OR REPLACE FUNCTION update_log_changes()
RETURNS TRIGGER
AS $$
BEGIN
	
	INSERT INTO rental_log_updates(old_value, new_value)
	VALUES(OLD, NEW);
	RETURN NEW;

END;
$$
LANGUAGE plpgsql;


CREATE TRIGGER trg_update_log_changes
AFTER UPDATE ON rental_log
FOR EACH ROW
EXECUTE FUNCTION update_log_changes();


UPDATE rental_log SET film_id = 101 WHERE log_id = 1;

SELECT * FROM rental_log_updates;	