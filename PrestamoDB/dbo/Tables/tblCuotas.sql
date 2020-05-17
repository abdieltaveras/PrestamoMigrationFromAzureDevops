
CREATE TABLE [dbo].[tblCuotas]
(	IdCuota INT  NOT NULL PRIMARY KEY identity(1,1),
	IdPrestamo INT references tblPrestamos(idPrestamo) not null,
	Numero int not null unique,
	Fecha Date not null,
	Capital Numeric(18,6) not null,
	Interes Numeric(18,6) not null,
)
	
