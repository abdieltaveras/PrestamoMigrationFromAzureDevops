CREATE PROCEDURE [dbo].[spBuscarPrestamosByCliente]
(
	@TextToSearch varchar(50),
	@IdNegocio int,
	@Usuario varchar(100) = '',
	@Anulado int=0
)
as
begin

select 
	tblPrestamos.IdPrestamo ,
	tblPrestamos.MontoPrestado as ids,
	tblPrestamos.prestamoNumero,
	tblPrestamos.MontoPrestado,
	tblClasificaciones.Nombre as Clasificacion,
	tblClientes.Nombres,
	tblClientes.Apellidos,
	tblClientes.Imagen1FileName as FotoCliente,
	tblTipoSexos.Nombre as Sexo
from 
	tblPrestamos, tblClientes, tblClasificaciones, tblTipoSexos
where 
	tblClientes.IdCliente = tblPrestamos.idCliente  and
	tblClasificaciones.IdClasificacion = tblPrestamos.idClasificacion  and
	tblTipoSexos.IdTipoSexo = tblClientes.Sexo and (
	tblClientes.Nombres LIKE '%' + @TextToSearch + '%' or
	tblClientes.Apellidos LIKE '%' + @TextToSearch + '%')

End



