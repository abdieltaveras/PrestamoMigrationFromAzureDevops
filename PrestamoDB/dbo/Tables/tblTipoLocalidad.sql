CREATE TABLE [dbo].[tblTipoLocalidad]
(
	[IdTipoLocalidad] INT NOT NULL PRIMARY KEY identity(1,1), 
    [PadreDe] INT NULL, 
    [IdNegocio] INT NOT NULL, 
    [Descripcion] VARCHAR(50) NOT NULL
)
