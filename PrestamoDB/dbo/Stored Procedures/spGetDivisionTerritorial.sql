CREATE PROCEDURE [dbo].[spGetDivisionTerritorial]
(
	@IdDivisionTerritorial int=-1,
	@IdLocalidadNegocio int= -1,
	@IdNegocio int=-1,
	@IdDivisionTerritorialPadre int=null,
	@Nombre varchar(50)='',
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0
)
as
begin

SELECT [IdDivisionTerritorial]
      ,[IdDivisionTerritorialPadre]
      ,[IdNegocio]
      ,[IdLocalidadNegocio]
      ,[Nombre]
      ,[Codigo]
      ,[Activo]
      ,[PermiteCalle]
      ,[InsertadoPor]
      ,[FechaInsertado]
      ,[ModificadoPor]
      ,[FechaModificado]
      ,[BorradoPor]
      ,[FechaBorrado]
	
	from 
	tblDivisionTerritorial
	where 
		((@IdDivisionTerritorial=-1) or (IdDivisionTerritorial = @IdDivisionTerritorial))
		and ((@IdNegocio=-1) or (IdNegocio=@idNegocio))
		and (@IdDivisionTerritorialPadre is null and IdDivisionTerritorialPadre is null)
		or ((@IdDivisionTerritorialPadre=-1) or (IdDivisionTerritorialPadre = @IdDivisionTerritorialPadre))
		and ((@Nombre='') or (Nombre like '%'+@Nombre+'%'))
		and ((@condicionBorrado= 0 and BorradoPor is null) 
		or (@condicionBorrado=1 and BorradoPor is not null)
		or (@condicionBorrado=-1))
End