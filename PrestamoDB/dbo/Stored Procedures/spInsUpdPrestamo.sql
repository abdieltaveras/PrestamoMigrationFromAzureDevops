CREATE PROCEDURE [dbo].[spInsUpdPrestamo]
(@idPrestamo int, @idnegocio int, @idprestamoarenovar int, @deudarenovacion decimal(14,2), @idclasificacion int,	@tipoamortizacion int, @fechaemisionReal date, @fechaemisionParaCalculos dateTime, @fechavencimiento datetime, @idtasainteres int, @idtipomora int, @idperiodo int, @cantidaddeperiodos int, @montoprestado decimal(14,2), @iddivisa int,  @interesgastodecierre decimal(6,2), @montogastodecierre decimal(14,2), @gastodecierreesdeducible bit, @cargarinteresalgastodecierre bit, @sumargastodecierrealascuotas bit, @acomodarfechaalascuotas bit, @fechainicioprimeracuota dateTime, @usuario varchar(40))
	
AS
begin
	if (@idPrestamo<=0)
	begin
		SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
		BEGIN TRANSACTION 
		begin try
			declare @prestamoNumero varchar(20) 
			exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output
			INSERT INTO dbo.tblPrestamos (idNegocio, PrestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, TipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa,  InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, SumarGastoDeCierreALasCuotas, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado)
			VALUES (@idnegocio, @prestamoNumero, @idprestamoarenovar, @deudarenovacion, @idclasificacion, @tipoamortizacion, @fechaemisionReal, @fechaemisionParaCalculos, @fechavencimiento, @idtasainteres, @idtipomora, @idperiodo, @cantidaddeperiodos, @montoprestado, @iddivisa, @interesgastodecierre, @montogastodecierre, @gastodecierreesdeducible, @cargarinteresalgastodecierre, @sumargastodecierrealascuotas, @acomodarfechaalascuotas, @fechainicioprimeracuota, @usuario, getdate())
			commit
			SELECT SCOPE_IDENTITY(); 
		end try
		begin catch
			rollback
			declare @errorMessage varchar(max) =  (select ERROR_MESSAGE()) 
			RAISERROR(@errorMessage,17,1); 
		end catch
	End
end

