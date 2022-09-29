CREATE TABLE [dbo].[tblEntidadEstatus] (
    [IdEntidadEstatus]             INT           IDENTITY (1, 1) NOT NULL,
    [Name]                         VARCHAR (50)  NOT NULL,
    [Description]                  VARCHAR (100) NULL,
    [IsNotPrintOnReport]           BIT           NOT NULL,
    [IsImpedirPagoEnCaja]          BIT           NOT NULL,
    [IsRequiereAutorizacionEnCaja] BIT           NOT NULL,
    [IsActivo]                     BIT           NOT NULL,
    [IsImpedirHacerPrestamo]       BIT           NOT NULL,
    CONSTRAINT [PK_EntidadEstatus] PRIMARY KEY CLUSTERED ([IdEntidadEstatus] ASC)
);

