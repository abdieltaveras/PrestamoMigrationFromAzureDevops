﻿CREATE PROCEDURE [dbo].[spGetClientes]
(
	@idCliente int,
	@idNegocio int=-1,
	@Codigo varchar(100)='',
	@Activo int=-1,
	@IdLocalidadNegocio int=-1,
	@IdLocalidad int=-1,
	@Anulado int=0,
	@NoIdentificacion varchar(20)='',
	@idTipoIdentificacion int= -1,
	@Nombres varchar(100)='',
	@Apellidos varchar(100)='',
	@InsertadoDesde DateTime=null,
	@InsertadoHasta DateTime=null,
    @CantidadRegistrosASeleccionar int = 100,
    @SeleccionarLuegoDelIdCliente  int = -1,
  	@Usuario varchar(100)=''
)
as
begin

    if (@CantidadRegistrosASeleccionar=-1)
	begin
		set @CantidadRegistrosASeleccionar=99999999
	end


	SELECT TOP (@CantidadRegistrosASeleccionar)    IdCliente,IdStatus, Codigo,Activo, AnuladoPor, Apodo, Apellidos, EstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, Sexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, Imagen1FileName, Imagen2FileName, TieneConyuge,infoReferencia
	FROM dbo.tblClientes(nolock) 
	where 
		((@idCliente=-1) or (IdCliente = @IdCliente))
		and ((@Codigo='') or (Codigo = @Codigo))
		and ((@Activo=-1) or (Activo=@Activo)) 
		and ((@idTipoIdentificacion=-1) or (idTipoIdentificacion =@idTipoIdentificacion)) 
		and ((@NoIdentificacion='') or (NoIdentificacion =@NoIdentificacion)) 
		and ((@Nombres='') or (Nombres like '%'+@Nombres+'%')) 
		and ((@Apellidos='') or (Apellidos like '%'+@Apellidos+'%')) 
		and (@IdLocalidadNegocio =1 or idLocalidadNegocio=@idLocalidadNegocio)
		and (@SeleccionarLuegoDelIdCliente =-1 or idCliente > @SeleccionarLuegoDelIdCliente)
End
