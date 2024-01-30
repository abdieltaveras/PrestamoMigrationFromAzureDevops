create TYPE [dbo].[tpMaestroCxCPrestamo] AS TABLE(
	[DetallesCargosJson] [varchar](1000),
	[IdTransaccion] [int] null, 
	[IdPrestamo] [int] NULL,
	CodigoTipoTransaccion varchar(10),
	IdReferencia varchar(50),	
	[NumeroTransaccion] [int] NULL,
	[Fecha] [DateTime] NULL,
	Monto numeric(18,2),
	Balance numeric(18,2),
	[OtrosDetallesJson] [varchar](500) NULL,
	TipoDrCr char
)
