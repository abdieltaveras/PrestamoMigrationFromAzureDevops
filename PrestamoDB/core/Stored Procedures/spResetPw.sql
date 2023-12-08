
CREATE procedure [core].[spResetPw](@ID uniqueidentifier, @NewPassword VARCHAR(255))as
begin
	declare @userId uniqueidentifier, @dt datetime = getdate()
	set @userId = (select top 1 [UserID] 
				   from core.tblPwdReset 
				   where id = @id and 
						 @dt between issueddt and validuntildt)
	if(@userId is null)
	begin
		RAISERROR ('Solicitud de cambio inválida', 16, 1);  
		return
	end
	else
	begin
		declare @salt UNIQUEIDENTIFIER = NEWID()
		DECLARE @hpass BINARY(64) = HASHBYTES('SHA2_512', @NewPassword+CAST(@salt AS NVARCHAR(36)))

		Update [core].[tblUsers]
			set PasswordHash = @hpass, 
				PassSlt = @salt,
				LastModifiedOn=GETDATE(),
				LastModifiedBy='ResetPW',
				MustChangePassword=0
		where [UserID]  = @userid 
			  and isnull(isActive,  0) = 1
			  and isnull(isDeleted, 0) = 0
	end
end