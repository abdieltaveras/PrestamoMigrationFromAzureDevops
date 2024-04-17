CREATE TABLE [dbo].[tblTiposCargo]
(
	IdTipoCargo INT NOT NULL PRIMARY KEY identity(1,1), 
    Idnegocio INT NOT NULL references tblNegocios(idnegocio),
	IdLocalidadNegocio INT NULL references tblLocalidadesNegocio(idLocalidadNegocio),
	Codigo varchar(20) not null, --I01-Interes, C01-Capital, M01-Moras, I02-InteresDVencido, GC01GestionCobro
	Cuenta varchar(20) default 'Ninguna',
	Descripcion varchar(50) 
	constraint TPCUniqueCodigo unique(idnegocio, idlocalidadNegocio,Codigo)
	-- evaluar si habilitar el constraint aqui no afectaria el initdatabase o agregarlo en el initdatabase
	-- luego que realice el initDatabase
	-- Constraint TPCAvoidCodigos check (not Codigo in ('I01','I02','C01','GC01','NTA01','PCL01'))
)
