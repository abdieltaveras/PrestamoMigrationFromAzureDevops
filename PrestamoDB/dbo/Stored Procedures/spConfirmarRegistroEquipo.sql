CREATE PROCEDURE dbo.spConfirmarRegistro
(
	@IdEquipo INT,
	@Usuario varchar(50)
)
AS
Begin
	update tblEquipos
		set 
			ConfirmadoPor=@Usuario,
			FechaConfirmado = getdate()
		where 
			IdEquipo = @idEquipo 
			
End

