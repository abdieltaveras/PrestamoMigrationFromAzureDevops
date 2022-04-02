create PROCEDURE [dbo].[spGetNegociosRecursive]
(
	@IdNegocio int,
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin	
	
--Consulta recursiva
WITH recursion_negocio
AS
(
    -- Anchor member
	SELECT
        neg1.IdNegocio,
		neg1.idNegocioPadre
    FROM
        tblNegocios neg1
    WHERE neg1.IdNegocio = @idNegocio

    UNION ALL
    -- Recursive member that references recursion_negocio.

	SELECT
        neg2.IdNegocio,
		neg2.idNegocioPadre
    FROM
        tblNegocios neg2
        INNER JOIN recursion_negocio recNeg
            ON recNeg.idNegocioPadre = neg2.IdNegocio
		
)
-- references recursion_negocio
SELECT *
FROM   recursion_negocio
end
