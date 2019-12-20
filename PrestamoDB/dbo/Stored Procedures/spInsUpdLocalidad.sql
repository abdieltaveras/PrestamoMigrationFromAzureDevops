CREATE PROCEDURE [dbo].[spInsUpdLocalidad]
	@idLocalidad int,
	@IdLocalidadPadre int,
	@IdNegocio int,
	@IdTipoLocalidad int,
	@Nombre varchar(100)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@idLocalidad = 0)
	begin
		insert into tblLocalidades
		(IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre)
		values
		(@IdLocalidadPadre, @IdNegocio, @IdTipoLocalidad, @Nombre)
	end
Else
	Begin
	update tblLocalidades 
		set 
			IdLocalidadPadre=@IdLocalidadPadre, 
			idNegocio = @idNegocio,
			IdTipoLocalidad =@IdTipoLocalidad, 
			Nombre=@Nombre
		where idLocalidad = @idLocalidad
	End
End

