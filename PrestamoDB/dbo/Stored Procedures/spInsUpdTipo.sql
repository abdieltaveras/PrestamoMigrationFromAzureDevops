CREATE PROCEDURE [dbo].[spInsUpdTipo]
	@IdTipo int,
	@IdClasificacion int,
	@Nombre varchar(50),
	@Activo int=1,
	@Codigo varchar(10)='',
	@IdNegocio int,
	@Usuario varchar(100)
AS
Begin
if (@IdTipo = 0)
	begin
		insert into tblTipos
		( IdClasificacion, Nombre, IdNegocio, InsertadoPor, FechaInsertado)
		values
		(@IdClasificacion, @Nombre, @IdNegocio, @Usuario, GETDATE())
	end
Else
	Begin
	update tblTipos
		set 
			IdClasificacion = @IdClasificacion,
			Nombre=@Nombre,
			IdNegocio = @IdNegocio,
			ModificadoPor = @Usuario,
			FechaModificado = GETDATE()
		where IdTipo = @IdTipo
	End
End

