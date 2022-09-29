CREATE proc [dbo].[spInsUpdEntidadEstatus]
@IdEntidadEstatus int,
@Name varchar(50),
@Description varchar(100),
@IsNotPrintOnReport bit,
@IsImpedirPagoEnCaja bit,
@IsRequiereAutorizacionEnCaja bit,
@IsActivo bit,
@IsImpedirHacerPrestamo bit
as
IF(@IdEntidadEstatus <= 0)
	BEGIN
		INSERT INTO tblEntidadEstatus (Name,Description,IsNotPrintOnReport,IsImpedirPagoEnCaja,IsRequiereAutorizacionEnCaja,IsActivo,IsImpedirHacerPrestamo)
		values (@Name,@Description,@IsNotPrintOnReport,@IsImpedirPagoEnCaja,@IsRequiereAutorizacionEnCaja,@IsActivo,@IsImpedirHacerPrestamo)
	END
	ELSE
	BEGIN
		UPDATE tblEntidadEstatus SET
		Name = @Name,
		Description = @Description,
		IsNotPrintOnReport = @IsNotPrintOnReport,
		IsImpedirPagoEnCaja = @IsImpedirPagoEnCaja,
		IsRequiereAutorizacionEnCaja = @IsRequiereAutorizacionEnCaja,
		IsActivo = @IsActivo,
		IsImpedirHacerPrestamo = @IsImpedirHacerPrestamo
		WHERE IdEntidadEstatus = @IdEntidadEstatus
	END