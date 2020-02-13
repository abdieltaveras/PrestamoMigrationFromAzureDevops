CREATE PROCEDURE [dbo].[spGetDivisionesTerritoriales]
(
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''

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
	and t1.IdLocalidadPadre = t2.IdTipoLocalidad 
	and t2.IdLocalidadPadre is null
End