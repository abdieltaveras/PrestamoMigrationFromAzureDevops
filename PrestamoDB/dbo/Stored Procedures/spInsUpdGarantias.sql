CREATE PROCEDURE [dbo].[spInsUpdGarantias]
	@IdGarantia int,
	@IdClasificacion int,
	@IdNegocio int,
	@IdTipo int,
	@IdModelo int,
	@NoIdentificacion varchar(100),
	@Detalles varchar(4000)
AS
Begin
 --verificar si id es 0 inserta si es diferente modificar
if (@IdGarantia = 0)
	begin
		insert into 
			tblGarantias
				(IdClasificacion, IdNegocio, IdTipo, IdModelo, NoIdentificacion, Detalles)
		values
			(@IdClasificacion, @IdNegocio, @IdTipo, @IdModelo, @NoIdentificacion, @Detalles)
	end
Else
	Begin
		update tblGarantias 
			set
				IdClasificacion = @IdClasificacion,
				IdNegocio = @IdNegocio,
				IdTipo = @IdTipo,
				IdModelo = @IdModelo,
				NoIdentificacion = @NoIdentificacion,
				Detalles = @Detalles
			where 
				IdGarantia = @IdGarantia
	End
End
