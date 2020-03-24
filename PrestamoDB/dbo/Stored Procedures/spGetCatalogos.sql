CREATE PROCEDURE [dbo].[spGetCatalogos]
(
	@Id int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@Usuario varchar(100)='',
	@NombreTabla varchar(100) = '',
	@IdTabla varchar(100) = ''
)
as
begin
	EXEC('SELECT '+ @IdTabla +', idNegocio, Activo, Codigo, Nombre, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado from ' + @NombreTabla + '  where (('+@Id+'=-1) or ('+ @IdTabla +' = '+@Id+'))')
	--SELECT *
	--FROM dbo.tblOcupaciones(nolock) 
	--where 
	--	((@Id=-1) or (IdOcupacion = @Id))
	--	and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
End
