CREATE procedure [core].[spAuthUser]
@UserName varchar(256), 
@Password varchar(256),
@CompanyId int,
@CompanyLocationId int

as
begin
  select UserID, NationalID, UserName, FirstName, LastName, GroupName, Email, Actions as ActionsSrt,
		 IsActive, MustChangePassword, isDeleted, DeletedOn, 
		 CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, RefreshToken, CompaniesAccess, CompanyId
  from core.tblUsers
  WHERE (UserName = @UserName)
		and (IsActive = 1) 
		and (isnull(isDeleted, 0) = 0)
		and (CompanyId = @CompanyId)
		and (CompaniesAccess LIKE '%'+ CONVERT(varchar(200), @CompanyLocationId )+ '%')
		AND (MustChangePassword=1 or  PasswordHash=HASHBYTES('SHA2_512', @Password+CAST(PassSlt AS NVARCHAR(36))))
end