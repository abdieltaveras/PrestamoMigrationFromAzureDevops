CREATE PROCEDURE [dbo].[spInsUpdLocalidad]
	@idLocalidad int,
	@IdLocalidadPadre int,
	@IdNegocio int,
	@IdTipoLocalidad int,
	@Nombre varchar(100),
	@PermiteCalle bit
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@idLocalidad = 0)
	begin
		insert into tblLocalidad
		(IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, PermiteCalle)
		values
		(@IdLocalidadPadre, @IdNegocio, @IdTipoLocalidad, @Nombre, @PermiteCalle)
	end
Else
	Begin
	update tblLocalidad 
		set 
			IdLocalidadPadre=@IdLocalidadPadre, 
			idNegocio = @idNegocio,
			IdTipoLocalidad =@IdTipoLocalidad, 
			Nombre=@Nombre,
			PermiteCalle=@PermiteCalle
		where idLocalidad = @idLocalidad
	End
End

