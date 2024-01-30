drop table tblDetallesDrCxc
drop procedure SpInsUpdDetallesDrCxC
drop type tpDetalleDrCxC

create table tblDetallesDrCxC
(
IdTransaccion int primary key identity(1,1),
idTransaccionMaestro int, --luego tendra que tener un references
CodigoCargo varchar(5),
Monto Numeric(18,2),
Balance Numeric (18,2)
)
go
create type tpDetalleDrCxC
as table
(
	IdTransaccion int,
	IdTransaccionMaestro int,
	idReferencia varchar(50),
	CodigoCargo varchar(5),
	Monto Numeric(18,2),
	Balance Numeric (18,2)
)
go
alter Procedure SpInsUpdDetallesDrCxC
(@detallesCargos tpDetalleDrCxC readonly)
as

begin
	--select * into tblTestDetallesCargos  from @detallesCargos
	declare @detalles_Cargos tpDetalleDrCxC 
	--insert  into @detalles_cargos (IdTransaccion, idTransaccionMaestro,IdReferencia, CodigoCargo, Balance, Monto)  select IdTransaccion, IdTransaccionMaestro, IdReferencia, CodigoCargo, Balance, Monto from @detallesCargos
	insert  into  @detalles_cargos select * from @detallesCargos
	while exists(select top 1 idTransaccion from @detalles_Cargos)
	begin
		declare @idReferencia varchar(50)= (select top 1 idReferencia from @detalles_Cargos)
		declare @codigoCargo varchar(5) = (select top 1 CodigoCargo from @detalles_Cargos)
		declare @monto numeric(18,2) = (select top 1 Monto from @detalles_Cargos)
		declare @balance numeric(18,2) = (select top 1 Balance from @detalles_Cargos)
		insert into tblDetallesDrCxC(Balance, Monto, CodigoCargo)
		values (@balance,@monto, @codigoCargo)  
		delete @detalles_Cargos where idReferencia=@idReferencia
	end	
		
end

-- hay que definir la relacion entre la tabla padre y la tabla hija
IdTransaccionDrCxc
IdTransaccionMDrCxC
-- Debemos definir las transaccion Cr
esta a diferencia de las Dr, el detalleDrCxC
sera 
IdTransaccion
IdTransaccionDrCxC
CodigoCargo  -- esto para determinar a que fue aplicado y que sea mas facil
             -- podria obtenerse tambien por relacion, pero 
MontoAplicado

delete tblDetallesDrCxC
drop table tblTestDetallesCargos
select count(*) from tblTestDetallesCargos

declare @data tpDetalleDrCxC
insert  into @data (idTransaccionMaestro, idReferencia, CodigoCargo, Balance, Monto)  select IdTransaccionMaestro, idReferencia,  CodigoCargo, Balance, Monto from tblTestDetallesCargos
select * from @data
exec SpInsUpdDetallesDrCxC @data 

CREATE APPLICATION ROLE [DetallesCaegosDrCxC]
	WITH PASSWORD = 'bmnuN|{aceJ{6|H{lPwz{HrNmsFT7_&#$!~<}zizpscJ{{wk'
