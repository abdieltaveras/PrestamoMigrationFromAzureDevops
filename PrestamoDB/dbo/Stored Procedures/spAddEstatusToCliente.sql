﻿--CREATE proc spAddEstatusToCliente
--@IdCliente int,
--@IdEstatus int
--as
--BEGIN
--	UPDATE tblClientes set
--	IdEstatus = @IdEstatus
--	WHERE IdCliente = @IdCliente
--	--SELECT SCOPE_IDENTITY()
--END