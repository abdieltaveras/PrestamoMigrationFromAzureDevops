CREATE TABLE [dbo].[tblTipoLocalidades]
(
	[IdTipoLocalidad] INT NOT NULL PRIMARY KEY identity(1,1),
    [HijoDe] INT NULL,
    [IdDivisionTerritorial] INT NULL,
    [IdNegocio] INT NOT NULL,
    [Descripcion] VARCHAR(50) NOT NULL,
    [PermiteCalle] BIT NOT NULL,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL
)