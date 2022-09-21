CREATE PROCEDURE [dbo].[LocalidadLocalidadesHijasDeLocalidadSpGet]
(
	@idlocalidad int,
	@IdNegocio int=-1,
	@IdLocalidadPadre int = -1,
    @IdLocalidadNegocio int = -1,
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin
	
	SELECT 
		tblLocalidades.IdLocalidad as IdLocalidad,
		tblDivisionTerritorial.Nombre as DivisionTerritorial,
		tblLocalidades.Nombre as Nombre 
	FROM 
		tblLocalidades 
	JOIN 
		tblDivisionTerritorial ON tblLocalidades.IdDivisionTerritorial = tblDivisionTerritorial.IdDivisionTerritorial 
	WHERE 
		tblLocalidades.IdLocalidadPadre = @IdLocalidad
		AND tblLocalidades.IdNegocio IN (SELECT idNegocio FROM dbo.fnGetNegocioAndPadres(@IdNegocio))
		
End
