CREATE PROCEDURE [dbo].[spComponentesDeDivisionTerritorial]
(
	@IdDivisionTerritorial int=-1,
	@IdNegocio int=-1
)
as
begin
	SELECT 
		IdTipoLocalidad,
		HijoDe,
		Descripcion,
		PermiteCalle
	FROM 
		tblTipoLocalidades 
	WHERE
		IdDivisionTerritorial = @IdDivisionTerritorial 
		and IdNegocio = @IdNegocio;
End
