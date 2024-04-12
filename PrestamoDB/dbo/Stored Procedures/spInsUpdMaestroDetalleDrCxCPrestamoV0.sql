create procedure [dbo].[spInsUpdMaestroDetalleDrCxCPrestamoV0]
(
     --@IdTransaccion int, @IdPrestamo int, @IdNegocio int, @IdLocalidadNegocio int,
	 @maestroCxC tpMaestroCxCPrestamo readonly, @detallesCargos tpDetalleDrCxC readonly, @crearTablas bit=0
)
as
begin

	-- solo para insertar transacciones de tipo debitos
	declare @TransRecibidas int = (select count(idTransaccion)  from @maestroCxC)
	declare  @registrosMaestroCxC tpMaestroCxCPrestamo
	insert  into  @registrosMaestroCxC select * from @maestroCxC
	if (@crearTablas=1)
	begin
		IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
				WHERE TABLE_SCHEMA = 'dbo' 
				AND  TABLE_NAME = 'tmpMaestroCxC'))
		begin
			drop table tmpMaestroCxC
		end

		select * into tmpMaestrocxc from @maestroCxC
		IF (EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
				WHERE TABLE_SCHEMA = 'dbo' 
				AND  TABLE_NAME = 'tmpDetalleDrCxC'))
		begin
			drop table tmpDetalleDrCxC
		end
		select * into tmpDetalleDrCxC from @detallesCargos
	end
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
			declare @updated bit = 0
			update tblMaestrosCxCPrestamo 
			set 
				Fecha =@fecha,
				Monto =@monto,
				Balance=@balance, 
				OtrosDetallesJson= @otrosDetallesJson,
				DetallesCargosJson = @detallesCargosJson,
				@updated = 1
			where idTransaccion = @idTransaccion
			if (@updated=0)
			begin
				declare @intToStrig varchar(10)=(select cast(@idTransaccion as varchar))
				declare @errorMessage varchar(100)=(select concat('el id',@intToStrig,'suministrado para actualizar el maestro de CxC no existe'))
				RAISERROR (
		           @errorMessage, -- Mensaje de ejemplo
				   10, -- Severity,  
					1   -- State
					);
					--RETURN 0
			end
		end
		-- insUpdDetalles
		-- primero obtener una lista de los detalles a insertar
		
		
		--procesando el detalle
		begin
			declare @detalles_cargos tpDetalleDrCxC 
			insert  into  @detalles_cargos select * from @detallesCargos where IdReferenciaMaestro=@idReferencia 
			declare @IdTransaccionMaestro int = @idTransaccion
			select * from @detalles_cargos
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
			--declare @countdespues int = (select count(IdTransaccion) from @registrosMaestroCxC)
		end
		delete @registrosMaestroCxC where idReferencia = @idReferencia
		
	end
	--eliminar solo funciona con las cuota, cuando modifiquen el prestamo y le pongan menos cuotas
	if (@CodigoTipoTransaccion = 'CT')
		begin 
			--determinar si son las misma cantidad de registros
			declare @TransGuardadas int = (select count(IdTransaccion)  from tblMaestrosCxCPrestamo where IdPrestamo=@idPrestamo and CodigoTipoTransaccion='CT')
			set @TransRecibidas = (select count(IdTransaccion)  from @maestroCxC)
			if (@TransGuardadas <> @TransRecibidas)
			begin
				declare @idsTransaccionMaestro table (id int) 
				insert into @idsTransaccionMaestro (id) select IdTransaccion  from tblMaestrosCxCPrestamo where IdPrestamo=@idPrestamo and CodigoTipoTransaccion='CT'
				declare @idInicial int =((select top 1 id from @idsTransaccionMaestro))
				
				while exists(select top 1 id from @idsTransaccionMaestro) 
				begin
					declare @id int = (select top 1 id from @idsTransaccionMaestro)
					declare @idEncontrada int = (select IdTransaccion from @maestroCxC where IdTransaccion=@id)	
					if isnull(@idEncontrada,0)=0
					begin
						delete tblMaestrosCxCPrestamo where IdTransaccion= @id
						delete tblDetallesDrCxC where idTransaccionMaestro= @id
					end
					delete @idsTransaccionMaestro where id=@id
				end
			end
		end
		return 1
end
