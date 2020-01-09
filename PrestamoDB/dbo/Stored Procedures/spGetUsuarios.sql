CREATE PROCEDURE [dbo].[spGetUsuarios]
(

	@IdUsuario int = -1,
	@IdNegocio int,
	@LoginName varchar(100),
	@NombreRealCompleto varchar(100),
    @Bloqueado int =-1,
    @Activo int=-1,
    @DebeCambiarContraseña int= -1,
	@Usuario varchar(100)='',
	@Anulado int=-1
)
as
begin
	SELECT 
	IdUsuario, IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseña, FechaExpiracionContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath
	FROM dbo.tblUsuarios(nolock) 
	where 
		((@idUsuario=-1) or (idUsuario = @IdUsuario))
		and ((@idNegocio=-1) or (idNegocio = @idNegocio))
		and ((@LoginName='') or (LoginName=@LoginName))
		and ((@NombreRealCompleto='') or (NombreRealCompleto=@NombreRealCompleto))
		and ((@Activo=-1) or (Activo=@Activo))
		and ((@DebeCambiarContraseña=-1) or (DebeCambiarContraseña=@DebeCambiarContraseña))
		and ((@Bloqueado=-1) or (Bloqueado=@Bloqueado))
		
End
