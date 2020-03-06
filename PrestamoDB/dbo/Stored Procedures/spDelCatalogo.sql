CREATE PROCEDURE [dbo].[spDelCatalogo]
(
	@Id int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)='',
	@NombreTabla varchar(100) = '',
	@IdTabla varchar(100) = ''
)
as
begin
	EXEC(
		'UPDATE '+ @NombreTabla +' SET AnuladoPor = '''+@Usuario+''', FechaAnulado = GETDATE() WHERE '+@IdTabla + ' = '+@Id + 'AND IdNegocio = ' + @IdNegocio
	)
End
