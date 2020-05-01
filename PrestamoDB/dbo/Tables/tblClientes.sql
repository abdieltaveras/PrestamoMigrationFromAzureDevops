﻿CREATE TABLE [dbo].[tblClientes]
(
	[IdCliente] INT NOT NULL PRIMARY KEY identity(1,1), 
	[IdNegocio] INT NOT NULL, 
	[Activo] bit default(1) not null,
	[AnuladoPor] VARCHAR(100) NULL, 
	[Apodo] varchar(100),
	[Apellidos] VARCHAR(100) NOT NULL,
	Codigo varchar(40) default NEWID(),
	[EstadoCivil] int not null,
	[FechaNacimiento] DateTime not null,
	[FechaModificado] DATETIME NULL, 
	[FechaInsertado] DateTime not null default getdate(), 
	[FechaAnulado] DATETIME NULL, 
	
	[IdTipoIdentificacion] INT NOT NULL, 
	[IdTipoProfesionUOcupacion] INT NOT NULL, 
	[InfoConyuge] VARCHAR(400),
	[InfoLaboral] VARCHAR(400),
	[InfoDireccion] VARCHAR(400),
	[InsertadoPor] varchar(100) not null,
	[ModificadoPor] VARCHAR(100) NULL, 
	[NoIdentificacion] varchar(20),
	[Nombres] VARCHAR(100) NOT NULL,
	[Sexo] int not null,
	[TelefonoCasa] VARCHAR(20) NOT NULL,
	[TelefonoMovil] VARCHAR(20) NOT NULL,
    [CorreoElectronico] VARCHAR(30) NOT NULL,
	Imagen1FileName varchar(50),
	Imagen2FileName varchar(50),
	[InfoReferencia] VARCHAR(4000),

    CONSTRAINT [FK_tblCliente_ToTblNegocios] FOREIGN KEY (IdNegocio) REFERENCES tblNegocios([IdNegocio]),
	CONSTRAINT [FK_tblCliente_UQ_IdentificationNumber] Unique NonClustered(IdNegocio, IdTipoIdentificacion,NoIdentificacion),
	CONSTRAINT [FK_tblCliente_UQ_Codigo] Unique NonClustered(IdNegocio, Codigo),
	CONSTRAINT [CK_Nombres_not_empty_string] CHECK  ((Nombres<>N''))
)
	
