CREATE PROCEDURE [dbo].[spAnularRegistro]
(
	@NombreTabla varchar(100),
	@IdRegistroValor int=-1,
	@IdRegistroNombreColumna varchar(100) = '',
	@Anulado int=1,
	@Usuario varchar(100)
)
as
begin
	EXEC(
		'UPDATE '+ @NombreTabla +' SET AnuladoPor = '''+@Usuario+''', FechaAnulado = GETDATE() WHERE '+@IdRegistroNombreColumna  + ' = '+@IdRegistroValor
	)
End
