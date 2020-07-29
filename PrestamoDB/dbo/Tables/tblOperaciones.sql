CREATE TABLE [dbo].[tblOperaciones]
(
	[IdOperacion] INT NOT NULL PRIMARY KEY identity(1,1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Codigo] NVARCHAR(50) NOT NULL,
	[Grupo] INT NOT NULL,
	[Descripcion] NVARCHAR(250) NOT NULL
)
