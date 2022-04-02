CREATE PROCEDURE [dbo].[spGetOperaciones]
(
	@IdOperacion int=-1,
	@IdNegocio int=-1,
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblOperaciones(nolock) 
	where 
		((@IdOperacion=-1) or (IdOperacion = @IdOperacion))
End
