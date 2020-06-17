CREATE PROCEDURE [dbo].[spInsUpdPeriodo]
	@idnegocio int, 
	@idPeriodo int,
	@periodobase int,
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
	
		INSERT INTO dbo.tblPeriodos (Idnegocio, PeriodoBase, Codigo, Activo, Nombre, MultiploPeriodoBase, RequiereAutorizacion, InsertadoPor, FechaInsertado)
		VALUES (@idnegocio, @periodobase, @codigo, @activo, @nombre, @multiploperiodobase, @requiereautorizacion, @usuario, getdate()) 
	end
Else
	Begin
	UPDATE dbo.tblPeriodos
	SET 
		PeriodoBase = @periodobase,
		Codigo = @codigo,
		Activo = @activo,
		Nombre = @nombre,
		MultiploPeriodoBase = @multiploperiodobase,
		RequiereAutorizacion = @requiereautorizacion,
		ModificadoPor = @usuario,
		FechaModificado = getDate()
		where idPeriodo = @idPeriodo
	End
End
