CREATE TABLE [core].[tblUserGroups] (
    [GroupID]        UNIQUEIDENTIFIER CONSTRAINT [DF__tblUserGr__Group__4DE98D56] DEFAULT (newsequentialid()) NOT NULL,
    [CompanyId]      INT              NOT NULL,
    [GroupName]      VARCHAR (256)    NOT NULL,
    [Description]    VARCHAR (256)    NULL,
    [CreatedBy]      VARCHAR (256)    NOT NULL,
    [CreatedOn]      DATETIME         NOT NULL,
    [LastModifiedBy] VARCHAR (256)    NULL,
    [LastModifiedOn] DATETIME         NULL,
    [isDeleted]      BIT              NULL,
    [DeletedOn]      DATETIME         NULL,
    [Actions]        NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK__tblUserG__149AF30A41E5C6BD] PRIMARY KEY CLUSTERED ([GroupID] ASC),
    CONSTRAINT [UQ__tblUserG__6EFCD4348A74FED2] UNIQUE NONCLUSTERED ([GroupName] ASC)
);



