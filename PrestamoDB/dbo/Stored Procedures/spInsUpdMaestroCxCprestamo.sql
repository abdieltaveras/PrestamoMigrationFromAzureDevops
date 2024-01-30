create procedure dbo.spInsUpdMaestroCxCPrestamo
(
	@maestroCxC tpMaestroCxCPrestamo readonly
)
as
begin
	declare  @registrosCuotaMaestro tpMaestroCxCPrestamo
	insert into @registrosCuotaMaestro (IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson)
		select  IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson from @maestroCxC
	while exists (select top 1 idTransaccion from @registrosCuotaMaestro)
	begin
		declare @registroActual tpMaestroCxCPrestamo 
		delete  @registroActual
		insert into @registroActual (IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson)
		select top 1 IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson, DetallesCargosJson from  @registrosCuotaMaestro
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
		--declare @cantidadRegistros int = (select count(Idtransaccion) from @registrosCuotaMaestro)
		--delete @registrosCuotaMaestro where IdReferencia = @idReferencia
		--primero se hacia por IdReferencia la eliminacion, pero se vio que no era necesaria
		delete @registrosCuotaMaestro where idprestamo = @idprestamo and NumeroTransaccion =@numeroTransaccion 
		--set @cantidadRegistros = (select count(Idtransaccion) from @registrosCuotaMaestro)

		-- IMPLEMENTAR ELIMINAR LAS CUOTAS en caso de moficacion y que el prestamo 
		-- le hayan asignado menos cuotas.
	end
end
