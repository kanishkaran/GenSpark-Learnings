SELECT * FROM people;

DECLARE people_cursor CURSOR
	FOR SELECT * FROM people;

OPEN people_cursor
FETCH NEXT FROM people_cursor
CLOSE people_cursor



OPEN people_cursor
DECLARE @id INT, @name NVARCHAR(20), @age INT

FETCH NEXT FROM people_cursor INTO @id, @name, @age
PRINT 'ID: '+ try_cast(@id as char) + 'NAME: '+ @name + ' AGE: ' + try_cast(@age as varchar(2)) 
PRINT ' FETCH STATUS: ' + TRY_CAST(@@FETCH_STATUS AS CHAR)


-- CURSOR W/ WHILE 

SELECT * FROM logs


DECLARE log_cursor CURSOR
FOR (SELECT * FROM logs)

OPEN log_cursor

FETCH NEXT FROM log_cursor


WHILE @@FETCH_STATUS = 0 
BEGIN
	FETCH NEXT FROM log_cursor 
	SELECT @@FETCH_STATUS AS 'fetch_status'
END

SELECT @@CURSOR_ROWS

CLOSE log_cursor