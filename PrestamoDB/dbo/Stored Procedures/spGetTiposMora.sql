CREATE PROCEDURE [dbo].[spGetTiposMora]
(
	@idTipoMora int=-1,
	@idNegocio int,
		@IdLocalidadNegocio int = -1,
	@Codigo varchar(10)='',
	@Activo int=-1,
	@RequiereAutorizacion int=-1,
	@Borrado int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblTiposMora(nolock) 
	where 
		((@idTipoMora=-1) or (idTipoMora = @idTipoMora))
		--and (IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))
		and ((@Codigo='') or (Codigo = @Codigo))
		and ((@Activo=-1) or (Activo=@Activo))
		and ((@RequiereAutorizacion=-1) or (RequiereAutorizacion = @RequiereAutorizacion))
End
