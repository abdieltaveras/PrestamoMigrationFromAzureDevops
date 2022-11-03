CREATE PROC [spInsUpdComentario]
@IdComentario int= 0,
@IdTransaccion int,
@TablaOrigen varchar(50),
@Detalle varchar(50),
@IdNegocio int, 
@IdLocalidadNegocio int, 
@Usuario varchar(100)
as
IF(@IdComentario = 0)
BEGIN
	INSERT INTO tblComentarios (IdTransaccion,TablaOrigen, Detalle,IdNegocio,
	IdLocalidadNegocio, InsertadoPor, FechaInsertado)
	values (@IdTransaccion,@TablaOrigen,@Detalle,@idnegocio,@idLocalidadNegocio,@Usuario,SYSDATETIME())
END
ELSE
BEGIN
	UPDATE tblComentarios SET
	Detalle = @Detalle,
	IdNegocio = @IdNegocio,
	IdLocalidadNegocio = @IdLocalidadNegocio,
	ModificadoPor = @Usuario,
	FechaModificado = SYSDATETIME()
END