CREATE PROCEDURE [dbo].[LocalidadPaisesSpGet]
(
	@IdNegocio int,
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin

	SELECT
		IdLocalidad,
		Nombre
	FROM
		tblLocalidades
	WHERE
		IdLocalidadPadre = 0
		AND IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))
		
End
