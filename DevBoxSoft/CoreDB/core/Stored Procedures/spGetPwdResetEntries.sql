CREATE procedure core.spGetPwdResetEntries (@id uniqueidentifier=null, @userID uniqueidentifier=null) as
begin
	SELECT [ID],[UserID],[IssuedDT],[ValidUntilDT],[ResetDT]
	FROM [core].[tblPwdReset]
	WHERE ((@id is null) or (id = @id))
		and ((@userID is null) or (userID = @userID))
end