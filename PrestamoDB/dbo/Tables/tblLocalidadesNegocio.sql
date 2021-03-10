CREATE TABLE [dbo].[tblLocalidadesNegocio]
(
	[IdLocalidadNegocio] INT PRIMARY KEY identity(1,1), 
    [Codigo] VARCHAR(5) NOT NULL,  -- este codigo se usara tambien para ponerlo como prefijo en la codificacion
                                    -- de las transacciones 
    [Nombre] VARCHAR(100) NOT NULL,
    MunicipioOCiudad varchar(100) not null,
    Calle varchar(100) not null,
    Telefono varchar(100) not null,
    [PrefijoPrestamo] VARCHAR(3),
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
    IdTipoLocalidad int null,
    CONSTRAINT [FK_tblLocalidadNegocio_UQ_Codigo] Unique NonClustered(Codigo),
    CONSTRAINT [FK_tblLocalidadNegocio_UQ_PrefijoPrestamo] Unique NonClustered(PrefijoPrestamo),
)

