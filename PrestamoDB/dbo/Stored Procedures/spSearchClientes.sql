CREATE PROCEDURE [dbo].[spSearchClientes]
(
	@TextToSearch varchar(50) = '',
	@IdNegocio int=-1,
	@idLocalidadNegocio int=-1,
	@Usuario varchar(100) = '',
	@Borrado int=0,
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
	--((@Codigo='') or (Codigo = @Codigo))
	CONCAT(tblClientes.Nombres, ' ', tblClientes.Apellidos) LIKE '%' + @TextToSearch + '%' 
	--OR tblClientes.NoIdentificacion LIKE '%' + @TextToSearch + '%'
	and ((@TextToSearch='') or (NoIdentificacion =@TextToSearch)) 
	and ((@TextToSearch='') or (Nombres like '%'+@TextToSearch+'%')) 
	and ((@TextToSearch='') or (Apellidos like '%'+@TextToSearch+'%')) 
End