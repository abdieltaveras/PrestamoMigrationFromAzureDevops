CREATE proc [dbo].[spSearchTableByColunm]
@SearchText varchar(100),
@Column varchar(50),
@Table varchar(50),
@OrderBy varchar(50) 
as
if(@OrderBy = '')
	EXEC('SELECT * from ' + @Table + '  where '+@Column+' like '''+@SearchText+'%''') 
else
	EXEC('SELECT * from ' + @Table + '  where '+@Column+' like '''+@SearchText+'%'' order by '+@OrderBy)