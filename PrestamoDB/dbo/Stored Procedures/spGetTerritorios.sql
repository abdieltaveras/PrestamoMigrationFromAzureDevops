CREATE PROCEDURE [dbo].[spGetTerritorios]
(
	@IdTipoLocalidad int=-1,
	@IdNegocio int=-1,
	@HijoDe  int=-1,
	@Descripcion varchar(50)=''
)
as
begin

select 
	t.*, m.Descripcion as NombreTipoHijoDe
	from 
	tblTipoLocalidades t
	left JOIN tblTipoLocalidades m ON m.IdTipoLocalidad = t.HijoDe
	where 
		((@IdTipoLocalidad=-1) or (t.IdTipoLocalidad = @IdTipoLocalidad))
		and ((@IdNegocio=-1) or (t.IdNegocio = @IdNegocio))
		and ((@HijoDe=-1) or (t.HijoDe = @HijoDe))
		and ((@Descripcion='') or (t.Descripcion=@Descripcion))	
	order by t.HijoDe asc
End
