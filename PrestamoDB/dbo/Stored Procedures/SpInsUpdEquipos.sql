CREATE PROCEDURE dbo.spInsUpdEquipo
	@IdEquipo INT,
	@IdNegocio INT  ,
	@Nombre VARCHAR(50),
	@Descripcion varchar(50),
	@UltimoAcceso datetime, 
	@Usuario varchar(50)
AS
Begin
declare @currentId int =@IdEquipo
declare @codigo varchar(40)= NEWID()
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
			Nombre = @Nombre,
			Descripcion = @Descripcion,
			ModificadoPor = @Usuario,
			FechaModificado = GETDATE()
		where IdEquipo = @IdEquipo
	End
End
select @currentId as IdEquipo, @Codigo as Codigo 
