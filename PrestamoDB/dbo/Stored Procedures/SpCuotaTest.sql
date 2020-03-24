CREATE PROCEDURE [dbo].[SpCuotaTest]
as
    begin
        DECLARE @cuotas AS tpCuota
        SET NOCOUNT ON
        INSERT INTO @cuotas values 
        (1,1,1,'20200101',100,10),
        (1,2,2,'20200101',100,10),
        (1,0,3,'20200101',100,10)
        while (exists(select 1 from @cuotas)) 
        begin
         declare @numero int = (select top 1 idNumero from @cuotas)
         declare @fecha date = (select top 1 fecha from @cuotas)
         declare @idCuota int = (select top 1 idCuota from @cuotas)
         print @numero
         print @fecha
         if (@idCuota <=0)
	        print 'insertar cuota'
         else
	        print 'modificar cuota'
         delete from @cuotas where idNumero = @numero
        end
        SET NOCOUNT OFF
    end
RETURN 0
