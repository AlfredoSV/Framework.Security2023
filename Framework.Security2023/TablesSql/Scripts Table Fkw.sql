﻿--Scripts Tables Framework
CREATE DATABASE Framework_Users;

Go

USE Framework_Users;

GO

 
 if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Users')
 begin 
	CREATE TABLE UserFkw(
	Id UNIQUEIDENTIFIER not null, 
	UserName VARCHAR(30) not null,
	Password VARCHAR(20) not null,
	DateCreated Datetime not null,
	UserCreated UNIQUEIDENTIFIER not null,
	LoginSessions INTEGER not null,
	RolId UNIQUEIDENTIFIER not null,
	ApplyToken bit NOT NULL,
	UserBlocked bit not null); 
 end

 if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'UserLoginAttempts')
 begin 
	CREATE TABLE UserLoginAttempts(
	IdUser UNIQUEIDENTIFIER not null,
	Description varchar(50) not null, 
	DateCreated Datetime not null); 
 end

 if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'UserInformation')
 begin 
	CREATE TABLE UserInformation(
	IdUser UNIQUEIDENTIFIER not null, 
	Name VARCHAR(30) not null,
	LastName VARCHAR(20) not null,
	Age integer not null,
	DateCreated Datetime not null,
	Address VARCHAR(60) not null,
	Email VARCHAR(30) NOT NUll,
	UserCreated UNIQUEIDENTIFIER not null)
 end


 if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'UserToken')
 begin 
	CREATE TABLE UserToken(
	Id UNIQUEIDENTIFIER not null, 
	UserId UNIQUEIDENTIFIER not null, 
	Token VARCHAR(MAX),
	DateCreated Datetime not null,
	DateExpiration Datetime not null);
 end


 if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Rol')
 begin 
	CREATE TABLE Rol(
	Id UNIQUEIDENTIFIER not null, 	
	RolName VARCHAR(30) not null,
	DateCreated Datetime not null,
	UserCreated UNIQUEIDENTIFIER not null,
	Active bit
	); 
 end

 if not exists (Select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Permission')
 begin 
	CREATE TABLE Permission(
	Id UNIQUEIDENTIFIER not null,
	RolId UNIQUEIDENTIFIER not null, 
	PermissionName VARCHAR(20) not null,
	PermissionDescription VARCHAR(100) not null,
	Module VARCHAR(30) not null,
	DateCreated Datetime not null,
	UserCreated UNIQUEIDENTIFIER not null,
	Active bit
	); 
 end

