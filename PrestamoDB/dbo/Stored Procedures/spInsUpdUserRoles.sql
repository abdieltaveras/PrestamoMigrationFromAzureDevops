CREATE PROCEDURE [dbo].[spInsUpdUserRoles]
	@UsuarioRoleInsertar tpUserRole READONLY,
	@UsuarioRoleModificar tpUserRole READONLY,
	@UsuarioRoleAnular tpUserRole READONLY,
	@Usuario varchar(50)
AS
Begin

	--DECLARE @prueba tpUserRole
	DECLARE @tempDataI tpUserRole
	DECLARE @tempDataM tpUserRole
	DECLARE @tempDataA tpUserRole

	insert into @tempDataI select * from @UsuarioRoleInsertar
	
	--Para insertar
	while (exists(select 1 from @tempDataI))
	BEGIN		
		declare @IdUser int = (SELECT TOP 1 IdUser FROM @tempDataI)
		declare @IdRole int = (SELECT TOP 1 IdRole FROM @tempDataI)

		INSERT INTO tblUsersRoles (IdUser, IdRole, InsertadoPor) VALUES (@IdUser, @IdRole, @Usuario)

		delete from @tempDataI where IdUser = @IdUser and IdRole = @IdRole
	END

	--Para modificar
	insert into @tempDataM select * from @UsuarioRoleModificar

	WHILE (exists(select 1 from @tempDataM))
	BEGIN
		
		declare @IdUserM int = (SELECT TOP 1 IdUser FROM @tempDataM)
		declare @IdRoleM int = (SELECT TOP 1 IdRole FROM @tempDataM)

		update 
			tblUsersRoles 
		set
			ModificadoPor = @Usuario, FechaModificado = getdate(), BorradoPor = NULL, FechaBorrado = null 
		where 
			IdRole = @IdRoleM and IdUser = @IdUserM;

		delete from @tempDataM where IdUser = @IdUserM and IdRole = @IdRoleM
	END

	--Para anular
	insert into @tempDataA select * from @UsuarioRoleAnular

	WHILE (exists(select 1 from @tempDataA))
	BEGIN
		
		declare @IdUserA int = (SELECT TOP 1 IdUser FROM @tempDataA)
		declare @IdRoleA int = (SELECT TOP 1 IdRole FROM @tempDataA)

		update 
			tblUsersRoles 
		set
			BorradoPor = @Usuario, FechaBorrado = getdate() 
		where 
			IdRole = @IdRoleA and IdUser = @IdUserA;

		delete from @tempDataA where IdUser = @IdUserA and IdRole = @IdRoleA


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