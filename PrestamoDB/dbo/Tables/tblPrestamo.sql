﻿CREATE TABLE [dbo].[tblPrestamos]
(
	IdPrestamo Int PRIMARY KEY identity(1,1),
	idNegocio int foreign key references tblNegocios(idNegocio) not null,
	idCliente int foreign key references tblClientes(idCliente) not null,
	prestamoNumero varchar(20) not null unique(idNegocio, PrestamoNumero),
	IdPrestamoARenovar int, --REFERENCES tblPrestamos(idPrestamo), 
	DeudaRenovacion decimal (14,2) default 0,
	idClasificacion int foreign key references tblClasificaciones(idClasificacion) not null,
	TipoAmortizacion int not null,
	FechaEmisionReal Date not null,
	FechaEmisionParaCalculo Date not null,
	FechaVencimiento Date not null,
	IdTasaInteres int foreign key references tblTasasInteres(IdTasaInteres) not null,
	idTipoMora int foreign key references tblTiposMora(idTipoMora) not null,
	idPeriodo int foreign key references tblPeriodos(idPeriodo) not null,
	CantidadDePeriodos int not null check (CantidadDePeriodos > 0),
	MontoPrestado Decimal(14,2) not null default 0,
	TotalPrestado as MontoPrestado + DeudaRenovacion persisted, 
	IdDivisa int references tblDivisas(idDivisa) default 1 not null, 
	InteresGastoDeCierre decimal (6,2) not null,
	MontoGastoDeCierre decimal (14,2) not null,
	GastoDeCierreEsDeducible bit not null,
	CargarInteresAlGastoDeCierre bit not null,
	SumarGastoDeCierreALasCuotas bit not null default 0,
	AcomodarFechaALasCuotas bit not null default 0,
	FechaInicioPrimeraCuota  dateTime not null,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL,
	constraint TotalCapitalMayorQueCero check (TotalPrestado > 0),
)
	
