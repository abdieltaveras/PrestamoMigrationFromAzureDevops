CREATE PROCEDURE [dbo].[spGetPrestamoCliente]
(
	@idPrestamo int =-1,
	@idGarantia int = -1,
	@idCliente int = -1,
	@NoIdentificacion varchar(50) = '',
	@Nombres varchar(100)='',
	@Apellidos varchar(100)=''
)
as
begin

	SELECT pres.IdPrestamo, pres.idNegocio, pres.idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, pres.idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, TotalPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota,
	pres.InsertadoPor, pres.FechaInsertado, pres.ModificadoPor, pres.FechaModificado, pres.BorradoPor, pres.FechaBorrado,
		clie.Codigo as CodigoCliente, clie.Nombres, clie.Apellidos, clie.IdTipoIdentificacion , clie.NoIdentificacion as NumeracionDocumentoIdentidad, clie.TelefonoCasa, clie.TelefonoMovil, clie.Imagen1FileName, clie.Imagen2FileName
	FROM	dbo.tblPrestamos as pres
	inner Join tblClientes clie 
	on clie.idCliente = pres.IdCliente
	where (@IdPrestamo=-1 or pres.idPrestamo = @IdPrestamo) and
	(@idCliente = -1 or pres.idCliente = @idCliente) 
	and ((@NoIdentificacion='') or (clie.NoIdentificacion =@NoIdentificacion)) 
	and ((@Nombres='') or (clie.Nombres like '%'+@Nombres+'%')) 
	and ((@Apellidos='') or (clie.Apellidos like '%'+@Apellidos+'%')) 
	

End