CREATE PROCEDURE [dbo].[spGetLocalidades]
(
	@idlocalidad int = 1
)
as
begin	
	
--Consulta recursiva
WITH recursion_location
AS
(
    -- Anchor member
	SELECT
        IdLocalidad,
        Nombre,
        IdLocalidadPadre
    FROM
        tblLocalidad
    WHERE IdLocalidad = @idlocalidad

    UNION ALL
    -- Recursive member that references recursion_location.

	SELECT
        e.IdLocalidad,
        e.Nombre,
        e.IdLocalidadPadre
    FROM
        tblLocalidad e
        INNER JOIN recursion_location o
            ON o.IdLocalidadPadre = e.IdLocalidad
)
-- references recursion_location
SELECT *
FROM   recursion_location
end