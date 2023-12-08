CREATE procedure [core].[spGetUserGroups](@GroupID uniqueidentifier = null, @GroupName varchar(256)='')as
begin
  SELECT  GroupID, GroupName, [Description], Actions as ActionsSrt,
		  isDeleted, DeletedOn, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn
  FROM  core.tblUserGroups WITH (nolock)
  WHERE ((@GroupID is null) or (GroupID = @GroupID))
    and ((@GroupName = '') or (GroupName = @GroupName))
end