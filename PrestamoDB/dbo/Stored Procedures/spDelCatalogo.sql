CREATE PROCEDURE [dbo].[spDelCatalogo]
(
	@IdRegistro int=-1,
	@Usuario varchar(100)='',
	@TableName varchar(100) = '',
	@IdColumnName varchar(100) = ''
)
as
begin
	EXEC (
		'UPDATE '+ @TableName +' SET BorradoPor = '''+@Usuario+''', FechaBorrado = GETDATE() WHERE '+@IdColumnName + ' = '+@IdRegistro
	)
End
