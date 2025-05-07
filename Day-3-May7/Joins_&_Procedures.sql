use pubs;

-- Joins Basics

SELECT a.au_id, t.title 
FROM titles t 
	JOIN titleauthor a ON t.title_id = a.title_id;

SELECT CONCAT(au_fname, ' ', au_lname) Author_Name, title Book_Name 
FROM authors a 
	JOIN titleauthor ta ON a.au_id = ta.au_id
	JOIN titles t ON ta.title_id = t.title_id;

-- Q1. select publisher name, book name and order date

SELECT pub_name, title, ord_date 
FROM publishers p 
	JOIN titles t ON p.pub_id = t.pub_id
	JOIN sales s ON t.title_id = s.title_id;

-- Q2. select publisher name and date of sale for the first book sold


SELECT pub_name Publisher_Name, MIN(ord_date) First_Order_Date 
FROM publishers p 
	LEFT OUTER JOIN titles t ON p.pub_id = t.pub_id
	LEFT OUTER JOIN sales s ON t.title_id = s.title_id
GROUP BY p.pub_name
ORDER BY 2 DESC;


-- practice

--	Books and store that sold the books
SELECT stor_name, title 
FROM stores 
JOIN sales ON stores.stor_id = sales.stor_id
JOIN titles ON sales.title_id = titles.title_id;

-- No of unique/ distinct books sold by the store
SELECT stor_name, COUNT(title) distinct_books
FROM stores 
	JOIN sales ON stores.stor_id = sales.stor_id
	JOIN titles ON sales.title_id = titles.title_id
GROUP BY stor_name;



-- Q.3 Print the Book Name and Store Address of the sale

SELECT title Book_Name, stor_address Store_Address 
FROM stores st
JOIN sales s ON st.stor_id = s.stor_id
JOIN titles t ON s.title_id = t.title_id
ORDER BY 1;


-- Store Procedure

--Syntax

CREATE PROCEDURE proc_get_books_at_store
AS
BEGIN
	SELECT stor_name, title 
	FROM stores 
	JOIN sales ON stores.stor_id = sales.stor_id
	JOIN titles ON sales.title_id = titles.title_id;
END

EXEC proc_get_books_at_store


-- Stored Procedure w/ parameters

CREATE TABLE Products(
id INT CONSTRAINT pk_productId PRIMARY KEY IDENTITY(1, 1),
Name NVARCHAR(50) NOT NULL,
Details NVARCHAR(MAX))

CREATE PROCEDURE proc_insertProduct(@Name NVARCHAR(50), @Details NVARCHAR(MAX))
AS
BEGIN
	INSERT INTO Products(Name, Details) VALUES(@Name, @Details)
END

proc_insertProduct 'Laptop','{"brand":"Dell","spec":{"ram":"16GB","cpu":"i5"}}'


SELECT * FROM Products;

-- JSON QUERY

SELECT JSON_QUERY(Details, '$.spec') Spec_Details FROM Products;

-- The above does not fetch Single Value, Thus we can use JSON_VALUE

SELECT JSON_VALUE(Details, '$.brand') Brand FROM Products;

SELECT JSON_VALUE(Details, '$.spec.ram') Brand FROM Products;


-- JSON_MODIFY

CREATE PROCEDURE proc_updateProductSpec(@pid INT, @new_value VARCHAR(20))
AS
BEGIN
	UPDATE Products SET Details = JSON_MODIFY(details, '$.spec.ram', @new_value)
	WHERE id = @pid
END

proc_updateProductSpec 1, '32GB'


proc_insertProduct 'Laptop','{"brand":"HP","spec":{"ram":"24GB","cpu":"i7"}}'

SELECT * FROM Products;
-- TRY CAST
-- The TRY_CAST function attempts to convert a value to a specified data type. If the conversion fails, it returns NULL instead of an error.

SELECT * FROM Products
try_cast(WHERE JSON_VALUE(Details, '$.spec.ram') AS VARCHAR(20))= '24GB'; --WHERE CLAUSE IN JSON DATA