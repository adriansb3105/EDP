use EDP;

create table categoria_laboral(
id_categoria_laboral int not null IDENTITY(1,1) Primary Key,
nombre nvarchar(110) not null
);

create table seccion(
id_seccion int not null IDENTITY(1,1) Primary Key,
nombre nvarchar(110) not null
);

create table unidad_programa_laboratorio(
id_unidad_programa_laboratorio int not null IDENTITY(1,1) Primary Key,
sigla nvarchar(15) not null,
nombre nvarchar(110) not null
);

create table funcionarios(
numero_identificacion nvarchar(15) not null Primary Key,
nombre nvarchar(110) not null,
primer_apellido nvarchar(110) not null,
segundo_apellido nvarchar(110) not null,
estado bit not null,
ruta_fotografia nvarchar(255) null,
numero_telefono nvarchar(25) null,
fecha_ingreso datetime null,
tipo_sangre nvarchar(25) null,
lugar_residencia nvarchar(255) null,
tipo_licencia_conducir nvarchar(25) null,
puesto nvarchar(255) null,
correo nvarchar(255) null,
extension nvarchar(25) null,
observaciones nvarchar(255) null,
id_categoria_laboral int null Foreign Key References categoria_laboral(id_categoria_laboral),
id_seccion int null Foreign Key References seccion(id_seccion),
id_unidad_programa_laboratorio int null Foreign Key References unidad_programa_laboratorio(id_unidad_programa_laboratorio)
);

create table tipos_estudios(
id_tipo_estudio int not null IDENTITY(1,1) Primary Key,
nombre nvarchar(110) not null,
clasificacion nvarchar(55) not null
);

create table estudios(
id_estudio int not null IDENTITY(1,1) Primary Key,
nombre nvarchar(110) not null,
nombre_documento nvarchar(255) null,
ruta_documento nvarchar(255) null,
fecha_inicio datetime not null,
fecha_finalizacion datetime null,
observacion nvarchar(255) null,
entregado bit not null,
numero_identificacion_funcionario nvarchar(15) not null Foreign Key References funcionarios(numero_identificacion),
id_tipo_estudio int not null Foreign Key References tipos_estudios(id_tipo_estudio)
);

create table experiencias_laborales(
id_experiencia_laboral int not null IDENTITY(1,1) Primary Key,
nombre_empresa nvarchar(110) not null,
fecha_inicio datetime not null,
fecha_finalizacion datetime null,
descripcion_puesto nvarchar(255) null,
numero_identificacion_funcionario nvarchar(15) not null Foreign Key References funcionarios(numero_identificacion),
);

create table habilidades_blandas(
id_habilidad_blanda int not null IDENTITY(1,1) Primary Key,
descripcion nvarchar(255) not null,
numero_identificacion_funcionario nvarchar(15) not null Foreign Key References funcionarios(numero_identificacion),
);

create table intereses_personales(
id_interes_personal int not null IDENTITY(1,1) Primary Key,
descripcion nvarchar(255) not null,
numero_identificacion_funcionario nvarchar(15) not null Foreign Key References funcionarios(numero_identificacion),
);

create table pensiones_o_embargos(
id_pension_o_embargo int not null IDENTITY(1,1) Primary Key,
ruta_documento nvarchar(255) not null,
nombre_documento nvarchar(255) not null,
fecha_ingreso datetime not null,
descripcion nvarchar(255) null,
numero_identificacion_funcionario nvarchar(15) not null Foreign Key References funcionarios(numero_identificacion),
);

create table tipo_acciones_de_personal(
id_tipo_accion_de_personal int not null IDENTITY(1,1) Primary Key,
nombre nvarchar(255) not null
);

create table acciones_de_personal(
id_accion_de_personal int not null IDENTITY(1,1) Primary Key,
nombre nvarchar(110) not null,
periodo datetime not null,
descripcion nvarchar(255) null,
ruta_documento nvarchar(255) null,
nombre_documento nvarchar(255) null,
numero_identificacion_funcionario nvarchar(15) not null Foreign Key References funcionarios(numero_identificacion),
id_tipo_accion_de_personal int not null Foreign Key References tipo_acciones_de_personal(id_tipo_accion_de_personal)
);

create table tipos_comprobantes(
id_tipo_comprobante int not null IDENTITY(1,1) Primary Key,
nombre nvarchar(255) not null
);

create table comprobante(
id_comprobante int not null IDENTITY(1,1) Primary Key,
fecha datetime not null,
descripcion nvarchar(255) null,
ruta_documento nvarchar(255) null,
nombre_documento nvarchar(255) null,
numero_identificacion_funcionario nvarchar(15) not null Foreign Key References funcionarios(numero_identificacion),
id_tipo_comprobante int not null Foreign Key References tipos_comprobantes(id_tipo_comprobante)
);

