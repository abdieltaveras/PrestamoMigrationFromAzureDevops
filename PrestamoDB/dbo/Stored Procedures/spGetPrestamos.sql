﻿create PROCEDURE [dbo].[spGetPrestamos]
(
	@idNegocio int,
	@idPrestamo int =-1,
	@FechaEmisionRealDesde dateTime='19000101',
	@FechaEmisionRealHasta dateTime='19000101',
	@IdCliente int =-1,
	@IdGarantia int =-1,
	@idLocalidadNegocio int =-1,
	@Borrado int =-1
)
as
begin
	SELECT IdPrestamo, pres.idNegocio, pres.idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, TotalPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, pres.InsertadoPor, pres.FechaInsertado, pres.ModificadoPor, pres.FechaModificado, pres.BorradoPor, pres.FechaBorrado
	FROM	dbo.tblPrestamos as pres
	-- condiciones organizar esta parte solo esta con el idPrestamo
	where (@IdPrestamo=-1 or idPrestamo = @IdPrestamo) 

		

End