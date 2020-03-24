CREATE PROCEDURE [dbo].[spInsUpdUserRoles]
	@UserRole tpUserRole READONLY
	--@IdUser int,
	--@Values varchar(300)
AS
Begin

	DECLARE @prueba tpUserRole

	insert into @prueba select * from @UserRole
	

	DELETE tblUsersRoles where IdUser = (SELECT TOP 1 IdUser FROM @prueba)

	while (exists(select 1 from @prueba))
	BEGIN
		
		declare @IdUser int = (SELECT TOP 1 IdUser FROM @prueba)
		declare @IdRole int = (SELECT TOP 1 IdRole FROM @prueba)

		INSERT INTO tblUsersRoles (IdUser, IdRole) VALUES (@IdUser, @IdRole)

		delete from @prueba where IdUser = @IdUser and IdRole = @IdRole

	END

	
End



	--EXEC
	--	(
	--		'DELETE tblUsersRoles where IdUser = ' + @IdUser
	--	)
	--EXEC
	--	(
	--		'INSERT INTO tblUsersRoles (IdUser, IdRole) VALUES ' + @Values
	--	)