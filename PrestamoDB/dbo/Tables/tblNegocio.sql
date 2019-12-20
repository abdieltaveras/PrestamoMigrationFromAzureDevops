CREATE TABLE [dbo].[tblNegocios]
(
	[IdNegocio] INT PRIMARY KEY identity(1,1), 
    [Codigo] VARCHAR(20) NOT NULL unique default NEWID(), 
	[NombreJuridico] VARCHAR(100) NULL,
    [NombreComercial] VARCHAR(100) NOT NULL,
    [CorreoElectronico] varchar(100),
    [Activo] BIT NOT NULL DEFAULT 1, 
    [Bloqueado] bit not null default 0,
	[idNegocioPadre] int,
    TaxIdNo varchar(20) NOT null,
    OtrosDetalles varchar(100) NOT null,
	InsertadoPor varchar(100) not null,
	FechaInsertado DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL, 
	CONSTRAINT [FK_tblNegocio_UQ_TaxIdNo] Unique NonClustered(IdNegocio, TaxIdNo),
)

