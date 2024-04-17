CREATE TABLE [dbo].[tblMaestrosCxCPrestamo] (
    [IdTransaccion]         INT              IDENTITY (1, 1) NOT NULL,
    [IdPrestamo]            INT              NULL,
    [TipoDrCr]              CHAR (1)         NULL,
    [CodigoTipoTransaccion] VARCHAR (10)     NULL,
    [NumeroTransaccion]     INT              NULL,
    [Fecha]                 DATETIME         NULL,
    [Monto]                 NUMERIC (18, 2)  NULL,
    [Balance]               NUMERIC (18, 2)  NULL,
    [OtrosDetallesJson]     VARCHAR (200)    NULL,
    [DetallesCargosJson]    VARCHAR (1000)   NULL,
    PRIMARY KEY CLUSTERED ([IdTransaccion] ASC)
);

