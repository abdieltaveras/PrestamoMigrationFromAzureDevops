CREATE PROCEDURE [dbo].[spGetTasasInteres]
(
	@idTasaInteres int=-1,
	@idNegocio int=-1,
	@Codigo varchar(10)='',
	@InteresMensual decimal (9,6)=-1,
	@Activo int=-1,
	@RequiereAutorizacion int=-1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT idTasaInteres, Codigo, Nombre, InteresMensual, Activo, RequiereAutorizacion, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado
	FROM dbo.tblTasasInteres(nolock) 
	where 
		((@idTasaInteres=-1) or (idTasaInteres = @IdTasaInteres))
		and ((@idNegocio=-1) or (idNegocio = @idNegocio))
		and ((@Codigo='') or (Codigo = @Codigo))
		and ((@InteresMensual =-1) or (InteresMensual=@InteresMensual))
		and ((@Activo=-1) or (Activo=@Activo))
		and ((@RequiereAutorizacion=-1) or (RequiereAutorizacion = @RequiereAutorizacion))
End
