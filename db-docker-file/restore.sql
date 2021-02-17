-------------------------------------------------
--> Restoring Reactivities DB
-------------------------------------------------
RESTORE DATABASE Reactivities FROM DISK = '/usr/app/Reactivities.bak' WITH MOVE 'Reactivities' TO '/var/opt/mssql/data/Reactivities.mdf', MOVE 'Reactivities_log' TO '/var/opt/mssql/data/Reactivities_log.ldf'
Go

-------------------------------------------------
--> Adding user ReactivitiesUser 
-------------------------------------------------
USE master;
GO
CREATE LOGIN ReactivitiesUser
WITH PASSWORD = N'Simple_Password',
     DEFAULT_DATABASE = Reactivities
GO
-------------------------------------------------
--> Adding permissions to ReactivitiesUser
-------------------------------------------------
USE Reactivities
GO
CREATE USER ReactivitiesUser FOR LOGIN ReactivitiesUser
GO
USE Reactivities
GO
ALTER ROLE db_owner ADD MEMBER ReactivitiesUser
GO


