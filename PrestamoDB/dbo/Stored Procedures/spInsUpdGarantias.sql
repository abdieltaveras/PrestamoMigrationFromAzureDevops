CREATE PROCEDURE [dbo].[spInsUpdGarantias]
	@IdGarantia int,
	@IdClasificacion int,
	@IdNegocio int,
	@IdTipoGarantia int,
	@IdModelo int,
	@IdMarca int,
	@NoIdentificacion varchar(100),
	@Detalles varchar(4000),
	@Imagen1FileName varchar(50),
	@Imagen2Filename varchar(50),
	@Imagen3FileName varchar(50),
	@Imagen4Filename varchar(50),
	@Usuario varchar(100)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdGarantia <= 0)
	begin
		insert into 
			tblGarantias
				(IdClasificacion, IdNegocio, IdTipoGarantia, IdModelo, IdMarca, NoIdentificacion, Detalles, Imagen1FileName,Imagen2FileName,Imagen3FileName,Imagen4FileName, InsertadoPor, FechaInsertado)
		values
				(@IdClasificacion, @IdNegocio, @IdTipoGarantia, @IdModelo, @IdMarca, @NoIdentificacion, @Detalles,@Imagen1FileName,@Imagen2Filename,@Imagen3FileName,@Imagen4Filename, @Usuario, GetDate())
	end
Else
	Begin
		update tblGarantias 
			set
				IdClasificacion = @IdClasificacion,
				IdNegocio = @IdNegocio,
				IdTipoGarantia = @IdTipoGarantia,
				IdMarca = @IdMarca,
				IdModelo = @IdModelo,
				NoIdentificacion = @NoIdentificacion,
				Detalles = @Detalles,
				Imagen1FileName = @Imagen1FileName,
				Imagen2FileName = @Imagen2Filename,
				Imagen3FileName = @Imagen3FileName,
				Imagen4FileName = @Imagen4Filename,
				ModificadoPor = @Usuario,
				FechaModificado = getdate()
			where 
				IdGarantia = @IdGarantia
	End
End
