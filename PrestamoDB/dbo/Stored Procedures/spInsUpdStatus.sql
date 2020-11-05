CREATE PROCEDURE [dbo].[spInsUpdStatus]
	@IdStatus int,
	@IdTipoStatus int,
	@Tipo varchar(50),
	@Concepto varchar(50),
	@Estado int,
	@IdNegocio int,
	@Usuario varchar
AS
BEGIN
	IF(@IdStatus<=0)
		BEGIN
			insert into tblStatus (IdTipoStatus,Tipo,Concepto,Estado)
			values (@IdTipoStatus,@Tipo,@Concepto,@Estado)
		END
	ELSE
		BEGIN
			update tblStatus set IdTipoStatus = @IdTipoStatus,
			Tipo = @Tipo,
			Concepto = @Concepto,
			Estado = @Estado
			where IdStatus = @IdStatus
		END
END
