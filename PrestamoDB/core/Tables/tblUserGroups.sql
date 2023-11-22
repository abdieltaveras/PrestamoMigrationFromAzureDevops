CREATE TABLE [core].[tblUserGroups] (
    [GroupID]        UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [GroupName]      VARCHAR (256)    NOT NULL,
    [Description]    VARCHAR (256)    NULL,
    [CreatedBy]      VARCHAR (256)    NOT NULL,
    [CreatedOn]      DATETIME         NOT NULL,
    [LastModifiedBy] VARCHAR (256)    NULL,
    [LastModifiedOn] DATETIME         NULL,
    [isDeleted]      BIT              NULL,
    [DeletedOn]      DATETIME         NULL,
    [Actions]        NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([GroupID] ASC),
    UNIQUE NONCLUSTERED ([GroupName] ASC)
);

