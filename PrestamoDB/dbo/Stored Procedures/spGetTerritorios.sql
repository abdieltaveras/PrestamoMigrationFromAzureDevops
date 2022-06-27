CREATE PROCEDURE [dbo].[spGetTerritorios]
(
	@IdDivisionTerritorial int=-1,
	@IdLocalidadNegocio int= -1,
	@IdNegocio int=-1,
	@IdDivisionTerritorialPadre  int=-1,
	@Nombre varchar(50)='',
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0
)
as
begin

select 
	t.*, m.Nombre as NombreTipoHijoDe
	from 
	tblDivisionTerritorial t
	left JOIN tblDivisionTerritorial m ON m.IdDivisionTerritorial = t.IdDivisionTerritorialPadre
	where 
		((@IdDivisionTerritorial=-1) or (t.IdDivisionTerritorial = @IdDivisionTerritorial))
		and ((@IdNegocio=-1) or (t.IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio))))
		and ((@IdDivisionTerritorialPadre=-1) or (t.IdDivisionTerritorialPadre = @IdDivisionTerritorialPadre))
		and ((@Nombre='') or (t.Nombre=@Nombre))
		and ((@condicionBorrado= 0 and t.BorradoPor is null) 
		or (@condicionBorrado=1 and t.BorradoPor is not null)
		or (@condicionBorrado=-1))

	order by t.IdDivisionTerritorialPadre asc
End
