-- use master;
-- go
-- ALTER DATABASE  [QuoteMe]
-- SET SINGLE_USER
-- WITH ROLLBACK IMMEDIATE
-- drop database [QuoteMe]
-- go
------ normal creation after here
use master;
go
if not exists (select name from master..syslogins where name = 'QuoteMeWeb')
    begin
        create login QuoteMeWeb with password = 'P4$$w0rd';
    end;
go


if not exists (select name from master..sysdatabases where name = 'QuoteMe')
begin
create database QuoteMe
end;
GO

use QuoteMe
if not exists (select * from sysusers where name = 'QuoteMeWeb')
begin
create user QuoteMeWeb
	for login QuoteMeWeb
	with default_schema = dbo
end;
GO
grant connect to QuoteMeWeb
go
exec sp_addrolemember N'db_datareader', N'QuoteMeWeb';
go
exec sp_addrolemember N'db_datawriter', N'QuoteMeWeb';
go
exec sp_addrolemember N'db_owner', N'QuoteMeWeb';
GO
use master;
GO

