CREATE PROCEDURE [dbo].[spComponentesDeDivisionTerritorial]
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
		HijoDe,
		Nombre,
		PermiteCalle
	FROM 
		tblTipoLocalidades 
	WHERE
		IdDivisionTerritorial = @IdDivisionTerritorial 
		and IdNegocio = @IdNegocio;
End
