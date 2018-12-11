
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/01/2018 11:26:56
-- Generated from EDMX file: D:\TeamProject\Messanger\SERVER\MessangerEDNXData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MeaaangerDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LoginMessageDataLoad]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MessageDataLoad] DROP CONSTRAINT [FK_LoginMessageDataLoad];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[LoginData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginData];
GO
IF OBJECT_ID(N'[dbo].[MessageDataLoad]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MessageDataLoad];
GO
IF OBJECT_ID(N'[Хранилище MeaaangerDBModelContainer].[DataDbLoadFile]', 'U') IS NOT NULL
    DROP TABLE [Хранилище MeaaangerDBModelContainer].[DataDbLoadFile];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LoginData'
CREATE TABLE [dbo].[LoginData] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LogName] nvarchar(max)  NOT NULL,
    [Pass] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MessageDataLoad'
CREATE TABLE [dbo].[MessageDataLoad] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [Login_Id] int  NOT NULL
);
GO

-- Creating table 'DataDbLoadFile'
CREATE TABLE [dbo].[DataDbLoadFile] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DataFile] varbinary(max)  NOT NULL,
    [Login_Id] int  NOT NULL,
    [LoginData_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LoginData'
ALTER TABLE [dbo].[LoginData]
ADD CONSTRAINT [PK_LoginData]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MessageDataLoad'
ALTER TABLE [dbo].[MessageDataLoad]
ADD CONSTRAINT [PK_MessageDataLoad]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [DataFile], [Login_Id] in table 'DataDbLoadFile'
ALTER TABLE [dbo].[DataDbLoadFile]
ADD CONSTRAINT [PK_DataDbLoadFile]
    PRIMARY KEY CLUSTERED ([Id], [DataFile], [Login_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Login_Id] in table 'MessageDataLoad'
ALTER TABLE [dbo].[MessageDataLoad]
ADD CONSTRAINT [FK_LoginMessageDataLoad]
    FOREIGN KEY ([Login_Id])
    REFERENCES [dbo].[LoginData]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoginMessageDataLoad'
CREATE INDEX [IX_FK_LoginMessageDataLoad]
ON [dbo].[MessageDataLoad]
    ([Login_Id]);
GO

-- Creating foreign key on [LoginData_Id] in table 'DataDbLoadFile'
ALTER TABLE [dbo].[DataDbLoadFile]
ADD CONSTRAINT [FK_LoginDataDataDbLoadFile]
    FOREIGN KEY ([LoginData_Id])
    REFERENCES [dbo].[LoginData]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoginDataDataDbLoadFile'
CREATE INDEX [IX_FK_LoginDataDataDbLoadFile]
ON [dbo].[DataDbLoadFile]
    ([LoginData_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------