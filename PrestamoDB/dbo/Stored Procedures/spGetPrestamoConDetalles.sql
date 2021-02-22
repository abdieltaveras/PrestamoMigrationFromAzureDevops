CREATE PROCEDURE [dbo].[spGetPrestamoConDetalle]
(
	@idPrestamo int
)
as
begin
SELECT Pres.IdPrestamo, pres.idNegocio, pres.idCliente, prestamoNumero, FechaEmisionReal, FechaVencimiento,					
	pres.IdTipoAmortizacion, pres.idClasificacion, pres.TotalPrestado, pres.idTipoMora,
	clie.Codigo as CodigoCliente,clie.InfoLaboral, clie.Nombres, clie.Apellidos, clie.IdTipoIdentificacion , clie.NoIdentificacion as NumeracionDocumentoIdentidad, clie.TelefonoCasa, clie.TelefonoMovil, clie.Imagen1FileName, clie.Imagen2FileName, clie.Activo,
	peri.Nombre as NombrePeriodo,
	clas.Nombre as NombreClasificacion,
	mora.Nombre as NombreTipoMora
	FROM	dbo.tblPrestamos  pres
	inner Join tblClientes clie on clie.idCliente = pres.IdCliente
	inner Join tblPeriodos peri on  peri.idPeriodo = pres.idPeriodo
	--si falla algo en el join no se genera nada y esto esta mas abajo
	--inner join tblPrestamoGarantias preGara on preGara.IdPrestamo = pres.IdPrestamo
	--inner join tblGarantias gara on gara.IdGarantia = preGara.Idgarantia
	inner join tblClasificaciones  clas on clas.IdClasificacion = pres.idClasificacion
	inner join tblTiposMora mora on mora.idTipoMora = pres.idTipoMora
	where pres.IdPrestamo = @IdPrestamo	
	select  Fecha, Capital, Interes, IdCuota, Numero from tblCuotas where IdPrestamo = @IdPrestamo
	
	SELECT IdGarantia, gara.IdClasificacion, NoIdentificacion as NumeracionGarantia, Detalles ,
	marc.Nombre as NombreMarca, mode.Nombre as NombreModelo, tiga.Nombre as NombreTipoGarantia
	from tblGarantias as gara
	inner join tblMarcas marc on marc.IdMarca = gara.IdMarca
	inner join tblModelos mode on mode.IdModelo = gara.IdModelo
	inner join tblTiposGarantia tiga on tiga.IdTipoGarantia = gara.IdTipoGarantia
	where IdGarantia in (select idgarantia from tblPrestamoGarantias where IdPrestamo = @idPrestamo)
End
