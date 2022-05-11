CREATE PROCEDURE [dbo].[spGetColores]
(
	@IdColor int=-1,
	@IdNegocio int=-1,
		@IdLocalidadNegocio int = -1,
	@Borrado int=0,
	@Usuario varchar(100)='',
		@Anulado varchar(100) = ''
)
as
begin
	SELECT *
	FROM dbo.tblColores(nolock) 
	where 
		((@IdColor=-1) or (IdColor = @IdColor))
		--and IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))
End
