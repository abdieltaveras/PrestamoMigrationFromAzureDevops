
CREATE procedure [core].[spGetUserGroups]
@GroupID uniqueidentifier = null,
@GroupName varchar(256)='',
@CompanyId int = 1
as
begin
  SELECT  us.GroupID, us.GroupName, us.[Description], us.Actions as ActionsSrt,
		  us.isDeleted, us.DeletedOn, us.CreatedBy, us.CreatedOn, us.LastModifiedBy, us.LastModifiedOn 
  FROM  core.tblUserGroups us WITH (nolock) 
  WHERE ((@GroupID is null) or (us.GroupID = @GroupID))
    and ((@GroupName = '') or (us.GroupName = @GroupName))
	and CompanyId = @CompanyId
end