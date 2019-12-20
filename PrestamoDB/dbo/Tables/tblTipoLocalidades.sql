CREATE TABLE [dbo].[tblTipoLocalidades]
(
	[IdTipoLocalidad] INT NOT NULL PRIMARY KEY identity(1,1),
    [HijoDe] INT NULL,
    [IdDivisionTerritorial] INT NULL,
    [IdNegocio] INT NOT NULL,
    [Descripcion] VARCHAR(50) NOT NULL,
    [PermiteCalle] BIT NOT NULL
)