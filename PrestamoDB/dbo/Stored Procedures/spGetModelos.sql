CREATE PROCEDURE [dbo].[spGetModelos]
	@IdMarca int=-1,
	@IdModelo int=-1,
	@IdNegocio int =-1,
	@IdLocalidadNegocio int = -1,
	@Nombre varchar(50) = '',
	@Borrado int=0,
	@Usuario varchar(100)='',
		@Anulado varchar(100) = ''
AS
	SELECT mo.*, ma.Nombre as NombreMarca
	FROM dbo.tblModelos mo, dbo.tblMarcas ma
	where 
		((@IdMarca=-1) or (mo.IdMarca = @IdMarca))
		and ((@IdModelo=-1) or (IdModelo = @IdModelo))
		--and (ma.IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))
		and (mo.IdMarca = ma.IdMarca)
		and ((@Nombre='') or (mo.Nombre like @Nombre + '%'))
RETURN 0
