CREATE PROCEDURE [dbo].[spBuscarClientes]
(
	@TextToSearch varchar(50),
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
)
as
begin

select 
	top 10
	tblClientes.IdCliente,
	tblClientes.Nombres,
	tblClientes.Apellidos,
	tblClientes.Imagen1FileName,
	tblClientes.NoIdentificacion,
	tblClientes.TelefonoMovil,
	tblClientes.Activo,
	tblClientes.IdSexo

from 
	tblClientes, tblTipoSexos
where
	tblClientes.IdNegocio = @IdNegocio AND
	tblTipoSexos.IdTipoSexo = tblClientes.IdSexo AND (
	CONCAT(tblClientes.Nombres, ' ', tblClientes.Apellidos) LIKE '%' + @TextToSearch + '%' 
	OR tblClientes.NoIdentificacion LIKE '%' + @TextToSearch + '%')
End
