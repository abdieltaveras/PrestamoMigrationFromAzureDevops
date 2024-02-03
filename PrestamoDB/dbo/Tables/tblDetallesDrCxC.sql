create table tblDetallesDrCxC
(
IdTransaccion int primary key identity(1,1),
idTransaccionMaestro int, --luego tendra que tener un references
CodigoCargo varchar(5),
Monto Numeric(18,2),
Balance Numeric (18,2)
)
