CREATE PROCEDURE [dbo].[spInsUpdRole]
	@IdRole int,
	@Nombre varchar(50),
	@Descripcion varchar(150),
	@idNegocio int=-1,
	@Usuario varchar(50),
	@Codigo varchar(10),
	@Activo bit = 1,
	@Anulado int=0
AS
Begin
if (@IdRole = 0)
	begin
		insert into tblRoles
			( Nombre, Descripcion)
		values
			( @Nombre, @Descripcion)

		EXEC
		(
			'SELECT IDENT_CURRENT(''tblRoles'') as last_id'
		)
	end
Else
	Begin
	update tblRoles
		set 
			Nombre=@Nombre,
			Descripcion = @Descripcion
		where IdRole = @IdRole
	End
End