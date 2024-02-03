create procedure spGetMaestroYDetallesCxC
(
	@idNegocio int,
	@idLocalidad int,
	@idPrestamo int,
	@codigoTipoTransaccion varchar(5)='',
	@TipoDrCr char(1),
	@conBalancePendiente bit=0
)
as
begin
	declare @maestrosCxC tpMaestroCxCPrestamo 
	insert  into  @maestrosCxC (IdTransaccion, IdPrestamo, TipoDrCr, CodigoTipoTransaccion, Fecha, Monto, Balance, OtrosDetallesJson, NumeroTransaccion)
	select IdTransaccion, IdPrestamo, TipoDrCr, CodigoTipoTransaccion, Fecha, Monto, Balance, OtrosDetallesJson,  NumeroTransaccion  from tblMaestrosCxCPrestamo where 
	IdPrestamo = @idPrestamo and
	CodigoTipoTransaccion = @codigoTipoTransaccion and
	@conBalancePendiente=0 or
	@conBalancePendiente=1 and Balance>0
	--pendiente TipoDrCr
	select * from @maestrosCxC
	declare @minId int = (select top 1 IdTransaccion from @maestrosCxC)-1
	declare @detallesCxC tpDetalleDrCxC

	while (isnull(@minId,0)<>0)
	begin
		set @minId = (select top 1 IdTransaccion from @maestrosCxC where IdTransaccion>@minId)
		insert  into  @detallesCxC (IdTransaccion,IdTransaccionMaestro, CodigoCargo, Monto, Balance)
		select IdTransaccion,IdTransaccionMaestro, CodigoCargo, Monto, Balance from tblDetallesDrCxC   where idTransaccionMaestro=@minId
	end
	select * from @detallesCxC
end

