CREATE PROCEDURE [dbo].[spInsUpdLocalidad]
	@idLocalidad int,
	@IdLocalidadPadre int,
	@IdTipoLocalidad int,
	@IdNegocio int,
	@IdLocalidadNegocio int = -1,
	@Codigo varchar(10) = '',
	@Nombre varchar(50),
	@Activo bit = 1,
	@Usuario varchar(100)

AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@idLocalidad = 0)
	begin
		insert into tblLocalidades
		(IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, InsertadoPor, FechaInsertado)
		values
		(@IdLocalidadPadre, @IdNegocio, @IdTipoLocalidad, @Nombre, @Usuario, GetDate())
	end
Else
	Begin
	update tblLocalidades 
		set 
			IdLocalidadPadre=@IdLocalidadPadre, 
			idNegocio = @idNegocio,
			IdTipoLocalidad =@IdTipoLocalidad, 
			Nombre=@Nombre,
			ModificadoPor=@Usuario,
			FechaModificado = getdate()
		where idLocalidad = @idLocalidad
	End
End

