CREATE PROCEDURE dbo.SpInsUpdUsuario
(
	@IdUsuario INT,
	@IdNegocio INT, 
	@LoginName varchar(50) ,
	@NombreRealCompleto varchar(50) ,
	@Contraseña varchar(50) , 
    @DebeCambiarContraseña BIT ,
    @FechaExpiracionContraseña  DateTime=null ,
    @Telefono1 VARCHAR(50) , 
    @Telefono2 VARCHAR(50), 
    @Activo BIT , 
    @Bloqueado BIT , 
    @CorreoElectronico VARCHAR(50), 
    @EsEmpleado BIT , 
	@IdPersonal int=null,
	@Usuario varchar(50)
)
AS
Begin
	if (@idUsuario<=0)	
		begin
			INSERT INTO dbo.tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseña, FechaExpiracionContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, idPersonal,InsertadoPor, FechaInsertado)
			VALUES (@idnegocio, @loginname, @nombrerealcompleto, @contraseña, @debecambiarcontraseña, @fechaexpiracioncontraseña, @telefono1, @telefono2, @activo, @bloqueado, @correoelectronico, @esempleado, @IdPersonal, @usuario, getdate())
			select @@identity
		end
		
	else
		begin
			UPDATE dbo.tblUsuarios
			SET LoginName = @loginname,
				NombreRealCompleto = @nombrerealcompleto,
				Contraseña = @contraseña,
				DebeCambiarContraseña = @debecambiarcontraseña,
				FechaExpiracionContraseña = @fechaexpiracioncontraseña,
				Telefono1 = @telefono1,
				Telefono2 = @telefono2,
				Activo = @activo,
				Bloqueado = @bloqueado,
				CorreoElectronico = @correoelectronico,
				EsEmpleado = @esempleado,
				ModificadoPor = @usuario,
				IdPersonal = @IdPersonal,
				FechaModificado = getdate()
				where IdUsuario = @IdUsuario
		End
End
