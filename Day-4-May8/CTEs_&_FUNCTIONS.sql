
CREATE TABLE people
(
    id INT CONSTRAINT pk_peopleId PRIMARY KEY,
    name NVARCHAR(20),
    age INT
)

-- Create procedure to bulk insert data into 'people' table
CREATE OR ALTER PROC proc_inserBulkData(@filepath NVARCHAR(500))
AS
BEGIN
    DECLARE @insertQuery NVARCHAR(MAX)

    SET @insertQuery = 'BULK INSERT people FROM ''' + @filepath + '''
    WITH(
        FIRSTROW = 2,
        FIELDTERMINATOR = '','',
        ROWTERMINATOR = ''\n''
    )'

    EXEC sp_executesql @insertQuery  -- In-Built stored-procedure.
END

EXEC proc_inserBulkData 'C:\Users\admin\Desktop\GenSpark\Day-4-May8\data.csv'

SELECT * FROM people

-- Create log table to track bulk insert operations
CREATE TABLE logs
(
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    FilePath NVARCHAR(1000),
    status NVARCHAR(50) CONSTRAINT ck_status CHECK(status IN('success','failed')),
    Message NVARCHAR(1000),
    InsertOn DATETIME DEFAULT GETDATE()
)

-- Update procedure to include error logging using TRY / CATCH
CREATE OR ALTER PROC proc_inserBulkData(@filepath NVARCHAR(500))
AS
BEGIN
    BEGIN TRY
        DECLARE @insertQuery NVARCHAR(MAX)

        SET @insertQuery = 'BULK INSERT people FROM ''' + @filepath + '''
        WITH(
            FIRSTROW = 2,
            FIELDTERMINATOR = '','',
            ROWTERMINATOR = ''\n''
        )'

        EXEC sp_executesql @insertQuery

        INSERT INTO logs(filepath, status, message)
        VALUES(@filepath, 'Success', 'Data insert complete')
    END TRY
    BEGIN CATCH
        INSERT INTO logs(filepath, status, message)
        VALUES(@filepath, 'Failed', ERROR_MESSAGE())
    END CATCH
END

TRUNCATE TABLE people

EXEC proc_inserBulkData 'C:\Users\admin\Desktop\GenSpark\Day-4-May8\data.csv'

-- View logs
SELECT * FROM logs


-- CTEs - COMMON TABLE EXPRESSIONS

-- Use CTE to retrieve book titles and prices
WITH cte_Books AS
(
    SELECT title, price FROM titles
)
SELECT * FROM cte_Books


DECLARE @page INT = 2, @pageSize INT = 10;
WITH PaginatedBooks AS
(
    SELECT title_id, title, price, ROW_NUMBER() OVER (ORDER BY price DESC) AS rowNumber
    FROM titles
)
SELECT * 
FROM PaginatedBooks 
WHERE rowNumber BETWEEN ((@page - 1) * @pageSize + 1) AND (@page * @pageSize)

-- Create paginated procedure using ROW_NUMBER
CREATE OR ALTER PROC proc_PaginateBooks(@page INT = 1, @pageSize INT = 10)
AS
BEGIN
    WITH PaginatedBooks AS
    (
        SELECT title_id, title, price, ROW_NUMBER() OVER (ORDER BY price DESC) AS rowNumber
        FROM titles
    )
    SELECT * 
    FROM PaginatedBooks 
    WHERE rowNumber BETWEEN ((@page - 1) * @pageSize + 1) AND (@page * @pageSize)
END

EXEC proc_PaginateBooks 2, 5

-- Pagination using OFFSET-FETCH
SELECT title_id, title, price
FROM titles
ORDER BY price DESC
OFFSET 10 ROWS FETCH NEXT 10 ROWS ONLY

-- Create pagination procedure using OFFSET-FETCH
CREATE OR ALTER PROC proc_PaginateBooks_offset(@page INT = 1, @pageSize INT = 10)
AS
BEGIN
    SELECT title_id, title, price
    FROM titles
    ORDER BY price DESC
    OFFSET (@page - 1) * @pageSize ROWS
    FETCH NEXT @pageSize ROWS ONLY;
END


EXEC proc_PaginateBooks_offset 2, 5

-- FUNCTIONS - SCALAR & TABLE VALUED FUNCTION


-- SCALAR
CREATE FUNCTION fn_CalculateTax(@price FLOAT, @tax FLOAT)
RETURNS FLOAT
AS
BEGIN
    RETURN (@price + (@price * @tax / 100))
END


SELECT dbo.fn_calculateTax(1000, 10)


SELECT title, dbo.fn_CalculateTax(price, 12) FROM titles


-- TVF

-- Create table-valued function to filter books by price
CREATE FUNCTION fn_tableSample(@minprice FLOAT)
RETURNS TABLE
AS
RETURN 
    SELECT title, price 
    FROM titles 
    WHERE price >= @minprice


SELECT * FROM dbo.fn_tableSample(10)
