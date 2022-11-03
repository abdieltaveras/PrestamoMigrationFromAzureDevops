CREATE TABLE [dbo].[tblLocalizadores] (
    [IdLocalizador]      INT            IDENTITY (1, 1) NOT NULL,
    [IdLocalidadNegocio] INT            NULL,
    [Codigo]             VARCHAR (20)   CONSTRAINT [DF__tmp_ms_xx__Codig__1B5E0D89] DEFAULT ('') NOT NULL,
    [Nombre]             NVARCHAR (50)  NOT NULL,
    [Apellidos]          VARCHAR (100)  NULL,
    [Direccion]          VARCHAR (1000) NULL,
    [Telefonos]          VARCHAR (400)  NULL,
    [IdNegocio]          INT            NOT NULL,
    [Activo]             BIT            CONSTRAINT [DF__tmp_ms_xx__Activ__1C5231C2] DEFAULT ((1)) NOT NULL,
    [InsertadoPor]       VARCHAR (100)  NOT NULL,
    [FechaInsertado]     DATETIME       CONSTRAINT [DF__tmp_ms_xx__Fecha__1D4655FB] DEFAULT (getdate()) NOT NULL,
    [ModificadoPor]      VARCHAR (100)  NULL,
    [FechaModificado]    DATETIME       NULL,
    [BorradoPor]         VARCHAR (100)  NULL,
    [FechaBorrado]       DATETIME       NULL,
    CONSTRAINT [PK__tmp_ms_x__CCB887664CF6998C] PRIMARY KEY CLUSTERED ([IdLocalizador] ASC)
);


