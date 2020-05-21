CREATE TABLE [dbo].[tblPrestamoGarantias]
(
	[IdPrestamo] INT NOT NULL references tblPrestamos(IdPrestamo),
	[IdGarantia] INT NOT NULL references tblGarantias(IdGarantia),
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL
)
