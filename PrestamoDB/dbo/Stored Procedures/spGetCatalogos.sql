
create PROCEDURE [dbo].[spGetCatalogos]
(
	@Id int=-1,
	@IdNegocio int,
	@Anulado int=0,
	@Usuario varchar(100)='',
	@NombreTabla varchar(100),
	@IdTabla varchar(100)
)
as
begin 
	--if (@idNegocio=-1) 
	--begin 
	--	 RAISERROR ('Envio un valor menor o igual a cero para el parametro idNegocio, lo cual no es aceptado',
	--	11,1)
	--end 
	-- original instruction by bryan EXEC('SELECT '+ @IdTabla +', idNegocio, Activo, Codigo, Nombre, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado from ' + @NombreTabla + '  where (('+@Id+'=-1) or ('+ @IdTabla +' = '+@Id+'))')

		exec ('SELECT  * from ' + @NombreTabla + '  where (('+@Id+'=-1) 
		or ('+ @IdTabla +' = '+@Id+'))') 

		--exec ('SELECT '+ @IdTabla +', idNegocio, Activo, Codigo, Nombre, AnuladoPor  from ' + @NombreTabla + '  where (('+@Id+'=-1) or ('+ @IdTabla +' = '+@Id+')) and IdNegocio in (select idNegocio from fnGetNegocioAndPadres('+@IdNegocio+'))')
	--SELECT *
	--FROM dbo.tblOcupaciones(nolock) 
	--where 
	--	((@Id=-1) or (IdOcupacion = @Id))
	--	and ((@IdNegocio=-1) or (IdNegocio = @IdNegocio))
End

