CREATE PROCEDURE [dbo].[CatalogoSpBuscar]
	@TextToSearch varchar(50),
	@TableName varchar(50),
	@IdNegocio int
as
	begin
		EXEC('SELECT * from ' + @TableName + ' WHERE Nombre LIKE ''%' + @TextToSearch + '%''') 
		--EXEC('SELECT * from ' + @TableName + '  where ( IdNegocio=-'+@IdNegocio+' )')
	end


	