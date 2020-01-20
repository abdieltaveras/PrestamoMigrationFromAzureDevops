create function fnUpdFechaJson(@JsonObj varchar(max))
returns varchar(max)
Begin
	declare @jsonResult varchar(max)=''
	if (@jsonObj<>'')
	begin
		set @jsonResult = ( SELECT JSON_MODIFY(@jsonObj, '$.Fecha', convert(char(23),getdate(),121)))
	end
	return @jsonResult
End
