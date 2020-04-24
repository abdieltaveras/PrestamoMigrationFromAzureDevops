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
	SELECT IdCliente, Codigo,Activo, AnuladoPor, Apodo, Apellidos, EstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, Sexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, Imagen1FileName, Imagen2FileName
	FROM dbo.tblClientes(nolock) 
	where 
		((@idCliente=-1) or (IdCliente = @IdCliente))
		and ((@idNegocio=-1) or (idNegocio = @idNegocio))
		--and ((@Codigo='') or (Codigo = @Codigo))
		and ((@Activo=-1) or (Activo=@Activo))

End
