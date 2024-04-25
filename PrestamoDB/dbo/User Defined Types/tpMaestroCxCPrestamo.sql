CREATE TYPE [dbo].[tpMaestroCxCPrestamo] AS TABLE(
	[IdTransaccion] [int] NULL,
	[IdPrestamo] [int] NULL,
	[CodigoTipoTransaccion] [varchar](10) NULL,
	[IdReferencia] [uniqueidentifier],
	[NumeroTransaccion] [int] NULL,
	[Fecha] [datetime] NULL,
	[Monto] [numeric](18, 2) NULL,
	[Balance] [numeric](18, 2) NULL,
	[OtrosDetallesJson] [varchar](500) NULL,
	[TipoDrCr] [char](1) NULL
)
