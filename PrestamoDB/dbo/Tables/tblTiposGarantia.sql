CREATE TABLE [dbo].[tblTiposGarantia]
(
	[IdTipoGarantia]INT NOT NULL PRIMARY KEY  identity(1,1),
	[IdClasificacion] INT NOT NULL,
	[Nombre] NVARCHAR(50) NOT NULL,
	[Codigo] VARCHAR(10) NOT NULL default '',
    [Activo] BIT NOT NULL DEFAULT 1,
	[IdNegocio] INT NOT NULL,
	[IdLocalidadNegocio] INT NOT NULL,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [BorradoPor] VARCHAR(100) NULL, 
    [FechaBorrado] DATETIME NULL
)
