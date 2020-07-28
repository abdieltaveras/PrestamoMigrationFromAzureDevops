CREATE PROCEDURE [dbo].[spBuscarRoleOperaciones]
	@IdRole int,
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
BEGIN

	SELECT
		IdRole, tblRolesOperaciones.IdOperacion, tblOperaciones.Grupo
	FROM 
		tblRolesOperaciones
	JOIN 
		tblOperaciones ON tblRolesOperaciones.IdOperacion = tblOperaciones.IdOperacion
	WHERE 
		IdRole = @IdRole AND AnuladoPor IS NULL;

End