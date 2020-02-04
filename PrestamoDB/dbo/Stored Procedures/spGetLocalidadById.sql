CREATE PROCEDURE [dbo].[spGetLocalidadById]
	@IdLocalidad varchar(50),
	@IdNegocio int,
	@Anulado int=0,
	@Usuario varchar(100)=''
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

