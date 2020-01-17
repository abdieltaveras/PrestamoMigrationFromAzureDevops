CREATE PROCEDURE [dbo].[spInsUpdTipo]
	@IdTipo int,
	@IdClasificacion int,
	@Nombre varchar(50),
	@IdNegocio int,
	@InsertadoPor varchar(50)
AS
Begin
if (@IdTipo = 0)
	begin
		insert into tblTipos
		( IdClasificacion, Nombre, IdNegocio, InsertadoPor, FechaInsertado)
		values
		(@IdClasificacion, @Nombre, @IdNegocio, @InsertadoPor, GETDATE())
	end
Else
	Begin
	update tblTipos
		set 
			IdClasificacion = @IdClasificacion,
			Nombre=@Nombre,
			IdNegocio = @IdNegocio,
			InsertadoPor = @InsertadoPor,
			FechaModificado = GETDATE()
		where IdTipo = @IdTipo
	End
End

