CREATE PROCEDURE [dbo].[spInsUpdStatus]
	@IdStatus int,
	@IdTipoStatus int,
	@Concepto varchar(50),
	@Estado int,
	@IdNegocio int,
	@Usuario varchar
AS
BEGIN
	IF(@IdStatus<=0)
		BEGIN
			insert into tblStatus (IdTipoStatus,Concepto,Estado)
			values (@IdTipoStatus,@Concepto,@Estado)
		END
	ELSE
		BEGIN
			update tblStatus set IdTipoStatus = @IdTipoStatus,
			Concepto = @Concepto,
			Estado = @Estado
			where IdStatus = @IdStatus
		END
END
