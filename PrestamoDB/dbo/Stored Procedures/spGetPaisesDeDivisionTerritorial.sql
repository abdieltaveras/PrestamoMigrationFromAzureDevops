CREATE PROCEDURE [dbo].[spGetPaisesDeDivisionTerritorial]
(
	@IdNegocio int,
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0 
)
as
begin

SELECT t1.IdDivisionTerritorial, t1.IdDivisionTerritorialPadre, t1.Nombre FROM tblDivisionTerritorial AS t1
inner join tblDivisionTerritorial AS t2 on t2.IdDivisionTerritorial = t1.IdDivisionTerritorialPadre
WHERE 
t2.IdDivisionTerritorialPadre IS null 
and ((@condicionBorrado= 0 and t1.BorradoPor is null) 
or (@condicionBorrado=1 and t1.BorradoPor is not null)
or (@condicionBorrado=-1))
End