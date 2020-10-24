CREATE PROCEDURE [dbo].[spGetDatosPIngreso]
	@IdPrestamo int,
	@Anulado int=0,
	@IdNegocio int=0,
	@Usuario varchar(50)=''
AS
IF  exists(select top 1 * from tblIngresos where IdPrestamo = @IdPrestamo)
	BEGIN
		select top 1 ing.IdPrestamo,
		ing.IdCuota,
		ing.Num_Cuota,
		ing.Monto_Abonado,
		ing.Balance,
		cuo.Capital + cuo.Interes + cuo.GastoDeCierre+cuo.InteresDelGastoDeCierre as Monto_Original_Cuota
		from tblClientes cl 
		inner join tblPrestamos pre on cl.IdCliente = pre.idCliente 
		inner join tblCuotas cuo on cuo.IdPrestamo= pre.IdPrestamo
		inner join tblIngresos ing on ing.IdPrestamo = pre.IdPrestamo
		where ing.IdPrestamo = @IdPrestamo
		order by ing.IdIngreso desc
	END
ELSE
	BEGIN
		select top 1 cuo.IdPrestamo as IdPrestamo,
		cuo.IdCuota as IdCuota,
		cuo.Numero as Num_Cuota,
		cuo.Capital + cuo.Interes + cuo.GastoDeCierre+cuo.InteresDelGastoDeCierre as Monto_Original_Cuota
		from tblClientes cl 
		inner join tblPrestamos pre on cl.IdCliente = pre.idCliente 
		inner join tblCuotas cuo on cuo.IdPrestamo= pre.IdPrestamo
		where cuo.IdPrestamo = @IdPrestamo
		order by cuo.Numero asc
	END
