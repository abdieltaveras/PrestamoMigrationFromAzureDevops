CREATE PROCEDURE [dbo].[spGetComponentesDeDivisionTerritorial]
(
	@IdDivisionTerritorial int=-1,
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
		AND IdNegocio = @IdNegocio;
End
