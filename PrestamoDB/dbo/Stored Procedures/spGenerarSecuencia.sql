CREATE procedure spGenerarSecuencia (@nombre varchar(100), @idNegocio int)
as
Begin
	if not exists(select 1 from tblSecuencias where nombre = @nombre )
	begin
		insert into tblSecuencias (nombre, idNegocio) values (@nombre, @IdNegocio)
	end
	update tblSecuencias set contador = contador +1
	select contador from tblSecuencias where nombre = @nombre
End
-- probando el generador de secuencia inicialmente
--exec spGenenerarSecuenciaNumerica @nombre='pruebaNumeroPrestamo',@idNegocio=1