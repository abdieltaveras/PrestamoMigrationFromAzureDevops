﻿CREATE TABLE [dbo].[tblMaestrosCxCPrestamo](
	[IdTransaccion] [int] primary key IDENTITY(1,1),
	[IdPrestamo] [int] references tblPrestamos(IdPrestamo),
	--[IdNegocio] int references tblNegocios(idNegocio),
	--[IdLocalidadNegocio] int references tblLocalidadesNegocio(idLocalidadNegocio),
    TipoDrCr char,
	[CodigoTipoTransaccion] [varchar](10),
	[IdReferencia] [int],
	[NumeroTransaccion] [int],
	[Fecha] [datetime],
	[Monto] [numeric](18, 2),
	[Balance] [numeric](18, 2) ,
	[OtrosDetallesJson] [varchar](200) NULL,
	[DetallesCargosJson] [varchar](1000),
)