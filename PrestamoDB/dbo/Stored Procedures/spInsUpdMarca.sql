CREATE PROCEDURE [dbo].[spInsUpdMarca]
	@IdMarca int,
	@Nombre varchar(50),
	@IdNegocio int,
	@InsertadoPor varchar(50)
AS
Begin
if (@IdMarca = 0)
	begin
		insert into tblMarcas
		( Nombre, IdNegocio, InsertadoPor, FechaInsertado)
		values
		( @Nombre, @IdNegocio, @InsertadoPor, GETDATE())
	end
Else
	Begin
	update tblMarcas 
		set 
			Nombre=@Nombre,
			IdNegocio = @IdNegocio,
			InsertadoPor = @InsertadoPor,
			FechaModificado = GETDATE()
		where IdMarca = @IdMarca
	End
End