create PROCEDURE [dbo].[spGetPrestamos]
(
	@idPrestamo int =-1,
	@FechaEmisionRealDesde dateTime,
	@FechaEmisionRealHasta dateTime,
	@IdCliente int =-1,
	@IdGarantia int =-1,
	@Anulado int =-1
)
as
begin
	SELECT IdPrestamo, idNegocio, idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, TotalPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, SumarGastoDeCierreALasCuotas, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado
	FROM	dbo.tblPrestamos
	where (@IdPrestamo=-1 or idPrestamo = @IdPrestamo) 
End