create procedure [dbo].[spInsUpdMaestroDetalleDrCxCPrestamo]
(
	@maestroCxC tpMaestroCxCPrestamo readonly, @detallesCargos tpDetalleDrCxC readonly 
)
as
begin
	declare @iteracion int = 0
	-- solo para insertar transacciones de tipo debitos
	declare  @registrosMaestroCxC tpMaestroCxCPrestamo
	insert  into  @registrosMaestroCxC select * from @maestroCxC
	--insert into @registrosMaestroCxC (IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson)
	--select  IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson from @maestroCxC
	while exists (select top 1 idTransaccion from @registrosMaestroCxC)
	begin
	    -- informacion del registro maestro
		declare @registroActual tpMaestroCxCPrestamo 
		delete  @registroActual -- si no elimina, se queda el registro anterior
		insert into @registroActual (IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson)
		select top 1 IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson from  @registrosMaestroCxC
		declare @idReferencia uniqueidentifier = (select IdReferencia from @registroActual)
		
		declare @idPrestamo int = (select idPrestamo from @registroActual)
		declare @CodigoTipoTransaccion varchar(10) = (select CodigoTipoTransaccion from @registroActual)
		declare @numeroTransaccion int = (select NumeroTransaccion from @registroActual)
		declare @fecha datetime = (select fecha from @registroActual)
		declare @monto numeric(18,2) = (select monto from @registroActual)
		declare @balance numeric(18,2) = (select balance from @registroActual)
		declare @otrosDetallesJson varchar(200) = (select otrosDetallesJson from @registroActual)
		declare @detallesCargosJson varchar(200) = (select detallesCargosJson from @registroActual)
		--notar que si idTransaccion no existe en tblCuotaMaestro no se va a actualizar el registro en las cuotas
		declare @idTransaccion int = 0
		--if (@CodigoTipoTransaccion='CA')
		--begin
			--set @idTransaccion =  (select idTransaccion  from tblMaestrosCxCPrestamo where idprestamo = @idprestamo and numeroTransaccion =@numeroTransaccion )
		--end
		--else
		--begin
			 set @idTransaccion = (select IdTransaccion from @registroActual)
		--end

		if isnull(@idTransaccion ,0)=0
		begin
			insert into tblMaestrosCxCPrestamo(idPrestamo, TipoDrCr, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson) 
			select top 1  IdPrestamo, 'D', CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson from  @registroActual
			set @idTransaccion  = scope_identity()
		end
		else
		begin
			update tblMaestrosCxCPrestamo 
			set 
				Fecha =@fecha,
				Monto =@monto,
				Balance=@balance, 
				OtrosDetallesJson= @otrosDetallesJson,
				DetallesCargosJson = @detallesCargosJson
			where idTransaccion = @idTransaccion
				
		end
		-- insUpdDetalles
		-- primero obtener una lista de los detalles a insertar
		
		--if (1=2)
		--procesando el detalle
		begin
			declare @detalles_Cargos tpDetalleDrCxC 
			insert  into  @detalles_cargos select * from @detallesCargos where IdReferenciaMaestro=@idReferencia 
			declare @IdTransaccionMaestro int = @idTransaccion
			exec SpInsUpdDetallesDrCxC  @detalles_cargos, @idTransaccion
			if (1=2)
			begin
				--insert  into @detalles_cargos (IdTransaccion, idTransaccionMaestro,IdReferencia, CodigoCargo, Balance, Monto)  select IdTransaccion, IdTransaccionMaestro, IdReferencia, CodigoCargo, Balance, Monto from @detallesCargos
				insert  into  @detalles_cargos select * from @detallesCargos 
				update @detalles_Cargos set IdTransaccionMaestro = @IdTransaccionMaestro
				--select * into  tblTestDetallesCargos from @detallesCargos
				while exists(select top 1 idTransaccion from @detalles_Cargos)
				begin
					declare @idReferenciaDetalle uniqueidentifier= (select top 1 IdReferenciaDetalle from @detalles_Cargos)
					declare @idReferenciaMaestro uniqueidentifier= (select top 1 IdReferenciaMaestro from @detalles_Cargos)
					declare @codigoCargo varchar(5) = (select top 1 CodigoCargo from @detalles_Cargos)
					declare @montoDetalle numeric(18,2) = (select top 1 Monto from @detalles_Cargos)
					declare @balanceDetalle numeric(18,2) = (select top 1 Balance from @detalles_Cargos)
					declare @idTransaccionDetalle int = (select IdTransaccion   from tblDetallesDrCxC  where idTransaccionMaestro=@idTransaccionMaestro and CodigoCargo=@codigoCargo)
					if (isnull(@idTransaccionDetalle,0)=0)
					begin
						insert into tblDetallesDrCxC(idTransaccionMaestro, Balance, Monto, CodigoCargo)
						values (@idTransaccionMaestro, @balanceDetalle,@montoDetalle, @codigoCargo)  
					end
					else
					begin
						update tblDetallesDrCxC
						set    Balance = @balanceDetalle, Monto   = @montoDetalle,CodigoCargo = @codigoCargo 
						where IdTransaccion = @idTransaccionDetalle
					end
					delete @detalles_Cargos where idTransaccionMaestro=@idTransaccionMaestro and CodigoCargo=@codigoCargo
				end	
			--eliminar los detalles guardados que ya estan eliminados (mayormente para la cuota)
				begin
				declare @detallesGuardados tpDetalleDrCxC 
				--insert into @detallesGuardados select * from tblDetallesDrCxC where idTransaccionMaestro=@idTransaccionMaestro
				while exists(select top 1 idTransaccion from @detallesGuardados)
				begin
					select getdate()
				end
			end
		end
			-- borra el registro maestro y continuar el loop
			--set @iteracion = @iteracion+1
			--declare @countantes int = (select count(IdTransaccion) from @registrosMaestroCxC)
			--declare @id int = (select IdTransaccion from @registrosMaestroCxC where idReferencia = @idReferencia)
			--if (isnull(@id,0))=0
			--begin
			--	select * from @registrosMaestroCxC
			--end
			delete @registrosMaestroCxC where idReferencia = @idReferencia
			--declare @countdespues int = (select count(IdTransaccion) from @registrosMaestroCxC)
		end
	end
end
