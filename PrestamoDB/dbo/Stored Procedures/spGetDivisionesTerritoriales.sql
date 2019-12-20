CREATE PROCEDURE [dbo].[spGetDivisionesTerritoriales]
(
	@IdNegocio int=-1
)
as
begin

select 
	t1.* 
from 
	tblTipoLocalidades t1, 
	tblTipoLocalidades t2 
where 
	t1.IdNegocio = @IdNegocio 
	and t1.HijoDe = t2.IdTipoLocalidad 
	and t2.HijoDe is null
End