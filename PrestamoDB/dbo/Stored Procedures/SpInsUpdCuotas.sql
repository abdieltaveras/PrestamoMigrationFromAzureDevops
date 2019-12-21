CREATE PROCEDURE [dbo].[SpInsUpdCuotas]
	@cuotas tpCuota READONLY
AS
	begin
		insert into tblCuotas (IdPrestamo, IdNumero, Fecha, Capital, Interes) select IdPrestamo, IdNumero, Fecha, Capital, Interes from @cuotas
	end
RETURN 
