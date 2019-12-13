CREATE PROCEDURE [dbo].spBuscarLocalidad
	@search varchar(50),
	@IdNegocio int
as
BEGIN
	SELECT IdLocalidad, IdLocalidadPadre, loc.IdNegocio, loc.IdTipoLocalidad, Nombre, Descripcion, tipo.PermiteCalle,	
	(SELECT Nombre FROM tblLocalidad where IdLocalidad = loc.IdLocalidadPadre) as NombrePadre,
	(SELECT Descripcion FROM tblTipoLocalidad where IdTipoLocalidad = tipo.PadreDe) as TipoNombrePadre
	from
	tblLocalidad loc, tblTipoLocalidad tipo
	where loc.IdTipoLocalidad = tipo.IdTipoLocalidad
	and loc.IdNegocio = @IdNegocio
	AND loc.Nombre LIKE '%' + @search + '%'	
End

--CREATE PROCEDURE [dbo].spBuscarLocalidad
--	@search varchar(50)
--as
--BEGIN
--	SELECT IdLocalidad, IdLocalidadPadre, loc.IdTipoLocalidad, Nombre, Descripcion, PermiteCalle,
--	IdTipoLocalidadHijo = (SELECT PadreDe as hijoDe FROM tblTipoLocalidad where  loc.IdTipoLocalidad = PadreDe ),
--	IdTipoLocalidadHijoNombre = (SELECT Descripcion as hijoDe FROM tblTipoLocalidad where  loc.IdTipoLocalidad = PadreDe )
--	from
--	tblLocalidad loc, tblTipoLocalidad tipo
--	where loc.IdTipoLocalidad = tipo.IdTipoLocalidad
--	AND loc.Nombre LIKE '%' + @search + '%'	
--End