﻿CREATE TABLE [dbo].[tblCodeudores]
(
	[IdCodeudor] INT NOT NULL PRIMARY KEY identity(1,1), 
	[IdStatus] INT NULL,
	[IdNegocio] INT NOT NULL, 
	[IdLocalidadNegocio] INT NOT NULL, 
	[Activo] bit default(1) not null,
	[Apodo] varchar(100),
	[Apellidos] VARCHAR(100) NOT NULL,
	Codigo varchar(40) default NEWID(),
	[IdEstadoCivil] int not null,
	[FechaNacimiento] DateTime not null,
	[IdTipoIdentificacion] INT NOT NULL, 
	[IdTipoProfesionUOcupacion] INT NOT NULL, 
	[InfoLaboral] VARCHAR(max),
	[InfoDireccion] VARCHAR(max),
	[NoIdentificacion] varchar(20),
	[Nombres] VARCHAR(100) NOT NULL,
	[IdSexo] int not null,
	[TelefonoCasa] VARCHAR(20) NOT NULL,
	[TelefonoMovil] VARCHAR(20) NOT NULL,
    [CorreoElectronico] VARCHAR(30) NOT NULL, 
	Imagen1FileName varchar(50),
	Imagen2FileName varchar(50),
	Imagenes varchar(max),
    [InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
	[ModificadoPor] VARCHAR(100) NULL, 
	[FechaModificado] DATETIME NULL, 
	[BorradoPor] VARCHAR(100) NULL, 
	[FechaBorrado] DATETIME NULL, 
    CONSTRAINT [FK_tblCodeudor_ToTblNegocios] FOREIGN KEY (IdNegocio) REFERENCES tblNegocios([IdNegocio]),
	CONSTRAINT [FK_tblCodeudor_UQ_IdentificationNumber] Unique NonClustered (idNegocio, IdTipoIdentificacion,NoIdentificacion),
	CONSTRAINT [FK_tblCodeudor_UQ_Codigo] Unique NonClustered(IdNegocio, Codigo),
	CONSTRAINT [CK_NombreCodeudor_not_empty_string] CHECK  ((Nombres<>N''))
)
	
