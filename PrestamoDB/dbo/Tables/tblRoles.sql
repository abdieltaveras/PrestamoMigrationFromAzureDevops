CREATE TABLE [dbo].[tblRoles]
(
	[IdRole] INT NOT NULL PRIMARY KEY identity(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Descripcion] NVARCHAR(100) NOT NULL
)
