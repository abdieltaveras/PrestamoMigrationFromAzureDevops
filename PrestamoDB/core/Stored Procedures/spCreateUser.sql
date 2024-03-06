
CREATE procedure [core].[spCreateUser](@UserName varchar(256)
								, @FirstName varchar(256)
								, @LastName varchar(256)
								, @NationalID varchar(256)
								, @Email varchar(256)
								, @GroupName varchar(256)
								, @IsActive bit
								, @CreatedBy varchar(256)
								,@CompanyId int,
								@CompaniesAccess varchar(max)) as
begin
  declare @RowID int = (select RowID from core.tblUsers where UserName=@UserName)
  if(@RowID is null)
  begin
	  DECLARE @Password VARCHAR(255) = '01234567890', @salt UNIQUEIDENTIFIER = NEWID()
	  DECLARE @hpass BINARY(64) = HASHBYTES('SHA2_512', @Password+CAST(@salt AS NVARCHAR(36))),
			  @actions varchar(max) ='gTM8G4ZNsb9wdy1DjSpv4A=='

	  insert into core.tblUsers(UserName, FirstName, LastName, Email, GroupName, IsActive, NationalID,
								CreatedBy, CreatedOn, MustChangePassword, Actions, PasswordHash, PassSlt, CompanyId,CompaniesAccess)
						values(@UserName, @FirstName, @LastName, @Email, @GroupName, 1, @NationalID,
							   @CreatedBy, GETDATE(), 1, @actions, @hpass, @salt,@CompanyId,@CompaniesAccess)
	 set @RowID = SCOPE_IDENTITY()
  end
  else
  begin
	update core.tblUsers
		set FirstName = @FirstName,
			LastName = @LastName,
			Email = @Email,
			NationalID = @NationalID,
			GroupName = @GroupName,
			IsActive = @isActive,
			LastModifiedBy = @CreatedBy,
			LastModifiedOn = GETDATE(),
			CompaniesAccess = @CompaniesAccess
	where rowid= @rowid
  end
  select UserID, UserName, FirstName, LastName, NationalID, GroupName, Email, Actions as ActionsSrt,
			 IsActive, MustChangePassword, isDeleted, DeletedOn, 
			 CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn
	  from core.tblUsers
	  where rowid= @rowid
end