CREATE DATABASE EdTech;

-- PHASE 2 - DDL & DML

-- CREATE TABLES WITH APPROPRIATE CONSTRAINTS

CREATE TABLE tbl_colleges(
college_id SERIAL CONSTRAINT pk_college_id PRIMARY KEY,
college_name VARCHAR(40) NOT NULL
);


CREATE TABLE tbl_students(
student_id SERIAL CONSTRAINT pk_student_id PRIMARY KEY,
student_name VARCHAR(30) NOT NULL,
email VARCHAR(80) UNIQUE,
st_phone_number VARCHAR(15),
college_id INT,
CONSTRAINT fk_college_student FOREIGN KEY (college_id) REFERENCES tbl_colleges(college_id)
);


CREATE TABLE tbl_technologies(
tech_id SERIAL CONSTRAINT pk_tech_id PRIMARY KEY,
tech_name VARCHAR(30) 
);

CREATE TABLE tbl_courses(
course_id SERIAL CONSTRAINT pk_course_id PRIMARY KEY,
course_name VARCHAR(50) NOT NULL,
tech_id INT,
duration_days INT,
CONSTRAINT fk_tech_course FOREIGN KEY (tech_id) REFERENCES tbl_technologies(tech_id)
);


CREATE TABLE tbl_trainers(
trainer_id SERIAL CONSTRAINT pk_trainer_id PRIMARY KEY,
trainer_name VARCHAR(40),
t_phone_number VARCHAR(15),
expertise INT,
CONSTRAINT fk_tech_trainer FOREIGN KEY (expertise) REFERENCES tbl_technologies(tech_id)
);


CREATE TABLE tbl_enrollment(
enrollment_id SERIAL CONSTRAINT pk_enrollment_id PRIMARY KEY,
student_id INT,
course_id INT,
enrollment_date DATE DEFAULT CURRENT_DATE,
iscomplete BOOLEAN DEFAULT FALSE,
CONSTRAINT fk_student_enroll FOREIGN KEY (student_id) REFERENCES tbl_students(student_id),
CONSTRAINT fk_course_enroll FOREIGN KEY (course_id) REFERENCES tbl_courses(course_id)
);


CREATE TABLE tbl_certificates(
cert_id SERIAL  CONSTRAINT pk_cert_id PRIMARY KEY,
serial_number INT NOT NULL UNIQUE,
enrollment_id INT,
issue_date DATE DEFAULT CURRENT_DATE,
CONSTRAINT fk_enroll_cert FOREIGN KEY (enrollment_id) REFERENCES tbl_enrollment(enrollment_id)
);

CREATE TABLE tbl_course_trainer(
course_id INT,
trainer_id INT,
CONSTRAINT fk_course_id FOREIGN KEY (course_id) REFERENCES tbl_courses(course_id),
CONSTRAINT fk_trainer_id FOREIGN KEY (trainer_id) REFERENCES tbl_trainers(trainer_id)
);



SELECT * FROM tbl_colleges;
SELECT * FROM tbl_students;
SELECT * FROM tbl_technologies;
SELECT * FROM tbl_courses;
SELECT * FROM tbl_trainers;
SELECT * FROM tbl_enrollment;
SELECT * FROM tbl_certificates;
SELECT * FROM tbl_course_trainer;
-- Create indexes on `student_id`, `email`, and `course_id`

CREATE INDEX idx_student_id ON tbl_students(student_id);
CREATE INDEX idx_student_email ON tbl_students(email);
CREATE INDEX idx_course_id ON tbl_courses(course_id);


-- Insert sample data using `INSERT` statements

INSERT INTO tbl_colleges (college_name) VALUES 
('ABC Engineering College'),
('XYZ Institute of Technology'),
('Global Tech University');

INSERT INTO tbl_students (student_name, email, st_phone_number, college_id) VALUES 
('Alice Johnson', 'alice.johnson@example.com', 8076543210, 1),
('Bob Smith', 'bob.smith@example.com', 70056789, 2),
('Charlie Brown', 'charlie.brown@example.com', 800776655, 3);

INSERT INTO tbl_technologies (tech_name) VALUES 
('Python'),
('Java'),
('.NET'),
('C++');

