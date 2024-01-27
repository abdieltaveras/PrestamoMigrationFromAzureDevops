create table dbo.[tblCuotasMaestro] (
	[IdTransaccion] [int] primary key identity(1,1), 
	[IdPrestamo] [int] references tblPrestamos(idPrestamo),
	CodigoTransaccion varchar(10) not null,
	IdReferencia int,
	[Numero] [int] NULL,
	[Fecha] [DateTime] NULL,
	Monto numeric(12,4),
	Balance numeric,
	OtrosDetalles varchar(200)
)
