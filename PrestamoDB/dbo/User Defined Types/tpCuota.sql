CREATE TYPE tpCuota AS TABLE
(	IdPrestamo INT,
	IdCuota INT,
	Numero int,
	Fecha Date,
	Capital Numeric(18,2),
	Interes Numeric(18,2),
	GastoDeCierre Numeric(18,2),
    InteresDelGastoDeCierre Numeric(18,2),
	OtrosCargosSinInteres Numeric(18,2)
)
	
	