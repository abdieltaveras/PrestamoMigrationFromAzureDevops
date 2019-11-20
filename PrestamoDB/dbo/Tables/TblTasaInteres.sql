CREATE TABLE [dbo].[tblTasaInteres]
(
	[idTasaInteres] INT NOT NULL PRIMARY KEY identity(1,1), 
	[idNegocio] INT NOT NULL, 
    [Codigo] VARCHAR(10) NOT NULL unique, 
	[Descripcion] VARCHAR(100) NOT NULL,
    [InteresMensual] DECIMAL(9, 6) NOT NULL, 
    [Activo] BIT NOT NULL DEFAULT 1, 
    [RequiereAutorizacion] NCHAR(10) NOT NULL DEFAULT 0,
	InsertadoPor varchar(100) not null,
	FechaInsertado DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [BorradoPor] VARCHAR(100) NULL, 
    [FechaBorrado] DATETIME NULL
)
