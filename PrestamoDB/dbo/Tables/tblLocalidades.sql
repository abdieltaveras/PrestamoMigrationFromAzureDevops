CREATE TABLE [dbo].[tblLocalidades] (
    [IdLocalidad]           INT           IDENTITY (1, 1) NOT NULL,
    [IdLocalidadPadre]      INT           NULL,
    [IdNegocio]             INT           NOT NULL,
    [IdLocalidadNegocio]    INT           NULL,
    [IdDivisionTerritorial] INT           NULL,
    [Nombre]                VARCHAR (50)  NOT NULL,
    [Codigo]                VARCHAR (10)  NULL,
    [Activo]                BIT           CONSTRAINT [DF__tmp_ms_xx__Activ__178D7CA5] DEFAULT ((1)) NOT NULL,
    [InsertadoPor]          VARCHAR (100) NOT NULL,
    [FechaInsertado]        DATETIME      CONSTRAINT [DF__tmp_ms_xx__Fecha__1881A0DE] DEFAULT (getdate()) NOT NULL,
    [ModificadoPor]         VARCHAR (100) NULL,
    [FechaModificado]       DATETIME      NULL,
    [BorradoPor]            VARCHAR (100) NULL,
    [FechaBorrado]          DATETIME      NULL,
    CONSTRAINT [PK__tmp_ms_x__27432612067DF6F8] PRIMARY KEY CLUSTERED ([IdLocalidad] ASC)
);






