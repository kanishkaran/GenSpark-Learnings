-- TRANSACTIONS USING PROCEDURE


CREATE OR REPLACE PROCEDURE proc_create_customer_rental(
p_first_name text,p_last_name text, p_email text,p_address_id int, 
p_inventory_id int, p_store_is int,
p_staff_id int,p_amount numeric
)
AS $$
DECLARE
	v_cust_id INT;
	v_rental_id INT;
BEGIN
	BEGIN
		INSERT INTO customer (store_id, first_name, last_name, email, address_id, active, create_date)
    	VALUES (p_store_is, p_first_name, p_last_name, p_email, p_address_id, 1, CURRENT_DATE)
    	RETURNING customer_id INTO v_cust_id;

		INSERT INTO rental (rental_date, inventory_id, customer_id, staff_id)
    	VALUES (CURRENT_TIMESTAMP, p_inventory_id, v_cust_id, p_staff_id)
    	RETURNING rental_id INTO v_rental_id;

		INSERT INTO payment (customer_id, staff_id, rental_id, amount, payment_date)
    	VALUES (v_cust_id, p_staff_id, v_rental_id, p_amount, CURRENT_TIMESTAMP);
	EXCEPTION
		WHEN OTHERS THEN
			RAISE EXCEPTION 'Transaction Failed %', sqlerrm;
	END;
END;
$$
LANGUAGE plpgsql;
CALL proc_create_customer_rental ('Keith','Hedger','kh@gmail.com',1,1,1,1,10)


select * from customer order by customer_id desc;
select * from rental order by rental_date desc;
select * from  payment order by payment_date desc 





-- UPDATE WITH CURSOR USING PROCEDURE


CREATE OR REPLACE PROCEDURE proc_update_rental_rate()
AS $$
DECLARE rental_counts CURSOR
FOR SELECT f.film_id, title, COUNT(rental_id) rental_count
FROM film f
LEFT JOIN inventory i
	ON f.film_id = i.film_id
LEFT JOIN rental r
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
	CLOSE rental_counts;
END;
$$
LANGUAGE plpgsql;



CALL proc_update_rental_rate()