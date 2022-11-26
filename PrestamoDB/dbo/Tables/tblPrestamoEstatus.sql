CREATE TABLE [dbo].[tblPrestamoEstatus] (
    [IdPrestamoEstatus]  INT           IDENTITY (1, 1) NOT NULL,
    [IdPrestamo]         INT           NOT NULL,
    [IdEstatus]          INT           NOT NULL,
    [Comentario]         VARCHAR (100) NULL,
    [IdNegocio]          INT           NOT NULL,
    [IdLocalidadNegocio] INT           NOT NULL,
    [Activo]             INT           NOT NULL,
    [InsertadoPor]       VARCHAR (100) NOT NULL,
    [FechaInsertado]     DATETIME      NOT NULL,
    [ModificadoPor]      VARCHAR (100) NULL,
    [FechaModificado]    DATETIME      NULL,
    [BorradoPor]         VARCHAR (100) NULL,
    [FechaBorrado]       DATETIME      NULL,
    CONSTRAINT [PK_PrestamoEstatus] PRIMARY KEY CLUSTERED ([IdPrestamoEstatus] ASC)
);

