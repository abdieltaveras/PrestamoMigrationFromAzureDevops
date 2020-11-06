CREATE PROCEDURE [dbo].[spGetStatus]
	@IdStatus int,
	@IdTipoStatus int,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
AS
	SELECT * from tblStatus
	where ((@IdStatus=-1) or IdStatus=@IdStatus and Estado = 1)
