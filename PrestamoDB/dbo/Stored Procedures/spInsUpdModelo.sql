CREATE PROCEDURE [dbo].[spInsUpdModelo]
	@IdModelo int,
	@IdMarca int,
	@Nombre varchar(50),
	@IdNegocio int,
	@Usuario varchar(100),
	@Codigo varchar(10),
	@Activo bit = 1
AS
Begin
if (@IdModelo = 0)
	begin
		insert into tblModelos
		(IdMarca, Nombre, IdNegocio, InsertadoPor, FechaInsertado)
		values
		(@IdMarca, @Nombre, @IdNegocio, @Usuario, GETDATE())
	end
Else
	Begin
	update tblModelos
		set 
			IdMarca = @IdMarca,
			Nombre  =@Nombre,
			IdNegocio = @IdNegocio,
			InsertadoPor = @Usuario,
			FechaModificado = GETDATE()
		where IdModelo = @IdModelo
	End
End
