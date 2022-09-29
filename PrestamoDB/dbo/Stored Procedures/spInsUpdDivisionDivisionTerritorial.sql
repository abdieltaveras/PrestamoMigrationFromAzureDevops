CREATE PROCEDURE [dbo].[spInsUpdDivisionTerritorial]
	@IdDivisionTerritorial int,
	@IdDivisionTerritorialPadre int= null,
	@IdNegocio int=null,
	@IdLocalidadNegocio int,
	@Nombre varchar(100),
	@PermiteCalle bit,
	@Activo bit = 1,
	@Codigo varchar(10) = '',
	@Usuario varchar(100)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdDivisionTerritorial <= 0)
	begin
		insert into tblDivisionTerritorial
			(IdDivisionTerritorialPadre, IdNegocio, Nombre, PermiteCalle, Activo, InsertadoPor, FechaInsertado)
		values
			( @IdDivisionTerritorialPadre, @IdNegocio, @Nombre, @PermiteCalle,1,  @Usuario, GetDate())
		set @IdDivisionTerritorial = (select SCOPE_IDENTITY())
	end
Else
	Begin
	update tblDivisionTerritorial 
		set
			idNegocio = @idNegocio,
			IdDivisionTerritorialPadre = @IdDivisionTerritorialPadre,
			Nombre = @Nombre,
			PermiteCalle=@PermiteCalle,
			ModificadoPor=@Usuario,
			Activo=@Activo,
			FechaModificado = getdate()
		where IdDivisionTerritorial = @IdDivisionTerritorial
	End
End
select @IdDivisionTerritorial

