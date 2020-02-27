CREATE PROCEDURE [dbo].[spGetVerificadoresDireccion]
(
	@IdVerificadorDireccion int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblVerificadorDirecciones(nolock) 
	where 
		((@IdVerificadorDireccion=-1) or (IdVerificadorDireccion = @IdVerificadorDireccion))
		and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
End
