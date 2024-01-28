create TYPE [dbo].[tpMaestroCxCPrestamo] AS TABLE(
	[IdTransaccion] [int] null, 
	[IdPrestamo] [int] NULL,
	TipoDrCr char,
	CodigoTipoTransaccion varchar(10),
	IdReferencia varchar(30),
	[NumeroTransaccion] [int] NULL,
	[Fecha] [DateTime] NULL,
	Monto numeric(12,4),
	Balance numeric(12,2),
	[OtrosDetallesJson] [varchar](200) NULL,
	[DetallesCargosJson] [varchar](1000)
)
