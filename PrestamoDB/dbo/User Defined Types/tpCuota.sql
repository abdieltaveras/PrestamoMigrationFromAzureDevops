CREATE TYPE tpCuota AS TABLE
(	
	IdCuota INT,
	IdPrestamo INT,
	Numero int,
	Fecha Date,
	Capital Numeric(18,2) null,
	Interes Numeric(18,2) null,
	GastoDeCierre Numeric(18,2) null,
    InteresDelGastoDeCierre Numeric(18,2) null,
	idTipoCargo int 
)
	
	