create table tipos_antecedentes(
id_tipo_antecedente int not null IDENTITY(1,1) Primary Key,
nombre nvarchar(255) not null
);

create table antecedentes(
id_antecedente int not null IDENTITY(1,1) Primary Key,
nombre nvarchar(110) not null,
descripcion nvarchar(255) null,
fecha datetime null,
ruta_documento nvarchar(255) null,
nombre_documento nvarchar(255) null,
numero_identificacion_funcionario nvarchar(15) not null Foreign Key References funcionarios(numero_identificacion),
id_tipo_antecedente int not null Foreign Key References tipos_antecedentes(id_tipo_antecedente)
);

create table suspensiones_o_permisos(
id_suspension_o_permiso int not null IDENTITY(1,1) Primary Key,
fecha_salida datetime not null,
fecha_regreso datetime null,
ruta_documento nvarchar(255) null,
nombre_documento nvarchar(255) null,
tipo int not null,
descripcion nvarchar(255) not null,
numero_identificacion_funcionario nvarchar(15) not null Foreign Key References funcionarios(numero_identificacion),
);

INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Primaria', 'estudio');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Secundaria incompleta', 'estudio');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Secundaria completa', 'estudio');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Diplomado', 'estudio');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Universidad incompleta', 'estudio');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Bachillerato', 'estudio');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Licenciatura', 'estudio');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Maestría', 'estudio');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Doctorado', 'estudio');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Curso', 'curso');
INSERT INTO tipos_estudios(nombre, clasificacion) VALUES('Certificado', 'certificado');

INSERT INTO categoria_laboral(nombre) VALUES('Técnico profesional A');
INSERT INTO categoria_laboral(nombre) VALUES('Técnico profesional B');

INSERT INTO seccion(nombre) VALUES('UCR');
INSERT INTO seccion(nombre) VALUES('Fundevi');

INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('AC', 'Área de Construcción');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('AM', 'Área de Meteorología');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('ATP', 'Área de Transportes y Pavimentos');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('UAT', 'Unidad Auditoria Técnica');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('UCTT', 'Unidad Centro de Transferencia Tecnológica');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('UGC', 'Unidad de Gestión de Calidad');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('UGERVN', 'Unidad Gestión y Evaluación de la Red Vial Nacional');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('UGM', 'Unidad Gestión Municipal');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('UIIT', 'Unidad de Investigación en Infraestructura y Transporte');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('UNAT', 'Unidad de Normativa y Actualización Técnica');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('UPBI', 'Unidad Proveeduría y Bienes Institucionales');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('USV', 'Unidad de Seguridad Vial');
INSERT INTO unidad_programa_laboratorio(sigla, nombre) VALUES('UTI', 'Unidad Tecnologías de Información');

INSERT INTO tipo_acciones_de_personal(nombre) VALUES('P1');
INSERT INTO tipo_acciones_de_personal(nombre) VALUES('P2');
INSERT INTO tipo_acciones_de_personal(nombre) VALUES('P3');
INSERT INTO tipo_acciones_de_personal(nombre) VALUES('P4');
INSERT INTO tipo_acciones_de_personal(nombre) VALUES('P5');
INSERT INTO tipo_acciones_de_personal(nombre) VALUES('P6');
INSERT INTO tipo_acciones_de_personal(nombre) VALUES('P7');
INSERT INTO tipo_acciones_de_personal(nombre) VALUES('P8');
INSERT INTO tipo_acciones_de_personal(nombre) VALUES('P9');

INSERT INTO tipos_comprobantes(nombre) VALUES('Tipo de comprobante 1');
INSERT INTO tipos_comprobantes(nombre) VALUES('Tipo de comprobante 2');
INSERT INTO tipos_comprobantes(nombre) VALUES('Tipo de comprobante 3');

INSERT INTO tipos_antecedentes(nombre) VALUES('Tipo de antecedente 1');
INSERT INTO tipos_antecedentes(nombre) VALUES('Tipo de antecedente 2');
INSERT INTO tipos_antecedentes(nombre) VALUES('Tipo de antecedente 3');


/*
use EDP;
drop table experiencias_laborales;
drop table estudios;
drop table tipos_estudios;
drop table antecedentes;
drop table tipos_antecedentes;
drop table comprobante;
drop table tipos_comprobantes;
drop table acciones_de_personal;
drop table tipo_acciones_de_personal;
drop table habilidades_blandas;
drop table intereses_personales;
drop table suspensiones_o_permisos;
drop table pensiones_o_embargos;
drop table funcionarios;
drop table categoria_laboral;
drop table seccion;
drop table unidad_programa_laboratorio;
*/