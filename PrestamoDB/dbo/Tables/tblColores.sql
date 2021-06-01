CREATE TABLE [dbo].[tblColores]
(
	[IdColor]INT NOT NULL PRIMARY KEY  identity(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[IdNegocio] INT NOT NULL,
		[IdLocalidadNegocio] INT  NULL,
	[Codigo] VARCHAR(10) NULL,
    [Activo] BIT NOT NULL DEFAULT 1,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL,
	constraint NotEmptyNombreColor CHECK (Nombre <> ''),
	constraint UniqueNombreColor Unique(Nombre),
	--constraint UniqueCodigoColor Unique(Codigo),
)
