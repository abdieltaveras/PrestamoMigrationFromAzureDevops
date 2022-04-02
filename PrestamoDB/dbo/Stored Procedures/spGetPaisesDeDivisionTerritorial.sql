CREATE PROCEDURE [dbo].[spGetPaisesDeDivisionTerritorial]
(
	@IdNegocio int,
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin

	SELECT
		t1.*,
		t2.Nombre as DescripcionPadre
	FROM 
		tblDivisionTerritorial t1,
		tblDivisionTerritorial t2
	WHERE
		t1.IdLocalidadPadre in (	SELECT
							t1.IdDivisionTerritorial
						FROM
							tblDivisionTerritorial t1,
							tblDivisionTerritorial t2
						WHERE
							--t1.IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)) and
							t1.IdLocalidadPadre = t2.IdDivisionTerritorial
							AND t2.IdLocalidadPadre IS NULL)
		AND t1.IdLocalidadPadre = t2.IdDivisionTerritorial
End