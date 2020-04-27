CREATE PROCEDURE [dbo].spBuscarLocalidad
	@search varchar(50),
	@IdNegocio int,
	@Anulado int=0,
	@Usuario varchar(100)=''
as
BEGIN
	SELECT IdLocalidad, loc.IdLocalidadPadre, loc.IdNegocio, loc.IdTipoLocalidad, loc.Nombre, tipo.Nombre as Descripcion, tipo.PermiteCalle,	
	(SELECT Nombre FROM tblLocalidades where IdLocalidad = loc.IdLocalidadPadre) as NombrePadre,
	(SELECT Nombre FROM tblTipoLocalidades where IdTipoLocalidad = tipo.IdLocalidadPadre) as TipoNombrePadre
	from
	tblLocalidades loc, tblTipoLocalidades tipo
	where loc.IdTipoLocalidad = tipo.IdTipoLocalidad
	and loc.IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))
	AND loc.Nombre LIKE '%' + @search + '%'	
End