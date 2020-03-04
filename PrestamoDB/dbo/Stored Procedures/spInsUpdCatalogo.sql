CREATE PROCEDURE [dbo].[spInsUpdCatalogo]
	@Id int,
	@IdTabla varchar(50),
	@Nombre varchar(50),
	@NombreTabla varchar(50),
	@IdNegocio int,
	@Usuario varchar(50),
	@Codigo varchar(10),
	@Activo bit = 1
AS
Begin
if (@Id = 0)
	begin
		EXEC
		(
			'INSERT INTO ' + @NombreTabla +' (Codigo, Nombre, IdNegocio, InsertadoPor, FechaInsertado) VALUES ('''+@Codigo+''','''+@Nombre+''', '''+@IdNegocio+''', '''+@Usuario+''', GETDATE() )'
		)
	end
else
	begin
		EXEC
		(
			'UPDATE '+ @NombreTabla +' SET Codigo = '''+@Codigo+''', Nombre = '''+@Nombre+''', IdNegocio = '''+@IdNegocio+''', ModificadoPor = '''+@Usuario+''', FechaModificado = GETDATE() WHERE '+@IdTabla + ' = '+@Id
		)
	end
End