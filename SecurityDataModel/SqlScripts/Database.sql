USE [master]
GO

:setvar DatabaseName "KusPhSecurity"

/*
Проверьте режим SQLCMD и отключите выполнение скрипта, если режим SQLCMD не поддерживается.
Чтобы повторно включить скрипт после включения режима SQLCMD выполните следующую инструкцию:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Для успешного выполнения этого скрипта должен быть включен режим SQLCMD.';
        SET NOEXEC ON;
    END
GO

if EXISTS(SELECT DB_ID(N'$(DatabaseName)'))
	BEGIN
		PRINT N'База данных $(DatabaseName) уже существует'
		SET NOEXEC ON;
	END

/****** Object:  Database [$(DatabaseName)]    Script Date: 14.04.2015 11:08:44 ******/
CREATE DATABASE [$(DatabaseName)]
GO
ALTER DATABASE [$(DatabaseName)] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [$(DatabaseName)].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [$(DatabaseName)] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET ARITHABORT OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [$(DatabaseName)] SET AUTO_SHRINK ON 
GO
ALTER DATABASE [$(DatabaseName)] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [$(DatabaseName)] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [$(DatabaseName)] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET  DISABLE_BROKER 
GO
ALTER DATABASE [$(DatabaseName)] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [$(DatabaseName)] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET RECOVERY FULL 
GO
ALTER DATABASE [$(DatabaseName)] SET  MULTI_USER 
GO
ALTER DATABASE [$(DatabaseName)] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [$(DatabaseName)] SET DB_CHAINING OFF 
GO
ALTER DATABASE [$(DatabaseName)] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [$(DatabaseName)] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'$(DatabaseName)', N'ON'
GO
USE [$(DatabaseName)]
GO
/****** Object:  Schema [sec]    Script Date: 14.04.2015 11:08:44 ******/
CREATE SCHEMA [sec]
GO
/****** Object:  StoredProcedure [sec].[AddGrant]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--exec [dbo].[Grant_Insert] @IdSecObject=6,@IdRole=1,@IdAccessType=11,@ObjectName=NULL,@Type1=NULL,@Type2=NULL,@Type3=NULL,@Type4=NULL,@Type5=NULL,@Type6=NULL,@Type7=NULL,@RoleName=NULL,@AccessName=NULL
create procedure [sec].[AddGrant]
	@IdSecObject int,
	@IdRole int,
	@IdAccessType int,
	@ObjectName varchar(500) = null,
	@Type1 varchar(500) = NULL,
	@Type2 varchar(500) = NULL,
	@Type3 varchar(500) = NULL,
	@Type4 varchar(500) = NULL,
	@Type5 varchar(500) = NULL,
	@Type6 varchar(500) = NULL,
	@Type7 varchar(500) = NULL,
	@RoleName varchar(500) = NULL,
	@AccessName varchar(500) = NULL
as
set nocount on
insert into sec._Grant(idSecObject, idRole, idAccessType) values(@IdSecObject, @IdRole, @IdAccessType)

GO
/****** Object:  StoredProcedure [sec].[AddGroup]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[AddGroup]
	@name varchar(200),
	@description varchar(max)
as
set nocount on
insert into sec.Groups(name, description) values(@name, @description)
select IDENT_CURRENT('sec.Member') as idMember

GO
/****** Object:  StoredProcedure [sec].[AddMemberRole]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [sec].[AddMemberRole]
	@idRole int,
	@idMember int,
	@roleName varchar(200) = null,
	@memberName varchar(200) = null,
	@isUser bit = null
as
set nocount on
insert into sec.MemberRole(idMember, idRole) values(@idMember, @idRole)

GO
/****** Object:  StoredProcedure [sec].[AddUser]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [sec].[AddUser]
	@login varchar(200),
	@password varbinary(16),
	@displayName varchar(200),
	@email varchar(300),
	@usersid varchar(300)
as
set nocount on
insert into sec.Users(login, password, displayName, email, usersid) values(@login, @password, @displayName, @email, @usersid)
select IDENT_CURRENT('sec.Member') as idMember
GO
/****** Object:  StoredProcedure [sec].[AddUserToGroup]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[AddUserToGroup]
	@idUser int,
	@idGroup int,
	@login varchar(200) = null,
	@displayName varchar(200) = null,
	@email varchar(100) = null,
	@usersid varchar(100) = null,
	@groupName varchar(200) = null,
	@groupDescription varchar(max) = null
as
set nocount on
insert into sec.UserGroups(idUser, idGroup) values(@idUser, @idGroup)

GO
/****** Object:  StoredProcedure [sec].[DeleteGrant]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [sec].[DeleteGrant]
	@IdSecObject int,
	@IdRole int,
	@IdAccessType int
as
set nocount on
delete from sec._Grant where idSecObject = @IdSecObject and idRole = @IdRole and idAccessType = @IdAccessType
GO
/****** Object:  StoredProcedure [sec].[DeleteGroup]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[DeleteGroup]
	@idGroup int
as
set nocount on
delete from sec.Groups where idGroup = @idGroup
GO
/****** Object:  StoredProcedure [sec].[DeleteMemberRole]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [sec].[DeleteMemberRole]
	@idRole int,
	@idMember int
as
set nocount on

delete from sec.MemberRole where idMember = @idMember and idRole = @idRole

GO
/****** Object:  StoredProcedure [sec].[DeleteUser]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[DeleteUser]
	@idUser int
as
set nocount on
delete from sec.Users where idUser = @idUser
GO
/****** Object:  StoredProcedure [sec].[DeleteUserFromGroup]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[DeleteUserFromGroup]
	@idUser int,
	@idGroup int
as
set nocount on
delete from sec.UserGroups where idUser = @idUser and idGroup = @idGroup

GO
/****** Object:  StoredProcedure [sec].[SetIdentificationMode]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[SetIdentificationMode]
	@mode varchar(100)
as
set nocount on
if not exists(select 1 from sec.Settings where name = 'identification_mode')
	insert into sec.Settings(name, value) values('identification_mode', @mode)
else
	update sec.Settings set value = @mode where name = 'identification_mode'
GO
/****** Object:  StoredProcedure [sec].[UpdateGrant]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[UpdateGrant]
	@IdSecObject int,
	@IdRole int,
	@IdAccessType int,
	@ObjectName varchar(500) = null,
	@Type1 varchar(500) = NULL,
	@Type2 varchar(500) = NULL,
	@Type3 varchar(500) = NULL,
	@Type4 varchar(500) = NULL,
	@Type5 varchar(500) = NULL,
	@Type6 varchar(500) = NULL,
	@Type7 varchar(500) = NULL,
	@RoleName varchar(500) = NULL,
	@AccessName varchar(500) = NULL
as
set nocount on
raiserror('not_modified', 16, 1)

GO
/****** Object:  StoredProcedure [sec].[UpdateGroup]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[UpdateGroup]
	@idGroup int,
	@name varchar(200),
	@description varchar(max)
as
set nocount on
update sec.Groups set name = @name, description = @description where idGroup = @idGroup

GO
/****** Object:  StoredProcedure [sec].[UpdateMemberRole]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [sec].[UpdateMemberRole]
	@idRole int,
	@idMember int,
	@roleName varchar(200),
	@memberName varchar(200),
	@isUser bit
as
raiserror('is_not_modified', 16, 10)
GO
/****** Object:  StoredProcedure [sec].[UpdateUser]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[UpdateUser]
	@idUser int,
	@login varchar(200),
	@password varbinary(16),
	@displayName varchar(200),
	@email varchar(300),
	@usersid varchar(300)
as
set nocount on
update sec.Users set login = @login, password = @password, displayName = @displayName, email = @email, usersid = @usersid where idUser = @idUser

GO
/****** Object:  StoredProcedure [sec].[UpdateUserGroup]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [sec].[UpdateUserGroup]
	@idUser int,
	@idGroup int,
	@login varchar(200) = null,
	@displayName varchar(200) = null,
	@email varchar(100) = null,
	@usersid varchar(100) = null,
	@groupName varchar(200) = null,
	@groupDescription varchar(max) = null
as
set nocount on
raiserror('is_not_modified', 16, 10)
GO
/****** Object:  UserDefinedFunction [sec].[GetIdentificationMode]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [sec].[GetIdentificationMode]()
returns varchar(max)
as
begin
	return sec.GetSettings('identification_mode')
end
GO
/****** Object:  UserDefinedFunction [sec].[GetSettings]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [sec].[GetSettings](@name varchar(100))
returns varchar(max)
as
begin
	declare @value varchar(max)

	select @value = value from sec.Settings where name = @name

	return @value
end

GO
/****** Object:  UserDefinedFunction [sec].[IsAllow]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [sec].[IsAllow](@objectName varchar(max), @idMember varchar(200), @idAccessType int)
returns bit
as
begin
	declare @result bit = 0

	--Сначала проверяем доступ, если участником безопасности является группа
	if not exists(select 1 from sec.Users where idUser  = @idMember)
	begin
		if exists
				(
					select
						1
					from
						sec._Grant gr inner join sec.SecObject so 
					on
						gr.idSecObject = so.idSecObject inner join sec.RoleOfMember rof 
					on
						gr.idRole = rof.idRole 
					where 
						so.ObjectName = @objectName 
						and rof.idMember = @idMember
						and idAccessType = @idAccessType
				)
		begin
			set @result = 1
		end
		
		return @result
	end

	--Проверяем доступ по списку всех групп, в которых состоит пользователь, не забывая при этом включить самого пользователя в этот список
	if exists
			(
				select
					1
				from
					sec._Grant gr inner join sec.SecObject so 
				on
					gr.idSecObject = so.idSecObject inner join sec.RoleOfMember rof 
				on
					gr.idRole = rof.idRole 
				where 
					so.ObjectName = @objectName 
					and rof.idMember in (select idGroup from sec.UserGroups where idUser = @idMember union all select @idMember)
					and idAccessType = @idAccessType
			)
	begin
		set @result = 1
	end

	return @result
end
GO
/****** Object:  UserDefinedFunction [sec].[IsAllowByAccessId]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [sec].[IsAllowByAccessId](@objectName varchar(max), @member varchar(200), @idAccessType int)
returns bit
as
begin
	declare @result bit = 0
	if exists
			(
				select
					1
				from
					sec._Grant gr inner join sec.SecObject so 
				on
					gr.idSecObject = so.idSecObject inner join sec.RoleOfMember rof 
				on
					gr.idRole = rof.idRole 
				where 
					so.ObjectName = @objectName 
					and rof.memberName = @member 
					and idAccessType = @idAccessType
			)
	begin
		set @result = 1
	end

	return @result
end

GO
/****** Object:  UserDefinedFunction [sec].[IsAllowByAccessName]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [sec].[IsAllowByAccessName](@objectName varchar(max), @member varchar(200), @accessName varchar(300))
returns bit
as
begin
	declare @result bit = 0
	if exists
			(
				select
					1
				from
					sec._Grant gr inner join sec.SecObject so
				on
					gr.idSecObject = so.idSecObject inner join sec.RoleOfMember rof
				on
					gr.idRole = rof.idRole inner join sec.AccessType at
				on
					gr.idAccessType = at.idAccessType
				where
					so.ObjectName = @objectName
					and rof.memberName = @member 
					and at.name = @accessName
			)
	begin
		set @result = 1
	end

	return @result
end

GO
/****** Object:  Table [sec].[_Grant]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[_Grant](
	[idSecObject] [int] NOT NULL,
	[idRole] [int] NOT NULL,
	[idAccessType] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idSecObject] ASC,
	[idRole] ASC,
	[idAccessType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [sec].[_Group]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[_Group](
	[idMember] [int] NOT NULL,
	[description] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[_Role]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[_Role](
	[idRole] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[_User]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[_User](
	[idMember] [int] NOT NULL,
	[usersid] [varchar](300) NULL,
	[displayName] [varchar](200) NULL,
	[email] [varchar](300) NULL,
	[password] [varbinary](16) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[AccessType]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[AccessType](
	[idAccessType] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](300) NULL,
PRIMARY KEY CLUSTERED 
(
	[idAccessType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[email]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[email](
	[email] [varchar](300) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[Member]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[Member](
	[idMember] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idMember] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[MemberRole]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[MemberRole](
	[idMember] [int] NOT NULL,
	[idRole] [int] NOT NULL,
 CONSTRAINT [PK_MemberRole] PRIMARY KEY CLUSTERED 
(
	[idMember] ASC,
	[idRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [sec].[SecObject]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[SecObject](
	[idSecObject] [int] IDENTITY(1,1) NOT NULL,
	[ObjectName] [varchar](200) NOT NULL,
	[type1] [varchar](100) NULL,
	[type2] [varchar](100) NULL,
	[type3] [varchar](100) NULL,
	[type4] [varchar](100) NULL,
	[type5] [varchar](100) NULL,
	[type6] [varchar](100) NULL,
	[type7] [varchar](100) NULL,
	[Discriminator] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[idSecObject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[Settings]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[Settings](
	[idSettings] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[value] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idSettings] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[sid]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [sec].[sid](
	[sid] [varchar](300) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [sec].[UserGroups]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [sec].[UserGroups](
	[idUser] [int] NOT NULL,
	[idGroup] [int] NOT NULL,
 CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC,
	[idGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [sec].[Groups]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create view [sec].[Groups]
as
select
	m.idMember idGroup,
	m.name,
	g.description
from
	sec._Group g inner join sec.Member m
on
	g.idMember = m.idMember

GO
/****** Object:  View [sec].[Users]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE view [sec].[Users]
as
select
	m.idMember idUser,
	m.name login,
	u.password,
	u.displayName,
	u.email,
	u.usersid
from 
	sec.Member m inner join sec._User u 
on
	m.idMember = u.idMember






GO
/****** Object:  View [sec].[UserGroupsDetail]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [sec].[UserGroupsDetail]
as
select
	u.idUser,
	u.login,
	u.displayName,
	u.email,
	u.usersid,
	g.idGroup,
	g.name as groupName,
	g.description as groupDescription
from
	sec.Users u inner join sec.UserGroups ug
on
	u.idUser = ug.idUser inner join sec.Groups g
on
	ug.idGroup = g.idGroup


GO
/****** Object:  View [sec].[Grants]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE view [sec].[Grants]
as
select
	so.idSecObject,
	so.ObjectName,
	so.type1,
	so.type2,
	so.type3,
	so.type4,
	so.type5,
	so.type6,
	so.type7,
	r.idRole,
	r.name roleName,
	at.idAccessType,
	at.name accessName
from 
	sec._Grant gr inner join sec.SecObject so
on
	gr.idSecObject = so.idSecObject inner join sec._Role r
on
	gr.idRole = r.idRole inner join sec.AccessType at
on
	gr.idAccessType = at.idAccessType




GO
/****** Object:  View [sec].[Members]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [sec].[Members]
as
select
	*,
	case
		when exists(select 1 from sec._User where idMember = sec.Member.idMember) then cast(1 as bit)
		else cast(0 as bit)
	end as isUser
from
	sec.Member
GO
/****** Object:  View [sec].[RoleOfMember]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE view [sec].[RoleOfMember]
as
select
	r.idRole,
	r.name roleName,
	m.idMember,
	m.name memberName,
	case
		when exists(select 1 from sec._User where idMember = m.idMember) then cast(1 as bit)
		else cast(0 as bit)
	end as isUser
from
	sec._Role r inner join sec.MemberRole mr 
on 
	r.idRole = mr.idRole inner join sec.Member m
on
	mr.idMember = m.idMember




GO
/****** Object:  Index [UQ_Group_idMember]    Script Date: 14.04.2015 11:08:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Group_idMember] ON [sec].[_Group]
(
	[idMember] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_Role_Name]    Script Date: 14.04.2015 11:08:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Role_Name] ON [sec].[_Role]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [UQ_User_idMember]    Script Date: 14.04.2015 11:08:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_User_idMember] ON [sec].[_User]
(
	[idMember] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_AccessType_AccessName]    Script Date: 14.04.2015 11:08:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_AccessType_AccessName] ON [sec].[AccessType]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_Member_Name]    Script Date: 14.04.2015 11:08:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Member_Name] ON [sec].[Member]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_SecObject_ObjectName]    Script Date: 14.04.2015 11:08:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SecObject_ObjectName] ON [sec].[SecObject]
(
	[ObjectName] ASC,
	[type1] ASC,
	[type2] ASC,
	[type3] ASC,
	[type4] ASC,
	[type5] ASC,
	[type6] ASC,
	[type7] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ_Settings_Name]    Script Date: 14.04.2015 11:08:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Settings_Name] ON [sec].[Settings]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [sec].[_Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_AccessType] FOREIGN KEY([idAccessType])
REFERENCES [sec].[AccessType] ([idAccessType])
GO
ALTER TABLE [sec].[_Grant] CHECK CONSTRAINT [FK_Grant_AccessType]
GO
ALTER TABLE [sec].[_Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_Role] FOREIGN KEY([idRole])
REFERENCES [sec].[_Role] ([idRole])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[_Grant] CHECK CONSTRAINT [FK_Grant_Role]
GO
ALTER TABLE [sec].[_Grant]  WITH CHECK ADD  CONSTRAINT [FK_Grant_SecObject] FOREIGN KEY([idSecObject])
REFERENCES [sec].[SecObject] ([idSecObject])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[_Grant] CHECK CONSTRAINT [FK_Grant_SecObject]
GO
ALTER TABLE [sec].[_Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Member] FOREIGN KEY([idMember])
REFERENCES [sec].[Member] ([idMember])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[_Group] CHECK CONSTRAINT [FK_Group_Member]
GO
ALTER TABLE [sec].[_User]  WITH CHECK ADD  CONSTRAINT [FK_User_Email] FOREIGN KEY([email])
REFERENCES [sec].[email] ([email])
GO
ALTER TABLE [sec].[_User] CHECK CONSTRAINT [FK_User_Email]
GO
ALTER TABLE [sec].[_User]  WITH CHECK ADD  CONSTRAINT [FK_User_Member] FOREIGN KEY([idMember])
REFERENCES [sec].[Member] ([idMember])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[_User] CHECK CONSTRAINT [FK_User_Member]
GO
ALTER TABLE [sec].[_User]  WITH CHECK ADD  CONSTRAINT [FK_User_Sid] FOREIGN KEY([usersid])
REFERENCES [sec].[sid] ([sid])
GO
ALTER TABLE [sec].[_User] CHECK CONSTRAINT [FK_User_Sid]
GO
ALTER TABLE [sec].[MemberRole]  WITH CHECK ADD  CONSTRAINT [FK_MemberRole_Member] FOREIGN KEY([idMember])
REFERENCES [sec].[Member] ([idMember])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[MemberRole] CHECK CONSTRAINT [FK_MemberRole_Member]
GO
ALTER TABLE [sec].[MemberRole]  WITH CHECK ADD  CONSTRAINT [FK_MemberRole_Role] FOREIGN KEY([idRole])
REFERENCES [sec].[_Role] ([idRole])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[MemberRole] CHECK CONSTRAINT [FK_MemberRole_Role]
GO
ALTER TABLE [sec].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserGroups_Groups] FOREIGN KEY([idGroup])
REFERENCES [sec].[_Group] ([idMember])
GO
ALTER TABLE [sec].[UserGroups] CHECK CONSTRAINT [FK_UserGroups_Groups]
GO
ALTER TABLE [sec].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserGroups_Users] FOREIGN KEY([idUser])
REFERENCES [sec].[_User] ([idMember])
ON DELETE CASCADE
GO
ALTER TABLE [sec].[UserGroups] CHECK CONSTRAINT [FK_UserGroups_Users]
GO
/****** Object:  Trigger [sec].[OnDeleteGroup]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE trigger [sec].[OnDeleteGroup] on [sec].[_Group]
after delete
as
set nocount on

if exists(select 1 from sec.Member where idMember in (select idMember from deleted))
	begin
		raiserror('fk_member_error', 16, 10)
		rollback
		return
	end

GO
/****** Object:  Trigger [sec].[OnAddGroups]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create trigger [sec].[OnAddGroups] on [sec].[Groups]
instead of insert
as
set nocount on
begin try
	begin transaction

	insert into sec.Member(name) select name from inserted
	insert into sec._Group(idMember, description)
	select
		idMember,
		description
	from
		(
			select
				m.idMember,
				ins.description
			from
				inserted ins inner join sec.Member m
			on
				ins.name = m.name
		) s1

	commit
end try
begin catch
	rollback
	declare @errorMessage nvarchar(max)
	select @errorMessage = ERROR_MESSAGE()
	raiserror(@errorMessage, 16, 10)
end catch

GO
/****** Object:  Trigger [sec].[OnDeleteGroups]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE trigger [sec].[OnDeleteGroups] on [sec].[Groups]
instead of delete
as
set nocount on
begin try
	begin transaction

	delete from sec.Member where idMember in (select idGroup from deleted)

	commit
end try
begin catch
	rollback
	declare @errorMessage nvarchar(max)
	select @errorMessage = ERROR_MESSAGE()
	raiserror(@errorMessage, 16, 10)
end catch

GO
/****** Object:  Trigger [sec].[OnUpdateGroups]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE trigger [sec].[OnUpdateGroups] on [sec].[Groups]
instead of update
as
set nocount on
begin try
	begin transaction

	update sec.Member set name = ins.name from inserted ins where idMember = idGroup
	update sec._Group set description = ins.description from inserted ins where idMember = ins.idGroup

	commit
end try
begin catch
	rollback
	declare @errorMessage nvarchar(max)
	select @errorMessage = ERROR_MESSAGE()
	raiserror(@errorMessage, 16, 10)
end catch


GO
/****** Object:  Trigger [sec].[OnAddUser]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE trigger [sec].[OnAddUser] on [sec].[Users]
instead of insert
as
set nocount on
begin try
	begin transaction

	insert into sec.Member(name) select login from inserted
	insert into sec.email(email) select email from inserted where email is not null
	insert into sec.sid(sid) select usersid from inserted where usersid is not null

	insert into sec._User(idMember, usersid, displayName, email, password) 
	select
		idMember,
		usersid,
		displayName,
		email,
		password
	from
		(
			select
				m.idMember,
				ins.usersid,
				ins.displayName,
				ins.email,
				ins.password
			from
				inserted ins inner join sec.Member m
			on
				ins.login = m.name
		) s1

	commit
end try
begin catch
	rollback
	declare @errorMessage nvarchar(max)
	select @errorMessage = ERROR_MESSAGE()
	raiserror(@errorMessage, 16, 10)
end catch
GO
/****** Object:  Trigger [sec].[OnDeleteUser]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE trigger [sec].[OnDeleteUser] on [sec].[Users]
instead of delete
as
set nocount on
begin try
begin transaction
	
	delete from sec.Member where idMember in (select idUser from deleted)
	delete from sec.email where email in (select email from deleted where email is not null)
	delete from sec.sid where sid in (select usersid from deleted where usersid is not null)

commit
end try
begin catch
	rollback
	declare @errorMessage nvarchar(max)
	select @errorMessage = ERROR_MESSAGE()
	raiserror(@errorMessage, 16, 10)
end catch
GO
/****** Object:  Trigger [sec].[OnUpdateUser]    Script Date: 14.04.2015 11:08:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE trigger [sec].[OnUpdateUser] on [sec].[Users]
instead of update
as
set nocount on
begin try
begin transaction

	declare @changed table
	(
		idUser			int,
		newLogin		varchar(200),
		newPassword		varbinary(16),
		newDisplayName	varchar(200),
		newEmail		varchar(300),
		newUsersid		varchar(300),
		oldLogin		varchar(200),
		oldPassword		varbinary(16),
		oldDisplayName	varchar(200),
		oldEmail		varchar(300),
		oldUsersid		varchar(300)
	)

	insert into @changed 
	select
		ins.idUser		idUser,		
		ins.Login		newLogin,	
		ins.Password	newPassword,
		ins.DisplayName	newDisplayName,
		ins.Email		newEmail,
		ins.Usersid		newUsersid,
		del.Login		oldLogin,
		del.Password	oldPassword,
		del.DisplayName	oldDisplayName,
		del.Email		oldEmail,
		del.Usersid		oldUsersid
	from 
		inserted ins inner join deleted del 
	on 
		ins.idUser = del.idUser

	update sec.Member set name = login from inserted where idMember = idUser
	insert into sec.email(email) select newEmail from @changed where newEmail is not null and oldEmail is null
	insert into sec.sid(sid) select newUsersid from @changed where newUsersid is not null and oldUsersid is null
	update sec.email set sec.Email.email = changed.newEmail from @changed changed where sec.Email.email = changed.oldEmail and changed.oldEmail is not null and changed.newEmail is not null
	update sec.sid set sec.sid.sid = changed.newUsersid from @changed changed where sec.sid.sid = changed.oldUsersid and changed.oldUsersid is not null and changed.newUsersid is not null
	update sec._User set usersid = changed.newUsersid, displayName = changed.newDisplayName, email = changed.newEmail, password = changed.newPassword from @changed changed where idMember = changed.idUser

commit
end try
begin catch
	rollback
	declare @errorMessage nvarchar(max)
	select @errorMessage = ERROR_MESSAGE()
	raiserror(@errorMessage, 16, 10)
end catch
GO
USE [master]
GO
ALTER DATABASE [$(DatabaseName)] SET  READ_WRITE 
GO
