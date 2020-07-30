CREATE PROCEDURE [dbo].[spBuscarGarantias]
	@search varchar(50),
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
BEGIN

SELECT
	garantias.*, marcas.nombre as NombreMarca, modelos.nombre as NombreModelo
from
	tblGarantias garantias
INNER JOIN 
	tblMarcas marcas ON garantias.IdMarca = marcas.IdMarca
left JOIN 
	tblModelos modelos ON garantias.IdModelo = modelos.IdModelo
left JOIN 
	tblLocalidades localidades ON JSON_VALUE(Detalles, '$.IdLocalidad') = localidades.IdLocalidad
where 
	garantias.IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@idNegocio))
	AND marcas.Nombre LIKE '%' + @search + '%'
	OR modelos.Nombre LIKE '%' + @search + '%'
	OR localidades.Nombre LIKE '%' + @search + '%'
	OR NoIdentificacion LIKE '%' + @search + '%'
	OR JSON_VALUE(Detalles, '$.NoMaquina') LIKE '%' + @search + '%'
	OR JSON_VALUE(Detalles, '$.Placa') LIKE '%' + @search + '%'
	OR JSON_VALUE(Detalles, '$.Ano') LIKE '%' + @search + '%'
	OR JSON_VALUE(Detalles, '$.Valor') LIKE '%' + @search + '%'

End