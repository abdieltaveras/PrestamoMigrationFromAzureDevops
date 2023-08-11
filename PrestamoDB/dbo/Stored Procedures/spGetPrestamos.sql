create PROCEDURE [dbo].[spGetPrestamos]
(
	@idNegocio int,
	@idPrestamo int =-1,
	@FechaEmisionRealDesde dateTime='19000101',
	@FechaEmisionRealHasta dateTime='19000101',
	@IdCliente int =-1,
	@IdGarantia int =-1,
	@idLocalidadNegocio int =-1,
	@condicionBorrado int = 0 
)
as
begin
	SELECT IdPrestamo, pres.idNegocio, pres.idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDeCuotas, MontoPrestado, TotalPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, pres.InsertadoPor, pres.FechaInsertado, pres.ModificadoPor, pres.FechaModificado, pres.BorradoPor, pres.FechaBorrado
	FROM	dbo.tblPrestamos as pres
	-- condiciones organizar esta parte solo esta con el idPrestamo
	where (@IdPrestamo=-1 or idPrestamo = @IdPrestamo)
		and ((@condicionBorrado= 0 and BorradoPor is null) 
		or (@condicionBorrado=1 and BorradoPor is not null)
		or (@condicionBorrado=-1))
End