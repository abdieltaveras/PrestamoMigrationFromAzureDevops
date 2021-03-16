CREATE PROCEDURE [dbo].[spInsUpdClasificacion]
	@IdClasificacion int,
	@Nombre varchar(50),
	@IdNegocio int,
	@IdLocalidadNegocio int = -1,
	@Usuario varchar(50),
	@Codigo varchar(10),
	@RequiereGarantia bit,
	@RequiereAutorizacion bit,
	@idClasificacionFinanciera int,
	@Activo bit = 1
AS
Begin
if (@IdClasificacion <= 0)
	begin
		insert into tblClasificaciones
		( Nombre, IdNegocio, Codigo, RequiereGarantia, RequiereAutorizacion, InsertadoPor, FechaInsertado)
		values
		( @Nombre, @IdNegocio,@codigo, @RequiereGarantia, @RequiereAutorizacion, @Usuario, GETDATE())
	end
Else
	Begin
	update tblClasificaciones
		set 
			Nombre=@Nombre,
			ModificadoPor = @Usuario,
			RequiereAutorizacion= @RequiereAutorizacion,
			RequiereGarantia = @RequiereGarantia,
			FechaModificado = GETDATE()
		where IdClasificacion = @IdClasificacion
	End
End