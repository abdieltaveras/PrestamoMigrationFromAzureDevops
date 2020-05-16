create table tblPeriodos
( idPeriodo int primary key identity(1,1),
  Idnegocio int foreign key references tblNegocios(idNegocio),
  PeriodoBase int not null,
  -- valores 1 para dia, 2 para semana, 3 para quincena, 4 para mes, 5 para año
  Codigo varchar(10) not null,
  Activo bit not null default 1,
  Nombre varchar(100) not null,
  MultiploPeriodoBase int not null,
  RequiereAutorizacion bit not null default 0,
  [InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL,
  CONSTRAINT PeriodoBaseCk CHECK (PeriodoBase IN (1, 2, 3, 4,5)),
  constraint UK_Codigo unique(IdNegocio,Codigo) 
)
