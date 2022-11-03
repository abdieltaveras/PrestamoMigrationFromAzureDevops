CREATE PROC spGetLocalizadores
@IdLocalizador int = -1
as
BEGIN
	SELECT * FROM tblLocalizadores
	WHERE (@IdLocalizador =-1 or IdLocalizador = @IdLocalizador)
END