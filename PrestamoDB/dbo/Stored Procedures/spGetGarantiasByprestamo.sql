create PROCEDURE [dbo].[spGetGarantiasByPrestamo]
	(
		@IdPrestamo int,
		@Usuario varchar(100)='',
		@condicionBorrado int = 0
	)
as
BEGIN
	SELECT 
		garantias.*, marcas.nombre as NombreMarca, modelos.nombre as NombreModelo, colores.Nombre as NombreColor
	from
		tblPrestamoGarantias  PrestamoGarantias
	inner join
		tblGarantias garantias on garantias.IdGarantia = PrestamoGarantias.IdGarantia
	left JOIN 
		tblColores colores ON JSON_VALUE(Detalles, '$.Color') = colores.IdColor
	left JOIN 
		tblMarcas marcas ON garantias.IdMarca = marcas.IdMarca
	left JOIN 
		tblModelos modelos ON garantias.IdModelo = modelos.IdModelo
	left JOIN 
		tblLocalidades localidades ON JSON_VALUE(Detalles, '$.IdLocalidad') = localidades.IdLocalidad
	where 
		IdPrestamo = @IdPrestamo
	and ((@condicionBorrado= 0 and garantias.BorradoPor is null) 
	or (@condicionBorrado=1 and garantias.BorradoPor is not null)
	or (@condicionBorrado=-1))
END
