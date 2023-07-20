--Scripts Tables Framework

CREATE DATABASE Framework_Users;

Go

USE Framework_Users;

GO
 
 if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Users')
 begin 
	CREATE TABLE Users(
	Id UNIQUEIDENTIFIER, 
	UserName VARCHAR(MAX),
	Password VARCHAR(MAX),
	DateCreated Datetime,
	UserCreated UNIQUEIDENTIFIER,
	LoginSessions INTEGER,
	UserBlocked bit); 
 end

