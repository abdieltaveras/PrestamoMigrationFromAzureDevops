CREATE PROCEDURE [dbo].[spInsUpdCatalogo]
	@IdRegistro int,
	@IdColumnName varchar(50),
	@Nombre varchar(50),
	@TableName varchar(50),
	@IdNegocio int,
	@IdLocalidadNegocio int,
	@Usuario varchar(50),
	@Codigo varchar(10)=null,
	@Activo bit = 1
AS
Begin
	if (@IdRegistro <= 0)
	begin
		EXEC
		(
			'INSERT INTO ' + @TableName +' (Codigo, Nombre, Activo, IdNegocio, IdLocalidadNegocio, InsertadoPor, FechaInsertado) 
			VALUES ('''+@Codigo+''','''+@Nombre+''','''+@Activo+''', '''+@IdNegocio+''','''+@IdlocalidadNegocio+''', '''+@Usuario+''', GETDATE() )'
		)
		set @IdRegistro = (Select SCOPE_IDENTITY())
	end
	else
	begin
		EXEC
		(
			'UPDATE '+ @TableName +' SET Codigo = '''+@Codigo+''', Nombre = '''+@Nombre+''', IdNegocio = '''+@IdNegocio+''',IdLocalidadNegocio = '''+@IdLocalidadNegocio+''',Activo = '''+@Activo+''', ModificadoPor = '''+@Usuario+''', FechaModificado = GETDATE() WHERE '+@IdColumnName + ' = '+@IdRegistro
		)
	end
	select @IdRegistro
End
