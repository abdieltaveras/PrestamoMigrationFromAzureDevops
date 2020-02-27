CREATE PROCEDURE [dbo].[spGetOcupaciones]
(
	@IdOcupacion int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblOcupaciones(nolock) 
	where 
		((@IdOcupacion=-1) or (IdOcupacion = @IdOcupacion))
		and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
End
