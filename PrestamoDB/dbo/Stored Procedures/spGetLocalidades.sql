create PROCEDURE [dbo].[spGetLocalidades]
(
	@idlocalidad int,
	@IdNegocio int=-1,
    @IdLocalidadNegocio int = -1,
	@Anulado int=0,
	@Usuario varchar(100)=''
)
as
begin	
	SELECT
        loc.IdLocalidad,
        loc.Nombre,
		loc.IdTipoLocalidad,
        loc.IdLocalidadPadre,
		tipo.Nombre as Descripcion
    FROM
        tblLocalidades loc 
	JOIN tblTipoLocalidades tipo ON loc.IdTipoLocalidad = tipo.IdTipoLocalidad
    WHERE (@idlocalidad<=0 or   loc.IdLocalidad = @idlocalidad)
	
end