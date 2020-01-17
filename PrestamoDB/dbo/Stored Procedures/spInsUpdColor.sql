CREATE PROCEDURE [dbo].[spInsUpdColor]
	@IdColor int,
	@Nombre varchar(50),
	@IdNegocio int,
	@Usuario varchar(50)
AS
Begin
if (@IdColor = 0)
	begin
		insert into tblColores
		( Nombre, IdNegocio, InsertadoPor, FechaInsertado)
		values
		( @Nombre, @IdNegocio, @Usuario, GETDATE())
	end
Else
	Begin
	update tblColores 
		set 
			Nombre=@Nombre,
			IdNegocio = @IdNegocio,
			ModificadoPor = @Usuario,
			FechaModificado = GETDATE()
		where IdColor = @IdColor
	End
End