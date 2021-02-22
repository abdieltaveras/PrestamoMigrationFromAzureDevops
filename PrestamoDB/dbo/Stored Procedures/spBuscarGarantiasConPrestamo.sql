CREATE PROCEDURE [dbo].[spBuscarGarantiasConPrestamos]
	@search varchar(50),
	@IdNegocio int=0,
	@Usuario varchar(100) = '',
	@Anulado int=0
as

BEGIN
	
	SELECT
		garantias.*, marcas.nombre as NombreMarca, modelos.nombre as NombreModelo 
		INTO #tempGarantias
	from
		tblGarantias garantias
	INNER JOIN 
		tblMarcas marcas ON garantias.IdMarca = marcas.IdMarca
	left JOIN 
		tblModelos modelos ON garantias.IdModelo = modelos.IdModelo
	left JOIN 
		tblLocalidades localidades ON JSON_VALUE(Detalles, '$.IdLocalidad') = localidades.IdLocalidad
	
	where 
		marcas.Nombre LIKE '%' + @search + '%'
		OR modelos.Nombre LIKE '%' + @search + '%'
		OR localidades.Nombre LIKE '%' + @search + '%'
		OR NoIdentificacion LIKE '%' + @search + '%'
		OR JSON_VALUE(Detalles, '$.NoMaquina') LIKE '%' + @search + '%'
		OR JSON_VALUE(Detalles, '$.Placa') LIKE '%' + @search + '%'
		OR JSON_VALUE(Detalles, '$.Ano') LIKE '%' + @search + '%'
	OR JSON_VALUE(Detalles, '$.Valor') LIKE '%' + @search + '%'	
	
	SELECT
		#tempGarantias.*, NombreMarca, NombreModelo FROM #tempGarantias
		WHILE EXISTS(SELECT * FROM #tempGarantias)
		BEGIN
			DECLARE @idGarantia INT = (SELECT TOP 1 idGarantia FROM #tempGarantias)
			SELECT idGarantia, prestamos.IdPrestamo, prestamoNumero FROM tblPrestamoGarantias AS presgar 
			JOIN 
			tblPrestamos  AS prestamos ON prestamos.idPrestamo = presgar.IdPrestamo
			WHERE IdGarantia = @idgarantia and prestamos.Saldado=0		
			DELETE FROM #tempGarantias WHERE idGarantia = @idGarantia
		End		
END
