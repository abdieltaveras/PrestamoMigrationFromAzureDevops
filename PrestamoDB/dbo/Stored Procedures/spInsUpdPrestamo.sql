CREATE PROCEDURE [dbo].[spInsUpdPrestamo]
(@idPrestamo int, @idLocalidadNegocio int, @idnegocio int, @idPrestamoArenovar int=null, @idCliente int,
 @deudarenovacion decimal(14,2),  @idclasificacion int,	@IdTipoamortizacion int, @fechaemisionReal date, 
 @fechaemisionParaCalculos dateTime,  @fechavencimiento datetime, @idtasainteres int, @idtipomora int, @idperiodo int, 
 @cantidaddeperiodos int, @montoprestado decimal(14,2), @iddivisa int,  @interesgastodecierre decimal(6,2),  @montogastodecierre decimal(14,2), 
 @gastodecierreesdeducible bit, @cargarinteresalgastodecierre bit,  @financiarGastoDeCierre bit, @acomodarfechaalascuotas bit,  @fechainicioprimeracuota dateTime,
 @cuotas tpCuota READONLY, @codeudores tpCodeudores readonly,  @garantias tpGarantias readonly, @usuario varchar(40), @otrosCargosSinInteres decimal(14,2))
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
			INSERT INTO dbo.tblPrestamos (idNegocio, idlocalidadNegocio, PrestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, idCliente, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa,  InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre,FinanciarGastoDeCierre,  AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, OtrosCargosSinInteres)
			VALUES (@idnegocio, @idlocalidadNegocio, @prestamoNumero, @idPrestamoArenovar, @deudarenovacion, @idclasificacion, @idCliente, @Idtipoamortizacion, @fechaemisionReal, @fechaemisionParaCalculos, @fechavencimiento, @idtasainteres, @idtipomora, @idperiodo, @cantidaddeperiodos, @montoprestado, @iddivisa, @interesgastodecierre, @montogastodecierre, @gastodecierreesdeducible, @cargarinteresalgastodecierre, @financiarGastoDeCierre, @acomodarfechaalascuotas, @fechainicioprimeracuota, @usuario, getdate(), @otrosCargosSinInteres)
			  set @idPrestamo = (SELECT SCOPE_IDENTITY());
			  if ((select count(*) from @cuotas) > 0)
				  begin
					insert into tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes, GastoDeCierre, InteresDelGastoDeCierre, BceCapital, BceInteres, BceGastoDeCierre, BceInteresDelGastoDeCierre) 
					select @IdPrestamo, Numero, Fecha, Capital, Interes, 
						GastoDeCierre, InteresDelGastoDeCierre, Capital, Interes, GastoDeCierre, InteresDelGastoDeCierre from @cuotas
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
	End
	else
	begin
		update tblPrestamos
		set  IdPrestamoARenovar=@idPrestamoArenovar, DeudaRenovacion =@deudarenovacion, idClasificacion =@idclasificacion,
		idCliente = @idCliente, IdTipoAmortizacion=@IdTipoamortizacion, FechaEmisionReal=@fechaemisionReal, FechaEmisionParaCalculo =@fechaemisionParaCalculos,
		FechaVencimiento =@fechavencimiento, IdTasaInteres =@idtasainteres, idTipoMora=@idtipomora, idPeriodo=@idperiodo,
		CantidadDePeriodos=@cantidaddeperiodos, MontoPrestado=@montoprestado, IdDivisa=@iddivisa,  
		InteresGastoDeCierre=@interesgastodecierre, MontoGastoDeCierre=@montogastodecierre, GastoDeCierreEsDeducible=@gastodecierreesdeducible, 
		CargarInteresAlGastoDeCierre =@cargarinteresalgastodecierre,FinanciarGastoDeCierre=@financiarGastoDeCierre,  AcomodarFechaALasCuotas=@acomodarfechaalascuotas, 
		FechaInicioPrimeraCuota=@fechainicioprimeracuota,  OtrosCargosSinInteres=@otrosCargosSinInteres, ModificadoPor = @Usuario, FechaModificado=getdate()
		where idPrestamo = @idPrestamo and BorradoPor is null
		--RAISERROR('Error: no se ha implementado la actualizacion aun URGENTE DEBE HACERLO',17,1); 
		
		merge tblCuotas as target
		using @Cuotas as source
		on (target.Numero = source.numero and target.IdPrestamo = @idPrestamo)
		when matched then update set 
			target.Numero = source.Numero, target.Fecha = source.Fecha, target.Capital = source.capital,
			target.Interes = source.interes, target.GastoDeCierre=source.GastodeCierre, 
			target.InteresDelGastoDeCierre = source.InteresDelGastoDeCierre, target.BceCapital = source.Capital, 
			target.BceInteres = source.Interes, target.BceGastoDeCierre = source.GastoDeCierre, target.BceInteresDelGastoDeCierre = source.InteresDelGastoDeCierre 
	    when not matched then insert		
			(IdPrestamo,Numero, Fecha, Capital, Interes, GastoDeCierre,
			InteresDelGastoDeCierre, BceCapital, BceInteres, BceGastoDeCierre, BceInteresDelGastoDeCierre) 
			values
			 (@IdPrestamo, source.Numero, source.Fecha, source.Capital, source.Interes, source.GastoDeCierre, 
			 source.InteresDelGastoDeCierre, source.Capital, source.Interes, source.GastoDeCierre, source.InteresDelGastoDeCierre)		
	   when not matched by source
	   then delete;
	end
	SELECT @IdPrestamo
end