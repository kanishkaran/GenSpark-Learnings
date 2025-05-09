-- Tasks Morning Session



-- Q1.List all films with their length and rental rate, sorted by length descending.

SELECT title, length, rental_rate 
FROM film
ORDER BY 2 DESC, 1;

-- Q2.Find the top 5 customers who have rented the most films.

SELECT c.customer_id, CONCAT(first_name, ' ', last_name) Name, COUNT(rental_id) Total_Rents
FROM customer c
JOIN rental r 
	ON c.customer_id = r.customer_id
GROUP BY c.customer_id, first_name, last_name
ORDER BY 2 DESC
LIMIT 5;

-- Q3.Display all films that have never been rented.

SELECT title
FROM film f
	 JOIN inventory i
		ON f.film_id = i.film_id
	LEFT JOIN rental r
		ON i.inventory_id = r.inventory_id
WHERE r.rental_id IS NULL;


-- Q4.List all actors who appeared in the film ‘Academy Dinosaur’.

SELECT title Movie, CONCAT(first_name, ' ', last_name) Actors
FROM actor a
	JOIN film_actor fa
		ON a.actor_id = fa.actor_id
	JOIN film f
		ON fa.film_id = f.film_id
WHERE f.title = 'Academy Dinosaur';

-- Q5.List each customer along with the total number of rentals they made and the total amount paid.

SELECT c.customer_id, CONCAT(first_name, ' ', last_name) Customer_Name, COUNT(r.rental_id) Total_Rental, SUM(amount) total_Amount
FROM payment p
	JOIN customer c
		ON p.customer_id = c.customer_id
	JOIN rental r
		ON 	c.customer_id = r.customer_id
GROUP BY c.customer_id
ORDER BY 1;

-- CTE-Based Queries
-- Q6.Using a CTE, show the top 3 rented movies by number of rentals.

WITH cte_Total_Rentals
AS
	(SELECT title, COUNT(rental_id) AS total_rents
	FROM film f
		JOIN inventory i
			ON 	f.film_id = i.film_id
		JOIN rental r
			ON i.inventory_id = r.inventory_id
	GROUP BY title
	ORDER BY 2 DESC)

SELECT * FROM cte_Total_Rentals
LIMIT 3;


-- Q7.Find customers who have rented more than the average number of films.

WITH cte_total_rents
AS
	(SELECT  r.customer_id, CONCAT(first_name, ' ', last_name) Customer_Name, COUNT(rental_id) total_rents
	FROM customer c
		JOIN rental r
			ON c.customer_id = r.customer_id
	GROUP BY r.customer_id, first_name, last_name
	ORDER BY 2 DESC),
	avg_rentals
	AS (SELECT AVG(total_rents) avg_rental FROM cte_total_rents)

SELECT customer_name, total_rents FROM cte_total_rents t
JOIN avg_rentals a
ON t.total_rents > a.avg_rental
ORDER BY 2 DESC

 


-- Q8.Write a function that returns the total number of rentals for a given customer ID.


CREATE FUNCTION get_total_rents(cust_id INT)
RETURNS INT
AS
$$
DECLARE total_rents INT;
BEGIN
	SELECT COUNT(*) INTO total_rents FROM rental WHERE customer_id = cust_id;
	RETURN total_rents;
END;
$$
LANGUAGE plpgsql	

SELECT get_total_rents(5);

-- Q9.Write a stored procedure that updates the rental rate of a film by film ID and new rate.

CREATE PROCEDURE proc_update_rentalRate(fid INT, new_rate NUMERIC)
AS
$$
BEGIN
	UPDATE film SET rental_rate = new_rate WHERE film_id = fid;
END
$$
LANGUAGE plpgsql


select * from film where film_id = 8
CALL proc_update_rentalrate(8, 5.99);


-- Q10.Write a procedure to list overdue rentals 

CREATE OR REPLACE PROCEDURE get_overdue()
AS
$$
DECLARE record RECORD;
BEGIN
	FOR record IN
		SELECT rental_id, CONCAT(first_name, ' ', last_name) Customer_Name
		FROM rental r
		JOIN customer c
			ON r.customer_id = c.customer_id
		WHERE return_date IS NULL 
	LOOP
		RAISE NOTICE 'id: % Customer Name: % ', record.rental_id, record.customer_name;
	END LOOP;
END;
$$
LANGUAGE plpgsql

CALL get_overdue();





