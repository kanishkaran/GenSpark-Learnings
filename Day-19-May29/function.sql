CREATE OR REPLACE FUNCTION func_get_doctors_by_specialization(p_specialization VARCHAR(20))
RETURNS TABLE(id INT, name TEXT)
AS
$$
BEGIN
	RETURN QUERY SELECT "Id", "DoctorName" FROM doctors d
			WHERE "Id" IN (SELECT "DoctorId" FROM public."doctorSpecializations"
			WHERE "SpecializationId" IN (SELECT "Id" FROM specializations 
			WHERE "Name" = p_specialization));			
END;
$$
LANGUAGE plpgsql;


SELECT id, name from func_get_doctors_by_specialization('ENT');