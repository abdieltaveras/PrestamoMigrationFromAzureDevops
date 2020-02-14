CREATE PROCEDURE [dbo].[spInsUpdGarantias]
	@IdGarantia int,
	@IdClasificacion int,
	@IdNegocio int,
	@IdTipoGarantia int,
	@IdModelo int,
	@IdMarca int,
	@NoIdentificacion varchar(100),
	@Detalles varchar(4000),
	@Usuario varchar(100)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdGarantia <= 0)
	begin
		insert into 
			tblGarantias
				(IdClasificacion, IdNegocio, IdTipoGarantia, IdModelo, IdMarca, NoIdentificacion, Detalles, InsertadoPor, FechaInsertado)
		values
				(@IdClasificacion, @IdNegocio, @IdTipoGarantia, @IdModelo, @IdMarca, @NoIdentificacion, @Detalles, @Usuario, GetDate())
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
				ModificadoPor = @Usuario,
				FechaModificado = getdate()
			where 
				IdGarantia = @IdGarantia
	End
End
