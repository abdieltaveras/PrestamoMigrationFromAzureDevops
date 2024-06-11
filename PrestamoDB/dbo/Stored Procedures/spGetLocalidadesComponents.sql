create procedure [dbo].[spGetLocalidadesComponents]
(
	@IdLocalidad int
)
as
begin
WITH DivLocalidadTree AS (
    SELECT       
		t1.IdLocalidad,
        t1.IdDivisionTerritorial,
        t1.Nombre,
		div.Nombre as DivisionTerritorial,
		t1.IdLocalidadPadre,
		t1.BorradoPor
    FROM       
        tblLocalidades as t1 inner join tblDivisionTerritorial div
		on t1.IdDivisionTerritorial= div.IdDivisionTerritorial
    WHERE (t1.IdLocalidad=@IdLocalidad  and t1.BorradoPor is null)
    UNION ALL
    SELECT 
		t2.IdLocalidad,
        t2.IdDivisionTerritorial,
        t2.Nombre,
		div.Nombre as DivisionTerritorial,
		t2.IdLocalidadPadre,
		t2.BorradoPor
    FROM 
        tblLocalidades as t2
		inner join tblDivisionTerritorial div
		on t2.IdDivisionTerritorial= div.IdDivisionTerritorial
		inner join DivLocalidadTree ch
		on t2.IdLocalidadPadre=ch.IdLocalidad
)
select * from DivLocalidadTree where BorradoPor is null;
End