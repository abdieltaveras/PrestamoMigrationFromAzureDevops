CREATE PROCEDURE [dbo].[spGetTerritorios]
(
	@IdTipoLocalidad int=-1,
	@IdNegocio int=-1,
	@PadreDe  int=-1,
	@Descripcion varchar(50)=''
)
as
begin
	SELECT *
	FROM dbo.tblTipoLocalidad(nolock) 
	where 
		((@IdTipoLocalidad=-1) or (IdTipoLocalidad = @IdTipoLocalidad))
		and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
		and ((@PadreDe=-1) or (PadreDe = @PadreDe))
		and ((@Descripcion='') or (Descripcion=@Descripcion))
End
