CREATE TABLE [dbo].[tblPrestamoGarantias]
(
	[IdPrestamo] INT NOT NULL references tblPrestamos(IdPrestamo),
	[IdGarantia] INT NOT NULL references tblGarantias(IdGarantia),
		[IdLocalidadNegocio] INT NULL, 
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [BorradoPor] VARCHAR(100) NULL, 
    [FechaBorrado] DATETIME NULL
)
