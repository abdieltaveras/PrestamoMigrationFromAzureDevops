CREATE PROCEDURE [dbo].[spInsUpdTerritorios]
	@IdDivisionTerritorial int,
	@IdLocalidadPadre int,
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
			(IdLocalidadPadre, IdDivisionTerritorialPadre, IdNegocio, Nombre, PermiteCalle, InsertadoPor, FechaInsertado)
		values
			(@IdLocalidadPadre, @IdDivisionTerritorialPadre, @IdNegocio, @Nombre, @PermiteCalle, @Usuario, GetDate())
	end
Else
	Begin
	update tblDivisionTerritorial 
		set
			idNegocio = @idNegocio,
			IdDivisionTerritorialPadre = @IdDivisionTerritorialPadre,
			Nombre = @Nombre,
			IdLocalidadPadre = @IdLocalidadPadre,
			PermiteCalle=@PermiteCalle,
			ModificadoPor=@Usuario,
			FechaModificado = getdate()
		where IdDivisionTerritorial = @IdDivisionTerritorial
	End
End

