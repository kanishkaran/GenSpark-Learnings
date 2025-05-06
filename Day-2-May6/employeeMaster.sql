-- Create the EmployeeMaster database
CREATE DATABASE EmployeeMaster;

-- Switch to the EmployeeMaster database
USE EmployeeMaster;

-- Create the 'employees' table
CREATE TABLE employees (
    emp_no INT PRIMARY KEY,
    emp_name VARCHAR(30) NOT NULL,
    salary DECIMAL(10,2),
    dept_name VARCHAR(50) NOT NULL,
    boss_no INT,
    FOREIGN KEY (boss_no) REFERENCES employees(emp_no)
);

-- Create the 'department' table
CREATE TABLE department (
    dept_name VARCHAR(50) PRIMARY KEY,
    dept_floor INT,
    phone_no INT,
    mgr_id INT NOT NULL,
    FOREIGN KEY (mgr_id) REFERENCES employees(emp_no)
);

-- Create the 'item' table
CREATE TABLE item (
    item_name VARCHAR(50) PRIMARY KEY,
    item_type CHAR(2),
    item_color VARCHAR(20)
);

-- Create the 'sales' table
CREATE TABLE sales (
    sales_no INT PRIMARY KEY,
    sales_qty INT,
    item_name VARCHAR(50) NOT NULL,
    dept_name VARCHAR(50) NOT NULL,
    FOREIGN KEY (item_name) REFERENCES item(item_name),
    FOREIGN KEY (dept_name) REFERENCES department(dept_name)
);

-- Insert initial data into the 'employees' table
INSERT INTO employees (emp_no, emp_name, salary, dept_name, boss_no) 
VALUES
(1, 'Alice', 75000, 'Management', NULL),
(2, 'Ned', 45000, 'Marketing', 1),
(3, 'Andrew', 25000, 'Marketing', 2),
(4, 'Clare', 22000, 'Marketing', 2),
(5, 'Todd', 38000, 'Accounting', 1),
(6, 'Nancy', 22000, 'Accounting', 5),
(7, 'Brier', 43000, 'Purchasing', 1),
(8, 'Sarah', 56000, 'Purchasing', 7),
(9, 'Sophia', 35000, 'Personnel', 1),
(10, 'Sanjay', 15000, 'Navigation', 3),
(11, 'Rita', 15000, 'Books', 4),
(12, 'Gigi', 16000, 'Clothes', 4),
(13, 'Maggie', 11000, 'Clothes', 4),
(14, 'Paul', 15000, 'Equipment', 3),
(15, 'James', 15000, 'Equipment', 3),
(16, 'Pat', 15000, 'Furniture', 3),
(17, 'Mark', 15000, 'Recreation', 3);

-- Insert data into the 'department' table
INSERT INTO department (dept_name, dept_floor, phone_no, mgr_id) 
VALUES
('Management', 5, 34, 1),
('Books', 1, 81, 4),
('Clothes', 2, 24, 4),
('Equipment', 3, 57, 3),
('Furniture', 4, 14, 3),
('Navigation', 1, 41, 3),
('Recreation', 2, 29, 4),
('Accounting', 5, 35, 5),
('Purchasing', 5, 36, 7),
('Personnel', 5, 37, 9),
('Marketing', 5, 38, 2);

-- Add foreign key to link employee's department with the department table
ALTER TABLE employees
ADD CONSTRAINT FK_EMP_DEPT FOREIGN KEY (dept_name)
REFERENCES department(dept_name);

-- Insert data into the 'item' table
INSERT INTO item (item_name, item_type, item_color) 
VALUES
('Pocket Knife-Nile', 'E', 'Brown'),
('Pocket Knife-Avon', 'E', 'Brown'),
('Compass', 'N', NULL),
('Geo positioning system', 'N', NULL),
('Elephant Polo stick', 'R', 'Bamboo'),
('Camel Saddle', 'R', 'Brown'),
('Sextant', 'N', NULL),
('Map Measure', 'N', NULL),
('Boots-snake proof', 'C', 'Green'),
('Pith Helmet', 'C', 'Khaki'),
('Hat-polar Explorer', 'C', 'White'),
('Exploring in 10 Easy Lessons', 'B', NULL),
('Hammock', 'F', 'Khaki'),
('How to win Foreign Friends', 'B', NULL),
('Map case', 'E', 'Brown'),
('Safari Chair', 'F', 'Khaki'),
('Safari cooking kit', 'F', 'Khaki'),
('Stetson', 'C', 'Black'),
('Tent - 2 person', 'F', 'Khaki'),
('Tent - 8 person', 'F', 'Khaki');

-- Insert data into the 'sales' table
INSERT INTO sales (sales_no, sales_qty, item_name, dept_name) 
VALUES
(101, 2, 'Boots-snake proof', 'Clothes'),
(102, 1, 'Pith Helmet', 'Clothes'),
(103, 1, 'Sextant', 'Navigation'),
(104, 3, 'Hat-polar Explorer', 'Clothes'),
(105, 5, 'Pith Helmet', 'Equipment'),
(106, 2, 'Pocket Knife-Nile', 'Clothes'),
(107, 3, 'Pocket Knife-Nile', 'Recreation'),
(108, 1, 'Compass', 'Navigation'),
(109, 2, 'Geo positioning system', 'Navigation'),
(110, 5, 'Map Measure', 'Navigation'),
(111, 1, 'Geo positioning system', 'Books'),
(112, 1, 'Sextant', 'Books'),
(113, 3, 'Pocket Knife-Nile', 'Books'),
(114, 1, 'Pocket Knife-Nile', 'Navigation'),
(115, 1, 'Pocket Knife-Nile', 'Equipment'),
(116, 1, 'Sextant', 'Clothes'),
(117, 1, 'Pocket Knife-Nile', 'Equipment'),
(118, 1, 'Pocket Knife-Nile', 'Recreation'),
(119, 1, 'Pocket Knife-Nile', 'Furniture'),
(120, 1, 'Pocket Knife-Nile', 'Clothes'),
(121, 1, 'Exploring in 10 easy lessons', 'Books'),
(122, 1, 'How to win foreign friends', 'Books'),
(123, 1, 'Compass', 'Clothes'),
(124, 1, 'Pith Helmet', 'Clothes'),
(125, 1, 'Elephant Polo stick', 'Recreation'),
(126, 1, 'Camel Saddle', 'Recreation');

-- Display all records from each table
SELECT * FROM employees;
SELECT * FROM department;
SELECT * FROM item;
SELECT * FROM sales;
