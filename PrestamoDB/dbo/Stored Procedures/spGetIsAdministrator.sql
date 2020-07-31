CREATE PROCEDURE [dbo].[spGetIsAdministrator]
	@IdUsuario int,
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
	begin
		SELECT
			*
		FROM
			tblUsersRoles
		WHERE
			IdUser = @IdUsuario AND IdRole = 1; -- Administrator Role = 1
	end