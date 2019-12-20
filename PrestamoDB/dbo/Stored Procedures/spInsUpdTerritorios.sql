CREATE PROCEDURE [dbo].[spInsUpdTerritorios]
	@IdTipoLocalidad int,
	@HijoDe int,
	@IdDivisionTerritorial int,
	@IdNegocio int,
	@Descripcion varchar(100),
	@PermiteCalle bit
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdTipoLocalidad = 0)
	begin
		insert into tblTipoLocalidades
			(HijoDe, IdDivisionTerritorial, IdNegocio, Descripcion, PermiteCalle)
		values
			(@HijoDe, @IdDivisionTerritorial, @IdNegocio, @Descripcion, @PermiteCalle)
	end
Else
	Begin
	update tblTipoLocalidades 
		set
			idNegocio = @idNegocio,
			IdDivisionTerritorial = @IdDivisionTerritorial,
			Descripcion = @Descripcion,
			HijoDe = @HijoDe,
			PermiteCalle=@PermiteCalle
		where IdTipoLocalidad = @IdTipoLocalidad
	End
End

