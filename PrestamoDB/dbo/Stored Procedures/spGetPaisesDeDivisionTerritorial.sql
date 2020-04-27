CREATE PROCEDURE [dbo].[spGetPaisesDeDivisionTerritorial]
(
	@IdNegocio int,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin

	SELECT
		t1.*,
		t2.Nombre as DescripcionPadre
	FROM 
		tblTipoLocalidades t1,
		tblTipoLocalidades t2
	WHERE
		t1.IdLocalidadPadre in (	SELECT
							t1.IdTipoLocalidad
						FROM
							tblTipoLocalidades t1,
							tblTipoLocalidades t2
						WHERE
							t1.IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))
							AND t1.IdLocalidadPadre = t2.IdTipoLocalidad
							AND t2.IdLocalidadPadre IS NULL)
		AND t1.IdLocalidadPadre = t2.IdTipoLocalidad
End