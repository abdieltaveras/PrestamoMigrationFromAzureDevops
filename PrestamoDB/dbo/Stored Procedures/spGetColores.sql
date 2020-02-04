CREATE PROCEDURE [dbo].[spGetColores]
(
	@IdColor int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblColores(nolock) 
	where 
		((@IdColor=-1) or (IdColor = @IdColor))
		and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
End
