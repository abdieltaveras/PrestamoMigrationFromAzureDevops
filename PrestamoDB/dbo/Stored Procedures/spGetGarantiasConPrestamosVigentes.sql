create PROCEDURE spGarantiasConPrestamosVigentes
(@IdGarantias tpGarantias readonly)
AS
BEGIN
	SELECT idGarantia, prestamos.IdPrestamo, prestamoNumero FROM tblPrestamoGarantias AS presgar 
			JOIN 
			tblPrestamos  AS prestamos ON prestamos.idPrestamo = presgar.IdPrestamo
			WHERE IdGarantia IN (SELECT idGarantia FROM @IdGarantias) and prestamos.Saldado=0 and prestamos.AnuladoPor is null
end
