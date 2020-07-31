CREATE PROCEDURE [dbo].[UsuarioListaOperacionesSpGet]
	@IdUsuario int,
	@IsAdmin bit = 0
as
if (@IsAdmin = 0)	
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
			tblUsersRoles.IdUser = @IdUsuario AND tblRolesOperaciones.AnuladoPor IS NULL
		GROUP BY 
			tblOperaciones.Codigo
	end
else
	SELECT 
		Codigo 
	FROM 
		tblOperaciones
