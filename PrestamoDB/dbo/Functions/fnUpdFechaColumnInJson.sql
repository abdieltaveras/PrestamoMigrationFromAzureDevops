create function fnUpdFechaJson(@JsonObj varchar(max))
returns varchar(max)
Begin
	declare @jsonResult varchar(max)=( SELECT JSON_MODIFY(@jsonObj, '$.Fecha', convert(char(23),getdate(),121)))
	return @jsonResult
End
