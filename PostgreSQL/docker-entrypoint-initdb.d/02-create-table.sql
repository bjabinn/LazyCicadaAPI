\c lazycicada;
SET ROLE 'misslazycicada';

CREATE TABLE headcount.role (
	pk BIGSERIAL,
	name VARCHAR(50) NOT NULL,
	PRIMARY KEY (pk),
	UNIQUE (name)
);

CREATE TABLE headcount.employee (
	pk BIGSERIAL,
	employee_number BIGINT NOT NULL,
	sam_account_name VARCHAR(20) NOT NULL,
	display_name VARCHAR(100) NOT NULL,
	PRIMARY KEY (pk),
	UNIQUE (employee_number),
	UNIQUE (sam_account_name)
);

CREATE TABLE headcount.role_employee (
	pk BIGSERIAL,
	role BIGINT NOT NULL,
	employee BIGINT NOT NULL,
	PRIMARY KEY (pk),
	FOREIGN KEY (role) REFERENCES headcount.role (pk),
	FOREIGN KEY (employee) REFERENCES headcount.employee (pk),
	UNIQUE (role, employee)
);