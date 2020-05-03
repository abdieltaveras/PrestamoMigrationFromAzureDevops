CREATE PROCEDURE [dbo].[LocalidadLocalidadesHijasDeLocalidadSpGet]
(
	@IdNegocio int,
	@Anulado int=0,
	@IdLocalidad int = 0,
	@Usuario varchar(100)=''
)
as
begin
	
	SELECT 
		tblLocalidades.IdLocalidad as IdLocalidad,
		tblTipoLocalidades.Nombre as TipoLocalidad,
		tblLocalidades.Nombre as Nombre 
	FROM 
		tblLocalidades 
	JOIN 
		tblTipoLocalidades ON tblLocalidades.IdTipoLocalidad = tblTipoLocalidades.IdTipoLocalidad 
	WHERE 
		tblLocalidades.IdLocalidadPadre = @IdLocalidad
		AND tblLocalidades.IdNegocio IN (SELECT idNegocio FROM dbo.fnGetNegocioAndPadres(@IdNegocio))
		
End
