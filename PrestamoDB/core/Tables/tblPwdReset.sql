CREATE TABLE [core].[tblPwdReset] (
    [ID]           UNIQUEIDENTIFIER NOT NULL,
    [UserID]       UNIQUEIDENTIFIER NOT NULL,
    [IssuedDT]     DATETIME         NULL,
    [ValidUntilDT] DATETIME         NULL,
    [ResetDT]      DATETIME         NULL
);

