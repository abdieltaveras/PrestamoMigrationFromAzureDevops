CREATE PROCEDURE [dbo].[SpInsUpdLocalidadNegocio]
(
	@IdLocalidadNegocio INT,
    @IdNegocio INT, 
    @NombreComercial VARCHAR(100) ,
    @PrefijoTransacciones VARCHAR(6),
	@PrefijoPrestamo VARCHAR(6),
    @Activo BIT=1 , 
    @Bloqueado bit =0,
	@TaxIdNacional varchar(40),
	@TaxIdLocalidad varchar(40),
    @PermitirOperaciones bit = 1,
    @Usuario VARCHAR(200), 
	@Logo VARCHAR(max)=null, 
    @OtrosDetalles VARCHAR(max)=''
)
AS
Begin
	
	if (@PrefijoPrestamo='') begin set @PrefijoPrestamo=null end
	if (@PrefijoTransacciones='') begin set @PrefijoTransacciones=null end
	if (@IdLocalidadNegocio<=0)	
		begin
		    declare @codigo  varchar(40) = NEWID();
			INSERT INTO dbo.tblLocalidadesNegocio ( Codigo, NombreComercial,  Activo, Bloqueado, TaxIdNacional, TaxIdLocalidad, OtrosDetalles, Logo, InsertadoPor, FechaInsertado, PrefijoTransacciones, PrefijoPrestamo )
			VALUES ( @Codigo, @nombrecomercial,  @activo, @bloqueado, @taxidNacional,@TaxIdLocalidad, @otrosdetalles,@Logo,  @usuario, getdate(), @prefijoTransacciones, @PrefijoPrestamo )
			SELECT SCOPE_IDENTITY(); 
		end
	else
		begin
		UPDATE dbo.tblLocalidadesNegocio
		SET 
			NombreComercial = @nombrecomercial,
			Activo = @activo,
			Bloqueado = @bloqueado,
			TaxIdNacional = @taxidNacional,
			TaxIdLocalidad = @TaxIdLocalidad,
			OtrosDetalles = @otrosdetalles,
			ModificadoPor = @Usuario,
			FechaModificado = getdate(),
			PrefijoPrestamo = @PrefijoPrestamo,
			PrefijoTransacciones = @PrefijoTransacciones
			where IdLocalidadNegocio = @IdLocalidadNegocio
			select @idLocalidadNegocio 
		end
End


