CREATE PROCEDURE [dbo].[spGetTiposDivisionTerritorial]
(
	@Usuario varchar(100)
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
		IdDivisionTerritorialPadre =0 and BorradoPor is null
		
End