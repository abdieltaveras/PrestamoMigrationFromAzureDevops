﻿CREATE PROCEDURE [dbo].[spGetTipos]
(
	@IdTipo int=-1,
	@IdClasificacion int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	--@Activo int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblTipos(nolock) 
	where 
		((@IdTipo=-1) or (IdTipo = @IdTipo))
		and ((@IdClasificacion=-1) or (IdClasificacion = @IdClasificacion))
		and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
End
