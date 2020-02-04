CREATE PROCEDURE [dbo].[spGetPaisesDeDivisionTerritorial]
(
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin

	SELECT
		t1.*,
		t2.Descripcion as DescripcionPadre
	FROM 
		tblTipoLocalidades t1,
		tblTipoLocalidades t2
	WHERE
		t1.HijoDe in (	SELECT
							t1.IdTipoLocalidad
						FROM
							tblTipoLocalidades t1,
							tblTipoLocalidades t2
						WHERE
							t1.IdNegocio = @IdNegocio
							AND t1.HijoDe = t2.IdTipoLocalidad
							AND t2.HijoDe IS NULL)
		AND t1.HijoDe = t2.IdTipoLocalidad
End