CREATE PROCEDURE [dbo].[spGetMarcas]
(
	@IdMarca int=-1,
	@IdNegocio int=-1,
	@Borrado int=0,
	@IdLocalidadNegocio int = -1,
	@Usuario varchar(100)='',
	@Anulado varchar(100) = '',
	@condicionBorrado int = 0 
)
as
begin
	SELECT *
	FROM dbo.tblMarcas(nolock) 
	where 
		((@IdMarca=-1) or (IdMarca = @IdMarca))
		and ((@condicionBorrado= 0 and BorradoPor is null) 
		or (@condicionBorrado=1 and BorradoPor is not null)
		or (@condicionBorrado=-1))
		--and IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))
End
