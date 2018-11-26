\c lazycicada;
SET ROLE 'misslazycicada';

INSERT INTO headcount.role (name) VALUES
('Administrador'),
('Supervisor');
	

INSERT INTO headcount.employee (employee_number, sam_account_name, display_name) VALUES
(6234122, 'mperez', 'Manuel Pérez'),
(3234232, 'pvazquez', 'Pedro Vázquez'),
(3935358, 'lmartin', 'Laura Martín'),
(1364700, 'cdiaz', 'Carmen Díaz'),
(3428812, 'mdouglas', 'María Douglas'),
(1091837, 'mtyson', 'Mike Tyson');


INSERT INTO headcount.role_employee (role, employee) VALUES
(1, 1),
(1, 4),
(2, 1),
(2, 2),
(2, 3),
(2, 4),
(2, 5),
(2, 6);