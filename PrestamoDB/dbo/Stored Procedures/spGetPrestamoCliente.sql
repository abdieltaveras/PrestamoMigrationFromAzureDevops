USE [PrestamoDB]
GO
/****** Object:  StoredProcedure [dbo].[spGetPrestamoCliente]    Script Date: 4/21/2024 11:15:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[spGetPrestamoCliente]
(
	@idPrestamo int =-1,
	@idGarantia int = -1,
	@idCliente int = -1,
	@NoIdentificacion varchar(50) = '',
	@Nombres varchar(100)='',
	@Apellidos varchar(100)='',
	@NombreCompleto varchar (100)= '',
	@Placa varchar(100)='',
	@Matricula varchar(100)='',
	@Chasis varchar(100)=''
)
as
begin

	SELECT pres.IdPrestamo, pres.idNegocio, pres.idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, pres.idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, TotalPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota,
	pres.InsertadoPor, pres.FechaInsertado, pres.ModificadoPor, pres.FechaModificado, pres.BorradoPor, pres.FechaBorrado,
		clie.Codigo as CodigoCliente, clie.Nombres, clie.Apellidos, clie.IdTipoIdentificacion , clie.NoIdentificacion as NumeracionDocumentoIdentidad, clie.TelefonoCasa, clie.TelefonoMovil 
	FROM	dbo.tblPrestamos as pres
	inner Join tblClientes clie 
	on clie.idCliente = pres.IdCliente
	left join tblPrestamoGarantias presgar
	on pres.IdPrestamo = presgar.IdPrestamo
	left join tblGarantias gar
	on presgar.IdGarantia = gar.IdGarantia
	where (@IdPrestamo=-1 or pres.idPrestamo = @IdPrestamo) 
	and (@idGarantia = -1 or presgar.IdGarantia = @idGarantia) 
	and (@idCliente = -1 or pres.idCliente = @idCliente) 
	and ((@NoIdentificacion='') or (clie.NoIdentificacion =@NoIdentificacion)) 
	and ((@Nombres='') or (clie.Nombres like '%'+@Nombres+'%')) 
	and ((@Apellidos='') or (clie.Apellidos like '%'+@Apellidos+'%')) 
	and ((@NombreCompleto='') or (CONCAT(clie.Nombres, ' ', clie.Apellidos) like '%'+@NombreCompleto+'%')) 
    and(@Matricula ='' or JSON_VALUE(Detalles, '$.Matricula') like '%'+@Matricula+'%') 
	and(@Placa ='' or JSON_VALUE(Detalles, '$.Placa') like '%'+@Placa+'%') 
	and(@Chasis ='' or JSON_VALUE(Detalles, '$.Chasis') like '%'+@Chasis+'%') 
End