CREATE PROCEDURE [dbo].[spInsUpdTipoMora]
(
	@idTipoMora int,
	@idNegocio int,
	@Codigo varchar(10),
	@Descripcion varchar(100),
	@Activo bit = 1,
	@RequiereAutorizacion bit = 0,
	@Usuario varchar(100),
	@AplicarA int,
	@CalcularCargoPor int,
	@TipoCargo int,
	@DiasDeGracia int, 
	@MontoOPorCientoACargar decimal (12,9),
	@MontoCuotaDesde decimal(12,2),
	@MontoCuotaHasta decimal (12,2)
)
AS
Begin
-- verificar si id es 0 inserta si es diferente modificar
if (@idTipoMora =0)
	begin
		insert into tblTiposMora
		(idNegocio, Codigo, Descripcion, Activo, RequiereAutorizacion, AplicarA, CalcularCargoPor, TipoCargo,DiasDeGracia,MontoOPorCientoACargar,MontoCuotaDesde,MontoCuotaHasta, InsertadoPor, FechaInsertado)
		values
		(@idNegocio, @Codigo, @Descripcion, @Activo, @RequiereAutorizacion, @AplicarA, @CalcularCargoPor, @TipoCargo,@DiasDeGracia,@MontoOPorCientoACargar,@MontoCuotaDesde,@MontoCuotaHasta,@Usuario, GetDate())
	end
Else
	Begin
	update tblTiposMora
		set 
			idNegocio = @idNegocio,
			Codigo=@Codigo, 
			Activo=@Activo,
			Descripcion=@Descripcion,
			RequiereAutorizacion=@RequiereAutorizacion,
			AplicarA =@AplicarA, 
			CalcularCargoPor = @CalcularCargoPor, 
			TipoCargo =@TipoCargo,
			DiasDeGracia = @DiasDeGracia,
			MontoOPorCientoACargar =@MontoOPorCientoACargar,
			MontoCuotaDesde =@MontoCuotaDesde,
			MontoCuotaHasta = @MontoCuotaHasta,
			ModificadoPor=@Usuario,
			FechaModificado = getdate()
		where idTipoMora = @idTipoMora
	End
End

