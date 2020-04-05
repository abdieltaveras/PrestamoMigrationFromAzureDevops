﻿CREATE PROCEDURE [dbo].[spBuscarUsuarioRoles]
	@IdUsuario int,
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
as
BEGIN

	SELECT 
		IdUser, IdRole, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado 
	FROM 
		tblUsersRoles 
	WHERE 
		IdUser = @IdUsuario AND AnuladoPor IS NULL;

End