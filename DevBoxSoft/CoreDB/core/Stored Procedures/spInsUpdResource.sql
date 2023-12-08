create procedure core.spInsUpdResource(@Resource varchar(150), @Value nvarchar(max))as
begin
  declare @idResource uniqueidentifier = (select idResource FROM core.tblResources WITH (nolock) where [Resource] = @Resource)
  if(@idResource is null)
  begin
	   INSERT INTO core.tblResources([Resource], [Value]) VALUES (@Resource, @Value)	   
  end
  else
  begin
     update core.tblResources
	   set [Value] = @Value	   
	WHERE (idResource = @idResource)
  end
end