CREATE TABLE [dbo].[tblRedesSociales]
(
    [IdRedSocial] INT NOT NULL identity (1,1),
    [Nombre] NVARCHAR(50) NULL, 
    [IdNegocio] INT NOT NULL,
	[Codigo] VARCHAR(10) NOT NULL default '',
    [Activo] BIT NOT NULL DEFAULT 1,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL
    CONSTRAINT [PK_tblRedesSociales] PRIMARY KEY ([IdRedSocial])
)
