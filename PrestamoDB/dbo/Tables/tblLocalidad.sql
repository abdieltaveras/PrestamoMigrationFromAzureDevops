﻿CREATE TABLE [dbo].[tblLocalidad]
(
	[IdLocalidad] INT NOT NULL PRIMARY KEY identity(1,1), 
    [IdLocalidadPadre] INT NULL, 
    [IdNegocio] INT NOT NULL, 
    [IdTipoLocalidad] INT NOT NULL, 
    [Nombre] VARCHAR(50) NOT NULL, 
    [PermiteCalle] BIT NOT NULL,

)
