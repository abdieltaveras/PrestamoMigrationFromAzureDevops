CREATE PROCEDURE [dbo].[spGetModelosGarantiaByMarca]
	@IdMarca int=-1,
	@IdModelo int=-1,
	@IdNegocio int=-1,
	@IdLocalidadNegocio int = -1,
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0 
AS
	SELECT mo.*
	FROM dbo.tblModelos mo
	where 
		((@IdMarca=-1) or (mo.IdMarca = @IdMarca))
		and ((@IdModelo=-1) or (IdModelo = @IdModelo))
		and ((@condicionBorrado= 0 and BorradoPor is null) 
		or (@condicionBorrado=1 and BorradoPor is not null)
		or (@condicionBorrado=-1))
		--and (IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))

