CREATE PROCEDURE [dbo].[spInsUpdRoleOperacion]
	@IdRole int,
	@Values varchar(300)
AS
Begin
	EXEC
		(
			'DELETE tblRolesOperaciones where IdRole = ' + @IdRole
		)
	EXEC
		(
			'INSERT INTO tblRolesOperaciones (IdRole, IdOperacion) VALUES ' + @Values
		)
End