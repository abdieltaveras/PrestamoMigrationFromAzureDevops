CREATE TABLE [dbo].[tblDirecciones]
(
	[IdDireccion] INT NOT NULL PRIMARY KEY identity(1,1), 
    [IdLocalidad] INT NULL, 
    	[IdLocalidadNegocio] INT NULL, 
    [IdNegocio] INT NULL, 
    [Calle] VARCHAR(50) NOT NULL, 
    [Detalles] VARCHAR(160) NULL, 
    [CoordenadasGPS] VARCHAR(50) NULL, 
    [CodigoPostal] VARCHAR(10) NULL,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [BorradoPor] VARCHAR(100) NULL, 
    [FechaBorrado] DATETIME NULL
)
