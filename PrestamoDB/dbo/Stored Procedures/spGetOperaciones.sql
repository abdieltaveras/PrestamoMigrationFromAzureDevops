CREATE PROCEDURE [dbo].[spGetOperaciones]
(
	@IdOperacion int=-1,
	@IdNegocio int=-1,
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0 
)
as
begin
	SELECT *
	FROM dbo.tblOperaciones(nolock) 
	where 
		((@IdOperacion=-1) or (IdOperacion = @IdOperacion))
		----and ((@condicionBorrado= 0 and BorradoPor is null) 
		----or (@condicionBorrado=1 and BorradoPor is not null)
		----or (@condicionBorrado=-1))
End
