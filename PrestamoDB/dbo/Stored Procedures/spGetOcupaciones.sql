CREATE PROCEDURE [dbo].[spGetOcupaciones]
	@IdOcupacion int=-1,
	@IdNegocio int=-1,
	@Borrado int=0,
	@IdLocalidadNegocio int = -1,
	@Usuario varchar(100)='',
	@condicionBorrado int = 0 
AS
SELECT * from tblOcupaciones
	where 
		((@IdOcupacion=-1) or (IdOcupacion = @IdOcupacion))
			and ((@condicionBorrado= 0 and BorradoPor is null) 
		or (@condicionBorrado=1 and BorradoPor is not null)
		or (@condicionBorrado=-1))