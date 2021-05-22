CREATE PROCEDURE [dbo].[spGetGarantias]
	@IdGarantia int=-1,
	@NoIdentificacion varchar(50)='',
	@Placa varchar(20)='',
	@Matricula varchar(20)='',
	@IdNegocio int=0,
	@IdLocalidadNegocio int=-1,
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
			(@IdGarantia=-1 or IdGarantia = @IdGarantia) and
			(@Matricula ='' or JSON_VALUE(Detalles, '$.Matricula') like '%'+@Matricula+'%') and
			(@Placa ='' or JSON_VALUE(Detalles, '$.Placa') like '%'+@Placa+'%') and
			(@NoIdentificacion='' or NoIdentificacion like '%'+@noidentificacion+'%')

END
