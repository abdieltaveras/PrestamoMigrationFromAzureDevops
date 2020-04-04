CREATE PROCEDURE dbo.spRegistrarAccesoAlEquipo
(
	@IdEquipo INT,
	@Usuario varchar(50)
)
AS
Begin
	update tblEquipos
		set 
			AccesadoPor = @Usuario,
			UltimoAcceso = getdate()
		where IdEquipo = @IdEquipo
End
