CREATE PROCEDURE [dbo].spBuscarLocalidad
	@search varchar(50)
as
BEGIN
	SELECT IdLocalidad, IdLocalidadPadre, loc.IdTipoLocalidad, Nombre, Descripcion, PermiteCalle
	from
	tblLocalidad loc, tblTipoLocalidad tipo
	where loc.IdTipoLocalidad = tipo.IdTipoLocalidad
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