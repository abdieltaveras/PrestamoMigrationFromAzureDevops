CREATE PROCEDURE [dbo].[spGetComponentesDeDivisionTerritorial]
(
	@IdDivisionTerritorial int=-1,
	@IdLocalidadNegocio int =-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT
		IdTipoLocalidad,
		IdLocalidadPadre,
		Nombre,
		PermiteCalle
	FROM
		tblTipoLocalidades
	WHERE
		IdDivisionTerritorial = @IdDivisionTerritorial
		--AND IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))
End
