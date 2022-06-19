CREATE PROCEDURE [dbo].[spGetEquipos]
	@IdEquipo int,
	@Codigo varchar (40)='',
	@usuario varchar(40) ='',
	@Borrado bit=0,
	@Idnegocio int =-1,
	@IdLocalidadNegocio int = -1,
	@condicionBorrado int = 0
AS
SELECT IdEquipo, IdNegocio, Codigo, Nombre, Descripcion, UltimoAcceso, AccesadoPor, FechaConfirmado, ConfirmadoPor, FechaBloqueado, BloqueadoPor, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado
FROM dbo.tblEquipos
where 
	(@IdEquipo <= -1 or IdEquipo = @idEquipo) and
	(@Codigo ='' or Codigo=@Codigo)
	and ((@condicionBorrado= 0 and BorradoPor is null) 
	or (@condicionBorrado=1 and BorradoPor is not null)
	or (@condicionBorrado=-1))
	-- and (IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))

	

