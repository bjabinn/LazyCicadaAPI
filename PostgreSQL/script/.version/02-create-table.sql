\c lazycicada;
SET ROLE 'misslazycicada';

CREATE TABLE headcount.role (
	rol_pk BIGSERIAL PRIMARY KEY,
	rol_name VARCHAR(50) NOT NULL
);

CREATE TABLE headcount.employee (
	epy_pk BIGSERIAL PRIMARY KEY,
	epy_number BIGINT NOT NULL,
	epy_first_name VARCHAR(50) NOT NULL,
	epy_last_name VARCHAR(50) NOT NULL,
	epy_short_name VARCHAR(50) NOT NULL
);

CREATE TABLE headcount.role_employee (
	rol_epy_pk BIGSERIAL PRIMARY KEY,
	rol_id BIGINT NOT NULL,
	epy_id BIGINT NOT NULL
);