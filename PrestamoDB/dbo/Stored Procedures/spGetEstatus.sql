CREATE PROC  [dbo].[spGetEstatus] 
@IdEstatus int = -1,
@Name varchar(50) = ''
as
SELECT * FROM tblEstatus
WHERE (@IdEstatus  = -1 or IdEstatus = @IdEstatus)
and (@Name  = '' or Name = @Name)