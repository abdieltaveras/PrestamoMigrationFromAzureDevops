CREATE PROCEDURE [dbo].[spGetModelosGarantiaByMarca]
	@IdMarca int=-1,
	@IdModelo int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
AS
	SELECT mo.*
	FROM dbo.tblModelos mo
	where 
		((@IdMarca=-1) or (mo.IdMarca = @IdMarca))
		and ((@IdModelo=-1) or (IdModelo = @IdModelo))
		--and (IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))

