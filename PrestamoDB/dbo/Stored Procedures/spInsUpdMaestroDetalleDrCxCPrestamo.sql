create procedure [dbo].[spInsUpdMaestroDetalleDrCxCPrestamo]
(
	@maestroCxC tpMaestroCxCPrestamo readonly, @detallesCargos tpDetalleDrCxC readonly 
)
as
begin
	declare  @registrosMaestroCxC tpMaestroCxCPrestamo
	insert  into  @registrosMaestroCxC select * from @maestroCxC
	--insert into @registrosMaestroCxC (IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson)
	--select  IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson from @maestroCxC
	declare @detalles_Cargos tpDetalleDrCxC 
	insert  into  @detalles_cargos select * from @detallesCargos
	while exists (select top 1 idTransaccion from @registrosMaestroCxC)
	begin
	    -- informacion del registro maestro
		declare @registroActual tpMaestroCxCPrestamo 
		delete  @registroActual
		insert into @registroActual (IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson)
		select top 1 IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson from  @registrosMaestroCxC
		declare @idReferencia varchar(30) = (select IdReferencia from @registroActual)
		declare @idPrestamo int = (select idPrestamo from @registroActual)
		declare @CodigoTipoTransaccion varchar(10) = (select CodigoTipoTransaccion from @registroActual)
		declare @numeroTransaccion int = (select NumeroTransaccion from @registroActual)
		declare @fecha datetime = (select fecha from @registroActual)
		declare @monto numeric(18,2) = (select monto from @registroActual)
		declare @balance numeric(18,2) = (select balance from @registroActual)
		declare @otrosDetallesJson varchar(200) = (select otrosDetallesJson from @registroActual)
		declare @detallesCargosJson varchar(200) = (select detallesCargosJson from @registroActual)
		--notar que si idTransaccion no existe en tblCuotaMaestro no se va a actualizar el registro en las cuotas
		declare @idTransaccion int =  (select idTransaccion  from tblMaestrosCxCPrestamo where idprestamo = @idprestamo and numeroTransaccion =@numeroTransaccion )

		
		if isnull(@idTransaccion ,0)=0
		begin
			insert into tblMaestrosCxCPrestamo(idPrestamo, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson) 
			select top 1  IdPrestamo, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson from  @registroActual
			set @idTransaccion  = scope_identity()
		end
	else
		begin
			update tblMaestrosCxCPrestamo 
			set 
			idPrestamo = @idPrestamo,
			CodigoTipoTransaccion =@CodigoTipoTransaccion,
			NumeroTransaccion = @numeroTransaccion,
			Fecha =@fecha,
			Monto =@monto,
			Balance=@balance, 
			OtrosDetallesJson= @otrosDetallesJson,
			DetallesCargosJson = @detallesCargosJson
			where idTransaccion = @idTransaccion
				
		end
		-- insUpdDetalles
		--insert  into @detalles_cargos (IdTransaccion, idTransaccionMaestro,IdReferencia, CodigoCargo, Balance, Monto)  select IdTransaccion, IdTransaccionMaestro, IdReferencia, CodigoCargo, Balance, Monto from @detallesCargos
		insert  into  @detalles_cargos select * from @detallesCargos
		declare @idTransaccionDetalleMaestro int = @idTransaccion
		while exists(select top 1 idTransaccion from @detalles_Cargos)
		begin
			declare @idReferenciaDetalle varchar(50)= (select top 1 idReferencia from @detalles_Cargos)
			declare @codigoCargo varchar(5) = (select top 1 CodigoCargo from @detalles_Cargos)
			declare @montoDetalle numeric(18,2) = (select top 1 Monto from @detalles_Cargos)
			declare @balanceDetalle numeric(18,2) = (select top 1 Balance from @detalles_Cargos)
			declare @idTransaccionDetalle int = (select IdTransaccion   from tblDetallesDrCxC  where idTransaccionMaestro=@idTransaccionDetalleMaestro and CodigoCargo=@codigoCargo)
			if (isnull(@idTransaccionDetalle,0)=0)
			begin
				insert into tblDetallesDrCxC(idTransaccionMaestro, Balance, Monto, CodigoCargo)
				values (@idTransaccionDetalleMaestro, @balanceDetalle,@montoDetalle, @codigoCargo)  
			end
			else
			begin
				update tblDetallesDrCxC
				set 		Balance = @balanceDetalle, Monto   = @montoDetalle,CodigoCargo = @codigoCargo where IdTransaccion = @idTransaccionDetalle
			end
			delete @detalles_Cargos where idReferencia=@idReferenciaDetalle
		end	
			--  borra el registro maestro y continuar el loop
		delete @registrosMaestroCxC where idprestamo = @idprestamo and NumeroTransaccion =@numeroTransaccion 
	end
end
