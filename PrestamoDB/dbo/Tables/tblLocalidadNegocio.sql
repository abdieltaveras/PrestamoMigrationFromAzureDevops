CREATE TABLE [dbo].[tblLocalidadNegocio]
(
	[IdLocalidad] INT PRIMARY KEY identity(1,1), 
    [Codigo] VARCHAR(50) NOT NULL default NEWID(), 
    [Nombre] VARCHAR(100) NOT NULL,
    [PrefijoPrestamo] VARCHAR(3) NOT NULL,
    [CorreoElectronico] varchar(100),
    [Activo] BIT NOT NULL DEFAULT 1, 
    [Bloqueado] bit not null default 0,
	[idNegocio] int,
	InsertadoPor varchar(200) not null,
	FechaInsertado DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(200) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(200) NULL, 
    [FechaAnulado] DATETIME NULL, 
	[Logo] VARCHAR(50) NULL, 
    CONSTRAINT [FK_tblLocalidadNegocio_UQ_Codigo] Unique NonClustered(Codigo),
    CONSTRAINT [FK_tblLocalidadNegocio_UQ_PrefijoPrestamo] Unique NonClustered(PrefijoPrestamo),
)

