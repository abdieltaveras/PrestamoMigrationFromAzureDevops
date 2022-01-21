CREATE PROCEDURE [dbo].[SpGetLocalidadesNegocio]
	
AS
begin
	SELECT * from tblLocalidadesNegocio
	--IdNegocio, Codigo, NombreJuridico, NombreComercial, CorreoElectronico, Activo, Bloqueado,TaxIdNo, OtrosDetalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, logo,  dbo.fnGetIdNegocioMatriz(@idNegocio) as idNegocioMatriz
	--FROM dbo.tblNegocios(nolock) 
	--where
	--	((@idNegocio=-1) or (IdNegocio = @IdNegocio)) and 
	--	((@Codigo='') or (Codigo=@Codigo)) and
	--	((@NombreComercial ='') or (NombreComercial=@NombreComercial)) and
	--	((@NombreJuridico ='') or (NombreJuridico=@NombreJuridico)) and
	--	((@TaxIdNo='') or (TaxIdNo =@TaxIdNo))
end
