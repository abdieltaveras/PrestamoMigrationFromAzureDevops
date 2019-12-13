CREATE PROCEDURE [dbo].[spGetTerritorios]
(
	@IdTipoLocalidad int=-1,
	@IdNegocio int=-1,
	@PadreDe  int=-1,
	@Descripcion varchar(50)=''
)
as
begin

select 
	t.*, m.Descripcion as HijoDe 
	from 
	tblTipoLocalidad t
	left JOIN tblTipoLocalidad m ON m.IdTipoLocalidad = t.PadreDe
	where 
		((@IdTipoLocalidad=-1) or (t.IdTipoLocalidad = @IdTipoLocalidad))
		and ((@IdNegocio=-1) or (t.IdNegocio = @IdNegocio))
		and ((@PadreDe=-1) or (t.PadreDe = @PadreDe))
		and ((@Descripcion='') or (t.Descripcion=@Descripcion))	
	order by t.PadreDe asc
End
