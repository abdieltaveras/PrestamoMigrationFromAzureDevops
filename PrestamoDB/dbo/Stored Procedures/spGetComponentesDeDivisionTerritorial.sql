CREATE PROCEDURE [dbo].[spGetComponentesDeDivisionTerritorial]
(
	@IdDivisionTerritorialPadre int=-1,
	@IdLocalidadNegocio int =-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT
		IdDivisionTerritorial,
		IdLocalidadPadre,
		Nombre,
		PermiteCalle
	FROM
		tblDivisionTerritorial
	WHERE
		IdDivisionTerritorialPadre = @IdDivisionTerritorialPadre
		--AND IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))
End
