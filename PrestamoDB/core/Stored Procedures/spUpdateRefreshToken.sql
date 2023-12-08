CREATE procedure [core].[spUpdateRefreshToken]
@UserName varchar(200),
@RefreshToken varchar(max) 
as
begin
	update [core].[tblUsers] 
		set RefreshToken = @RefreshToken
	WHERE UserName = @UserName
end