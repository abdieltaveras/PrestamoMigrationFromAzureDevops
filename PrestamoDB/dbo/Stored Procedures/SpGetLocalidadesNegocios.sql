CREATE PROCEDURE [dbo].[SpGetLocalidadesNegocio]
@IdLocalidadNegocio int = -1,
@IdNegocio int = -1,
@PermitirOperaciones int = -1,
@SearchText varchar(50) = '',
@Opcion int =-1,
@Activo int=-1,
@IdLocalidad int=-1,
@Borrado int=0,
@Usuario varchar(50)
AS
BEGIN
	IF(@Opcion = 1)
	BEGIN
		SELECT * FROM tblLocalidadesNegocio WITH(NOLOCK)
	END
	IF(@Opcion = 2)
	BEGIN
		SELECT * FROM tblLocalidadesNegocio WITH(NOLOCK)
		WHERE IdLocalidadNegocio = @IdLocalidadNegocio
	END
	IF(@Opcion = 3)
	BEGIN
		SELECT * FROM tblLocalidadesNegocio WITH(NOLOCK)
		WHERE NombreComercial like @SearchText + '%'
	END
	
END
