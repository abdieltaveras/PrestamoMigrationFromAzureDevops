create procedure [core].[spDelDiaFeriado](@idDiaFeriado int) as
begin
    delete from tblDiasFeriados where idDiaFeriado =@idDiaFeriado
end