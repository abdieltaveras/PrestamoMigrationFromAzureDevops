create table tblPeriodos
( idPeriodo int primary key identity(1,1),
  Idnegocio int foreign key references tblNegocios(IdNegocio),
  IdLocalidadNegocio int foreign key references tblLocalidadesNegocio(idLocalidadNegocio),
  IdPeriodoBase int not null,
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
    [BorradoPor] VARCHAR(100) NULL, 
    [FechaBorrado] DATETIME NULL,
  CONSTRAINT [PeriodoBaseCk] CHECK ([IdPeriodoBase]=(5) OR [IdPeriodoBase]=(4) OR [IdPeriodoBase]=(3) OR [IdPeriodoBase]=(2) OR [IdPeriodoBase]=(1)),
  constraint UK_Codigo unique(IdNegocio,Codigo) 
)
