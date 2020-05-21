CREATE TYPE tpCuota AS TABLE
(	IdPrestamo INT,
	IdCuota INT,
	Numero int,
	Fecha Date,
	Capital Numeric(18,6),
	Interes Numeric(18,6)
)
	
	