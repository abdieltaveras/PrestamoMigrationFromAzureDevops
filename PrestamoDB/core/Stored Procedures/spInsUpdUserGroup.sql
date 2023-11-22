
CREATE procedure [core].[spInsUpdUserGroup](@GroupName varchar(256), @Description varchar(256),@CreatedBy varchar(256), @Actions nvarchar(max))as
begin
  declare @GroupID uniqueidentifier = (select GroupID FROM core.tblUserGroups WITH (nolock) where GroupName = @GroupName)
  if(@GroupID is null)
  begin
		set @GroupID= newid()
	   INSERT INTO core.tblUserGroups(GroupName, [Description], Actions, CreatedBy, CreatedOn) 
	   VALUES (@GroupName, @Description, 'gTM8G4ZNsb9wdy1DjSpv4A==', @CreatedBy, GETDATE())	   
  end
  else
  begin
	if(isnull(@Actions,'')='')
	begin
		set @Actions = 'gTM8G4ZNsb9wdy1DjSpv4A=='
	end
     update core.tblUserGroups
	   set Actions = @Actions,
		   [Description] = @Description,
		   LastModifiedBy =@CreatedBy,
		   LastModifiedOn = GETDATE()
	WHERE (GroupID = @GroupID)
  end
  SELECT  GroupID, GroupName, [Description], Actions as ActionsSrt,
		  isDeleted, DeletedOn, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn
  FROM  core.tblUserGroups WITH (nolock)
  WHERE GroupID = @GroupID
end