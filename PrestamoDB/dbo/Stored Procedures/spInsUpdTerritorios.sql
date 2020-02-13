CREATE PROCEDURE [dbo].[spInsUpdTerritorios]
	@IdTipoLocalidad int,
	@IdLocalidadPadre int,
	@IdDivisionTerritorial int,
	@IdNegocio int,
	@Nombre varchar(100),
	@PermiteCalle bit,
	@Activo bit = 1,
	@Codigo varchar(10) = '',
	@Usuario varchar(100)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdTipoLocalidad = 0)
	begin
		insert into tblTipoLocalidades
			(IdLocalidadPadre, IdDivisionTerritorial, IdNegocio, Nombre, PermiteCalle, InsertadoPor, FechaInsertado)
		values
			(@IdLocalidadPadre, @IdDivisionTerritorial, @IdNegocio, @Nombre, @PermiteCalle, @Usuario, GetDate())
	end
Else
	Begin
	update tblTipoLocalidades 
		set
			idNegocio = @idNegocio,
			IdDivisionTerritorial = @IdDivisionTerritorial,
			Nombre = @Nombre,
			IdLocalidadPadre = @IdLocalidadPadre,
			PermiteCalle=@PermiteCalle,
			ModificadoPor=@Usuario,
			FechaModificado = getdate()
		where IdTipoLocalidad = @IdTipoLocalidad
	End
End

