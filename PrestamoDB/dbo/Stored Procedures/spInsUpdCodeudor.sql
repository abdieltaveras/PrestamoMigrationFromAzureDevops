CREATE PROCEDURE [dbo].[spInsUpdCodeudor]
(@idCodeudor int,
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
	@infolaboral varchar(max), 
	@infodireccion varchar(max),  
	@noidentificacion varchar(20), 
	@imagen1FileName varchar(50), 
	@imagen2FileName varchar(50), 
	@nombres varchar(400), 
	@idSexo int, 
	@tieneConyuge bit, 
	@telefonocasa varchar(20), 
	@telefonomovil varchar(20),
	@correoElectronico varchar(30),
	@Imagenes varchar(max),
    @Usuario varchar(100))
AS
Begin
	if (@idCodeudor<=0)
		begin
			SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
			BEGIN TRANSACTION 
			begin try
				declare @codigo varchar(10)
				if (@GenerarSecuencia=1)
				begin
					exec dbo.spGenerarSecuenciaString 'Codigo de Codeudores',10,1, @codigo output
				end
				INSERT INTO dbo.tblCodeudores(Activo,  Apodo, Apellidos, IdEstadoCivil, FechaNacimiento, idNegocio, idTipoIdentificacion, IdTipoProfesionUOcupacion, InfoLaboral, InfoDireccion,InsertadoPor, FechaInsertado, NoIdentificacion, Nombres, idSexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, Imagen1FileName, Imagen2FileName, codigo, IdLocalidadNegocio, Imagenes)

				VALUES (@activo, @apodo, @apellidos, @IdEstadocivil, @fechanacimiento, @idnegocio, @idtipoidentificacion, @IdTipoProfesionUOcupacion, @infolaboral, @infodireccion, @usuario,getdate(), @NoIdentificacion, @Nombres, @idSexo, @TelefonoCasa, @TelefonoMovil, @correoElectronico, @Imagen1FileName, @imagen2FileName,  @codigo, @idLocalidadNegocio, @Imagenes)
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
			UPDATE dbo.tblCodeudores
			SET Activo = @activo,
				Apodo = @apodo,
				Apellidos = @apellidos,
				IdEstadoCivil = @idEstadocivil,
				FechaNacimiento = @fechanacimiento,
				FechaModificado = getdate(),
				idTipoIdentificacion = @idtipoidentificacion,
				IdTipoProfesionUOcupacion = @IdTipoProfesionUOcupacion,
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
				Imagen1FileName = @imagen1FileName,
				Imagen2FileName = @imagen2FileName
				where IdCodeudor = @idCodeudor
				select @idCodeudor
		End
End

