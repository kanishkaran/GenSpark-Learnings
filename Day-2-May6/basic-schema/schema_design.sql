-- Table to store product categories
CREATE TABLE categories(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(30) NOT NULL UNIQUE
);

-- Table to store countries
CREATE TABLE country(
	id INT PRIMARY KEY IDENTITY(1,1),
	cname VARCHAR(20) NOT NULL UNIQUE
);

-- Table to store states associated with countries
CREATE TABLE state(
	id INT PRIMARY KEY IDENTITY(1,1),
	sname VARCHAR(30) NOT NULL,
	c_id INT,
	FOREIGN KEY (c_id) REFERENCES country(id)
);

-- Table to store cities associated with states
CREATE TABLE city(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(30) NOT NULL,
	s_id INT,
	FOREIGN KEY (s_id) REFERENCES state(id)
);

-- Table to store areas (ZIP codes) associated with cities
CREATE TABLE area(
	zipcode INT PRIMARY KEY,
	name VARCHAR(30) NOT NULL,
	city_id INT,
	FOREIGN KEY (city_id) REFERENCES city(id)
);

-- Table to store full address details linked to ZIP codes
CREATE TABLE address(
	add_id INT PRIMARY KEY IDENTITY(1,1),
	doorNo VARCHAR(8) NOT NULL,
	addressLine VARCHAR(50) NOT NULL,
	zipcode INT,
	FOREIGN KEY (zipcode) REFERENCES area(zipcode)
);

-- Table to store supplier details
CREATE TABLE supplier(
	id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(20),
	point_of_contact VARCHAR(25),
	phone_number INT NOT NULL,
	email VARCHAR(50) NOT NULL,
	address_id INT,
	FOREIGN KEY (address_id) REFERENCES address(add_id)
);

-- Table to store product details
CREATE TABLE products(
	prod_id INT PRIMARY KEY IDENTITY(1,1),
	name VARCHAR(50) NOT NULL,
	price DECIMAL(10, 2) NOT NULL,
	quantity INT,
	description TEXT,
	image TEXT
);

-- Table to record supply transactions of products from suppliers
CREATE TABLE product_supplier(
	transaction_id INT PRIMARY KEY,
	product_id INT NOT NULL,
	supplier_id INT NOT NULL,
	supplyDate DATE,
	qty INT,
	FOREIGN KEY (product_id) REFERENCES products(prod_id),
	FOREIGN KEY (supplier_id) REFERENCES supplier(id)
);

-- Table to store customer details
CREATE TABLE customer(
	cus_id INT PRIMARY KEY IDENTITY(1,1),
	Fname VARCHAR(30) NOT NULL,
	Lname VARCHAR(30) NOT NULL,
	phone_number INT NOT NULL,
	address_id INT,
	FOREIGN KEY (address_id) REFERENCES address(add_id)
);

-- Table to record customer orders
CREATE TABLE orders (
    order_number INT PRIMARY KEY,
    customer_id INT NOT NULL,
    date_of_order DATE NOT NULL,
    amount DECIMAL(10, 2),
    order_status VARCHAR(50),
	FOREIGN KEY (customer_id) REFERENCES customer(cus_id)
);

-- Table to store details of products in each order
CREATE TABLE order_details (
    id INT PRIMARY KEY IDENTITY(1, 1),
    order_number INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    unit_price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (order_number) REFERENCES orders(order_number),
	FOREIGN KEY (product_id) REFERENCES products(prod_id)
);
