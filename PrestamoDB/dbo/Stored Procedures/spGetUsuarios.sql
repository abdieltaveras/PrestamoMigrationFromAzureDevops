CREATE PROCEDURE [dbo].[spGetUsuarios]
(
	@IdUsuario int = -1,
	@IdNegocio int,
		@IdLocalidadNegocio int = -1,
	@LoginName varchar(100),
	@NombreRealCompleto varchar(100)='',
    @Bloqueado int =-1,
    @Activo int=-1,
    @DebeCambiarContraseñaAlIniciarSesion int= -1,
	@Usuario varchar(100)='',
	@Anulado int=-1
)
as
begin
	SELECT 
	IdUsuario, IdNegocio, LoginName, NombreRealCompleto, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña , Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath, ContraseñaExpiraCadaXMes, VigenteHasta, VigenteDesde, RazonBloqueo
	FROM dbo.tblUsuarios(nolock) 
	where 
		((@idUsuario=-1) or (idUsuario = @IdUsuario))
		and (idNegocio in (select idNegocio from  dbo.fnGetNegocioAndPadres(@IdNegocio)))
		--and ((@idNegocio=-1) or (idNegocio =@idNegocio))
		and ((@LoginName='') or (LoginName=@LoginName))
		and ((@NombreRealCompleto='') or (NombreRealCompleto=@NombreRealCompleto))
		and ((@Activo=-1) or (Activo=@Activo))
		and ((@DebeCambiarContraseñaAlIniciarSesion=-1) or (DebeCambiarContraseñaAlIniciarSesion=@DebeCambiarContraseñaAlIniciarSesion))
		and ((@Bloqueado=-1) or (Bloqueado=@Bloqueado))
End
