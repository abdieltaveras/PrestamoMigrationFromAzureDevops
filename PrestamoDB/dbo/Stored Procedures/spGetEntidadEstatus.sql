CREATE PROC [dbo].[spGetEntidadEstatus]
@Option int,
@IdEntidadEstatus int,
@Name varchar(50) = ''
as
IF(@Option = 1)
BEGIN
	SELECT top 30 * FROM tblEntidadEstatus
END
IF(@Option = 2)
BEGIN
	SELECT * FROM tblEntidadEstatus
	WHERE IdEntidadEstatus = @IdEntidadEstatus
END