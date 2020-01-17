CREATE PROCEDURE [dbo].[spBuscarGarantias]
	@search varchar(50),
	@IdNegocio int
as
BEGIN
	SELECT *
	from
	tblGarantias
	where IdNegocio = @IdNegocio
	AND NoIdentificacion LIKE '%' + @search + '%'	
End