﻿CREATE PROCEDURE [dbo].[spInsUpdMarca]
	@IdMarca int,
	@Nombre varchar(50),
	@IdNegocio int,
	@IdLocalidadNegocio int = -1,
	@Usuario varchar(100),
	@Codigo varchar(10)='',
	@Activo bit = 1
AS
Begin
if (@IdMarca <= 0)
	begin
		insert into tblMarcas
		( Nombre,Codigo, IdNegocio, InsertadoPor, FechaInsertado)
		values
		( @Nombre,@Codigo, @IdNegocio, @Usuario, GETDATE())
	end
Else
	Begin
	update tblMarcas 
		set 
			Nombre=@Nombre,
			Codigo = @Codigo,
			IdNegocio = @IdNegocio,
			ModificadoPor = @Usuario,
			FechaModificado = GETDATE()
		where IdMarca = @IdMarca
	End
End