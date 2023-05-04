CREATE PROCEDURE [dbo].[spBuscarPrestamosByCliente]
(
	@TextToSearch varchar(50),
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Borrado int=0
)
as
begin

select 
	top 10
	tblPrestamos.IdPrestamo ,
	tblPrestamos.MontoPrestado as ids,
	tblPrestamos.prestamoNumero,
	tblPrestamos.MontoPrestado,
	tblClasificaciones.Nombre as Clasificacion,
	tblClientes.Nombres,
	tblClientes.Apellidos,
	tblTipoSexos.Nombre as Sexo
from 
	tblPrestamos, tblClientes, tblClasificaciones, tblTipoSexos
where 
	tblClientes.IdCliente = tblPrestamos.idCliente  and
	tblClasificaciones.IdClasificacion = tblPrestamos.idClasificacion  and
	tblTipoSexos.IdTipoSexo = tblClientes.IdSexo and (
	CONCAT(tblClientes.Nombres, ' ', tblClientes.Apellidos) LIKE '%' + @TextToSearch + '%')

End



