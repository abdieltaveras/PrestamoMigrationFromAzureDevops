CREATE PROCEDURE [dbo].[spBuscarGarantias]
	@search varchar(50),
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
BEGIN
	SELECT *
	from
	tblGarantias
	where IdNegocio = @IdNegocio
	AND NoIdentificacion LIKE '%' + @search + '%'	
End