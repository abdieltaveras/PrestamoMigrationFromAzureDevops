create PROCEDURE [dbo].[SpUpdContraseñaUsuario]
(
	@IdUsuario INT,
	@Contraseña varchar(50), 
	@Usuario varchar(50)
)
AS
Begin
			-- does not update contraseña column  here, that action has a special procedure for it
			UPDATE dbo.tblUsuarios
			SET Contraseña = @contraseña,
				ModificadoPor = @usuario,
				FechaModificado = getdate()
				where IdUsuario = @IdUsuario
End