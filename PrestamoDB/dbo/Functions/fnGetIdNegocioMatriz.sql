create function fnGetIdNegocioMatriz
(
	@idNegocio int
)
returns int
as
begin	
	declare @idNegocioMatriz int = (select IdNegocio from dbo.fnGetNegocioAndPadres(@idNegocio) where idNegocioPadre is NUll )
	return @idNegocioMatriz
end