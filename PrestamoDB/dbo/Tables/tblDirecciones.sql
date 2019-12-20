CREATE TABLE [dbo].[tblDirecciones]
(
	[IdDireccion] INT NOT NULL PRIMARY KEY identity(1,1), 
    [IdLocalidad] INT NULL, 
    [IdNegocio] INT NULL, 
    [Calle] VARCHAR(50) NOT NULL, 
    [Detalles] VARCHAR(160) NULL, 
    [CoordenadasGPS] VARCHAR(50) NULL, 
    [CodigoPostal] VARCHAR(10) NULL,
)
