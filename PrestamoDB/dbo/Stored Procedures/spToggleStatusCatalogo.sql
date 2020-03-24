CREATE PROCEDURE [dbo].[spToggleStatusCatalogo]
	@id int,
	@IdTabla varchar(100),
	@NombreTabla varchar(100),
	@Usuario varchar(100),
	@Activo bit = 1
AS
	begin
		EXEC
		(
			'UPDATE '+ @NombreTabla +' SET Activo = '+@Activo+' WHERE '+@IdTabla + ' = '+@Id
		)
	end
