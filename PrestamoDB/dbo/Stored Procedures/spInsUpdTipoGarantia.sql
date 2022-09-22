CREATE PROCEDURE [dbo].[spInsUpdTipoGarantia]
	@IdTipoGarantia int,
	@IdClasificacion int,
	@Nombre varchar(50),
	@Activo int=1,
	@Codigo varchar(10)='',
	@IdNegocio int,
	@Usuario varchar(100)
AS
Begin
if (@IdTipoGarantia <= 0)
	begin
		insert into tblTiposGarantia
		( IdClasificacion, Nombre, IdNegocio, InsertadoPor, FechaInsertado)
		values
		(@IdClasificacion, @Nombre, @IdNegocio, @Usuario, GETDATE())
	end
Else
	Begin
	update tblTiposGarantia
		set 
			IdClasificacion = @IdClasificacion,
			Nombre=@Nombre,
			IdNegocio = @IdNegocio,
			ModificadoPor = @Usuario,
			FechaModificado = GETDATE()
		where IdTipoGarantia = @IdTipoGarantia
	End
End

