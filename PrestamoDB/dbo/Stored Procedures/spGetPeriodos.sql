CREATE PROCEDURE [dbo].[spGetPeriodos]
(
	@idPeriodo int=-1,
	@idNegocio int=-1,
	@idLocalidadNegocio int=-1,
	@Codigo varchar(10)='',
	@Activo int=1,
	@RequiereAutorizacion int=-1,
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0 
)
as
begin
	SELECT idPeriodo, Codigo, Nombre, Activo, RequiereAutorizacion, IdPeriodoBase, MultiploPeriodoBase,idLocalidadNegocio, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado
	FROM dbo.tblPeriodos(nolock) 
	where 
		((@idPeriodo=-1) or (idPeriodo = @IdPeriodo))
		--and (IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))
		and ((@Codigo='') or (Codigo = @Codigo))
		and ((@Activo=-1) or (Activo=@Activo))
		and ((@RequiereAutorizacion=-1) or (RequiereAutorizacion = @RequiereAutorizacion))
		and ((@condicionBorrado= 0 and BorradoPor is null) 
		or (@condicionBorrado=1 and BorradoPor is not null)
		or (@condicionBorrado=-1))
End
