CREATE PROC spInsUpdIngresos
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
if(@IdIngreso <= 0)
	begin
		insert into tblIngresos 
		(Idprestamo,IdCuota,Num_Cuota,Monto_Original_Cuota,Monto_Abonado,Balance,InsertadoPor)
		values
		(@IdPrestamo,@IdCuota,@Num_Cuota,@Monto_Original_Cuota,@Monto_Abonado,@Balance,@Usuario)
	end

End