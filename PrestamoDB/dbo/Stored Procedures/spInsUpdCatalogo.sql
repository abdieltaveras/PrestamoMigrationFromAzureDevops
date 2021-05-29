CREATE PROCEDURE [dbo].[spInsUpdCatalogo]
	@Id int,
	@IdTabla varchar(50),
	@Nombre varchar(50),
	@NombreTabla varchar(50),
	@IdNegocio int,
	@IdLocalidadNegocio int,
	@Usuario varchar(50),
	@Codigo varchar(10),
	@Activo bit = 1
AS
Begin
if (@Id = 0)
	begin
		EXEC
		(
			'INSERT INTO ' + @NombreTabla +' (Codigo, Nombre, IdNegocio, IdLocalidadNegocio, InsertadoPor, FechaInsertado) VALUES ('''+@Codigo+''','''+@Nombre+''', '''+@IdNegocio+''','''+@IdlocalidadNegocio+''', '''+@Usuario+''', GETDATE() )'
		)
		EXEC
		(
			'SELECT IDENT_CURRENT(''' + @NombreTabla + ''') as last_role'
		)
	end
else
	begin
		EXEC
		(
			'UPDATE '+ @NombreTabla +' SET Codigo = '''+@Codigo+''', Nombre = '''+@Nombre+''', IdNegocio = '''+@IdNegocio+''',IdLocalidadNegocio = '''+@IdLocalidadNegocio+''', ModificadoPor = '''+@Usuario+''', FechaModificado = GETDATE() WHERE '+@IdTabla + ' = '+@Id
		)
	end
End