CREATE proc [dbo].[spAddEstatusToCliente]
@IdClienteEstatus int= 0,
@IdCliente int,
@IdEstatus int
as
BEGIN
	UPDATE tblClienteEstatus set
	IdEstatus = @IdEstatus
	WHERE IdCliente = @IdCliente
	--SELECT SCOPE_IDENTITY()
END