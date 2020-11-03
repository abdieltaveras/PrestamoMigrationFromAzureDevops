CREATE PROCEDURE [dbo].[spGetStatus]
	@IdStatus int,
	@IdTipo int
AS
	SELECT * from tblStatus
	where IdStatus = @IdStatus or @IdTipo = @IdTipo and Estado = 1
