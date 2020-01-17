CREATE PROCEDURE [dbo].[spGetLocalidadById]
	@IdLocalidad varchar(50),
	@IdNegocio int
as
BEGIN
	SELECT 
		nombre 
	FROM 
		tblLocalidades
	WHERE 
		IdLocalidad = @IdLocalidad
		and IdNegocio = @IdNegocio
End

