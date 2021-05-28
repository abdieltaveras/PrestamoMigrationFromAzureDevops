
CREATE TABLE [dbo].[tblCuotas]
(	IdCuota INT  NOT NULL PRIMARY KEY identity(1,1),
	IdPrestamo INT references tblPrestamos(idPrestamo) not null,
	Numero numeric(7,3) not null unique(IdPrestamo,Numero),
	Fecha Date not null,
	Capital Numeric(18,2) not null,
	BceCapital Numeric(18,2) not null,
	Interes Numeric(18,2) null,
	BceInteres Numeric(18,2) null,
	GastoDeCierre Numeric(18,2) null,
	BceGastoDeCierre Numeric(18,2) null,
    InteresDelGastoDeCierre Numeric(18,2) null ,
	BceInteresDelGastoDeCierre Numeric(18,2) null,
)
	
