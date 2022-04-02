CREATE PROCEDURE [dbo].[spBuscarPrestamos]
(
	@TextToSearch varchar(50),
	@IdNegocio int=-1,
	@Usuario varchar(100) = '',
	@Borrado int=0
)
as
begin

select
	top 10
	tblPrestamos.IdPrestamo,
	tblPrestamos.MontoPrestado,
	tblPrestamos.prestamoNumero,
	tblClasificaciones.Nombre as Clasificacion,
	tblClientes.Nombres,
	tblClientes.Apellidos,
	tblClientes.Imagen1FileName as FotoCliente,
	tblTipoSexos.Nombre as Sexo
from
	tblPrestamos
join 
	tblClientes on tblPrestamos.idCliente = tblClientes.IdCliente
join 
	tblClasificaciones on tblPrestamos.idClasificacion = tblClasificaciones.IdClasificacion
join 
	tblTipoSexos on tblClientes.IdSexo = tblTipoSexos.IdTipoSexo
where 
	tblPrestamos.prestamoNumero LIKE '%' + @TextToSearch + '%';

End
