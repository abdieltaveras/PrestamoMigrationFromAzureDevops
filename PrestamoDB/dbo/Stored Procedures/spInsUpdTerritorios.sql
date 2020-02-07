CREATE PROCEDURE [dbo].[spInsUpdTerritorios]
	@IdTipoLocalidad int,
	@HijoDe int,
	@IdDivisionTerritorial int,
	@IdNegocio int,
	@Nombre varchar(100),
	@PermiteCalle bit,
	@Activo bit = 1,
	@Codigo varchar(50) = '',
	@Usuario varchar(100)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdTipoLocalidad = 0)
	begin
		insert into tblTipoLocalidades
			(HijoDe, IdDivisionTerritorial, IdNegocio, Nombre, PermiteCalle, InsertadoPor, FechaInsertado)
		values
			(@HijoDe, @IdDivisionTerritorial, @IdNegocio, @Nombre, @PermiteCalle, @Usuario, GetDate())
	end
Else
	Begin
	update tblTipoLocalidades 
		set
			idNegocio = @idNegocio,
			IdDivisionTerritorial = @IdDivisionTerritorial,
			Nombre = @Nombre,
			HijoDe = @HijoDe,
			PermiteCalle=@PermiteCalle,
			ModificadoPor=@Usuario,
			FechaModificado = getdate()
		where IdTipoLocalidad = @IdTipoLocalidad
	End
End

