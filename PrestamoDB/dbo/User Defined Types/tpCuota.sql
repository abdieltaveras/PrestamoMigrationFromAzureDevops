CREATE TYPE tpCuota AS TABLE
(	IdPrestamo INT,
	IdCuota INT,
	IdNumero NUMERIC (18,6),
	Fecha Date,
	Capital Numeric(18,6),
	Interes Numeric(18,6)
)
	
	