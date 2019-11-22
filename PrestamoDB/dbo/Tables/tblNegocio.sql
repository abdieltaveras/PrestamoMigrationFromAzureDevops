CREATE TABLE [dbo].[tblNegocios]
(
	[idNegocio] INT PRIMARY KEY identity(1,1), 
    [Codigo] VARCHAR(10) NOT NULL unique, 
	[NombreJuridico] VARCHAR(100) NOT NULL,
    [NombreComercial] VARCHAR(100) NOT NULL,
    [Activo] BIT NOT NULL DEFAULT 1, 
    [RequiereAutorizacion] bit NOT NULL DEFAULT 0,
	InsertadoPor varchar(100) not null,
	FechaInsertado DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL, 
)
