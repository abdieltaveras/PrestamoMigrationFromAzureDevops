CREATE TABLE [dbo].[tblOperaciones]
(
	[IdOperacion] INT NOT NULL PRIMARY KEY identity(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Codigo] NVARCHAR(20) NOT NULL,
	[Descripcion] NVARCHAR(100) NOT NULL
)
