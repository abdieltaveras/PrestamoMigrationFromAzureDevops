CREATE PROCEDURE [dbo].[spGetLocalidades]
(
	@idlocalidad int,
	@IdNegocio int=-1,
    @IdLocalidadNegocio int = -1,
	@IdLocalidadPadre int = -1,
	@Borrado int=0,
	@Usuario varchar(100)='',
	@IdDivisionTerritorial int = -1,
	@Anulado varchar(100) = '',
	@condicionBorrado int = 0
)
as
begin	
	SELECT
        loc.IdLocalidad,
        loc.Nombre,
		loc.IdDivisionTerritorial,
		tipo.IdDivisionTerritorialPadre as IdTipoDivisionTerritorial,
        loc.IdLocalidadPadre,
		tipo.Nombre as Descripcion
    FROM
        tblLocalidades loc 
	JOIN tblDivisionTerritorial tipo ON loc.IdDivisionTerritorial = tipo.IdDivisionTerritorial
    WHERE (@idlocalidad=-1 or   loc.IdLocalidad = @idlocalidad)
    and (@IdDivisionTerritorial=-1 or   loc.IdDivisionTerritorial = @IdDivisionTerritorial)

	--and ((@condicionBorrado= 0 and BorradoPor is null) 
	--or (@condicionBorrado=1 and BorradoPor is not null)
	--or (@condicionBorrado=-1))
	
end
GO

