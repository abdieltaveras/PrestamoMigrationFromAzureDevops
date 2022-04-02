CREATE PROCEDURE [dbo].[spGetStatus]
	@IdStatus int,
	@IdTipoStatus int,
	@IdNegocio int=-1,
	@Borrado int=0,
	@Usuario varchar(100)=''
AS
BEGIN
	IF(@IdTipoStatus > 0)
		BEGIN
			SELECT * from tblStatus
			where (IdTipoStatus = @IdTipoStatus and Estado = 1)
		END
	ELSE
		BEGIN
			SELECT * from tblStatus
			where ((@IdStatus=-1) or IdStatus=@IdStatus and Estado = 1)
		END
END
