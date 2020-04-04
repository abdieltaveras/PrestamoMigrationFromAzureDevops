﻿CREATE TABLE [dbo].[tblEquipos]
(
	[IdEquipo]INT NOT NULL PRIMARY KEY  identity(1,1),
	[IdNegocio] INT NOT NULL ,
	[Codigo] varchar (40), 
	[Nombre] VARCHAR(50) NOT NULL,
	[Descripcion] varchar(200),
	[UltimoAcceso] datetime, 
	[AccesadoPor] varchar(50),
	[FechaConfirmado] datetime,
	[ConfirmadoPor] varchar(100),
	[FechaBloqueado] datetime,
	[BloqueadoPor] varchar(100),
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL,
	CONSTRAINT [FK_tblEquipo_ToTblNegocios] FOREIGN KEY (IdNegocio) REFERENCES tblNegocios([IdNegocio]),
	CONSTRAINT [FK_tblEquipo_UQ_Codigo] Unique NonClustered(Codigo)
)