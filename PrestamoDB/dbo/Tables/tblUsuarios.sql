CREATE TABLE [dbo].[tblUsuarios]
(
	[IdUsuario] INT NOT NULL PRIMARY KEY Identity(1,1),
	[IdNegocio] INT not null, 
	LoginName varchar(50) not null,
	NombreRealCompleto varchar(50) not null,
	Contraseña varchar(50) not null, 
    [DebeCambiarContraseña] BIT NOT NULL,
    [FechaExpiracionContraseña] NCHAR(10) NULl,
    [Telefono1] VARCHAR(50) NOT NULL, 
    [Telefono2] VARCHAR(50) NULL, 
    [Activo] BIT NOT NULL, 
    [Bloqueado] BIT NOT NULL, 
    [CorreoElectronico] VARCHAR(50) NOT NULL, 
    [EsEmpleado] BIT NOT NULL, 
    IdPersonal int,
    InsertadoPor varchar(100) NOT null,
	FechaInsertado DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL, 
    
)
