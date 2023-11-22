
CREATE procedure [core].[spSetPwReset](@UserID uniqueidentifier, @forced bit = 0)as
begin
	if(@forced = 1)
	begin
		update core.tblUsers
			set MustChangePassword = 1
		where UserID = @UserID
	end
	else
	begin
		declare @id uniqueidentifier = NEWID()	
		insert into core.tblPwdReset(id, userid, issueddt, validuntildt) 
				values(@id, @userid, getdate(), dateadd(Hour, 1, getdate()))
		--select * from core.tblPwdReset where id = @id
		exec core.spGetPwdResetEntries @id
	end
end