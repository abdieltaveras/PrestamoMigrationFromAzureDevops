CREATE TABLE [dbo].[tblCodigosCargosDebitos] (
    [IdCodigoCargo]   INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]          VARCHAR (100) NOT NULL,
    [Descripcion]     VARCHAR (100) NOT NULL,
    [InsertadoPor]    VARCHAR (100) NOT NULL,
    [FechaInsertado]  DATETIME      NOT NULL,
    [ModificadoPor]   VARCHAR (100) NULL,
    [FechaModificado] DATETIME      NULL,
    [BorradoPor]      VARCHAR (100) NULL,
    [FechaBorrado]    DATETIME      NULL
);

