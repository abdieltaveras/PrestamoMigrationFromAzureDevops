﻿CREATE PROCEDURE [dbo].[spInsUpdPrestamo]
(@idPrestamo int, @idnegocio int, @idprestamoarenovar int=null, @idCliente int,@IdStatus int, @deudarenovacion decimal(14,2), 
 @idclasificacion int,	@IdTipoamortizacion int, @fechaemisionReal date, @fechaemisionParaCalculos dateTime, 
 @fechavencimiento datetime, @idtasainteres int, @idtipomora int, @idperiodo int, 
 @cantidaddeperiodos int, @montoprestado decimal(14,2), @iddivisa int,  @interesgastodecierre decimal(6,2), 
 @montogastodecierre decimal(14,2), @gastodecierreesdeducible bit, @cargarinteresalgastodecierre bit, 
 @financiarGastoDeCierre bit, @acomodarfechaalascuotas bit, 
 @fechainicioprimeracuota dateTime, @cuotas tpCuota READONLY, @codeudores tpCodeudores readonly, 
 @garantias tpGarantias readonly, @usuario varchar(40), @otrosCargosSinInteres decimal(14,2))
	
AS
begin
	if (@idPrestamoArenovar<=0) 
		begin  
			set @idPrestamoARenovar=null; 
		end

	if (@idPrestamo<=0)
	begin
		SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
		BEGIN TRANSACTION 
		begin try
			declare @prestamoNumero varchar(20) 
			exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output
			INSERT INTO dbo.tblPrestamos (idNegocio, PrestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, idCliente,IdStatus, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa,  InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre,FinanciarGastoDeCierre,  AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, OtrosCargosSinInteres)
			VALUES (@idnegocio, @prestamoNumero, @idprestamoarenovar, @deudarenovacion, @idclasificacion, @idCliente,@IdStatus, @Idtipoamortizacion, @fechaemisionReal, @fechaemisionParaCalculos, @fechavencimiento, @idtasainteres, @idtipomora, @idperiodo, @cantidaddeperiodos, @montoprestado, @iddivisa, @interesgastodecierre, @montogastodecierre, @gastodecierreesdeducible, @cargarinteresalgastodecierre, @financiarGastoDeCierre, @acomodarfechaalascuotas, @fechainicioprimeracuota, @usuario, getdate(), @otrosCargosSinInteres)
			  set @idPrestamo = (SELECT SCOPE_IDENTITY());
			  if ((select count(*) from @cuotas) > 0)
			  begin
				insert into tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) select @IdPrestamo, Numero, Fecha, Capital, Interes from @cuotas
			  end
			  else
			  begin
			    RAISERROR('Error: no envio ninguna cuota a ser generada',17,1); 
			  end
			  if (exists (select 1 from @garantias))
			  begin
				insert into tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor) select @IdPrestamo, IdGarantia, @usuario from @garantias
			  end
			  if (exists (select 1 from @codeudores))
			  begin
				insert into tblPrestamoCodeudores (IdPrestamo, IdCodeudor, InsertadoPor) select @IdPrestamo, IdCodeudor, @usuario from @codeudores
			  end
			commit
		end try
		begin catch
			rollback
			declare @errorMessage varchar(max) =  (select ERROR_MESSAGE()) 
			RAISERROR(@errorMessage,17,1); 
		end catch
		SELECT @IdPrestamo
	End
	else
	begin
		RAISERROR('Error: no se ha implementado la actualizacion aun URGENTE DEBE HACERLO',17,1); 
	end
end

