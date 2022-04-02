CREATE PROCEDURE [dbo].[RoleOperacionesSpGet]
(
	@IdRole int=-1
)
as
begin
	SELECT 
		IdRole, IdOperacion, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado
	FROM 
		tblRolesOperaciones 
	WHERE 
		IdRole = @IdRole
End
