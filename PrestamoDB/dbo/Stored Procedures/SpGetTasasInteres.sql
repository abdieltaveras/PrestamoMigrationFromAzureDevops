CREATE PROCEDURE [dbo].[SpGetTasasInteres]
(
	@idTasaInteres int=-1,
	@idNegocio int=-1,
	@Codigo varchar(10)='',
	@InteresMensual decimal (9,6)=-1,
	@Activo int=-1,
	@RequiereAutorizacion int=-1,
	@Borrado int=0
)
as
begin
	SELECT idTasaInteres, Codigo, InteresMensual, Activo, RequiereAutorizacion, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado
	FROM dbo.tblTasaInteres(nolock) 
	where 
		((@idTasaInteres=-1) or (idTasaInteres = @IdTasaInteres))
		and ((@idNegocio=-1) or (idNegocio = @idNegocio))
		and ((@Codigo='') or (Codigo = @Codigo))
		and ((@InteresMensual =-1) or (InteresMensual=@InteresMensual))
		and ((@Activo=-1) or (Activo=@Activo))
		and ((@RequiereAutorizacion=-1) or (RequiereAutorizacion = @RequiereAutorizacion))
End
