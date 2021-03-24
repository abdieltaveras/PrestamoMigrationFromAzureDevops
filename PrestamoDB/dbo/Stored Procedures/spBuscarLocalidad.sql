CREATE PROCEDURE [dbo].spBuscarLocalidad
(
	@search varchar(50) ='',
	@idLocalidad INT =-1
)
as
BEGIN
	SELECT IdLocalidad, loc.IdLocalidadPadre, loc.IdTipoLocalidad, loc.Nombre, tipo.Nombre as Descripcion, tipo.PermiteCalle,	
	(SELECT Nombre FROM tblLocalidades where IdLocalidad = loc.IdLocalidadPadre) as NombrePadre,
	(SELECT Nombre FROM tblTipoLocalidades where IdTipoLocalidad = tipo.IdLocalidadPadre) as TipoNombrePadre
	from
	tblLocalidades loc, tblTipoLocalidades tipo
	where 
	(loc.IdTipoLocalidad = tipo.IdTipoLocalidad) 
	AND (@search='' OR loc.Nombre LIKE '%' + @search + '%')	
	AND (@idLocalidad=-1 OR loc.IdLocalidad = @idLocalidad)
	AND  loc.AnuladoPor IS null
End
