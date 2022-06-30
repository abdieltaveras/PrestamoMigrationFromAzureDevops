CREATE PROCEDURE [dbo].[spGetTiposDivisionesTerritoriales]
(
	@Usuario varchar(100)='',
	@condicionBorrado int = 0 
)
as
begin

SELECT IdDivisionTerritorial, IdDivisionTerritorialPadre, Nombre FROM tblDivisionTerritorial AS t1
WHERE 
IdDivisionTerritorialPadre IS null 
and ((@condicionBorrado= 0 and t1.BorradoPor is null) 
or (@condicionBorrado=1 and t1.BorradoPor is not null)
or (@condicionBorrado=-1))
End