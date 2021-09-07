CREATE PROCEDURE [dbo].[spRptClientes]
@OrdenarPor varchar(50)= 'Nombres',
@ODesde varchar(50) = 'a',
@OHasta varchar(50) = 'z',
@Rango varchar(50)= 'Nombres',
@RDesde varchar(50) = 'a',
@RHasta varchar(50) = 'z',
@ReportType int = -1,
@sql nvarchar(max) = ''
as
--Eliminamos la tabla temporal si existe
DROP TABLE IF EXISTS  #tempClientes 
--- si no asignamos estas variables con todos estos espacios, dará un error de columnas
-- si encuentra otra manera de que no presente dicho error, favor, no borrar el codigo existente sino comentarlo
SET @ODesde = ''''+@ODesde+ ''''
SET @OHasta = ''''+@OHasta+ ''''
SET @RDesde = ''''+@RDesde+ ''''
SET @RHasta = ''''+@RHasta+ ''''
set @sql=N'select * into #tempClientes from  tblClientes '+ ' WHERE '+ @OrdenarPor + 
' between ' + @Odesde + ' and ' + @Ohasta +'order by ' + @OrdenarPor +
'  select * from #tempClientes' + ' WHERE '+ @Rango + 
' between ' + @Rdesde + ' and ' + @Rhasta 
exec (@sql)