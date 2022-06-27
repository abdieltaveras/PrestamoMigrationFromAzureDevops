ALTER TABLE dbo.tblDivisionTerritorial DROP COLUMN IdDivisionTerritorialPadre
--IdDivisionTerritorialPadre antes idLocalidadPadre
EXEC SP_RENAME 'dbo.tblDivisionTerritorial.idLocalidadPadre' , 'IdDivisionTerritorialPadre', 'COLUMN'
DELETE FROM tblDivisionTerritorial WHERE IdDivisionTerritorial=1
