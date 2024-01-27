create procedure dbo.spInsUpdCuotasMaestro
(
	@cuotasMaestra tpCuotaMaestro readonly
)
as
begin
	declare  @registroActualsCuotaMaestro tpCuotaMaestro 
	insert into @registroActualsCuotaMaestro (IdTransaccion, idPrestamo, IdReferencia, CodigoTransaccion, Numero,Fecha,Monto,Balance, OtrosDetalles)
		select  IdTransaccion, idPrestamo, IdReferencia, CodigoTransaccion, Numero,Fecha,Monto,Balance, OtrosDetalles from @cuotasMaestra
	while exists (select top 1 idTransaccion from @registroActualsCuotaMaestro)
	begin
		declare @registroActual tpCuotaMaestro 
		delete  @registroActual
		insert into @registroActual (IdTransaccion, idPrestamo, IdReferencia, CodigoTransaccion, Numero,Fecha,Monto,Balance, OtrosDetalles)
		select top 1 IdTransaccion, idPrestamo, IdReferencia, CodigoTransaccion, Numero,Fecha,Monto,Balance, OtrosDetalles from  @registroActualsCuotaMaestro
		declare @idReferencia varchar(30) = (select IdReferencia from @registroActual)
		declare @idPrestamo int = (select idPrestamo from @registroActual)
		declare @codigoTransaccion varchar(10) = (select CodigoTransaccion from @registroActual)
		declare @numero int = (select numero from @registroActual)
		declare @fecha datetime = (select fecha from @registroActual)
		declare @monto numeric(12,2) = (select monto from @registroActual)
		declare @balance numeric(12,2) = (select balance from @registroActual)
		declare @otrosDetalles varchar(200) = (select otrosDetalles from @registroActual)
		--notar que si idTransaccion no existe en tblCuotaMaestro no se va a actualizar el registro en las cuotas
		declare @idTransaccion int =  (select idTransaccion  from tblCuotasMaestro where idprestamo = @idprestamo and numero=@numero)

		if isnull(@idTransaccion ,0)=0
			begin
				insert into tblCuotasMaestro(idPrestamo, CodigoTransaccion, Numero,Fecha,Monto,Balance, OtrosDetalles) 
				select top 1  IdPrestamo, CodigoTransaccion, Numero,Fecha,Monto,Balance, OtrosDetalles from  @registroActual
			end
		else
			begin
				update tblCuotasMaestro 
				set 
				idPrestamo = @idPrestamo,
				CodigoTransaccion =@codigoTransaccion,
				Numero = @numero,
				Fecha =@fecha,
				Monto =@monto,
				Balance=@balance, 
				OtrosDetalles= @otrosDetalles
				where idTransaccion = @idTransaccion
			end
		--declare @cantidadRegistros int = (select count(Idtransaccion) from @registroActualsCuotaMaestro)
		--delete @registroActualsCuotaMaestro where IdReferencia = @idReferencia
		--primero se hacia por IdReferencia la eliminacion, pero se vio que no era necesaria
		delete @registroActualsCuotaMaestro where idprestamo = @idprestamo and numero=@numero
		--set @cantidadRegistros = (select count(Idtransaccion) from @registroActualsCuotaMaestro)

		-- IMPLEMENTAR ELIMINAR LAS CUOTAS en caso de moficacion y que el prestamo 
		-- le hayan asignado menos cuotas.
	end
end
