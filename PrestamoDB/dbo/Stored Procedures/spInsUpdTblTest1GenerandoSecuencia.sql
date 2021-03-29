create procedure spInsUpdTblTest1GenerandoSecuencia
(@nombre varchar(40))
as
Begin
	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
	BEGIN TRANSACTION 
		begin try
			declare @codigo varchar(20)
			exec dbo.spGenerarSecuenciaString 'Codigo de pruebas',10,1, @codigo output
			insert into tblTest1 (nombre, codigo) values (@nombre, @codigo)
			commit
		end try
	begin catch
		rollback
	end catch
End

--to practice and check the behavior of this stored procedure	
--exec spInsUpdTblTest1GenerandoSecuencia 'Uno'
--go
--exec spInsUpdTblTest1GenerandoSecuencia null
--go
--exec spInsUpdTblTest1GenerandoSecuencia 'tres'
--go

--select * from tblTest1
--select * from tblSecuencias 
