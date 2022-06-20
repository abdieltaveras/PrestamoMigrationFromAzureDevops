CREATE PROCEDURE [dbo].[spGetTasasInteres]
(
	@idTasaInteres int=-1,
	@idNegocio int=-1,
	@idLocalidadNegocio int=-1,
	@Codigo varchar(10)='',
	@InteresMensual decimal (9,6)=-1,
	@Activo int=-1,
	@RequiereAutorizacion int=-1,
	@Borrado int=0,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0 

)
as
begin
	SELECT idTasaInteres, Codigo, Nombre, InteresMensual, Activo, RequiereAutorizacion, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado
	FROM dbo.tblTasasInteres(nolock) 
	where 
		((@idTasaInteres=-1) or (idTasaInteres = @IdTasaInteres))
		--and (IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))
		and ((@Codigo='') or (Codigo = @Codigo))
		and ((@InteresMensual =-1) or (InteresMensual=@InteresMensual))
		and ((@Activo=-1) or (Activo=@Activo))
		--and ((@idLocalidadNegocio=-1) or (idLocalidadNegocio=@idLocalidadNegocio))
		and ((@RequiereAutorizacion=-1) or (RequiereAutorizacion = @RequiereAutorizacion))
			and ((@condicionBorrado= 0 and BorradoPor is null) 
		or (@condicionBorrado=1 and BorradoPor is not null)
		or (@condicionBorrado=-1))
End
