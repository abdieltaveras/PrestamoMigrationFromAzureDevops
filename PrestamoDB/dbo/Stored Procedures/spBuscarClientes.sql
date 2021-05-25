create PROCEDURE [dbo].[spBuscarClientes]
(
	@TextToSearch varchar(50),
	@IdNegocio int=-1,
	@idLocalidadNegocio int=-1,
	@Usuario varchar(100) = '',
	@Anulado int=0,
	@cantidadRegistro int = 30
)
as
begin
select 
	top (@cantidadRegistro)
	tblClientes.IdCliente,
	tblClientes.Nombres,
	tblClientes.Apellidos,
	tblClientes.Imagen1FileName,
	tblClientes.NoIdentificacion,
	tblClientes.TelefonoMovil,
	tblClientes.Activo,
	tblClientes.IdSexo
from 
	tblClientes
where
	CONCAT(tblClientes.Nombres, ' ', tblClientes.Apellidos) LIKE '%' + @TextToSearch + '%' 
	OR tblClientes.NoIdentificacion LIKE '%' + @TextToSearch + '%'
End