INSERT INTO tbl_courses (course_name, tech_id, duration_days) VALUES 
('Intro to Python', 1, 30),
('Advanced Java', 2, 45),
('.NET Full Stack Web Dev', 3, 60),
('Deep Dive into C++', 4, 90);

INSERT INTO tbl_trainers (trainer_name, t_phone_number, expertise) VALUES 
('David Miller', 9012345678, 1),
('Emma Wilson', 9023456789, 2),
('Frank Thompson', 9034567890, 3),
('Grace Lee', 9045678901, 4);

INSERT INTO tbl_enrollment (student_id, course_id, iscomplete) VALUES 
(1, 1, FALSE),
(2, 3, TRUE),
(3, 4, FALSE),
(1, 2, TRUE);

INSERT INTO tbl_certificates (serial_number, enrollment_id) VALUES 
(1001, 2),
(1002, 4);

INSERT INTO tbl_course_trainer (course_id, trainer_id) VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4);



-- Phase 3: SQL Joins Practice

-- Q1. List students and the courses they enrolled in


SELECT student_name, course_name
FROM tbl_students s
	JOIN tbl_enrollment e
		ON s.student_id = e.student_id
	JOIN tbl_courses c
		ON e.course_id = c.course_id
ORDER BY 1;

-- Q2. Show students who received certificates with trainer names

SELECT student_name,  trainer_name
FROM tbl_certificates ct
	JOIN tbl_enrollment e
		ON ct.enrollment_id = e.enrollment_id
	JOIN tbl_students s
		ON e.student_id = s.student_id
	JOIN tbl_course_trainer ctr
		ON e.course_id = ctr.course_id
	JOIN tbl_trainers t
		ON ctr.trainer_id = t.trainer_id
ORDER BY 2;
		

-- Q3. Count number of students per course

SELECT course_name, COUNT(student_id) AS total_students
FROM tbl_enrollment e
	JOIN tbl_courses c
		ON e.course_id = c.course_id
GROUP BY c.course_id
ORDER BY 2 DESC;

SELECT * FROM tbl_enrollment



-- Phase 4: Functions & Stored Procedures


-- Functions

-- 1. Create `get_certified_students(course_id INT)`
-- 		→ Returns a list of students who completed the given course and received certificates.


CREATE OR REPLACE FUNCTION get_certified_students(p_course_id INT)
RETURNS TABLE (student_name VARCHAR(30), course_name VARCHAR(50))
AS $$
BEGIN
	RETURN QUERY
	SELECT s.student_name, c.course_name
	FROM tbl_students s
		JOIN tbl_enrollment e
			ON s.student_id = e.student_id
		JOIN tbl_certificates crt
			ON e.enrollment_id = crt.enrollment_id
		JOIN tbl_courses c
			ON e.course_id = c.course_id
	WHERE e.course_id = p_course_id;
END;
$$
LANGUAGE plpgsql;

SELECT student_name, course_name FROM get_certified_students(2);
SELECT student_name, course_name FROM get_certified_students(3);


-- Stored Procedures


-- Create `sp_enroll_student(p_student_id, p_course_id)`
-- → Inserts into `enrollments` and conditionally adds a certificate if completed (simulate with status flag).

ALTER TABLE tbl_certificates
ALTER COLUMN serial_number ADD
GENERATED BY DEFAULT AS IDENTITY;

ALTER SEQUENCE
tbl_certificates_serial_number_seq RESTART
WITH 1003;

CREATE OR REPLACE PROCEDURE proc_enroll_student(p_student_id INT, p_course_id INT, p_is_complete BOOLEAN DEFAULT FALSE)
AS $$
DECLARE
	v_enrollment_id INT;
BEGIN
	INSERT INTO tbl_enrollment(student_id, course_id, iscomplete)
	VALUES (p_student_id, p_course_id, p_is_complete)
	RETURNING enrollment_id INTO v_enrollment_id;

	IF p_is_complete THEN
		INSERT INTO tbl_certificates(enrollment_id) 
		VALUES(v_enrollment_id);
	END IF;
END;
$$
LANGUAGE plpgsql;


CALL proc_enroll_student(3, 3);
CALL proc_enroll_student(2, 1, TRUE);


SELECT * FROM tbl_enrollment;
SELECT * FROM tbl_certificates;



-- Phase 5: Cursor


