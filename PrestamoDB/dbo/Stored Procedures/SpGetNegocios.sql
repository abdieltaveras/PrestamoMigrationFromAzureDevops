CREATE PROCEDURE [dbo].[SpGetNegocios]
	@IdNegocio int,
	@IdLocalidadNegocio int,
	@IdNegocioPadre int=-1,
	@Codigo varchar(20)='',
	@NombreComercial varchar(100)='',
	@NombreJuridico varchar(100)='',
	@TaxIdNo varchar(20)='',
	@Borrado int=0,
	@PermitirOperaciones int,
	@Usuario varchar(100)=''
AS
begin
	SELECT IdNegocio, Codigo, NombreJuridico, NombreComercial, CorreoElectronico, Activo, Bloqueado,TaxIdNo, OtrosDetalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado, logo,  dbo.fnGetIdNegocioMatriz(@idNegocio) as idNegocioMatriz
	FROM dbo.tblNegocios(nolock) 
	where
		((@idNegocio=-1) or (IdNegocio = @IdNegocio)) and 
		((@Codigo='') or (Codigo=@Codigo)) and
		((@NombreComercial ='') or (NombreComercial=@NombreComercial)) and
		((@NombreJuridico ='') or (NombreJuridico=@NombreJuridico)) and
		((@TaxIdNo='') or (TaxIdNo =@TaxIdNo))
end
