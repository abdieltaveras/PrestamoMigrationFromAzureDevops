CREATE PROCEDURE [dbo].[spInsUpdUserRoles]
	@IdUser int,
	@Values varchar(300)
AS
Begin
	EXEC
		(
			'DELETE tblUsersRoles where IdUser = ' + @IdUser
		)
	EXEC
		(
			'INSERT INTO tblUsersRoles (IdUser, IdRole) VALUES ' + @Values
		)
End