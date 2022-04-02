CREATE PROCEDURE [dbo].[spBuscarTodosUsuarioRoles]
	@IdUsuario int,
	@IdNegocio int,
		@IdLocalidadNegocio int = -1,
	@Usuario varchar(100) = '',
	@Borrado int=0
as
BEGIN

	SELECT 
		IdUser, IdRole, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado 
	FROM 
		tblUsersRoles 
	WHERE 
		IdUser = @IdUsuario 

End