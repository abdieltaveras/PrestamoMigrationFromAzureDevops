create procedure dbo.SpGetCuotas
(
	@idPrestamo int
)
as
Begin
SELECT [IdCuota]
      ,[IdLocalidad]
      ,[IdPrestamo]
      ,[Numero]
      ,[Fecha]
      ,[Capital]
      ,[BceCapital]
      ,[Interes]
      ,[BceInteres]
      ,[GastoDeCierre]
      ,[BceGastoDeCierre]
      ,[InteresDelGastoDeCierre]
      ,[BceInteresDelGastoDeCierre]
	 From tblCuotas where IdPrestamo=@idPrestamo
End