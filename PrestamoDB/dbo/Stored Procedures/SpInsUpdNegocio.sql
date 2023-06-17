CREATE PROCEDURE [dbo].[SpInsUpdNegocio]
(
	@IdNegocio       INT,
	@Codigo          VARCHAR (20),
	@NombreJuridico  VARCHAR (100),
	@NombreComercial VARCHAR (100),
	@CorreoElectronico VARCHAR (100) ,
	@Activo          BIT,
	@Bloqueado       BIT,
	@idNegocioPadre  INT=null,
	@TaxIdNo         VARCHAR (20),
	@OtrosDetalles   VARCHAR (100),
	@Logo   VARCHAR (100) = '',
	@InfoAccion		 varchar (max) = '',
	@Usuario varchar(50),
	@Prefijo varchar(3)='',
	@PermitirOperaciones int,
	@IdLocalidadNegocio int 
	--@GenerarSecuencia bit
)
AS
Begin
	if (@idNegocioPadre=-1)
		set @idNegocioPadre =null;

	set @InfoAccion = (select dbo.fnUpdFechaJson(@InfoAccion))
	if (@idNegocio<=0)	
		begin
			INSERT INTO dbo.tblNegocios (Codigo, NombreJuridico, NombreComercial, CorreoElectronico, Activo, Bloqueado, TaxIdNo, OtrosDetalles, Logo, InsertadoPor, FechaInsertado, Prefijo)
			VALUES (@codigo, @nombrejuridico, @nombrecomercial, @correoElectronico, @activo, @bloqueado, @taxidno, @otrosdetalles,@Logo,  @infoAccion, getdate(), @prefijo )
			SELECT SCOPE_IDENTITY(); 
		end
	else
		begin
		UPDATE dbo.tblNegocios
		SET 
			--Codigo = @codigo,
			NombreJuridico = @nombrejuridico,
			NombreComercial = @nombrecomercial,
			CorreoElectronico = @correoElectronico,
			Activo = @activo,
			Bloqueado = @bloqueado,
			TaxIdNo = @taxidno,
			OtrosDetalles = @otrosdetalles,
			ModificadoPor = @InfoAccion,
			FechaModificado = getdate(),
			Prefijo = @Prefijo
			where idNegocio = @IdNegocio
			select @idNegocio 
		end
End


