CREATE PROCEDURE [dbo].[spToggleStatusCatalogo]
	@id int,
	@IdTabla varchar(100),
	@IdNegocio int,
	@NombreTabla varchar(100),
	@Usuario varchar(100),
	@Activo bit = 1
AS
	begin
		EXEC
		(
			'UPDATE '+ @NombreTabla +' SET Activo = '+@Activo+' WHERE '+@IdTabla + ' = '+@Id + 'AND IdNegocio = ' + @IdNegocio
		)
	end
