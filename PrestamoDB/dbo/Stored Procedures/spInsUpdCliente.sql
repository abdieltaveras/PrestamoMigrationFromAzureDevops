CREATE PROCEDURE [dbo].[spInsUpdCliente]
	(@idCliente int,
	@activo bit, 
	@apodo varchar(100), 
	@apellidos varchar(100), 
	@idEstadocivil int, 
	@fechanacimiento datetime, 
	@GenerarSecuencia bit=1,
	@idnegocio int, 
	@idLocalidadNegocio int, 
	@idtipoidentificacion int, 
	@IdTipoProfesionUOcupacion int, 
	@infoconyuge varchar(max), 
	@infolaboral varchar(max), 
	@infodireccion varchar(max),  
	@noidentificacion varchar(20), 
	@nombres varchar(400), 
	@idSexo int, 
	@tieneConyuge bit, 
	@telefonocasa varchar(20), 
	@telefonomovil varchar(20),
	@correoElectronico varchar(30),
	@inforeferencias varchar(max),
	@Imagenes varchar(max),
    @Usuario varchar(100))
AS
Begin
	if (@idCliente<=0)
		begin
			SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
			BEGIN TRANSACTION 
			begin try
				declare @codigo varchar(10)
				if (@GenerarSecuencia=1)
				begin
					exec dbo.spGenerarSecuenciaString 'Codigo de Clientes',10,1, @codigo output
				end
				INSERT INTO dbo.tblClientes (Activo,  Apodo, Apellidos, IdEstadoCivil, FechaNacimiento, idNegocio, idTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion,InsertadoPor, FechaInsertado, NoIdentificacion, Nombres, idSexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, TieneConyuge, infoReferencias, codigo, IdLocalidadNegocio, Imagenes)

				VALUES (@activo, @apodo, @apellidos, @IdEstadocivil, @fechanacimiento, @idnegocio, @idtipoidentificacion, @IdTipoProfesionUOcupacion,@infoconyuge, @infolaboral, @infodireccion, @usuario,getdate(), @NoIdentificacion, @Nombres, @idSexo, @TelefonoCasa, @TelefonoMovil, @correoElectronico, @tieneConyuge, @infoReferencias, @codigo, @idLocalidadNegocio, @Imagenes)
				SELECT SCOPE_IDENTITY(); 
				commit
			end try
			begin catch
				rollback
				declare @errorMessage varchar(max) =  (select ERROR_MESSAGE()) 
				RAISERROR(@errorMessage,17,1); 
			end catch
		end
	Else
		Begin
			UPDATE dbo.tblClientes
			SET Activo = @activo,
				Apodo = @apodo,
				Apellidos = @apellidos,
				IdEstadoCivil = @idEstadocivil,
				FechaNacimiento = @fechanacimiento,
				FechaModificado = getdate(),
				idTipoIdentificacion = @idtipoidentificacion,
				IdTipoProfesionUOcupacion = @IdTipoProfesionUOcupacion,
				InfoConyuge = @infoconyuge,
				InfoLaboral = @infolaboral,
				InfoDireccion = @infodireccion,
				[ModificadoPor] = @usuario,
				NoIdentificacion = @noidentificacion,
				Imagenes = @Imagenes,
				--Codigo = @codigo, este codigo una vez es creado jamas debe ser actualizado
				-- de hacerse estos tipos de operacion requeriran un procedimiento especial
				-- que deje un informe de estos tipos de cambios
				Nombres = @nombres,
				idSexo = @idSexo,
				TelefonoCasa = @telefonocasa,
				TelefonoMovil = @telefonomovil,
				CorreoElectronico = @correoElectronico,
				TieneConyuge = @tieneConyuge,
				InfoReferencias = @infoReferencias
				where IdCliente = @IdCliente
				select @idCliente
		End
End

