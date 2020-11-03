CREATE PROCEDURE [dbo].[spGetClientes]
(
	@idCliente int=-1,
	@idNegocio int,
	@Activo int=-1,
	@Anulado int=0,
	@NoIdentificacion varchar(20)='',
	@idTipoIdentificacion int= -1,
	@Usuario varchar(100)=''
)
as
begin
	SELECT IdCliente, Codigo,Activo, AnuladoPor, Apodo, Apellidos, EstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, Sexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, Imagen1FileName, Imagen2FileName, TieneConyuge,infoReferencia,infoRedesSociales,Imagen1DocumentoName,Imagen2DocumentoName
	FROM dbo.tblClientes(nolock) 
	where 
		((@idCliente=-1) or (IdCliente = @IdCliente))
		--and ((@Codigo='') or (Codigo = @Codigo))
		and ((@Activo=-1) or (Activo=@Activo)) 
		and ((@idTipoIdentificacion=-1) or (idTipoIdentificacion =@idTipoIdentificacion)) 
		and ((@NoIdentificacion='') or (NoIdentificacion =@NoIdentificacion)) 
End
