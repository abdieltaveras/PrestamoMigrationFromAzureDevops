CREATE PROCEDURE [dbo].[spBuscarRoleOperaciones]
	@IdRole int,
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
BEGIN

select IdRole, IdOperacion from tblRolesOperaciones where IdRole = @IdRole;

End