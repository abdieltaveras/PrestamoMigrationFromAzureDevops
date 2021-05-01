CREATE TABLE [dbo].[tblDivisionTerritorial]
(
	[IdDivisionTerritorial] INT NOT NULL PRIMARY KEY identity(1,1),
    [IdLocalidadPadre] INT NULL,
    [IdDivisionTerritorialPadre] INT NULL,
    [IdNegocio] INT NOT NULL,
	[Nombre] VARCHAR(100) NOT NULL,
	[Codigo] VARCHAR(10) NOT NULL default '',
    [Activo] BIT NOT NULL DEFAULT 1,
    [PermiteCalle] BIT NOT NULL,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL
)