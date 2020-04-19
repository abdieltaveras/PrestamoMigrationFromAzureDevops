create function dbo.fnGetNegocioAndHijos
(
	@IdNegocioPadre int
)
returns @NegociosTree table
(
	idNegocio int,
	idNegocioPadre int
)
as
begin	
--Consulta recursiva
WITH recursion_negocio
AS
(
SELECT
        neg1.IdNegocio,
		neg1.idNegocioPadre
    FROM
        tblNegocios neg1
    WHERE neg1.IdNegocio = @idNegocioPadre

    UNION ALL
    -- Recursive member that references recursion_negocio.

	SELECT
        neg2.IdNegocio,
		neg2.idNegocioPadre
    FROM
        tblNegocios neg2
        INNER JOIN recursion_negocio recNeg
            ON recNeg.idNegocio = neg2.IdNegocioPadre
		
)
-- references recursion_negocio
insert @NegociosTree (idNegocio, idNegocioPadre) select idNegocio, idNegocioPadre from recursion_negocio
return
end