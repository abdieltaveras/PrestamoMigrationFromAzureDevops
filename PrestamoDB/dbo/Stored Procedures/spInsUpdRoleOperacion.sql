CREATE PROCEDURE [dbo].[spInsUpdRoleOperacion]	
	@RoleOperacionInsertar tpRoleOperacion READONLY,
	@RoleOperacionModificar tpRoleOperacion READONLY,
	@RoleOperacionAnular tpRoleOperacion READONLY,
	@Usuario varchar(50)
AS
Begin
	DECLARE @tempDataI tpRoleOperacion
	DECLARE @tempDataM tpRoleOperacion
	DECLARE @tempDataA tpRoleOperacion

	insert into @tempDataI select * from @RoleOperacionInsertar

	--Se borran todos los registros de roles-operacion para ingresarlos nuevamente, este es el metodo para poder actualizar
	--los registros en la tabla
	--DELETE tblRolesOperaciones where IdRole = (SELECT TOP 1 IdRole FROM @tempData)

	--Para insertar
	while (exists(select 1 from @tempDataI))
	BEGIN
		
		declare @IdRoleI int = (SELECT TOP 1 IdRole FROM @tempDataI)
		declare @IdOperacionI int = (SELECT TOP 1 IdOperacion FROM @tempDataI)

		INSERT INTO tblRolesOperaciones(IdRole, IdOperacion, InsertadoPor) VALUES (@IdRoleI, @IdOperacionI, @Usuario)

		delete from @tempDataI where IdRole = @IdRoleI and IdOperacion = @IdOperacionI

	END

	--Para modificar
	insert into @tempDataM select * from @RoleOperacionModificar

	WHILE (exists(select 1 from @tempDataM))
	BEGIN
		
		declare @IdRoleM int = (SELECT TOP 1 IdRole FROM @tempDataM)
		declare @IdOperacionM int = (SELECT TOP 1 IdOperacion FROM @tempDataM)

		update 
			tblRolesOperaciones 
		set
			ModificadoPor = @Usuario, FechaModificado = getdate(), AnuladoPor = NULL, FechaAnulado = null 
		where 
			IdRole = @IdRoleM and IdOperacion = @IdOperacionM;

		delete from @tempDataM where IdRole = @IdRoleM and IdOperacion = @IdOperacionM
	END

	--Para anular
	insert into @tempDataA select * from @RoleOperacionAnular

	WHILE (exists(select 1 from @tempDataA))
	BEGIN
		
		declare @IdRoleA int = (SELECT TOP 1 IdRole FROM @tempDataA)
		declare @IdOperacionA int = (SELECT TOP 1 IdOperacion FROM @tempDataA)

		update 
			tblRolesOperaciones 
		set
			AnuladoPor = @Usuario, FechaAnulado = getdate() 
		where 
			IdRole = @IdRoleA and IdOperacion = @IdOperacionA;

		delete from @tempDataA where IdRole = @IdRoleA and IdOperacion = @IdOperacionA

	END

End