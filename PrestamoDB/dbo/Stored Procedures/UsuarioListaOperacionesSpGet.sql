CREATE PROCEDURE [dbo].[UsuarioListaOperacionesSpGet]
	@IdUsuario int
as
	begin
		SELECT 
			tblOperaciones.Codigo
		FROM
			tblUsersRoles 
		JOIN 
			tblRolesOperaciones ON tblUsersRoles.IdRole = tblRolesOperaciones.IdRole 
		JOIN 
			tblOperaciones ON tblRolesOperaciones.IdOperacion = tblOperaciones.IdOperacion
		WHERE 
			tblUsersRoles.IdUser = @IdUsuario AND tblRolesOperaciones.BorradoPor = NULL
		GROUP BY 
			tblOperaciones.Codigo
	end