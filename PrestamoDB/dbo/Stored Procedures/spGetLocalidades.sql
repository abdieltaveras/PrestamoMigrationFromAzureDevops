CREATE PROCEDURE [dbo].[spGetLocalidades]
(
	@idlocalidad int = 1,
	@IdNegocio int,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin	
	
--Consulta recursiva
WITH recursion_location
AS
(
    -- Anchor member
	SELECT
        loc.IdLocalidad,
        loc.Nombre,
		loc.IdTipoLocalidad,
        loc.IdLocalidadPadre,
		
		tipo.Nombre as Descripcion
    FROM
        tblLocalidades loc, tblTipoLocalidades tipo
    WHERE loc.IdLocalidad = @idlocalidad
	AND tipo.IdTipoLocalidad = loc.IdTipoLocalidad

    UNION ALL
    -- Recursive member that references recursion_location.

	SELECT
        e.IdLocalidad,
        e.Nombre,
		e.IdTipoLocalidad,
        e.IdLocalidadPadre,
		t.Nombre
    FROM
        tblLocalidades e
        INNER JOIN recursion_location o
            ON o.IdLocalidadPadre = e.IdLocalidad
		JOIN tblTipoLocalidades t
			ON e.IdTipoLocalidad = t.IdTipoLocalidad
)
-- references recursion_location
SELECT *
FROM   recursion_location
end