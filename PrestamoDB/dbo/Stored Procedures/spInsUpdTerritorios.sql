CREATE PROCEDURE [dbo].[spInsUpdTerritorios]
	@IdTipoLocalidad int,
	@PadreDe int,
	@IdNegocio int,
	@Descripcion varchar(100)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdTipoLocalidad = 0)
	begin
		insert into tblTipoLocalidad
			(PadreDe, IdNegocio, Descripcion)
		values
			(@PadreDe, @IdNegocio, @Descripcion)
	end
Else
	Begin
	update tblTipoLocalidad 
		set
			idNegocio = @idNegocio,
			Descripcion = @Descripcion,
			PadreDe = @PadreDe
		where IdTipoLocalidad = @IdTipoLocalidad
	End
End

