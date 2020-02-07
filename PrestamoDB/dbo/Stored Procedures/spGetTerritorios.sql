CREATE PROCEDURE [dbo].[spGetTerritorios]
(
	@IdTipoLocalidad int=-1,
	@IdNegocio int=-1,
	@HijoDe  int=-1,
	@Nombre varchar(50)='',
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin

select 
	t.*, m.Nombre as NombreTipoHijoDe
	from 
	tblTipoLocalidades t
	left JOIN tblTipoLocalidades m ON m.IdTipoLocalidad = t.HijoDe
	where 
		((@IdTipoLocalidad=-1) or (t.IdTipoLocalidad = @IdTipoLocalidad))
		and ((@IdNegocio=-1) or (t.IdNegocio = @IdNegocio))
		and ((@HijoDe=-1) or (t.HijoDe = @HijoDe))
		and ((@Nombre='') or (t.Nombre=@Nombre))	
	order by t.HijoDe asc
End
