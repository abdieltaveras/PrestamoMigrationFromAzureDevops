create type tpDetalleDrCxC
as table
(
	IdTransaccion int,
	IdTransaccionMaestro int,
	IdReferenciaMaestro uniqueIdentifier,
	IdReferenciaDetalle uniqueIdentifier,
	CodigoCargo varchar(5),
	Monto Numeric(18,2),
	Balance Numeric (18,2)
)

