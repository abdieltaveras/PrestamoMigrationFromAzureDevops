create TYPE [dbo].[tpCuotaMaestro] AS TABLE(
	[IdTransaccion] [int] null, 
	[IdPrestamo] [int] NULL,
	CodigoTransaccion varchar(10),
	NombreTransaccion varchar(30),
	IdReferencia varchar(30),
	[Numero] [int] NULL,
	[Fecha] [DateTime] NULL,
	Monto numeric(12,4),
	Balance numeric(12,2),
	OtrosDetalles varchar(200)
)
