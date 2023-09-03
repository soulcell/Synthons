-- =============================================
-- Create database template
-- =============================================
USE master
GO

DROP DATABASE IF EXISTS SYNTHONS
GO

CREATE DATABASE SYNTHONS
ON
( NAME = Synthons_dat,
  FILENAME = 'D:\Study\FITR\DB2\DATA\Synthons_dat.mdf',
  SIZE = 1MB,
  MAXSIZE = 50MB,
  FILEGROWTH = 1MB )
LOG ON
( NAME = Synthons_log,
  FILENAME = 'D:\Study\FITR\DB2\DATA\Synthons_log.ldf',
  SIZE = 512KB,
  MAXSIZE = 25MB,
  FILEGROWTH = 1MB )
GO