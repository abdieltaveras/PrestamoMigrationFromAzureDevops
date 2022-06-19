CREATE PROCEDURE [dbo].[spGetClasificaciones]
	@IdClasificacion int=-1,
	@IdNegocio int =-1,
	@IdLocalidadNegocio int = -1,
	@Nombre varchar(50)='',
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0
AS
	SELECT * from tblClasificaciones
	where 
		((@IdClasificacion=-1) or (IdClasificacion = @IdClasificacion))
		--and (ma.IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))
		and ((@Nombre='') or (Nombre like @Nombre + '%'))
				and ((@condicionBorrado= 0 and BorradoPor is null) 
			or (@condicionBorrado=1 and BorradoPor is not null)
			or (@condicionBorrado=-1))
