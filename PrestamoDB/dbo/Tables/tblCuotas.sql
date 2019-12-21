
CREATE TABLE [dbo].[tblCuotas]
(	IdCuota INT  NOT NULL PRIMARY KEY identity(1,1),
	IdPrestamo INT not null,
	IdNumero NUMERIC not null,
	Fecha Date not null,
	Capital Numeric(18,6) not null,
	Interes Numeric(18,6) not null,
)
	
