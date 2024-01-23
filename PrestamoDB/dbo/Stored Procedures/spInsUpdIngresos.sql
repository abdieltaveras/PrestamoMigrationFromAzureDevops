CREATE PROCEDURE [dbo].[spInsUpdIngresos]
	@IdIngreso int,
	@IdPrestamo int,
	@IdCuota int,
	@Num_Cuota numeric(7,3),
	@Monto_Original_Cuota numeric(18,2),
	@Monto_Abonado numeric(18,2),
	@Balance numeric(18,2),
	@IdNegocio int,
	@Usuario varchar(100)
AS
Begin
IF(@IdIngreso <= 0)
	BEGIN
		insert into tblIngresos 
		(Idprestamo,Monto, InsertadoPor)
		values
		(@IdPrestamo,@Monto_Abonado,@Usuario)
	END

END