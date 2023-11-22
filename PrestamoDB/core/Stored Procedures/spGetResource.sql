
CREATE procedure [core].[spGetResource](@Resource varchar(150)='')as
begin
  SELECT  idResource, [Resource], [Value]
    FROM  core.tblResources WITH (nolock)
    WHERE ((@Resource = '') or ([Resource] = @Resource))
end