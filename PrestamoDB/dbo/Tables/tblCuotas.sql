
CREATE TABLE [dbo].[tblCuotas]
(	IdCuota INT  NOT NULL PRIMARY KEY identity(1,1),
	IdPrestamo INT references tblPrestamos(idPrestamo) not null,
	Numero int not null unique(IdPrestamo,Numero),
	Fecha Date not null,
	Capital Numeric(18,2) not null,
	Interes Numeric(18,2) not null,
	GastoDeCierre Numeric(18,2) not null,
    InteresDelGastoDeCierre Numeric(18,2) not null,
	OtrosCargosSinInteres Numeric(18,2) not null
)
	
