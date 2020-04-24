CREATE PROCEDURE [dbo].[SpGetNegocios]
	@IdNegocio int,
	@IdNegocioPadre int=-1,
	@Codigo varchar(20)='',
	@NombreComercial varchar(100)='',
	@NombreJuridico varchar(100)='',
	@TaxIdNo varchar(20)='',
	@Anulado int=0,
	@PermitirOperaciones int,
	@Usuario varchar(100)=''
AS
begin
	SELECT IdNegocio, Codigo, NombreJuridico, NombreComercial, CorreoElectronico, Activo, Bloqueado, idNegocioPadre, TaxIdNo, OtrosDetalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, logo, PermitirOperaciones,  dbo.fnGetIdNegocioMatriz(@idNegocio) as idNegocioMatriz
	FROM dbo.tblNegocios(nolock) 
	where
		((@idNegocio=-1) or (IdNegocio = @IdNegocio)) and 
		((@PermitirOperaciones=-1) or (PermitirOperaciones = @PermitirOperaciones)) and 
		((@idNegocioPadre=-1) or (idNegocioPadre = @idNegocioPadre)) and
		((@Codigo='') or (Codigo=@Codigo)) and
		((@NombreComercial ='') or (NombreComercial=@NombreComercial)) and
		((@NombreJuridico ='') or (NombreJuridico=@NombreJuridico)) and
		((@TaxIdNo='') or (TaxIdNo =@TaxIdNo))
end
