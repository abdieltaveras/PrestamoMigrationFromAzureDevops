CREATE PROC [dbo].[spInsUpdPrestamoEstatus]
	@IdPrestamoEstatus int = 0,
	@IdPrestamo int,
	@IdEstatus int,
	@Comentario varchar(1000) = '',
	@IdNegocio int,
	@IdLocalidadNegocio int = -1,
	@Activo bit = 1,
	@Usuario varchar(100)

AS
IF(@IdPrestamoEstatus = 0)
BEGIN
INSERT INTO tblPrestamoEstatus(IdPrestamo,IdEstatus,Comentario,IdNegocio,IdLocalidadNegocio, Activo, InsertadoPor, FechaInsertado)
values (@IdPrestamo,@IdEstatus,@Comentario,@IdNegocio,@IdLocalidadNegocio, @Activo, @Usuario,SYSDATETIME())
END
ELSE
BEGIN
	UPDATE tblPrestamoEstatus SET
	IdEstatus = @IdEstatus
	WHERE IdPrestamoEstatus = @IdPrestamoEstatus
END