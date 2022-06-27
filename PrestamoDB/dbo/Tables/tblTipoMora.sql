CREATE TABLE [dbo].[tblTiposMora]
(
	[idTipoMora] INT NOT NULL PRIMARY KEY identity(1,1), 
	[idNegocio] INT NOT NULL, 
	[IdLocalidadNegocio] INT NOT NULL,
    [Codigo] VARCHAR(10) NOT NULL unique, 
	[Nombre] VARCHAR(100) NOT NULL,
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
    [BorradoPor] VARCHAR(100) NULL, 
    [FechaBorrado] DATETIME NULL, 
    CONSTRAINT [CK_tblTipoMora_CodigoUnico] UNIQUE(Codigo), 
    CONSTRAINT [FK_tblTiposMora_ToTblNegocios] FOREIGN KEY (idNegocio) REFERENCES tblNegocios([IdNegocio])
)
	

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'contains  items of  tipomora, tipomora is an object that will be use to calculate extra charge when an cxc item is due',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'tblTiposMora',
    @level2type = NULL,
    @level2name = NULL