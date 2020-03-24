CREATE PROCEDURE dbo.SpInsUpdUsuario
(
	@IdUsuario INT,
	@IdNegocio INT, 
	@LoginName varchar(50) ,
	@NombreRealCompleto varchar(50) ,
	@Contraseña varchar(50), 
    @DebeCambiarContraseñaAlIniciarSesion BIT ,
    @Telefono1 VARCHAR(50)='' , 
    @Telefono2 VARCHAR(50)='', 
    @Activo BIT , 
	@imgFilePath varchar(100),
    @Bloqueado BIT, 
    @CorreoElectronico VARCHAR(50)='', 
    @EsEmpleado BIT , 
	@IdPersonal int=null,
	@VigenteDesde dateTime,
	@VigenteHasta dateTime,
	@ContraseñaExpiraCadaXMes int,
	@RazonBloqueo int,
	@Usuario varchar(50)
)
AS
Begin
	if (@idUsuario<=0)	
		begin
			INSERT INTO dbo.tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion,  Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, idPersonal,ImgFilePath, InsertadoPor, FechaInsertado, VigenteDesde,VigenteHasta, ContraseñaExpiraCadaXMes,RazonBloqueo, InicioVigenciaContraseña)
			VALUES (@idnegocio, @loginname, @nombrerealcompleto, @contraseña, @DebeCambiarContraseñaAlIniciarSesion, @telefono1, @telefono2, @activo, @bloqueado, @correoelectronico, @esempleado, @IdPersonal, @imgFilePath,@usuario, getdate(), @VigenteDesde,@VigenteHasta, @ContraseñaExpiraCadaXMes, @RazonBloqueo, getdate() )
			select @@identity
		
			SELECT IDENT_CURRENT('tblUsers') as last_id
		
		end
	else
		begin
			-- does not update contraseña column  here, that action has a special procedure for it
			UPDATE dbo.tblUsuarios
			SET LoginName = @loginname,
				NombreRealCompleto = @nombrerealcompleto,
				DebeCambiarContraseñaAlIniciarSesion = @DebeCambiarContraseñaAlIniciarSesion,
				VigenteHasta = @VigenteHasta,
				VigenteDesde = @VigenteDesde,
				Telefono1 = @telefono1,
				Telefono2 = @telefono2,
				Activo = @activo,
				Bloqueado = @bloqueado,
				CorreoElectronico = @correoelectronico,
				EsEmpleado = @esempleado,
				ModificadoPor = @usuario,
				IdPersonal = @IdPersonal,
				ImgFilePath = @imgFilePath,
				ContraseñaExpiraCadaXMes=@ContraseñaExpiraCadaXMes,
				RazonBloqueo=@RazonBloqueo,
				FechaModificado = getdate()
				where IdUsuario = @IdUsuario
		End
End
