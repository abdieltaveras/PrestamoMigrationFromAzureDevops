create procedure spLoginUsuarioByNegocioMatriz
(

	@IdNegocioMatriz int,
	@LoginName varchar(100),
	@contraseña varchar(100)
)
as
begin
		SELECT 
		IdUsuario, IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña , Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath, ContraseñaExpiraCadaXMes, VigenteHasta, VigenteDesde, RazonBloqueo
		into #tempData
		FROM dbo.tblUsuarios(nolock) 
		where 
			(idNegocio in (select idNegocio from  dbo.fnGetNegocioAndHijos(@IdNegocioMatriz)))
			and (LoginName=@LoginName)
		declare @contraseñaValida bit =0
		declare @contraseña1 varchar(100) = (select contraseña from #tempData)
		if (@contraseña1 = @contraseña) 
		begin 
			set @contraseñaValida=1
		end
		SELECT @contraseñaValida as ContraseñaValida,
		IdUsuario, IdNegocio, LoginName, NombreRealCompleto, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña , Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath, ContraseñaExpiraCadaXMes, VigenteHasta, VigenteDesde, RazonBloqueo from #tempdata
End



