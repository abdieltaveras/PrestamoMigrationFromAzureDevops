CREATE PROCEDURE [dbo].[spInsUpdTerritorios]
	@IdTipoLocalidad int,
	@PadreDe int,
	@IdNegocio int,
	@Descripcion varchar(100),
	@PermiteCalle bit
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdTipoLocalidad = 0)
	begin
		insert into tblTipoLocalidad
			(PadreDe, IdNegocio, Descripcion, PermiteCalle)
		values
			(@PadreDe, @IdNegocio, @Descripcion, @PermiteCalle)
	end
Else
	Begin
	update tblTipoLocalidad 
		set
			idNegocio = @idNegocio,
			Descripcion = @Descripcion,
			PadreDe = @PadreDe,
			PermiteCalle=@PermiteCalle
		where IdTipoLocalidad = @IdTipoLocalidad
	End
End

