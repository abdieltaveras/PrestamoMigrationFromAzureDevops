CREATE PROCEDURE [dbo].[spInsUpdTerritorios]
	@IdTipoLocalidad int,
	@HijoDe int,
	@IdDivisionTerritorial int,
	@IdNegocio int,
	@Descripcion varchar(100),
	@PermiteCalle bit,
	@Usuario varchar(100)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdTipoLocalidad = 0)
	begin
		insert into tblTipoLocalidades
			(HijoDe, IdDivisionTerritorial, IdNegocio, Descripcion, PermiteCalle, InsertadoPor, FechaInsertado)
		values
			(@HijoDe, @IdDivisionTerritorial, @IdNegocio, @Descripcion, @PermiteCalle, @Usuario, GetDate())
	end
Else
	Begin
	update tblTipoLocalidades 
		set
			idNegocio = @idNegocio,
			IdDivisionTerritorial = @IdDivisionTerritorial,
			Descripcion = @Descripcion,
			HijoDe = @HijoDe,
			PermiteCalle=@PermiteCalle,
			ModificadoPor=@Usuario,
			FechaModificado = getdate()
		where IdTipoLocalidad = @IdTipoLocalidad
	End
End

