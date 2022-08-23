CREATE PROCEDURE [dbo].[spDeleteRegistro]
(
	@NombreTabla varchar(100),
	@IdRegistroValor int=-1,
	@IdRegistroNombreColumna varchar(100) = '',
	@Motivo varchar(max),
	@Usuario varchar(100)
)
as
begin
	EXEC(
		'UPDATE '+ @NombreTabla +' SET BorradoPor = '''+@Usuario+''', FechaBorrado = GETDATE() WHERE '+@IdRegistroNombreColumna  + ' = '+@IdRegistroValor +'and borradoPor is null'
	)

End
