-- Create the Posts table to store post data
CREATE TABLE Posts(
id INT CONSTRAINT pk_postId PRIMARY KEY,
userid INT,
title NVARCHAR(100),
body NVARCHAR(MAX))

-- OPENJSON 
-- Create a stored procedure to insert bulk data from JSON into the Posts table
CREATE PROC proc_insertBulkData (@jsonData NVARCHAR(MAX))
AS
BEGIN
  INSERT INTO Posts(userid, id, title, body)
  SELECT userId, id, title, body FROM OPENJSON(@jsonData)
  WITH (userId INT,id INT, title NVARCHAR(100), body NVARCHAR(max))
END

-- Call the procedure.
proc_insertBulkData '[
  {
  "userId": 1,
  "id": 1,
  "title": "sunt aut facere repellat provident occaecati excepturi optio reprehenderit",
  "body": "quia et suscipit\nsuscipit recusandae consequuntur expedita et cum\nreprehenderit molestiae ut ut quas totam\nnostrum rerum est autem sunt rem eveniet architecto"
  },
  {
  "userId": 1,
  "id": 2,
  "title": "qui est esse",
  "body": "est rerum tempore vitae\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\nqui aperiam non debitis possimus qui neque nisi nulla"
  },
  {
  "userId": 1,
  "id": 3,
  "title": "ea molestias quasi exercitationem repellat qui ipsa sit aut",
  "body": "et iusto sed quo iure\nvoluptatem occaecati omnis eligendi aut ad\nvoluptatem doloribus vel accusantium quis pariatur\nmolestiae porro eius odio et labore et velit aut"
  },
  {
  "userId": 1,
  "id": 4,
  "title": "eum et est occaecati",
  "body": "ullam et saepe reiciendis voluptatem adipisci\nsit amet autem assumenda provident rerum culpa\nquis hic commodi nesciunt rem tenetur doloremque ipsam iure\nquis sunt voluptatem rerum illo velit"
  }]'

-- View all data from the Posts table
SELECT * FROM Posts;

-- Create a procedure to get posts by user ID
CREATE PROC proc_get_PostByID (@pid INT)
AS
BEGIN
  SELECT * FROM Posts
  WHERE userid = @pid
END

-- Call the procedure to get posts by user ID
proc_get_PostByID 1
