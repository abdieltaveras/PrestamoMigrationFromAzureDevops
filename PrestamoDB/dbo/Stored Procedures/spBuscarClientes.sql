CREATE PROCEDURE [dbo].[spBuscarClientes]
(
	@TextToSearch varchar(50),
	@IdNegocio int=-1,
	@idLocalidadNegocio int=-1,
	@Usuario varchar(100) = '',
	@Borrado int=0,
	@cantidadRegistro int = 30,
	@NoIdentificacion varchar(20)='',
	@idTipoIdentificacion int= -1,
	@Nombres varchar(100)='',
	@Apellidos varchar(100)=''
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
	and ((@NoIdentificacion='') or (NoIdentificacion =@NoIdentificacion)) 
	and ((@Nombres='') or (Nombres like '%'+@Nombres+'%')) 
	and ((@Apellidos='') or (Apellidos like '%'+@Apellidos+'%')) 
End
