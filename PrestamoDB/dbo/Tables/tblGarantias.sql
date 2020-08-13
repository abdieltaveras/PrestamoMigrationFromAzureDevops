﻿CREATE TABLE [dbo].[tblGarantias]
(
	[IdGarantia] INT NOT NULL PRIMARY KEY  identity(1,1), 
    [IdClasificacion] INT NOT NULL, 
    [IdTipoGarantia] INT not NULL, 
    [IdModelo] INT NULL,
	IdMarca INT NULL,
    [NoIdentificacion] NVARCHAR(50) NOT NULL, 
    [IdNegocio] INT NOT NULL,
    [Detalles] VARCHAR(4000) NULL,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL,
    constraint fk_GarantiaMarca foreign KEY (IdMarca) REFERENCES tblMarcas(IdMarca),
    constraint idMarcaNotNullForIdClasificacion_2 check (idclasificacion=2 and idMarca is not null ),
    constraint fk_GarantiaModelo foreign KEY (IdModelo) REFERENCES tblModelos(IdModelo),
    constraint idModeloNotNullForIdClasificacion_2 check (idclasificacion=2 and idModelo is not null ),    
    constraint fk_GarantiaTiposGarantia foreign KEY (IdTipoGarantia) REFERENCES tblTiposGarantia(IdTipoGarantia),
    
)
