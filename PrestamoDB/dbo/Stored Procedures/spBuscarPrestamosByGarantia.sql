CREATE PROCEDURE [dbo].[spBuscarPrestamosByGarantia]
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
	tblGarantias.IdGarantia,
	tblPrestamos.MontoPrestado,
	tblClasificaciones.Nombre as Clasificacion,
	tblClientes.Nombres,
	tblClientes.Apellidos,
	tblTipoSexos.Nombre as Sexo,
	tblGarantias.NoIdentificacion
from 
	tblPrestamos, tblGarantias, tblClientes, tblClasificaciones, tblTipoSexos, tblPrestamoGarantias
where 
	tblPrestamoGarantias.IdPrestamo = tblPrestamos.IdPrestamo and
	tblClientes.IdCliente = tblPrestamos.idCliente  and
	tblClasificaciones.IdClasificacion = tblPrestamos.idClasificacion  and
	tblTipoSexos.IdTipoSexo = tblClientes.IdSexo and
	tblGarantias.IdGarantia = tblPrestamoGarantias.IdGarantia and (
	tblGarantias.NoIdentificacion LIKE '%' + @TextToSearch + '%')

End



