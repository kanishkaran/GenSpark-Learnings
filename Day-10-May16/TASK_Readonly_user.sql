-- readonly user

-- 1. Create a `readonly_user` role:

--    * Can run `SELECT` on `students`, `courses`, and `certificates`
--    * Cannot `INSERT`, `UPDATE`, or `DELETE`

-- HAS ACCESS, ONLY TO SELECT THESE
SELECT * FROM tbl_students;
SELECT * FROM tbl_courses;
SELECT * FROM tbl_certificates;


-- DENIED ACCESS :(
SELECT * FROM tbl_technologies;
SELECT * FROM tbl_colleges;

-- No DML Access :(
UPDATE tbl_students SET student_name = 'Alex' 
WHERE student_id = 1;

INSERT INTO tbl_courses(course_name, tech_id, duration_days)
VALUES ('Advanced Python', 1, 45);

DELETE FROM tbl_certificates 
WHERE cert_id = 3;



