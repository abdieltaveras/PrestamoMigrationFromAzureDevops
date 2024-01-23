Create table tblDetalleOtrasCxC
(
	idDetallesOtrasCxC int primary key identity(1,1),
	DocFuente  varchar(30),
	Fecha Datetime,  
	IdDocFuente int,
	IdCargo varchar(15), 
	MontoOriginal decimal,
	Balance decimal,
	Descripcion varchar(100),
)
