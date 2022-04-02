CREATE PROCEDURE [dbo].[spGetRoles]
(
	@IdRole int=-1,
	@IdNegocio int=-1,
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblRoles(nolock) 
	where 
		((@IdRole=-1) or (IdRole = @IdRole))
End
