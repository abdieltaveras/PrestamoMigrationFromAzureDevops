CREATE TABLE [core].[tblUsers] (
    [RowID]              INT              IDENTITY (1, 1) NOT NULL,
    [UserID]             UNIQUEIDENTIFIER CONSTRAINT [DF__tblUsers__UserID__51BA1E3A] DEFAULT (newsequentialid()) NOT NULL,
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
    [CompanyId]          INT              NULL,
    [CompaniesAccess]    VARCHAR (MAX)    NULL,
    [RefreshToken]       VARCHAR (MAX)    NULL,
    CONSTRAINT [PK__tblUsers__FFEE7451F474BA6C] PRIMARY KEY CLUSTERED ([RowID] ASC),
    CONSTRAINT [UQ__tblUsers__C9F284566A4CE0D3] UNIQUE NONCLUSTERED ([UserName] ASC)
);



