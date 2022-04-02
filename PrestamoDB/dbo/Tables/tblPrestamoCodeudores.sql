CREATE TABLE [dbo].[tblPrestamoCodeudores]
(
	[IdPrestamo] INT foreign key references tblPrestamos(IdPrestamo)NOT NULL,
	[IdCodeudor] INT foreign key references tblCodeudores(IdCodeudor) NOT NULL,
		[IdLocalidadNegocio] INT NULL, 
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [BorradoPor] VARCHAR(100) NULL, 
    [FechaBorrado] DATETIME NULL
)
