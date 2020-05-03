CREATE PROCEDURE [dbo].[spInsUpdCliente](@idCliente varchar(100),
	@activo bit, 
	@apodo varchar(100), 
	@apellidos varchar(100), 
	@codigo varchar(100), 
	@estadocivil int, 
	@fechanacimiento datetime, 
	@GenerarSecuencia bit,
	@idnegocio int, 
	@idtipoidentificacion int, 
	@IdTipoProfesionUOcupacion int, 
	@infoconyuge varchar(400), 
	@infolaboral varchar(400), 
	@infodireccion varchar(400),  
	@noidentificacion varchar(20), 
	@imagen1FileName varchar(50), 
	@imagen2FileName varchar(50), 
	@nombres varchar(400), 
	@sexo int, 
	@tieneConyuge bit, 
	@telefonocasa varchar(20), 
	@telefonomovil varchar(20),
	@correoElectronico varchar(30),
	@infoReferencia varchar(4000),
    @Usuario varchar(100))
AS
Begin
	if (@idCliente<=0)
		
		begin
			INSERT INTO dbo.tblClientes (Activo,  Apodo, Apellidos, EstadoCivil, FechaNacimiento, idNegocio, idTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion,InsertadoPor, FechaInsertado,
			 NoIdentificacion, Nombres, Sexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, Imagen1FileName, Imagen2FileName, TieneConyuge, infoReferencia)
			VALUES (@activo, @apodo, @apellidos, @estadocivil, @fechanacimiento, @idnegocio, @idtipoidentificacion, @IdTipoProfesionUOcupacion,@infoconyuge, @infolaboral, @infodireccion, @usuario,getdate(), @NoIdentificacion, @Nombres, @Sexo, @TelefonoCasa, @TelefonoMovil, @correoElectronico, @Imagen1FileName, @imagen2FileName, @tieneConyuge, @infoReferencia)
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
				Codigo = @codigo,
				Nombres = @nombres,
				Sexo = @sexo,
				TelefonoCasa = @telefonocasa,
				TelefonoMovil = @telefonomovil,
				CorreoElectronico = @correoElectronico,
				Imagen1FileName = @imagen1FileName,
				Imagen2FileName = @imagen2FileName,
				TieneConyuge = @tieneConyuge,
				InfoReferencia = @infoReferencia
				where IdCliente = IdCliente
		End
End

