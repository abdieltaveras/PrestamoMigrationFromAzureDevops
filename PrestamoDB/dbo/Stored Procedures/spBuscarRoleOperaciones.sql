CREATE PROCEDURE [dbo].[spBuscarRoleOperaciones]
	@IdRole int,
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Borrado int=0
as
BEGIN
	SELECT 
		IdRole, IdOperacion 
	FROM 
		tblRolesOperaciones 
	WHERE 
		IdRole = @IdRole AND BorradoPor IS NULL;
End