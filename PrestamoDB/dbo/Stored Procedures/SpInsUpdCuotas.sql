CREATE PROCEDURE [dbo].[SpInsUpdCuotas]
	@cuotas tpCuota READONLY
AS
	begin
		insert into tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) select IdPrestamo, Numero, Fecha, Capital, Interes from @cuotas
	end
RETURN 
