create PROCEDURE [dbo].[spGetCatalogosV2]
(
	@IdNegocio int,
	@IdLocalidadNegocio int = -1,
	@Usuario varchar(100)='',
	@TableName varchar(100),
	@IdColumnName varchar(100),
	@IdRegistro int=-1
)
as
begin 
	EXEC ('SELECT *, '+@IdColumnName+' as IdRegistro from ' + @TableName+'  where ('
    +'('+@IdRegistro+'<=-1 or '+@IdColumnName+'='+@IdRegistro+')'
    +'and ('+@IdNegocio+'<=-1 or IdNegocio='+@IdNegocio+')'
    +'and ('+@IdLocalidadNegocio+'<=-1 or IdLocalidadNegocio='+@IdLocalidadNegocio+')'
	+'and BorradoPor is null'
	+')')

End