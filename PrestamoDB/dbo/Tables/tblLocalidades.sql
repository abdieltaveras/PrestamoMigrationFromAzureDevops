CREATE TABLE [dbo].[tblLocalidades]
(
	[IdLocalidad] INT NOT NULL PRIMARY KEY identity(1,1), 
    [IdLocalidadPadre] INT NULL, 
    [IdNegocio] INT NOT NULL, 
    [IdTipoLocalidad] INT NOT NULL, 
    [Nombre] VARCHAR(50) NOT NULL, 

)
