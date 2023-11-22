
CREATE procedure [core].[spGetDiasFeriados](@idDiaFeriado int = -1, @Dia datetime ='01/01/1900', @Ano int = -1, @desdeDia datetime = '01/01/2000', @hastaDia datetime = '01/01/2030')
as
begin
    SELECT idDiaFeriado, Dia, Descripcion
    FROM   tblDiasFeriados WITH (nolock)
    where
    (
	   ((@idDiaFeriado < 1) or(idDiaFeriado = @idDiaFeriado))
	   and
	   ((@Dia < '01/01/2000') or(Dia = @Dia))
	   and
	   ((@Ano < 1) or(Year(Dia) = @Ano ))
	   and
	   (Dia between @desdeDia and @hastaDia)
    )
    order by dia
end