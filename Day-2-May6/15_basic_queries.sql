USE pubs;

-- 1) Print all the titles names
SELECT title FROM titles;

-- 2) Print all the titles that have been published by 1389
SELECT title FROM titles
WHERE pub_id = '1389';

-- 3) Print the books that have price in range of 10 to 15
SELECT * FROM titles
WHERE price BETWEEN 10 AND 15;

-- 4) Print those books that have no price
SELECT * FROM titles
WHERE price IS NULL;

-- 5) Print the book names that start with 'The'
SELECT title FROM titles
WHERE title LIKE 'The%';

-- 6) Print the book names that do not have 'v' in their name
SELECT title FROM titles
WHERE title NOT LIKE '%v%';

-- 7) Print the books sorted by the royalty
SELECT * FROM titles
ORDER BY royalty;

-- 8) Print the books sorted by publisher in descending, then by types in ascending, then by price in descending
SELECT t.title, p.pub_name, t.type, t.price 
FROM titles t
JOIN publishers p ON t.pub_id = p.pub_id
ORDER BY p.pub_name DESC, t.type, t.price DESC;

-- 9) Print the average price of books in every type
SELECT type, AVG(price) AS avg_price 
FROM titles
GROUP BY type;

-- 10) Print all the types in unique
SELECT DISTINCT type FROM titles;

-- 11) Print the first 2 costliest books
SELECT TOP 2 title, price 
FROM titles
ORDER BY price DESC;

-- 12) Print books that are of type business and have price less than 20 which also have advance greater than 7000
SELECT title 
FROM titles
WHERE type = 'business' 
  AND price < 20 
  AND advance > 7000;

-- 13) Select those publisher id and number of books which have price between 15 to 25 and have 'It' in its name. 
--     Print only those which have count greater than 2. Also sort the result in ascending order of count
SELECT pub_id, COUNT(title) AS book_count
FROM titles
WHERE price BETWEEN 15 AND 25
  AND title LIKE '%It%'
GROUP BY pub_id
HAVING COUNT(title) > 2
ORDER BY book_count;

-- 14) Print the Authors who are from 'CA'
SELECT CONCAT(au_fname, ' ', au_lname) AS Name, state 
FROM authors 
WHERE state = 'CA';

-- 15) Print the count of authors from every state
SELECT COUNT(au_id) AS author_count, state 
FROM authors
GROUP BY state;
