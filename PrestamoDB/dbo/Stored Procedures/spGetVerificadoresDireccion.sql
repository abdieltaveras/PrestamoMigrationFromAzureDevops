CREATE PROCEDURE [dbo].[spGetVerificadoresDireccion]
(
	@Id int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblVerificadorDirecciones(nolock) 
	where 
		((@Id=-1) or (IdVerificadorDireccion = @Id))
		and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
End
