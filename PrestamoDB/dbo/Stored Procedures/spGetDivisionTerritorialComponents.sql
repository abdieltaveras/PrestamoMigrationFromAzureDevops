create procedure [dbo].[spGetDivisionTerritorialComponents]
(
	@IdDivisionTerritorial int,
	@Usuario varchar(100)=''
)
as
begin
WITH DivTerritorioTree AS (
    SELECT       
        IdDivisionTerritorial,
        Nombre,
		IdDivisionTerritorialPadre,
		BorradoPor
    FROM       
        tblDivisionTerritorial as t1
    WHERE (idDivisionTerritorial=@IdDivisionTerritorial  and BorradoPor is null)
    UNION ALL
    SELECT 
        t2.IdDivisionTerritorial,
        t2.Nombre,
		t2.IdDivisionTerritorialPadre,
		t2.BorradoPor
    FROM 
        tblDivisionTerritorial as t2
		inner join DivTerritorioTree ch
		on t2.IdDivisionTerritorialPadre=ch.IdDivisionTerritorial
)
select * from DivTerritorioTree where BorradoPor is null;
End