drop procedure sptestTVP
drop type tpCuotaMaestroTest

create table test (fecha varchar(40))

CREATE TYPE [dbo].[tpCuotaMaestroTest] AS TABLE(
	Fecha varchar(50)
)
GO
alter procedure dbo.spTestTVP
(
	@cuotasMaestra tpCuotaMaestroTest readonly
)
as
begin
	declare @fecha varchar(40) = (select fecha from @cuotasMaestra)

	insert into Test(fecha) values (@fecha)
end
go

declare @fecha varchar(40) = (select fecha from test)
select @fecha
declare @size int = (select len(@fecha))
set  @fecha=(select substring((select fecha from test),1,@size-5)) 
select @fecha
select convert(DATETIME, @fecha,103)
select * from test

declare @cuotasMaestraTest tpCuotaMaestroTest 
insert into @cuotasMaestraTest
(IdTransaccion, idPrestamo, IdReferencia, CodigoTransaccion,  Numero,Monto,Balance, OtrosDetalles)
values
(0,12,null,'Cta',1,  100.50,100.25,null),
(0,12,null,'Cta',2,  500.50,500.25,null)

select * from @cuotasMaestraTest

exec .spInsUpdCuotasMaestro @cuotasMaestra = @cuotasMaestra
