CREATE PROCEDURE [dbo].[spInsUpdRedesSociales]
	@IdRedSocial int,
	@Nombre varchar(50),
	@IdNegocio int,
	@Usuario varchar(50),
	@Codigo varchar(10),
	@Activo bit = 1
AS
BEGIN 
IF (@IdRedSocial = 0 )
	begin 
		insert into tblRedesSociales
		(Nombre, IdNegocio, InsertadoPor, FechaInsertado)
		values
		(@Nombre, @IdNegocio, @Usuario, GETDATE())
	end
ELSE
	Begin
	update tblRedesSociales
		set 
		Nombre = @Nombre,
		IdNegocio = @IdNegocio,
		ModificadoPor = @Usuario,
		FechaModificado = GETDATE()
	where IdRedSocial = @IdRedSocial
	end
end

