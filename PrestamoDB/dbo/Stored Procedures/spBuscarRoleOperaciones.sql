CREATE PROCEDURE [dbo].[spBuscarRoleOperaciones]
	@IdRole int,
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
BEGIN
	SELECT 
		IdRole, IdOperacion 
	FROM 
		tblRolesOperaciones 
	WHERE 
		IdRole = @IdRole AND AnuladoPor IS NULL;
End