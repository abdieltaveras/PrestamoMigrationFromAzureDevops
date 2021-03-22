CREATE TABLE [dbo].[tblLocalidades]
(
	[IdLocalidad] INT NOT NULL PRIMARY KEY identity(1,1), 
    [IdLocalidadPadre] INT NULL, 
    [IdNegocio] INT NOT NULL, 
    [IdTipoLocalidad] INT NOT NULL, 
    [Nombre] VARCHAR(50) NOT NULL,
	[Codigo] VARCHAR(10) NULL,
    [Activo] BIT NOT NULL DEFAULT 1,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL

)
