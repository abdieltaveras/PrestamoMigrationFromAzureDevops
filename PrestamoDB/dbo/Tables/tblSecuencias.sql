create table tblSecuencias
(
	Nombre varchar(100) unique not null,
	IdNegocio int not null,
	Contador int default 0 not null
)
