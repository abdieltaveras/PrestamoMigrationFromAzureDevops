CREATE PROCEDURE [dbo].[spGetEquipos]
	@IdEquipo int,
	@Codigo varchar (40),
	@usuario varchar(40) ='',
	@Anulado bit=0,
	@Idnegocio int =-1
AS
SELECT IdEquipo, IdNegocio, Nombre, Descripcion, UltimoAcceso, AccesadoPor, FechaConfirmado, ConfirmadoPor, FechaBloqueado, BloqueadoPor, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado
FROM dbo.tblEquipos
where 
	(@IdEquipo <= -1 or IdEquipo = @idEquipo) and
	(@Codigo ='' or Codigo=@Codigo) and
	(IdNegocio in (select idNegocio from dbo.fnGetNegocioAndPadres(@IdNegocio)))

	

