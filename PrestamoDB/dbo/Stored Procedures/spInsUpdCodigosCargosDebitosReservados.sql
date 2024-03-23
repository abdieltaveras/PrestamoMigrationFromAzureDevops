CREATE proc [dbo].[spInsUpdCodigosCargosDebitosReservados]
	@IdCodigoCargo int = -1
	, @nombre varchar(50)
	, @descripcion  varchar(100)
	, @IdNegocio int
	, @IdLocalidadNegocio int
	, @Usuario varchar(100)
as
if @IdCodigoCargo <= 0 	
BEGIN
INSERT INTO dbo.tblCodigosCargosDebitosReservados(
	Nombre
	, Descripcion
	, InsertadoPor
	, FechaInsertado
	)
VALUES
	(
	@nombre
	, @descripcion
	, @Usuario
	, SYSDATETIME()
	)
	select @@IDENTITY
END
	ELSE
BEGIN
	UPDATE tblCodigosCargosDebitosReservados
	set Nombre = @Nombre,
	Descripcion = @Descripcion,
	ModificadoPor = @Usuario,
	FechaModificado = SYSDATETIME()
	where IdCodigoCargo = @IdCodigoCargo
	select @IdCodigoCargo
END