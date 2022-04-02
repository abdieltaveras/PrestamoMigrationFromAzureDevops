CREATE TABLE [dbo].[tblIngresos]
(
	[IdIngreso] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [IdPrestamo] INT NOT NULL, 
    [IdCuota] INT NOT NULL, 
	[IdLocalidadNegocio] INT NULL, 
    [Num_Cuota] NUMERIC(7, 3) NOT NULL,
	[Monto_Original_Cuota] NUMERIC(18,2) NOT NULL,
	[Monto_Abonado] NUMERIC(18,2) NOT NULL,
	[Balance] NUMERIC(18,2) NOT NULL,
	[InsertadoPor] VARCHAR(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [BorradoPor] VARCHAR(100) NULL, 
)
