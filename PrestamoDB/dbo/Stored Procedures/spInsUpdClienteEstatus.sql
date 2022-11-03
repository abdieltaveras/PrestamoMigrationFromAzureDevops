CREATE PROC [dbo].[spInsUpdClienteEstatus]
	@IdClienteEstatus int = 0,
	@IdCliente int,
	@IdEstatus int,
	@Comentario varchar(1000) = '',
	@IdNegocio int,
	@IdLocalidadNegocio int = -1,
	@Activo bit = 1,
	@Usuario varchar(100)

AS
IF(@IdClienteEstatus = 0)
BEGIN
INSERT INTO tblClienteEstatus (IdCliente,IdEstatus,Comentario,IdNegocio,IdLocalidadNegocio, Activo, InsertadoPor, FechaInsertado)
values (@IdCliente,@IdEstatus,@Comentario,@IdNegocio,@IdLocalidadNegocio, @Activo, @Usuario,SYSDATETIME())
END
ELSE
BEGIN
	UPDATE tblClienteEstatus SET
	IdEstatus = @IdEstatus
	WHERE IdClienteEstatus = @IdClienteEstatus
END