create procedure core.spDelUser(@UserID uniqueidentifier )as
begin
	delete from [core].[tblUsers] where UserID= @UserID
end