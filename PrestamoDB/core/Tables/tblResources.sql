CREATE TABLE [core].[tblResources] (
    [idResource]   UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Resource]     VARCHAR (256)    NOT NULL,
    [Value]        NVARCHAR (MAX)   NULL,
    [LastModified] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([idResource] ASC)
);

