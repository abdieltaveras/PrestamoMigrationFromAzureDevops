CREATE PROCEDURE [dbo].[spGetDivisionesTerritoriales]
(
	@IdNegocio int=-1,
	@IdLocalidadNegocio int=-1,
	@Borrado int=0,
	@Usuario varchar(100)=''

)
as
begin

select 
	t1.* 
from 
	tblDivisionTerritorial t1, 
	tblDivisionTerritorial t2 
where 
	--t1.IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)) and
	t1.IdLocalidadPadre = t2.IdDivisionTerritorial 
	and t2.IdLocalidadPadre is null
End