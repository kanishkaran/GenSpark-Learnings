-- 2. Create a `data_entry_user` role:

--    * Can `INSERT` into `students`, `enrollments`
--    * Cannot modify certificates directly

-- Have access to Insert :)
INSERT INTO tbl_students (student_name, email, st_phone_number, college_id) VALUES 
('Alex John', 'alex.john@example.com', 8076543210, 3),
('Boby Zane', 'bob.zane@example.com', 70056789, 1);


INSERT INTO tbl_enrollment (student_id, course_id, iscomplete) VALUES 
(4, 1, FALSE),
(5, 3, TRUE);

-- Does Not Have access to Update :<
UPDATE tbl_certificates SET cert_id = 1 WHERE cert_id = 1;