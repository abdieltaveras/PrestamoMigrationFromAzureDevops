CREATE PROCEDURE dbo.spInsUpdEquipo
	@IdEquipo INT,
	@IdNegocio INT  ,
	@IdLocalidadNegocio int = -1,
	@Codigo VARCHAR(40),
	@Nombre VARCHAR(50),
	@Descripcion varchar(50),
	@UltimoAcceso datetime, 
	@Usuario varchar(50)
AS
Begin
declare @currentId int =@IdEquipo
if (@codigo='')
	begin
		set @codigo = NEWID()
	end
if (@IdEquipo = 0)
	begin
		insert into tblEquipos
		(Nombre, IdNegocio, Codigo, Descripcion, InsertadoPor, FechaInsertado)
		values
		(@Nombre, @idNegocio, @codigo, @Descripcion,@Usuario, GETDATE())
		set @currentId= (SELECT IDENT_CURRENT('tblEquipos'))
	end
Else
	Begin
	update tblEquipos
		set 
			Codigo = @Codigo,
			Nombre = @Nombre,
			Descripcion = @Descripcion,
			ModificadoPor = @Usuario,
			FechaModificado = GETDATE()
		where IdEquipo = @IdEquipo
	End
End
select @currentId as IdEquipo, @Codigo as Codigo 
