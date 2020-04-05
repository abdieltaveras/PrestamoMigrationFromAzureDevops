CREATE PROCEDURE [dbo].[RoleOperacionesSpGet]
(
	@IdRole int=-1
)
as
begin
	SELECT 
		IdRole, IdOperacion, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado
	FROM 
		tblRolesOperaciones 
	WHERE 
		IdRole = @IdRole
End
