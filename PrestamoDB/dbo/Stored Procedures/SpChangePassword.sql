CREATE PROCEDURE [dbo].[SpChangePassword]
	@IdUsuario INT,
	@Contraseña varchar(50), 
	@Usuario varchar(50)
AS
	begin
			UPDATE dbo.tblUsuarios
			SET Contraseña = @contraseña,
				ModificadoPor = @usuario,
				FechaModificado = getdate(),
				InicioVigenciaContraseña = getdate(),
				DebeCambiarContraseñaAlIniciarSesion=0
			where IdUsuario = @IdUsuario
	end
RETURN 
