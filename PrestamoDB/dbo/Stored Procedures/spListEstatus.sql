CREATE PROC [dbo].[spListEstatus]
@IdEstatus int = -1,
@Name varchar(50) = ''
as
SELECT * FROM tblEstatus