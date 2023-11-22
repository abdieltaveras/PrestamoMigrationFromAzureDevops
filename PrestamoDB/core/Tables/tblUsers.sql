CREATE TABLE [core].[tblUsers] (
    [RowID]              INT              IDENTITY (1, 1) NOT NULL,
    [UserID]             UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [UserName]           VARCHAR (256)    NOT NULL,
    [FirstName]          VARCHAR (256)    NOT NULL,
    [LastName]           VARCHAR (256)    NOT NULL,
    [GroupName]          VARCHAR (256)    NOT NULL,
    [Email]              VARCHAR (256)    NOT NULL,
    [IsActive]           BIT              NULL,
    [CreatedBy]          VARCHAR (256)    NOT NULL,
    [CreatedOn]          DATETIME         NOT NULL,
    [LastModifiedBy]     VARCHAR (256)    NULL,
    [LastModifiedOn]     DATETIME         NULL,
    [isDeleted]          BIT              NULL,
    [MustChangePassword] BIT              NULL,
    [DeletedOn]          DATETIME         NULL,
    [Actions]            NVARCHAR (MAX)   NULL,
    [PasswordHash]       BINARY (64)      NULL,
    [PassSlt]            UNIQUEIDENTIFIER NULL,
    [NationalID]         VARCHAR (256)    NULL,
    PRIMARY KEY CLUSTERED ([RowID] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);

