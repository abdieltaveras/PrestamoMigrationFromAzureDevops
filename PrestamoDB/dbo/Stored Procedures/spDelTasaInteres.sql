﻿--Este procedimiento almacenado se usa para marcar como Borrado 
--una tasa de interes e indicar por quien y cuando fue realizada 
--esta accion.

CREATE PROCEDURE [dbo].[spDelTasaInteres]
	@id int,
	@Usuario varchar(100)
AS
	begin
	update tblTasasInteres
		SET
			Activo = 0,
			BorradoPor = @Usuario,
			FechaBorrado = getdate()
		WHERE 
			idTasaInteres = @id	
End


