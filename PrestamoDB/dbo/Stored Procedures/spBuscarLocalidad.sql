CREATE PROCEDURE [dbo].spBuscarLocalidad

(
	@search varchar(50) ='',
	@SoloLosQuePermitenCalle bit= 0
	--@idLocalidadNegocio INT =-1
)
as
BEGIN
	SELECT IdLocalidad, loc.IdLocalidadPadre, loc.IdNegocio, loc.IdTipoLocalidad, loc.Nombre, divTerr.Nombre as Descripcion, divTerr.PermiteCalle,	
	(SELECT Nombre FROM tblLocalidades where IdLocalidad = loc.IdLocalidadPadre) as NombrePadre,
	(SELECT Nombre FROM tblDivisionTerritorial where IdTipoLocalidad = divTerr.IdDivisionTerritorialPadre) as TipoNombrePadre
	from
	tblLocalidades loc, tblDivisionTerritorial divTerr
	where 
	(loc.IdTipoLocalidad = divTerr.IdDivisionTerritorial)
	AND (@search='' OR loc.Nombre LIKE '%' + @search + '%')	
	and (@SoloLosQuePermitenCalle=0 or divTerr.PermiteCalle=1)
	--AND (@idLocalidadNegocio =-1 OR loc.IdLocalidadN  = @idLocalidad)
	AND  loc.AnuladoPor IS null
End

