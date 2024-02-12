﻿CREATE TABLE [dbo].[tblTiposCargosDebito]
(
	[IdTipoCargo]INT NOT NULL PRIMARY KEY  identity(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[IdNegocio] INT NOT NULL,
		[IdLocalidadNegocio] INT  NULL,
	[Codigo] VARCHAR(10) NULL,
    [Activo] BIT NOT NULL DEFAULT 1,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [BorradoPor] VARCHAR(100) NULL, 
    [FechaBorrado] DATETIME NULL,
	constraint NotEmptyNombreTipoCargo CHECK (Nombre <> ''),
	constraint UniqueTipoCargoNombre Unique(idnegocio,Nombre),
	--constraint UniqueCodigoColor Unique(Codigo),
)
