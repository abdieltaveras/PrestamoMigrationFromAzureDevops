CREATE PROCEDURE [dbo].[sDeleteRegistro]
(
	@NombreTabla varchar(100),
	@IdRegistroValor int=-1,
	@IdRegistroNombreColumna varchar(100) = '',
	@Borrado int=1,
	@Usuario varchar(100)
)
as
begin
	EXEC(
		'UPDATE '+ @NombreTabla +' SET BorradoPor = '''+@Usuario+''', FechaBorrado = GETDATE() WHERE '+@IdRegistroNombreColumna  + ' = '+@IdRegistroValor
	)
End
