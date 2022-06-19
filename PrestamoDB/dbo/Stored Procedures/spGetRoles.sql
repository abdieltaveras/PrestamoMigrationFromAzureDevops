CREATE PROCEDURE [dbo].[spGetRoles]
(
	@IdRole int=-1,
	@IdNegocio int=-1,
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0 
)
as
begin
	SELECT *
	FROM dbo.tblRoles(nolock) 
	where 
		((@IdRole=-1) or (IdRole = @IdRole))
		--and ((@condicionBorrado= 0 and BorradoPor is null) 
		--or (@condicionBorrado=1 and BorradoPor is not null)
		--or (@condicionBorrado=-1))
End
