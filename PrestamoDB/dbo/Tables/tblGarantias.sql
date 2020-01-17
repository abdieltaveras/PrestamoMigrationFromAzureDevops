CREATE TABLE [dbo].[tblGarantias]
(
	[IdGarantia] INT NOT NULL PRIMARY KEY  identity(1,1), 
    [IdClasificacion] INT NOT NULL, 
    [IdTipo] INT NULL, 
    [IdModelo] INT NULL,
	IdMarca INT NULL,
    [NoIdentificacion] NVARCHAR(50) NOT NULL, 
    [IdNegocio] INT NOT NULL,
    [Detalles] VARCHAR(4000) NULL, 
    
)
