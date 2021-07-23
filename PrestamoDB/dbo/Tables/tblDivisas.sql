CREATE TABLE [dbo].[tblDivisas]
(
	[idDivisa] INT NOT NULL PRIMARY KEY identity(1,1), 
    [IdLocalidadNegocio] INT NULL, 
    idNegocio int not null,
    [Codigo] VARCHAR(10) NOT NULL unique, 
	[Nombre] VARCHAR(100) NOT NULL,
    [Activo] BIT NOT NULL DEFAULT 1, 
    [RequiereAutorizacion] bit NOT NULL DEFAULT 0,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL,
	CONSTRAINT [FK_tblDivisas_ToTblNegocios] FOREIGN KEY (idNegocio) REFERENCES tblNegocios(idNegocio)
)
