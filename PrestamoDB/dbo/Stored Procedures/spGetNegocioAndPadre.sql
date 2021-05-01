create procedure dbo.spgetNegocioAndPadres
(
	@idNegocio int
)
as
begin

	SELECT IdNegocio, Codigo, NombreJuridico, NombreComercial, CorreoElectronico, Activo, Bloqueado, idNegocioPadre, TaxIdNo, OtrosDetalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, logo, dbo.fnGetIdNegocioMatriz(@idNegocio) as idNegocioMatriz
	FROM dbo.tblNegocios(nolock) 
	--where 
	--	(idNegocio in (select idNegocio from  dbo.fnGetNegocioAndPadres(@idNegocio)))
end
return
