CREATE PROCEDURE dbo.spBloquearEquipo
(
	@IdEquipo INT,
	@Usuario varchar(50)
)
AS
Begin
	update tblEquipos
		set 
			BloqueadoPor=@Usuario,
			FechaBloqueado = getdate()
	where 
			IdEquipo = @idEquipo 
End


