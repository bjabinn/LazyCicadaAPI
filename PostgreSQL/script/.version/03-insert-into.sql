\c lazycicada;
SET ROLE 'misslazycicada';

INSERT INTO headcount.role (rol_name) VALUES
('Administrador'),
('Supervisor');
	

INSERT INTO headcount.employee (epy_number, epy_first_name, epy_last_name, epy_short_name) VALUES
(6234122, 'Manuel', 'Pérez', 'mperez'),
(3234232, 'Pedro', 'Vázquez', 'pvazquez'),
(3935358, 'Laura', 'Martín', 'lmartin'),
(1364700, 'Carmen', 'Díaz', 'cdiaz'),
(3428812, 'María', 'Douglas', 'mdouglas'),
(1091837, 'Mike', 'Tyson', 'mtyson');


INSERT INTO headcount.role_employee (rol_id, epy_id) VALUES
(1, 1),
(1, 4),
(2, 1),
(2, 2),
(2, 3),
(2, 4),
(2, 5),
(2, 6);