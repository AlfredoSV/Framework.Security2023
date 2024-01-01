USE Framework_Users;

GO

CREATE OR ALTER PROCEDURE ValidateUser @value varchar(50), @type bit, @result bit output
as
begin
	
	if @type = 0
	begin
		if exists(Select usF.Id from UserFkw as usF
									INNER JOIN UserInformation as usI ON usF.Id= usI.IdUser
									where usI.email = @value)
			set @result = cast(1 as bit);
		else
			set @result = cast(0 as bit);
	end

	if @type = 1
	begin
		if exists(Select usF.Id from UserFkw as usF
									INNER JOIN UserInformation as usI ON usF.Id= usI.IdUser
									where usF.UserName = @value)
			set @result = cast(1 as bit);
		else
			set @result = cast(0 as bit);
	end

end;
