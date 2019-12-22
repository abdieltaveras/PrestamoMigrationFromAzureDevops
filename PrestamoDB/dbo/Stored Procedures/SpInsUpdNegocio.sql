CREATE PROCEDURE [dbo].[SpInsUpdNegocio]
(
	@IdNegocio       INT,
	@Codigo          VARCHAR (20),
	@NombreJuridico  VARCHAR (100),
	@NombreComercial VARCHAR (100),
	@CorreoElectronico VARCHAR (100),
	@Activo          BIT,
	@Bloqueado       BIT,
	@idNegocioPadre  INT=null,
	@TaxIdNo         VARCHAR (20),
	@OtrosDetalles   VARCHAR (100),
	@InfoAccion		 varchar (max),
	@Usuario varchar(50),
	@GenerarSecuencia bit
)
AS
Begin
	
	set @InfoAccion = (select dbo.fnUpdFechaJson(@InfoAccion))
	if (@idNegocio<=0)	
		begin
			INSERT INTO dbo.tblNegocios (Codigo, NombreJuridico, NombreComercial, CorreoElectronico, Activo, Bloqueado, idNegocioPadre, TaxIdNo, OtrosDetalles, InsertadoPor, FechaInsertado )
			VALUES (@codigo, @nombrejuridico, @nombrecomercial, @correoElectronico, @activo, @bloqueado, @idnegociopadre, @taxidno, @otrosdetalles, @infoAccion, getdate() )
		end
	else
		begin
		UPDATE dbo.tblNegocios
		SET 
			Codigo = @codigo,
			NombreJuridico = @nombrejuridico,
			NombreComercial = @nombrecomercial,
			CorreoElectronico = @correoElectronico,
			Activo = @activo,
			Bloqueado = @bloqueado,
			TaxIdNo = @taxidno,
			OtrosDetalles = @otrosdetalles,
			ModificadoPor = @InfoAccion,
			FechaModificado = getdate()
			where idNegocio = @IdNegocio
		end
End

