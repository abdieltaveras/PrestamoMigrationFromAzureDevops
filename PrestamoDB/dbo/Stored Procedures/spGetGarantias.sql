CREATE PROCEDURE [dbo].[spGetGarantias]
	@IdGarantia int=-1,
	@IdNegocio int=0,
		@IdLocalidad int=-1,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
BEGIN

	SELECT
		garantias.*, marcas.nombre as NombreMarca, modelos.nombre as NombreModelo  
	from
		tblGarantias garantias
	INNER JOIN 
		tblMarcas marcas ON garantias.IdMarca = marcas.IdMarca
	left JOIN 
		tblModelos modelos ON garantias.IdModelo = modelos.IdModelo
	left JOIN 
		tblLocalidades localidades ON JSON_VALUE(Detalles, '$.IdLocalidad') = localidades.IdLocalidad
		where 
			((@IdGarantia=-1) or (IdGarantia = @IdGarantia))

END
