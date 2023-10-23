USE Framework_Users;

GO

CREATE OR ALTER PROCEDURE ValidateUser @username varchar(50), @email varchar(50), @result bit
as
begin

	if exists(Select usF.Id from UserFkw as usF
								INNER JOIN UserInformation as usI ON usF.Id= usI.IdUser
								where usF.UserName = @userName
								and usI.email = @email)
		set @result = cast(1 as bit);
	else
		set @result = cast(0 as bit);

end;

