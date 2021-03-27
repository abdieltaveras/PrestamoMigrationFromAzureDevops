CREATE PROCEDURE [dbo].spBuscarLocalidad

(
	@search varchar(50) ='',
	@SoloLosQuePermitenCalle bit= 0
	--@idLocalidadNegocio INT =-1
)
as
BEGIN
	SELECT IdLocalidad, loc.IdLocalidadPadre, loc.IdNegocio, loc.IdTipoLocalidad, loc.Nombre, tipo.Nombre as Descripcion, tipo.PermiteCalle,	
	(SELECT Nombre FROM tblLocalidades where IdLocalidad = loc.IdLocalidadPadre) as NombrePadre,
	(SELECT Nombre FROM tblTipoLocalidades where IdTipoLocalidad = tipo.IdLocalidadPadre) as TipoNombrePadre
	from
	tblLocalidades loc, tblTipoLocalidades tipo
	where 
	(loc.IdTipoLocalidad = tipo.IdTipoLocalidad) 
	AND (@search='' OR loc.Nombre LIKE '%' + @search + '%')	
	and (@SoloLosQuePermitenCalle=0 or tipo.PermiteCalle=1)
	--AND (@idLocalidadNegocio =-1 OR loc.IdLocalidadN  = @idLocalidad)
	AND  loc.AnuladoPor IS null
End

