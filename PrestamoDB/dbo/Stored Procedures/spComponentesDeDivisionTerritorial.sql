CREATE PROCEDURE [dbo].[spComponentesDeDivisionTerritorial]
(
	@IdDivisionTerritorialPadre int=-1,
	@IdNegocio int=-1,
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT 
		IdDivisionTerritorial,
		IdLocalidadPadre,
		IdDivisionTerritorialPadre,
		Nombre,
		PermiteCalle
	FROM 
		tblDivisionTerritorial 
	WHERE
		IdDivisionTerritorialPadre = @IdDivisionTerritorialPadre 
		and IdNegocio = @IdNegocio;
End
