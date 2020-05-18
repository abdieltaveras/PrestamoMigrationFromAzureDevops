CREATE PROCEDURE [dbo].[spGetPrestamoConDetalle]
(
	@idPrestamo int
)
as
begin
SELECT Pres.IdPrestamo, pres.idNegocio, pres.idCliente, prestamoNumero, FechaEmisionReal, FechaVencimiento, IdTipoAmortizacion, pres.idClasificacion, pres.TotalPrestado,
	clie.Nombres, clie.Apellidos, clie.IdTipoIdentificacion, clie.NoIdentificacion, clie.TelefonoCasa, clie.TelefonoMovil, clie.Imagen1FileName, clie.Imagen2FileName,
	peri.Nombre,
	preGara.IdGarantia,
	clas.Nombre,
	Gara.IdClasificacion, gara.NoIdentificacion, gara.IdMarca, gara.IdModelo, gara.Detalles
	FROM	dbo.tblPrestamos  pres
	inner Join tblClientes clie on clie.idCliente = pres.IdCliente
	inner Join tblPeriodos peri on  peri.idPeriodo = pres.idPeriodo
	inner join tblPrestamoGarantias preGara on preGara.IdPrestamo = pres.IdPrestamo
	inner join tblGarantias gara on gara.IdGarantia = preGara.Idgarantia
	inner join tblClasificaciones  clas on clas.IdClasificacion = pres.idClasificacion
	where pres.IdPrestamo = @IdPrestamo	

	select  Fecha, Capital, Interes, IdCuota, Numero from tblCuotas where IdPrestamo = @IdPrestamo

End
