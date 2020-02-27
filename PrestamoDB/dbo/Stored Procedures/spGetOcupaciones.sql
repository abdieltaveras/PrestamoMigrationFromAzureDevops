CREATE PROCEDURE [dbo].[spGetOcupaciones]
(
	@Id int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblOcupaciones(nolock) 
	where 
		((@Id=-1) or (IdOcupacion = @Id))
		and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
End
