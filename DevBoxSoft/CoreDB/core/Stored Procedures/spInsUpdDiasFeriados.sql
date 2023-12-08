create procedure [core].[spInsUpdDiasFeriados](@idDiaFeriado int=-1, @Dia date='01/01/1900', @Descripcion varchar(200))
as
begin
	 if(@idDiaFeriado < 1)
	 begin
		set @idDiaFeriado = ISNULL((select idDiaFeriado from tblDiasFeriados where Dia=@Dia),-1)
	 end
	 if(@Descripcion='')
	 begin
		delete from tblDiasFeriados where (idDiaFeriado = @idDiaFeriado)
		return
	 end
    if(@idDiaFeriado < 1)
    begin
	   INSERT INTO tblDiasFeriados(Dia, Descripcion) VALUES (@Dia, @Descripcion)
	   set @idDiaFeriado = @@identity
    end
    else
    begin
	   UPDATE tblDiasFeriados
		  SET /*Dia = @Dia,*/ Descripcion = @Descripcion
	   where (idDiaFeriado = @idDiaFeriado)
    end    
    select @idDiaFeriado as idDiaFeriado
end