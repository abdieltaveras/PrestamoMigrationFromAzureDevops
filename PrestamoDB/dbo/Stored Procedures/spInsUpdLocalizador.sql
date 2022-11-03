CREATE PROC spInsUpdLocalizador
@IdLocalizador int
,@IdLocalidadNegocio int
, @IdNegocio	   int
, @Codigo		   VARCHAR(50) = ''
, @Nombre		   VARCHAR(50)
, @Apellidos	   VARCHAR(100)
, @Direccion	   VARCHAR(1000)
, @Telefonos	   VARCHAR(400)
, @Activo		   bit = 1
, @Usuario	   VARCHAR(100)=''
as
IF(@IdLocalizador = 0)
BEGIN
	INSERT INTO tblLocalizadores (IdLocalidadNegocio,IdNegocio,Codigo,Nombre,Apellidos,Direccion,Telefonos,InsertadoPor,FechaInsertado, Activo)
	VALUES (@IdLocalidadNegocio,@IdNegocio,@Codigo,@Nombre,@Apellidos,@Direccion,@Telefonos,@Usuario,SYSDATETIME(),1)
END
ELSE
BEGIN
	UPDATE tblLocalizadores SET
	Codigo = @Codigo,
	Nombre = @Nombre,
	Apellidos = @Apellidos,
	Direccion = @Direccion,
	Telefonos= @Telefonos,
	ModificadoPor = @Usuario,
	FechaModificado = SYSDATETIME()
	WHERE IdLocalizador = @IdLocalizador
END