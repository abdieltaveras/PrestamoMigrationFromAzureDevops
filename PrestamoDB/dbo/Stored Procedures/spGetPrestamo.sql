﻿create PROCEDURE [dbo].[spGetPrestamo]
(
	@idPrestamo int =-1
)
as
begin
	SELECT IdPrestamo, pres.idNegocio, pres.idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDeCuotas, MontoPrestado, TotalPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, pres.InsertadoPor, pres.FechaInsertado, pres.ModificadoPor, pres.FechaModificado, pres.BorradoPor, pres.FechaBorrado,
		clie.Codigo as CodigoCliente, clie.Nombres, clie.Apellidos, clie.IdTipoIdentificacion , clie.NoIdentificacion as NumeracionDocumentoIdentidad, clie.TelefonoCasa, clie.TelefonoMovil 
	FROM	dbo.tblPrestamos as pres
	inner Join tblClientes as clie on clie.idCliente = pres.IdCliente
	where (@IdPrestamo=-1 or idPrestamo = @IdPrestamo) 

	SELECT IdGarantia, gara.IdClasificacion, NoIdentificacion as NumeracionGarantia, Detalles ,
	marc.Nombre as NombreMarca, mode.Nombre as NombreModelo, tiga.Nombre as NombreTipoGarantia
	from tblGarantias as gara
	inner join tblMarcas marc on marc.IdMarca = gara.IdMarca
	inner join tblModelos mode on mode.IdModelo = gara.IdModelo
	inner join tblTiposGarantia tiga on tiga.IdTipoGarantia = gara.IdTipoGarantia
	where IdGarantia in (select idgarantia from tblPrestamoGarantias where IdPrestamo = @idPrestamo)
End