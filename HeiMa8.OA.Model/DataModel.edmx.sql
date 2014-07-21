
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/26/2014 15:22:11
-- Generated from EDMX file: D:\我的文档\VS2012\Websites\HeiMa8.OA\HeiMa8.OA.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HeiMa8OADB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserInfoOrderInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderInfo] DROP CONSTRAINT [FK_UserInfoOrderInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_R_UserInfo_ActionInfoActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_R_UserInfo_ActionInfoActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_WF_TempWF_Instance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WF_Instance] DROP CONSTRAINT [FK_WF_TempWF_Instance];
GO
IF OBJECT_ID(N'[dbo].[FK_WF_InstanceFileInfo_WF_Instance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WF_InstanceFileInfo] DROP CONSTRAINT [FK_WF_InstanceFileInfo_WF_Instance];
GO
IF OBJECT_ID(N'[dbo].[FK_WF_InstanceFileInfo_FileInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WF_InstanceFileInfo] DROP CONSTRAINT [FK_WF_InstanceFileInfo_FileInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_WF_InstanceWF_Step]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WF_Step] DROP CONSTRAINT [FK_WF_InstanceWF_Step];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[OrderInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderInfo];
GO
IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[R_UserInfo_ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfoExt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoExt];
GO
IF OBJECT_ID(N'[dbo].[WF_Temp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Temp];
GO
IF OBJECT_ID(N'[dbo].[WF_Instance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Instance];
GO
IF OBJECT_ID(N'[dbo].[FileInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileInfo];
GO
IF OBJECT_ID(N'[dbo].[WF_Step]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_Step];
GO
IF OBJECT_ID(N'[dbo].[UserInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoRoleInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfoActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfoActionInfo];
GO
IF OBJECT_ID(N'[dbo].[WF_InstanceFileInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WF_InstanceFileInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UName] nvarchar(32)  NOT NULL,
    [Pwd] nvarchar(max)  NOT NULL,
    [ShowName] nvarchar(max)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [ReMark] nvarchar(64)  NULL,
    [ModfiedOn] datetime  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'OrderInfo'
CREATE TABLE [dbo].[OrderInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(64)  NOT NULL,
    [UserInfoID] int  NOT NULL
);
GO

-- Creating table 'ActionInfo'
CREATE TABLE [dbo].[ActionInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [ReMark] nvarchar(64)  NULL,
    [ModfiedOn] datetime  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [Url] nvarchar(512)  NOT NULL,
    [HttpMethod] nvarchar(32)  NULL,
    [ActionName] nvarchar(32)  NOT NULL,
    [IsMenu] bit  NOT NULL,
    [MenuIcon] nvarchar(512)  NULL
);
GO

-- Creating table 'RoleInfo'
CREATE TABLE [dbo].[RoleInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(32)  NOT NULL,
    [ReMark] nvarchar(64)  NULL,
    [ModfiedOn] datetime  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL
);
GO

-- Creating table 'R_UserInfo_ActionInfo'
CREATE TABLE [dbo].[R_UserInfo_ActionInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [HasPermission] bit  NOT NULL,
    [UserInfoID] int  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [ActionInfoID] int  NOT NULL
);
GO

-- Creating table 'UserInfoExt'
CREATE TABLE [dbo].[UserInfoExt] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserInfoID] int  NOT NULL,
    [Age] int  NULL,
    [Email] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NULL,
    [DelFlag] smallint  NOT NULL
);
GO

-- Creating table 'WF_Temp'
CREATE TABLE [dbo].[WF_Temp] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TempName] nvarchar(32)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [TempForm] nvarchar(max)  NULL,
    [ReMark] nvarchar(64)  NULL,
    [DelFlag] smallint  NOT NULL,
    [ActityType] nvarchar(max)  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'WF_Instance'
CREATE TABLE [dbo].[WF_Instance] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InstanceName] nvarchar(128)  NOT NULL,
    [StartBy] int  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [Level] smallint  NOT NULL,
    [Content] nvarchar(max)  NULL,
    [ReMark] nvarchar(64)  NULL,
    [DelFlag] smallint  NOT NULL,
    [FilePath] nvarchar(max)  NOT NULL,
    [Status] smallint  NOT NULL,
    [WF_TempID] int  NOT NULL,
    [WFInstanceId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'FileInfo'
CREATE TABLE [dbo].[FileInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(128)  NOT NULL,
    [FileType] nvarchar(32)  NULL,
    [FilePath] nvarchar(max)  NULL,
    [FileSize] nvarchar(32)  NULL,
    [ReMark] nvarchar(64)  NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'WF_Step'
CREATE TABLE [dbo].[WF_Step] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StepName] nvarchar(32)  NULL,
    [ProcessBy] int  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ProcessTime] datetime  NOT NULL,
    [ProcessResult] nvarchar(256)  NOT NULL,
    [ProcessContent] nvarchar(256)  NULL,
    [StepStaus] smallint  NOT NULL,
    [IsStartStep] bit  NOT NULL,
    [IsEndStep] bit  NOT NULL,
    [ParentStepId] int  NOT NULL,
    [WF_InstanceID] int  NOT NULL
);
GO

-- Creating table 'UserInfoRoleInfo'
CREATE TABLE [dbo].[UserInfoRoleInfo] (
    [UserInfo_ID] int  NOT NULL,
    [RoleInfo_ID] int  NOT NULL
);
GO

-- Creating table 'RoleInfoActionInfo'
CREATE TABLE [dbo].[RoleInfoActionInfo] (
    [RoleInfo_ID] int  NOT NULL,
    [ActionInfo_ID] int  NOT NULL
);
GO

-- Creating table 'WF_InstanceFileInfo'
CREATE TABLE [dbo].[WF_InstanceFileInfo] (
    [WF_Instance_ID] int  NOT NULL,
    [FileInfo_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'OrderInfo'
ALTER TABLE [dbo].[OrderInfo]
ADD CONSTRAINT [PK_OrderInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ActionInfo'
ALTER TABLE [dbo].[ActionInfo]
ADD CONSTRAINT [PK_ActionInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'RoleInfo'
ALTER TABLE [dbo].[RoleInfo]
ADD CONSTRAINT [PK_RoleInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [PK_R_UserInfo_ActionInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'UserInfoExt'
ALTER TABLE [dbo].[UserInfoExt]
ADD CONSTRAINT [PK_UserInfoExt]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WF_Temp'
ALTER TABLE [dbo].[WF_Temp]
ADD CONSTRAINT [PK_WF_Temp]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [PK_WF_Instance]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'FileInfo'
ALTER TABLE [dbo].[FileInfo]
ADD CONSTRAINT [PK_FileInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WF_Step'
ALTER TABLE [dbo].[WF_Step]
ADD CONSTRAINT [PK_WF_Step]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserInfo_ID], [RoleInfo_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [PK_UserInfoRoleInfo]
    PRIMARY KEY NONCLUSTERED ([UserInfo_ID], [RoleInfo_ID] ASC);
GO

-- Creating primary key on [RoleInfo_ID], [ActionInfo_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [PK_RoleInfoActionInfo]
    PRIMARY KEY NONCLUSTERED ([RoleInfo_ID], [ActionInfo_ID] ASC);
GO

-- Creating primary key on [WF_Instance_ID], [FileInfo_ID] in table 'WF_InstanceFileInfo'
ALTER TABLE [dbo].[WF_InstanceFileInfo]
ADD CONSTRAINT [PK_WF_InstanceFileInfo]
    PRIMARY KEY NONCLUSTERED ([WF_Instance_ID], [FileInfo_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserInfoID] in table 'OrderInfo'
ALTER TABLE [dbo].[OrderInfo]
ADD CONSTRAINT [FK_UserInfoOrderInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoOrderInfo'
CREATE INDEX [IX_FK_UserInfoOrderInfo]
ON [dbo].[OrderInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [UserInfo_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_UserInfo]
    FOREIGN KEY ([UserInfo_ID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleInfo_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_ID])
    REFERENCES [dbo].[RoleInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoRoleInfo_RoleInfo'
CREATE INDEX [IX_FK_UserInfoRoleInfo_RoleInfo]
ON [dbo].[UserInfoRoleInfo]
    ([RoleInfo_ID]);
GO

-- Creating foreign key on [RoleInfo_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_ID])
    REFERENCES [dbo].[RoleInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActionInfo_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo]
    FOREIGN KEY ([ActionInfo_ID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleInfoActionInfo_ActionInfo'
CREATE INDEX [IX_FK_RoleInfoActionInfo_ActionInfo]
ON [dbo].[RoleInfoActionInfo]
    ([ActionInfo_ID]);
GO

-- Creating foreign key on [UserInfoID] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_UserInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [ActionInfoID] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_R_UserInfo_ActionInfoActionInfo]
    FOREIGN KEY ([ActionInfoID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_R_UserInfo_ActionInfoActionInfo'
CREATE INDEX [IX_FK_R_UserInfo_ActionInfoActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([ActionInfoID]);
GO

-- Creating foreign key on [WF_TempID] in table 'WF_Instance'
ALTER TABLE [dbo].[WF_Instance]
ADD CONSTRAINT [FK_WF_TempWF_Instance]
    FOREIGN KEY ([WF_TempID])
    REFERENCES [dbo].[WF_Temp]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_TempWF_Instance'
CREATE INDEX [IX_FK_WF_TempWF_Instance]
ON [dbo].[WF_Instance]
    ([WF_TempID]);
GO

-- Creating foreign key on [WF_Instance_ID] in table 'WF_InstanceFileInfo'
ALTER TABLE [dbo].[WF_InstanceFileInfo]
ADD CONSTRAINT [FK_WF_InstanceFileInfo_WF_Instance]
    FOREIGN KEY ([WF_Instance_ID])
    REFERENCES [dbo].[WF_Instance]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FileInfo_ID] in table 'WF_InstanceFileInfo'
ALTER TABLE [dbo].[WF_InstanceFileInfo]
ADD CONSTRAINT [FK_WF_InstanceFileInfo_FileInfo]
    FOREIGN KEY ([FileInfo_ID])
    REFERENCES [dbo].[FileInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_InstanceFileInfo_FileInfo'
CREATE INDEX [IX_FK_WF_InstanceFileInfo_FileInfo]
ON [dbo].[WF_InstanceFileInfo]
    ([FileInfo_ID]);
GO

-- Creating foreign key on [WF_InstanceID] in table 'WF_Step'
ALTER TABLE [dbo].[WF_Step]
ADD CONSTRAINT [FK_WF_InstanceWF_Step]
    FOREIGN KEY ([WF_InstanceID])
    REFERENCES [dbo].[WF_Instance]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WF_InstanceWF_Step'
CREATE INDEX [IX_FK_WF_InstanceWF_Step]
ON [dbo].[WF_Step]
    ([WF_InstanceID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------