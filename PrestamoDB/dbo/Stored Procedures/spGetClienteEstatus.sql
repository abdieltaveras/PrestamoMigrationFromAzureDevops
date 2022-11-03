CREATE proc spGetClienteEstatus
@IdCliente int = -1,
@IdEstatus int = -1
as
SELECT ce.IdCliente,ce.IdEstatus, ce.Activo,e.Name EstatusName,e.Description EstatusDescription
FROM tblClienteEstatus ce
INNER JOIN tblEstatus e
ON ce.IdEstatus =e.IdEstatus
WHERE (@IdEstatus  = -1 or ce.IdEstatus = @IdEstatus)
and (@IdCliente  = -1 or ce.IdCliente = @IdCliente)