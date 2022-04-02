CREATE PROCEDURE [dbo].[spInsUpdPeriodo]
	@idnegocio int, 
	@IdLocalidadNegocio int ,
	@idPeriodo int,
	@idPeriodobase int,
	@PeriodoBase int,
	@codigo varchar(10), 
	@activo bit,
	@nombre varchar(50),
	@multiploperiodobase int =1,
	@requiereautorizacion bit,
	@usuario varchar(50)

AS
Begin
if (@idPeriodo <= 0)
	begin
		INSERT INTO dbo.tblPeriodos (Idnegocio, IdPeriodoBase, Codigo, Activo, Nombre, MultiploPeriodoBase, RequiereAutorizacion, InsertadoPor, FechaInsertado)
		VALUES (@idnegocio, --@idPeriodobase, 
		@PeriodoBase,
		@codigo, @activo, @nombre, @multiploperiodobase, @requiereautorizacion, @usuario, getdate()) 
	end
Else
	Begin
	UPDATE dbo.tblPeriodos
	SET 
		IdPeriodoBase = @IdPeriodobase,
		Codigo = @codigo,
		Activo = @activo,
		Nombre = @nombre,
		MultiploPeriodoBase = @multiploperiodobase,
		RequiereAutorizacion = @requiereautorizacion,
		ModificadoPor = @usuario,
		FechaModificado = getDate()
		where idPeriodo = @idPeriodo and BorradoPor is null
	End
End
