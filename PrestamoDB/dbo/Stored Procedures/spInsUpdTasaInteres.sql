CREATE PROCEDURE [dbo].[spInsUpdTasaInteres]
	@idTasaInteres int,
	@idNegocio int,
	@Codigo varchar(10),
	@Descripcion varchar(100),
	@InteresMensual decimal (9,6),
	@Activo bit = 1,
	@RequiereAutorizacion bit = 0,
	@Usuario varchar(100)
AS
Begin
-- verificar si id es 0 inserta si es diferente modificar
if (@idTasaInteres =0)
	begin
		insert into tblTasaInteres 
		(Codigo, Descripcion, idNegocio, InteresMensual, Activo, RequiereAutorizacion, InsertadoPor, FechaInsertado)
		values
		(@Codigo, @Descripcion, @idNegocio, @InteresMensual, @Activo, @RequiereAutorizacion, @Usuario, GetDate())
	end
Else
	Begin
	update tblTasaInteres 
		set 
			Codigo=@Codigo, 
			InteresMensual =@InteresMensual, 
			Activo=@Activo,
			Descripcion=@Descripcion,
			RequiereAutorizacion=@RequiereAutorizacion,
			ModificadoPor=@Usuario,
			idNegocio = @idNegocio,
			FechaModificado = getdate()
		where idTasaInteres = @idTasaInteres
	End
End

