CREATE TABLE [dbo].[tblTiposMora]
(
	[idTipoMora] INT NOT NULL PRIMARY KEY identity(1,1), 
	[idNegocio] INT NOT NULL, 
    [Codigo] VARCHAR(10) NOT NULL unique, 
	[Descripcion] VARCHAR(100) NOT NULL,
    [Activo] BIT NOT NULL DEFAULT 1, 
	[RequiereAutorizacion] bit NOT NULL DEFAULT 0,
    [TipoCargo] int NOT NULL, 
    [CalcularCargoPor] int NOT NULL, 
	[AplicarA] int NOT NULL, 
	DiasDeGracia int not null,
	MontoOPorCientoACargar decimal(12,9),
	MontoCuotaDesde decimal(12,2),
	MontoCuotaHasta decimal(12,2),
	InsertadoPor varchar(100) not null,
	FechaInsertado DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL, 
    CONSTRAINT [CK_tblTipoMora_CodigoUnico] UNIQUE(Codigo), 
    CONSTRAINT [FK_tblTiposMora_ToTblNegocios] FOREIGN KEY (idNegocio) REFERENCES tblNegocios([IdNegocio])
)
	
