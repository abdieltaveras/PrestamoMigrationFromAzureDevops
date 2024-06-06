create procedure [dbo].[spInsMaestroDetalleCuotas]
(
     --@IdTransaccion int, @IdPrestamo int, @IdNegocio int, @IdLocalidadNegocio int,
	@idPrestamo int, @maestroCxC tpMaestroCxCPrestamo readonly, @detallesCargos tpDetalleDrCxC readonly, @crearTablas bit=0
)
as
begin
	-- solo para insertar transacciones de tipo debitos
	declare  @registrosMaestroCxC tpMaestroCxCPrestamo
	declare @CodigoTipoTransaccion varchar(10) = (select top 1  CodigoTipoTransaccion from @maestroCxC where NumeroTransaccion>0)
	--declare @idPrestamo int = (select top 1 idPrestamo from @maestroCxC)
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
	-----------------------
	-- vamos a borrar todas las cuotas y generarlas de nuevo
	-----------------------
	if (@CodigoTipoTransaccion='CT')
	begin
	    -- eliminar gasto de cierre cuota 0
		while (1=1)
		begin
			declare @idTrans int =(select top 1 IdTransaccion from tblMaestrosCxCPrestamo where IdPrestamo=@idPrestamo and CodigoTipoTransaccion='CI'and numeroTransaccion=0)
			if ISNULL(@idTrans,0)<>0
			begin
				delete tblMaestrosCxCPrestamo where IdTransaccion=@idTrans
				delete tblDetallesDrCxC where idTransaccionMaestro = @idTrans
			end
			else
			break
		end
		declare @idsTransaccionMaestro table (id int) 
		insert into @idsTransaccionMaestro (id) select IdTransaccion  from tblMaestrosCxCPrestamo where IdPrestamo=@idPrestamo and CodigoTipoTransaccion='CT' order by IdTransaccion
		declare @idInicial int =((select top 1 id from @idsTransaccionMaestro))
		declare @transMaestras int = (select count(id) from @idsTransaccionMaestro)
		while exists(select top 1 id from @idsTransaccionMaestro) 
		begin
			declare @id int = (select top 1 id from @idsTransaccionMaestro)
			declare @idEncontrada int = (select IdTransaccion from @maestroCxC where IdTransaccion=@id)	
			if isnull(@idEncontrada,0)=0
			begin
				delete tblDetallesDrCxC where idTransaccionMaestro= @id
				delete tblMaestrosCxCPrestamo where IdTransaccion= @id
			end
			delete @idsTransaccionMaestro where id=@id
		end
	end
	while exists (select top 1 idTransaccion from @registrosMaestroCxC) 
	begin
	    -- informacion del registro maestro
		declare @registroActual tpMaestroCxCPrestamo 
		delete  @registroActual -- si no elimina, se queda el registro anterior
		insert into @registroActual (IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson)
		select top 1 IdTransaccion, idPrestamo, IdReferencia, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson  from  @registrosMaestroCxC
		declare @idReferencia uniqueidentifier = (select IdReferencia from @registroActual)
		declare @idTransaccionMaestro int = 0
		begin
			insert into tblMaestrosCxCPrestamo(idPrestamo, TipoDrCr, CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson ) 
			select idPrestamo, 'D', CodigoTipoTransaccion, NumeroTransaccion ,Fecha,Monto,Balance, OtrosDetallesJson from  @registroActual
			set @idTransaccionMaestro = scope_identity()
			begin
				declare @detalles_cargos tpDetalleDrCxC 
				delete @detalles_cargos
				insert  into  @detalles_cargos select * from @detallesCargos where IdReferenciaMaestro=@idReferencia 
				begin
					update @detalles_Cargos set IdTransaccionMaestro = @idTransaccionMaestro
					insert into tblDetallesDrCxC(idTransaccionMaestro, Balance, Monto, CodigoCargo)
					select idTransaccionMaestro, Balance, Monto, CodigoCargo from @detalles_Cargos 
				end
			end
			delete @registrosMaestroCxC where IdReferencia = @idReferencia
		end
	end
end