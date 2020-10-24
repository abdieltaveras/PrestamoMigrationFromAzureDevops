CREATE TABLE [dbo].[tblRedesSociales]
(
    [IdRedesSociales] INT NOT NULL identity (1,1),
    [Nombre] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_tblRedesSociales] PRIMARY KEY ([IdRedesSociales])
)
