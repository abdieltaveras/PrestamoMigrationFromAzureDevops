CREATE PROCEDURE [dbo].[spInsUpdCliente]
	(@idCliente int,
	@activo bit, 
	@apodo varchar(100), 
	@apellidos varchar(100), 
	@estadocivil int, 
	@fechanacimiento datetime, 
	@GenerarSecuencia bit=1,
	@idnegocio int, 
	@idtipoidentificacion int, 
	@IdTipoProfesionUOcupacion int, 
	@infoconyuge varchar(400), 
	@infolaboral varchar(400), 
	@infodireccion varchar(400),  
	@noidentificacion varchar(20),
	@Imagen1DocumentoName varchar(50),
	@Imagen2DocumentoName varchar(50),
	@imagen1FileName varchar(50), 
	@imagen2FileName varchar(50), 
	@nombres varchar(400), 
	@sexo int, 
	@tieneConyuge bit, 
	@telefonocasa varchar(20), 
	@telefonomovil varchar(20),
	@correoElectronico varchar(30),
	@inforeferencia varchar(4000),
	@infoRedesSociales varchar (800),
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
				INSERT INTO dbo.tblClientes (Activo,  Apodo, Apellidos, EstadoCivil, FechaNacimiento, idNegocio, idTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion,InsertadoPor, FechaInsertado, NoIdentificacion, Nombres, Sexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, Imagen1FileName, Imagen2FileName, Imagen1DocumentoName,Imagen2DocumentoName,  TieneConyuge, infoReferencia, infoRedesSociales, codigo)

				VALUES (@activo, @apodo, @apellidos, @estadocivil, @fechanacimiento, @idnegocio, @idtipoidentificacion, @IdTipoProfesionUOcupacion,@infoconyuge, @infolaboral, @infodireccion, @usuario,getdate(), @NoIdentificacion, @Nombres, @Sexo, @TelefonoCasa, @TelefonoMovil, @correoElectronico, @Imagen1FileName, @imagen2FileName, @Imagen1DocumentoName, @Imagen2DocumentoName, @tieneConyuge, @infoReferencia,@infoRedesSociales , @codigo)
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
				EstadoCivil = @estadocivil,
				FechaNacimiento = @fechanacimiento,
				FechaModificado = getdate(),
				idTipoIdentificacion = @idtipoidentificacion,
				IdTipoProfesionUOcupacion = @IdTipoProfesionUOcupacion,
				InfoConyuge = @infoconyuge,
				InfoLaboral = @infolaboral,
				InfoDireccion = @infodireccion,
				[ModificadoPor] = @usuario,
				NoIdentificacion = @noidentificacion,
				--Codigo = @codigo, este codigo una vez es creado jamas debe ser actualizado
				-- de hacerse estos tipos de operacion requeriran un procedimiento especial
				-- que deje un informe de estos tipos de cambios
				Nombres = @nombres,
				Sexo = @sexo,
				TelefonoCasa = @telefonocasa,
				TelefonoMovil = @telefonomovil,
				CorreoElectronico = @correoElectronico,
				Imagen1FileName = @imagen1FileName,
				Imagen2FileName = @imagen2FileName,
				Imagen1DocumentoName = @Imagen1DocumentoName,
				Imagen2DocumentoName = @Imagen2DocumentoName,
				TieneConyuge = @tieneConyuge,
				InfoReferencia = @infoReferencia,
				infoRedesSociales = @infoRedesSociales
				where IdCliente = @IdCliente
				select @idCliente
		End
End