-- Loop through all the students and print name & email of students yet to receive a certificate


CREATE OR REPLACE PROCEDURE proc_get_students_without_cert()
AS $$
DECLARE
	student_record RECORD;
	v_email VARCHAR(80);
	student_cursor CURSOR FOR
	SELECT * 
	FROM tbl_enrollment e
		JOIN tbl_students s
			ON e.student_id = s.student_id;
BEGIN
	OPEN student_cursor;

	LOOP
		FETCH student_cursor INTO student_record;
		EXIT WHEN NOT FOUND;

		IF (NOT student_record.iscomplete) THEN
			SELECT email INTO v_email FROM tbl_students
			WHERE student_id = student_record.student_id;
			RAISE NOTICE 'Student Name: %, Email: %', student_record.student_name, v_email;
		END IF;
	END LOOP;
	CLOSE student_cursor;
END;
$$
LANGUAGE plpgsql;


CALL proc_get_students_without_cert();


-- WITHOUT USAGE OF ISCOMPLETE COLUMN
DO $$
DECLARE
	student_record RECORD;
	student_cursor CURSOR FOR
	SELECT student_name, email
	FROM tbl_students s
	 	JOIN tbl_enrollment e
		 	ON s.student_id = e.student_id
		LEFT JOIN tbl_certificates c
			ON e.enrollment_id = c.enrollment_id
	WHERE cert_id IS NULL;
BEGIN 
	OPEN student_cursor; 
	
	LOOP
		FETCH student_cursor INTO student_record;
		EXIT WHEN NOT FOUND;

		RAISE NOTICE 'Student Name: %, Email: %', student_record.student_name, student_record.email;
	END LOOP;
END;
$$


-- Phase 6: Security & Roles

-- The respective test queries are in seperate file each :)
-- Each User is connected via query tool GUI 
-- OR can connect Using psql -d edtech -U [readonly_user | data_entry_user] -p 3432

-- Q1. Create a `readonly_user` role:

CREATE ROLE readonly_user WITH LOGIN PASSWORD 'readonly_pass';

GRANT SELECT ON tbl_students, tbl_courses, tbl_certificates TO readonly_user;

-- Q2. Create a `data_entry_user` role:
CREATE ROLE data_entry_user WITH LOGIN PASSWORD 'data_pass';

GRANT INSERT ON tbl_students, tbl_enrollment TO data_entry_user;

GRANT USAGE ON tbl_students_student_id_seq, tbl_enrollment_enrollment_id_seq TO data_entry_user;

SELECT * FROM tbl_enrollment;


-- Phase 7: Transactions & Atomicity
-- Write a transaction block that:

-- * Enrolls a student
-- * Issues a certificate
-- * Fails if certificate generation fails (rollback)

-- ```sql
-- BEGIN;
--  insert into enrollments
--  insert into certificates
--  COMMIT or ROLLBACK on error
-- ```

CREATE OR REPLACE PROCEDURE proc_enroll_student_transaction(p_student_id INT, p_course_id INT, p_is_complete BOOLEAN DEFAULT FALSE)		-- Usage of Transaction inside procedure 
AS $$
DECLARE
	v_enrollment_id INT;
BEGIN
	BEGIN
		INSERT INTO tbl_enrollment(student_id, course_id, iscomplete)
		VALUES (p_student_id, p_course_id, p_is_complete)
		RETURNING enrollment_id INTO v_enrollment_id;
	
		IF p_is_complete THEN
			INSERT INTO tbl_certificates(enrollment_id) 							-- If not completed, insert is not done :/
			VALUES(v_enrollment_id);
		ELSE 
			RAISE NOTICE 'Failed to issue certificate! Complete the Course!!';		
			RAISE EXCEPTION '';
		END IF;
	EXCEPTION
		WHEN OTHERS THEN
			RAISE NOTICE 'Transaction Failed %', SQLERRM;							-- Exception raised above or any un-foreseen errors are handled here :>
	END;
END;
$$
LANGUAGE plpgsql;


CALL proc_enroll_student_transaction(4, 2, TRUE);

SELECT * FROM tbl_enrollment;
SELECT * FROM tbl_certificates;

CALL proc_enroll_student_transaction(2, 2, TRUE);
CALL proc_enroll_student_transaction(5, 4, FALSE);