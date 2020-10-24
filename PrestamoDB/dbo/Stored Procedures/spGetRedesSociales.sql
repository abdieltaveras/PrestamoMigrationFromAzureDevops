CREATE PROCEDURE [dbo].[spGetRedesSociales]
	
AS

	begin
		SELECT * From tblRedesSociales(nolock)
	end

