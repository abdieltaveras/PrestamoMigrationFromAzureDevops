﻿CREATE PROCEDURE [dbo].[spGetTiposGarantia]
(
	@IdTipoGarantia int=-1,
	@IdClasificacion int=-1,
	@IdNegocio int,
	@Anulado int=0,
	--@Activo int=0,
	@Usuario varchar(100)=''
)
as
begin
	SELECT *
	FROM dbo.tblTiposGarantia(nolock)
	where
		((@IdTipoGarantia=-1) or (IdTipoGarantia = @IdTipoGarantia))
		and ((@IdClasificacion=-1) or (IdClasificacion = @IdClasificacion))
		--and (IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))
End
