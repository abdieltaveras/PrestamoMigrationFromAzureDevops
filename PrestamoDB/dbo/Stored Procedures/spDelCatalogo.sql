CREATE PROCEDURE [dbo].[spDelCatalogo]
(
	@IdRegistro int=-1,
	@Usuario varchar(100)='',
	@NombreTabla varchar(100) = '',
	@IdNombreColumna varchar(100) = ''
)
as
begin
	EXEC(
		'UPDATE '+ @NombreTabla +' SET BorradoPor = '''+@Usuario+''', FechaBorrado = GETDATE() WHERE '+@IdNombreColumna + ' = '+@IdRegistro
	)
End
