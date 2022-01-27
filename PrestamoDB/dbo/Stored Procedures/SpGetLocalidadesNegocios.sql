CREATE PROCEDURE [dbo].[SpGetLocalidadesNegocio]
@IdLocalidadNegocio int = -1,
@SearchText varchar(50) = '',
@Opcion int
AS
BEGIN
	IF(@Opcion = 1)
	BEGIN
		SELECT * from tblLocalidadesNegocio with(nolock)
	END
	IF(@Opcion = 2)
	BEGIN
		SELECT * from tblLocalidadesNegocio with(nolock)
		WHERE IdLocalidadNegocio = @IdLocalidadNegocio
	END
	if(@Opcion = 3)
	BEGIN
		SELECT * FROM tblLocalidadesNegocio with(nolock)
		WHERE NombreComercial like @SearchText + '%'
	END
	if(@Opcion = 4)
	BEGIN
		SELECT * FROM tblLocalidadesNegocio with(nolock)
		WHERE NombreJuridico like @SearchText + '%'
	END
END
