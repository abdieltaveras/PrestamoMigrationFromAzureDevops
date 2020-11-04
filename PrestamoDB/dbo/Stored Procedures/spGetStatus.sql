CREATE PROCEDURE [dbo].[spGetStatus]
	@IdStatus int,
	@IdTipoStatus int,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
AS
	SELECT * from tblStatus
	where IdStatus = @IdStatus or IdTipoStatus = @IdTipoStatus and Estado = 1
