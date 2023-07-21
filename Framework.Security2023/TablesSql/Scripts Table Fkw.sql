--Scripts Tables Framework

CREATE DATABASE Framework_Users;

Go

USE Framework_Users;

GO

 
 if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Users')
 begin 
	CREATE TABLE Users(
	Id UNIQUEIDENTIFIER not null, 
	UserName VARCHAR(MAX) not null,
	Password VARCHAR(MAX) not null,
	DateCreated Datetime not null,
	UserCreated UNIQUEIDENTIFIER not null,
	LoginSessions INTEGER not null,
	UserBlocked bit not null); 
 end

 if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'UsersRol')
 begin 
	CREATE TABLE UsersRol(
	Id UNIQUEIDENTIFIER not null, 
	IdRol VARCHAR(MAX) not null,
	IdUser VARCHAR(MAX) not null,
	DateCreated Datetime not null,
	UserCreated UNIQUEIDENTIFIER not null
	); 
 end

  if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Rol')
 begin 
	CREATE TABLE Rol(
	Id UNIQUEIDENTIFIER not null, 	
	RolName VARCHAR(MAX) not null,
	Permission UNIQUEIDENTIFIER not null,
	DateCreated Datetime not null,
	UserCreated UNIQUEIDENTIFIER not null
	); 
 end

