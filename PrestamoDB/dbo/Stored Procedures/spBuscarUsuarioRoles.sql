CREATE PROCEDURE [dbo].[spBuscarUsuarioRoles]
	@IdUsuario int,
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
BEGIN

select IdUser, IdRole from tblUsersRoles where IdUser = @IdUsuario;

End