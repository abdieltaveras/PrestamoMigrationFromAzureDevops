CREATE Procedure SpInsUpdDetallesDrCxC
(@detallesCargos tpDetalleDrCxC readonly, @IdTransaccionMaestro int)
as
begin
	declare @detalles_Cargos tpDetalleDrCxC 
	--insert  into @detalles_cargos (IdTransaccion, idTransaccionMaestro,IdReferencia, CodigoCargo, Balance, Monto)  select IdTransaccion, IdTransaccionMaestro, IdReferencia, CodigoCargo, Balance, Monto from @detallesCargos
	insert  into  @detalles_cargos select * from @detallesCargos 
	update @detalles_Cargos set IdTransaccionMaestro = @IdTransaccionMaestro
	--select * into  tblTestDetallesCargos from @detallesCargos
	while exists(select top 1 idTransaccion from @detalles_Cargos)
	begin
		declare @idReferenciaDetalle uniqueidentifier= (select top 1 IdReferenciaDetalle from @detalles_Cargos)
		declare @idReferenciaMaestro uniqueidentifier= (select top 1 IdReferenciaMaestro from @detalles_Cargos)
		declare @codigoCargo varchar(5) = (select top 1 CodigoCargo from @detalles_Cargos)
		declare @monto numeric(18,2) = (select top 1 Monto from @detalles_Cargos)
		declare @balanceDetalle numeric(18,2) = (select top 1 Balance from @detalles_Cargos)
		declare @idTransaccionDetalle int = (select IdTransaccion   from tblDetallesDrCxC  where idTransaccionMaestro=@idTransaccionMaestro and CodigoCargo=@codigoCargo)
		if (isnull(@idTransaccionDetalle,0)=0)
		begin
			insert into tblDetallesDrCxC(idTransaccionMaestro, Balance, Monto, CodigoCargo)
			values (@idTransaccionMaestro, @balanceDetalle,@monto, @codigoCargo)  
		end
		else
		begin
			update tblDetallesDrCxC
			set    Balance = @balanceDetalle, Monto   = @monto,CodigoCargo = @codigoCargo 
			where IdTransaccion = @idTransaccionDetalle
		end
		delete @detalles_Cargos where idTransaccionMaestro=@idTransaccionMaestro and CodigoCargo=@codigoCargo
	end	

	--eliminar los detalles guardados que ya estan eliminados (mayormente para la cuota)
	begin
	declare @detallesGuardados tpDetalleDrCxC 
	--insert into @detallesGuardados select * from tblDetallesDrCxC where idTransaccionMaestro=@idTransaccionMaestro
	while exists(select top 1 idTransaccion from @detallesGuardados)
		select getdate()
	end
end
