create PROCEDURE spGarantiasConPrestamosVigentes
@IdGarantias tpGarantias readonly,
 @condicionBorrado int = 0

AS
BEGIN
	SELECT idGarantia, prestamos.IdPrestamo, prestamoNumero FROM tblPrestamoGarantias AS presgar 
			JOIN 
			tblPrestamos  AS prestamos ON prestamos.idPrestamo = presgar.IdPrestamo
			WHERE IdGarantia IN (SELECT idGarantia FROM @IdGarantias) and prestamos.Saldado=0
			and ((@condicionBorrado= 0 and prestamos.BorradoPor is null) 
			or (@condicionBorrado=1 and prestamos.BorradoPor is not null)
			or (@condicionBorrado=-1))
end
