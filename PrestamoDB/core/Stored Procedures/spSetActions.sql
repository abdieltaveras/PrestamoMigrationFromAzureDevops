
CREATE procedure [core].[spSetActions](@ID uniqueidentifier, @ActionsStr varchar(max)) as
begin
	 if(exists(select * FROM core.tblUserGroups WITH (nolock) WHERE (GroupID = @ID)))
	 begin
		update core.tblUserGroups 
			set actions = @actionsstr 
		WHERE (GroupID = @ID)
	 end
	 else
	 if(exists(select * FROM core.tblUsers WITH (nolock) WHERE (UserID = @ID)))
	 begin
		update [core].[tblUsers] 
			set actions = @actionsstr 
		WHERE (UserID = @ID)
	 end
end