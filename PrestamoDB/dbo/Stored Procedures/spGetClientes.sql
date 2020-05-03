CREATE PROCEDURE [dbo].[spGetClientes]
(
	@idCliente int=-1,
	@idNegocio int=-1,
	@Activo int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT IdCliente, Codigo,Activo, AnuladoPor, Apodo, Apellidos, EstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, Sexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, Imagen1FileName, Imagen2FileName, TieneConyuge,infoReferencia
	FROM dbo.tblClientes(nolock) 
	where 
		((@idCliente=-1) or (IdCliente = @IdCliente))
		and (IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))
		--and ((@Codigo='') or (Codigo = @Codigo))
		and ((@Activo=-1) or (Activo=@Activo))

End
