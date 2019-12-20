CREATE PROCEDURE [dbo].spBuscarLocalidad
	@search varchar(50),
	@IdNegocio int
as
BEGIN
	SELECT IdLocalidad, IdLocalidadPadre, loc.IdNegocio, loc.IdTipoLocalidad, Nombre, Descripcion, tipo.PermiteCalle,	
	(SELECT Nombre FROM tblLocalidades where IdLocalidad = loc.IdLocalidadPadre) as NombrePadre,
	(SELECT Descripcion FROM tblTipoLocalidades where IdTipoLocalidad = tipo.HijoDe) as TipoNombrePadre
	from
	tblLocalidades loc, tblTipoLocalidades tipo
	where loc.IdTipoLocalidad = tipo.IdTipoLocalidad
	and loc.IdNegocio = @IdNegocio
	AND loc.Nombre LIKE '%' + @search + '%'	
End