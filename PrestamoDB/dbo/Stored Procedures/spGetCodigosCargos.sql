create proc spGetCodigosCargos
@IdCodigoCargo int = -1
as
SELECT * FROM tblCodigosCargosDebitosReservados
WHERE  ((@IdCodigoCargo=-1) or (IdCodigoCargo = @IdCodigoCargo))