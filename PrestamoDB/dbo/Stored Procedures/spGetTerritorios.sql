CREATE PROCEDURE [dbo].[spGetTerritorios]
(
	@IdDivisionTerritorial int=-1,
	@IdLocalidadNegocio int= -1,
	@IdNegocio int=-1,
	@IdLocalidadPadre  int=-1,
	@Nombre varchar(50)='',
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin

select 
	t.*, m.Nombre as NombreTipoHijoDe
	from 
	tblDivisionTerritorial t
	left JOIN tblDivisionTerritorial m ON m.IdDivisionTerritorial = t.IdLocalidadPadre
	where 
		((@IdDivisionTerritorial=-1) or (t.IdDivisionTerritorial = @IdDivisionTerritorial))
		and ((@IdNegocio=-1) or (t.IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))))
		and ((@IdLocalidadPadre=-1) or (t.IdLocalidadPadre = @IdLocalidadPadre))
		and ((@Nombre='') or (t.Nombre=@Nombre))	
	order by t.IdLocalidadPadre asc
End
