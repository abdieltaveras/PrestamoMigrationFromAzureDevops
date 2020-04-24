create procedure spgetNegociosAndHijos
(
	@idNegocio int
)
as
begin

	SELECT IdNegocio, Codigo, NombreJuridico, NombreComercial, CorreoElectronico, Activo, Bloqueado, idNegocioPadre, TaxIdNo, OtrosDetalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, logo, PermitirOperaciones,  dbo.fnGetIdNegocioMatriz(@idNegocio) as idNegocioMatriz
	FROM dbo.tblNegocios(nolock) 
	where 
		(idNegocio in (select idNegocio from  dbo.fnGetNegocioAndHijos(@idNegocio)))
end