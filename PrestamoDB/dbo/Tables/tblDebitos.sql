Create Table tblDebitos 
(
	idDebito int primary key identity(1,1),
	IdPrestamo int references tblPrestamos(idPrestamo),
	idLocalidadNegocio int references TblLocalidadesNegocio(idLocalidadNegocio),
	Notransaccion varchar(15) not null,
	Fecha Datetime not null,
	Monto numeric(12,2) not null,
	Descripcion varchar(max) not null,
	InsertadoPor varchar(30) not null,
	FechaInsertado DateTime not null default getdate(), 
	ModificadoPor varchar(30),
	FechaModificado Datetime,
	AnuladoPor varchar(30),
	FechaAnulado Datetime
)
-- analizaremos si para Moras, CargoInteres, vamos a crear una tabla madre
-- lo determinaran las pruebas que hagamos