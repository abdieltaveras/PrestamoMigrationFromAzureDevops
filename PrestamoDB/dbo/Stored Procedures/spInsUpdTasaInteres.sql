CREATE PROCEDURE [dbo].[spInsUpdTasaInteres]
	@idTasaInteres int,
	@idNegocio int,
	@IdLocalidadNegocio int = -1 ,
	@Codigo varchar(10),
	@Activo bit = 1,
	@Nombre varchar(100),
	@InteresMensual decimal (12,9),
	@RequiereAutorizacion bit = 0,
	@Usuario varchar(100)
AS
Begin
-- verificar si id es 0 inserta si es diferente modificar
if (@idTasaInteres =0)
	begin
		insert into tblTasasInteres
		(idNegocio, Codigo, Nombre,  Activo, RequiereAutorizacion,InteresMensual, InsertadoPor, FechaInsertado)
		values
		(@idNegocio, @Codigo, @Nombre, @Activo, @RequiereAutorizacion,@InteresMensual, @Usuario, GetDate())
	end
Else
	Begin
	update tblTasasInteres 
		set 
			Codigo=@Codigo, 
			InteresMensual =@InteresMensual, 
			Activo=@Activo,
			Nombre=@Nombre,
			RequiereAutorizacion=@RequiereAutorizacion,
			idNegocio = @idNegocio,
			ModificadoPor=@Usuario,
			FechaModificado = getdate()
		where idTasaInteres = @idTasaInteres
	End
End

