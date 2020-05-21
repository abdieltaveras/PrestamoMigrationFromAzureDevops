﻿CREATE TABLE [dbo].[tblClasificaciones]
(
	[IdClasificacion]INT NOT NULL PRIMARY KEY  identity(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[IdNegocio] INT NOT NULL,
	ClasificacionFinanciera varchar(3),
	[Codigo] VARCHAR(10) NOT NULL default '' unique (idNegocio, codigo),
    [Activo] BIT NOT NULL DEFAULT 1,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL
)
