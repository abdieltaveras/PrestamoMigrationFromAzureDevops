CREATE PROCEDURE [dbo].[spGetMarcas]
(
	@IdMarca int=-1,
	@IdNegocio int=-1
)
as
begin
	SELECT *
	FROM dbo.tblMarcas(nolock) 
	where 
		((@IdMarca=-1) or (IdMarca = @IdMarca))
		and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
End
