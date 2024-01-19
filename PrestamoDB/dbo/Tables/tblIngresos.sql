CREATE TABLE [dbo].[tblIngresos]
(
	IdIngreso INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    IdPrestamo INT NOT NULL, 
	IdLocalidadNegocio INT NULL, 
	Notransaccion varchar(15) not null,
    Fecha dateTime not null,
	Monto NUMERIC(18,2) NOT NULL,
	OtrosDatos varchar(200),
	DistribucionDelpago varchar(200) not null,
	[InsertadoPor] VARCHAR(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100), 
    [FechaModificado] DATETIME, 
    [AnuladoPor] VARCHAR(100), 
	[FechaAnulado] DATETIME2 
)
