CREATE procedure [dbo].[spGenerarSecuenciaString] (@nombre varchar(100), @digitos int,  @idNegocio int=-1, @secuenciaSt varchar(20) output)
-- @digitos, este parametro se le suministra la cantidad de digitos que es el codigo
-- si es de 10 digito toma el numero y genera los digitos faltantes con 0 
-- ejemplo la secuenci 55 genera el codigo 0000000055
-- tambien se va a colocar un prefijo del negocio
-- ejemplo IRO0000000001 Para intagsa romana PBA0000000252 
-- los prefijos seran de 3 digitos y unicos dentro de una federacion
as
Begin
	if not exists(select 1 from tblSecuencias where nombre = @nombre) 
	begin
		insert into tblSecuencias (nombre, idNegocio) values (@nombre, @idNegocio)
	end
	update tblSecuencias  set contador = contador +1
	
	declare @secuencia int  = (select contador from tblSecuencias where nombre = @nombre)
	set @secuenciaSt = (select Replace(Str(@secuencia, @digitos), ' ', '0'))
End

-- vea en el store procesude spInsUpdTest1GenerarSecuencia ejemplos de como usarlo
-- correctamente