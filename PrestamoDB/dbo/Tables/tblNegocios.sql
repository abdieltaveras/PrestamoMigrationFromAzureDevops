CREATE TABLE [dbo].[tblNegocios] (
    [IdNegocio]          INT           IDENTITY (1, 1) NOT NULL,
    [Codigo]             VARCHAR (50)  NOT NULL,
    [NombreJuridico]     VARCHAR (100) NULL,
    [NombreComercial]    VARCHAR (100) NOT NULL,
    [CorreoElectronico]  VARCHAR (100) NULL,
    [Activo]             BIT           DEFAULT ((1)) NOT NULL,
    [Bloqueado]          BIT           DEFAULT ((0)) NOT NULL,
    [idNegocioPadre]     INT           NULL,
    [TaxIdNo]            VARCHAR (20)  NOT NULL,
    [OtrosDetalles]      VARCHAR (100) NOT NULL,
    [InsertadoPor]       VARCHAR (200) NOT NULL,
    [FechaInsertado]     DATETIME      DEFAULT (getdate()) NOT NULL,
    [ModificadoPor]      VARCHAR (200) NULL,
    [FechaModificado]    DATETIME      NULL,
    [BorradoPor]         VARCHAR (200) NULL,
    [FechaBorrado]       DATETIME      NULL,
    [Logo]               VARCHAR (50)  NULL,


    PRIMARY KEY CLUSTERED ([IdNegocio] ASC),
    CONSTRAINT [FK_tblNegocio_UQ_Codigo] UNIQUE NONCLUSTERED ([Codigo] ASC),
    CONSTRAINT [FK_tblNegocio_UQ_TaxIdNo] UNIQUE NONCLUSTERED ([IdNegocio] ASC, [TaxIdNo] ASC)
);



