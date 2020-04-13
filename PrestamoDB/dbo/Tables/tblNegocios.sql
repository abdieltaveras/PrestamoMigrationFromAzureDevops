CREATE TABLE [dbo].[tblNegocios]
(
	[IdNegocio] INT PRIMARY KEY identity(1,1), 
    [Codigo] VARCHAR(50) NOT NULL default NEWID(), 
	[NombreJuridico] VARCHAR(100) NULL,
    [NombreComercial] VARCHAR(100) NOT NULL,
    [CorreoElectronico] varchar(100),
    [Activo] BIT NOT NULL DEFAULT 1, 
    [Bloqueado] bit not null default 0,
	[idNegocioPadre] int,
    TaxIdNo varchar(20) NOT null,
    OtrosDetalles varchar(100) NOT null,
	InsertadoPor varchar(200) not null,
	FechaInsertado DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(200) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(200) NULL, 
    [FechaAnulado] DATETIME NULL, 
	[Logo] VARCHAR(50) NULL, 
    CONSTRAINT [FK_tblNegocio_UQ_TaxIdNo] Unique NonClustered(IdNegocio, TaxIdNo),
    CONSTRAINT [FK_tblNegocio_UQ_Codigo] Unique NonClustered(Codigo),
)

