CREATE PROCEDURE [dbo].[spGetMarcas]
(
	@IdMarca int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
		@IdLocalidadNegocio int = -1,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblMarcas(nolock) 
	where 
		((@IdMarca=-1) or (IdMarca = @IdMarca))
		--and IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))
End
