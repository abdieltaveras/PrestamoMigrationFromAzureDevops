CREATE proc [dbo].[spGetPrestamoEstatus]
@IdPrestamo int = -1,
@IdEstatus int = -1
as
SELECT ce.IdPrestamo,ce.IdEstatus, ce.Activo,e.Name EstatusName,e.Description EstatusDescription
FROM tblPrestamoEstatus ce
INNER JOIN tblEstatus e
ON ce.IdEstatus =e.IdEstatus
WHERE (@IdEstatus  = -1 or ce.IdEstatus = @IdEstatus)
and (@IdPrestamo  = -1 or ce.IdPrestamo = @IdPrestamo)