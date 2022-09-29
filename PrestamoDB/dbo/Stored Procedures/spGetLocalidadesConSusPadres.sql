create PROCEDURE [dbo].[spGetLocalidadesConSusPadres]
(
	@idlocalidad int,
	@IdNegocio int=-1,
    @IdLocalidadNegocio int = -1,
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin	
--Consulta recursiva
-- indica la localidad pero tambien envia informacion de las localidades padres que estan 
-- involucradas
WITH recursion_location
AS
(
    -- Anchor member
	SELECT
        loc.IdLocalidad,
        loc.Nombre,
		loc.IdDivisionTerritorial,
        loc.IdLocalidadPadre,
		tipo.Nombre as Descripcion
    FROM
        tblLocalidades loc, tblDivisionTerritorial tipo
    WHERE (@idlocalidad<=0 or   loc.IdLocalidad = @idlocalidad)
	AND tipo.IdDivisionTerritorial = loc.IdDivisionTerritorial
    UNION ALL
    -- Recursive member that references recursion_location.

	SELECT
        e.IdLocalidad,
        e.Nombre,
		e.IdTipoDivisionTerritorial,
        e.IdLocalidadPadre,
		t.Nombre
    FROM
        tblLocalidades e
        INNER JOIN recursion_location o
            ON o.IdLocalidadPadre = e.IdLocalidad
		JOIN tblDivisionTerritorial t
			ON e.IdTipoDivisionTerritorial = t.IdDivisionTerritorial
)
-- references recursion_location
SELECT *
FROM   recursion_location

end