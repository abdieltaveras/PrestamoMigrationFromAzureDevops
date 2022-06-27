CREATE PROCEDURE [dbo].[spInsUpdTerritorios]
	@IdDivisionTerritorial int,
	@IdDivisionTerritorialPadre int,
	@IdNegocio int,
	@IdLocalidadNegocio int=-1,
	@Nombre varchar(100),
	@PermiteCalle bit,
	@Activo bit = 1,
	@Codigo varchar(10) = '',
	@Usuario varchar(100)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdDivisionTerritorial = 0)
	begin
		insert into tblDivisionTerritorial
			(IdDivisionTerritorialPadre, IdNegocio, Nombre, PermiteCalle, InsertadoPor, FechaInsertado)
		values
			( @IdDivisionTerritorialPadre, @IdNegocio, @Nombre, @PermiteCalle, @Usuario, GetDate())
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
			FechaModificado = getdate()
		where IdDivisionTerritorial = @IdDivisionTerritorial
	End
End

