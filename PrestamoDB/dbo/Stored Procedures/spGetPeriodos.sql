CREATE PROCEDURE [dbo].[spGetPeriodos]
(
	@idPeriodo int=-1,
	@idNegocio int=-1,
	@Codigo varchar(10)='',
	@Activo int=1,
	@RequiereAutorizacion int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT idPeriodo, Codigo, Nombre, Activo, RequiereAutorizacion, MultiploPeriodoBase, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado
	FROM dbo.tblPeriodos(nolock) 
	where 
		((@idPeriodo=-1) or (idPeriodo = @IdPeriodo))
		and (IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))
		and ((@Codigo='') or (Codigo = @Codigo))
		and ((@Activo=-1) or (Activo=@Activo))
		and ((@RequiereAutorizacion=-1) or (RequiereAutorizacion = @RequiereAutorizacion))
End
