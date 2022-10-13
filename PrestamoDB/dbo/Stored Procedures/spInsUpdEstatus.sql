CREATE proc [dbo].[spInsUpdEstatus]
@IdEstatus int,
@Name varchar(50),
@Description varchar(100),
@IsNotPrintOnReport bit,
@IsImpedirPagoEnCaja bit,
@IsRequiereAutorizacionEnCaja bit,
@IsActivo bit,
@IsImpedirHacerPrestamo bit
as
IF(@IdEstatus <= 0)
	BEGIN
		INSERT INTO tblEstatus(Name,Description,IsNotPrintOnReport,IsImpedirPagoEnCaja,IsRequiereAutorizacionEnCaja,IsActivo,IsImpedirHacerPrestamo)
		values (@Name,@Description,@IsNotPrintOnReport,@IsImpedirPagoEnCaja,@IsRequiereAutorizacionEnCaja,@IsActivo,@IsImpedirHacerPrestamo)
	END
	ELSE
	BEGIN
		UPDATE tblEstatus SET
		Name = @Name,
		Description = @Description,
		IsNotPrintOnReport = @IsNotPrintOnReport,
		IsImpedirPagoEnCaja = @IsImpedirPagoEnCaja,
		IsRequiereAutorizacionEnCaja = @IsRequiereAutorizacionEnCaja,
		IsActivo = @IsActivo,
		IsImpedirHacerPrestamo = @IsImpedirHacerPrestamo
		WHERE IdEstatus = @IdEstatus
	END