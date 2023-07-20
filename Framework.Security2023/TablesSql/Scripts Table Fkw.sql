--Scripts Tables Framework

CREATE DATABASE Framework_Users;

Go

USE Framework_Users;

GO

CREATE TABLE Users(
Id UNIQUEIDENTIFIER, 
UserName VARCHAR(MAX),
Password VARCHAR(MAX),
DateCreated Datetime,
UserCreated UNIQUEIDENTIFIER,
LoginSessions INTEGER,
UserBlocked bit);