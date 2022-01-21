CREATE TABLE [dbo].[tblLocalidadesNegocio]
(
    [IdLocalidadNegocio] INT PRIMARY KEY identity(1,1), 
    [IdLocalidadNegocioPadre] INT , 
    [IdNegocio] INT, 
    [Codigo] VARCHAR(40) NOT NULL,  -- este codigo se usara tambien para ponerlo como prefijo en la codificacion
                                   -- de las transacciones 
    [NombreJuridico] VARCHAR(100) NOT NULL,
    [NombreComercial] VARCHAR(100) NOT NULL,
    [PrefijoPrestamo] VARCHAR(3),
    [PrefijoTransacciones] VARCHAR(3),
    TaxIdNacional VARCHAR(40),
	TaxIdLocalidad VARCHAR(40),
    [Activo] BIT NOT NULL DEFAULT 1, 
    [Bloqueado] bit not null default 0,
    PermitirOperaciones bit default 1 NOT NULL,
	InsertadoPor varchar(200) not null,
	FechaInsertado DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(200) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(200) NULL, 
    [FechaAnulado] DATETIME NULL, 
	[Logo] VARCHAR(max) NULL, 
    [OtrosDetalles] VARCHAR(max) NULL, 
    
    CONSTRAINT [FK_tblLocalidadNegocio_UQ_Codigo] Unique NonClustered(Codigo),
    
)

