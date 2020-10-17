CREATE PROCEDURE [dbo].[spGetGarantias]
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


END