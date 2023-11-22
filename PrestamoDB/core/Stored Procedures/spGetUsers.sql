
CREATE procedure [core].[spGetUsers](@UserID uniqueidentifier = null,
								 @UserName varchar(256) = '',
								 @NationalID varchar(256) = '',								 
								 @GroupName varchar(256)= '',
								 @Email varchar(256)= '',
								 @IsActive int = -1)as
begin
	select UserID, UserName, FirstName, LastName, NationalID, GroupName, Email, Actions as ActionsSrt,
		 IsActive, MustChangePassword, isDeleted, DeletedOn, 
		 CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn
  from core.tblUsers
  WHERE ((@UserID is null) or (UserID = @UserID))
		and ((@UserName = '') or (UserName = @UserName))
		and ((@NationalID = '') or (NationalID = @NationalID))
		and ((@GroupName = '') or (GroupName = @GroupName))
		and ((@Email = '') or (Email = @Email))
		and ((@IsActive < 0) or (IsActive = @IsActive))
  order by GroupName, UserName
end