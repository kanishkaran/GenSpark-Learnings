CREATE TABLE audit_logs(
audit_id SERIAL CONSTRAINT pk_aud_id PRIMARY KEY,
table_name TEXT,
col_name TEXT,
old_value TEXT,
new_value TEXT,
Update_date DATE DEFAULT NOW()
)

-- TRIGGERS
CREATE OR REPLACE FUNCTION update_audit_log()
RETURNS TRIGGER
AS 
$$
BEGIN
	INSERT INTO audit_logs(table_name, col_name, old_value, new_value)
	VALUES('customer', 'email', OLD.email, NEW.email);
	RETURN NEW;
END;
$$
LANGUAGE plpgsql;



CREATE TRIGGER trg_update_logs
BEFORE UPDATE ON customer
FOR EACH ROW
EXECUTE FUNCTION update_audit_log();


UPDATE customer SET email = 'NewChange' 
WHERE customer_id = 1;


SELECT * FROM audit_logs


-- Using Parameters (sort of)
 
CREATE OR REPLACE FUNCTION update_logs()
RETURNS TRIGGER
AS
$$
DECLARE 
	col_name TEXT := TG_ARGV[0];
	table_name TEXT := TG_ARGV[1];
	o_value TEXT;
	n_value TEXT;
BEGIN
	EXECUTE FORMAT('SELECT ($1).%I', col_name) INTO o_value USING OLD;
	EXECUTE FORMAT('SELECT ($1).%I', col_name) INTO n_value USING NEW;
	IF o_value IS DISTINCT FROM n_value THEN
		INSERT INTO audit_logs(table_name, col_name, old_value, new_value)
		VALUES(table_name, col_name, o_value, n_value);
		RETURN NEW;
	END IF;
END;
$$
LANGUAGE plpgsql;

DROP TRIGGER trg_update_logs ON customer;

CREATE TRIGGER trg_update_logs
BEFORE UPDATE ON customer
FOR EACH ROW
EXECUTE FUNCTION update_logs('last_name', 'customer');


UPDATE customer SET last_name = 'S' 
WHERE customer_id = 1;

SELECT * FROM customer 
ORDER BY customer_id;

SELECT * FROM audit_logs;