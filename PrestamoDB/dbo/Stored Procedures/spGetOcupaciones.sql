CREATE PROCEDURE [dbo].[spGetOcupaciones]
	@IdOcupacion int=-1,
	@IdNegocio int=-1,
	@Anulado int=0,
	@IdLocalidadNegocio int = -1,
	@Usuario varchar(100)=''
AS
SELECT * from tblOcupaciones
	where 
		((@IdOcupacion=-1) or (IdOcupacion = @IdOcupacion))