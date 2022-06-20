CREATE PROCEDURE [dbo].[spGetLocalidadById]
	@IdLocalidad varchar(50),
	@IdNegocio int,
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0
as
BEGIN
	SELECT 
		nombre 
	FROM 
		tblLocalidades
	WHERE 
		IdLocalidad = @IdLocalidad
	and ((@condicionBorrado= 0 and BorradoPor is null) 
	or (@condicionBorrado=1 and BorradoPor is not null)
	or (@condicionBorrado=-1))
		--and IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)) 
End

