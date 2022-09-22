CREATE PROCEDURE [dbo].[spInsUpdOcupacion]
	@IdOcupacion int,
	@Nombre varchar(50),
	@IdNegocio int,
	@IdLocalidadNegocio int = -1,
	@Usuario varchar(50),
	@Codigo varchar(10),
	@Activo bit = 1
AS
Begin
if (@IdOcupacion <= 0)
	begin
		insert into tblOcupaciones
		( Nombre, IdNegocio, InsertadoPor, FechaInsertado)
		values
		( @Nombre, @IdNegocio, @Usuario, GETDATE())
	end
Else
	Begin
	update tblOcupaciones
		set 
			Nombre=@Nombre,
			IdNegocio = @IdNegocio,
			ModificadoPor = @Usuario,
			FechaModificado = GETDATE()
		where IdOcupacion = @IdOcupacion
	End
End
