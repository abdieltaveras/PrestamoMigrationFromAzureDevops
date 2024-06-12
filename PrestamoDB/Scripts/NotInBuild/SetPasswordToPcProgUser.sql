declare @UserName varchar(30)= 'PcProg'
declare @_UserId uniqueIdentifier =  (select UserId from core.tblUsers where UserName=@UserName)
declare @temp as table
(
	Id uniqueIdentifier,
	UserId uniqueIdentifier,
	IssuedDT DateTime,
	ValidUntiltDT DateTime,
	ResetDt DateTime
)
insert into @temp execute core.spSetPwReset @UserId = @_UserId
declare @id uniqueIdentifier = (select id from @temp)