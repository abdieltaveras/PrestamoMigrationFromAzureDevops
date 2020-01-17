CREATE PROCEDURE [dbo].[spInsUpdModelo]
	@IdModelo int,
	@IdMarca int,
	@Nombre varchar(50),
	@IdNegocio int,
	@InsertadoPor varchar(50)
AS
Begin
if (@IdModelo = 0)
	begin
		insert into tblModelos
		(IdMarca, Nombre, IdNegocio, InsertadoPor, FechaInsertado)
		values
		(@IdMarca, @Nombre, @IdNegocio, @InsertadoPor, GETDATE())
	end
Else
	Begin
	update tblModelos
		set 
			IdMarca = @IdMarca,
			Nombre  =@Nombre,
			IdNegocio = @IdNegocio,
			InsertadoPor = @InsertadoPor,
			FechaModificado = GETDATE()
		where IdModelo = @IdModelo
	End
End
