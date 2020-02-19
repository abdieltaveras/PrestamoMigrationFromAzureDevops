CREATE PROCEDURE [dbo].[spBuscarGarantias]
	@search varchar(50),
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
BEGIN

SELECT
	garantias.*
from
	tblGarantias garantias
INNER JOIN 
	tblMarcas marcas ON garantias.IdMarca = marcas.IdMarca
INNER JOIN 
	tblModelos modelos ON garantias.IdModelo = modelos.IdModelo
where 
	garantias.IdNegocio = 1
	AND marcas.Nombre LIKE '%' + @search + '%'
	OR modelos.Nombre LIKE '%' + @search + '%'
	OR NoIdentificacion LIKE '%' + @search + '%'
	OR JSON_VALUE(Detalles, '$.NoMaquina') LIKE '%' + @search + '%'
	OR JSON_VALUE(Detalles, '$.Placa') LIKE '%' + @search + '%'
	OR JSON_VALUE(Detalles, '$.Ano') LIKE '%' + @search + '%'

End