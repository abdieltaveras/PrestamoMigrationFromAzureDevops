CREATE TABLE [dbo].[tblTiposCargo]
(
	IdTipoCargo INT NOT NULL PRIMARY KEY identity(1,1), 
    Idnegocio INT NOT NULL references tblNegocios(idnegocio),
	IdLocalidadNegocio INT NULL references tblLocalidadesNegocio(idLocalidadNegocio),
	Codigo varchar(20) not null, --I01-Interes, C01-Capital, M01-Moras, I02-InteresDVencido, GC01GestionCobro
	Cuenta varchar(20) default 'Ninguna',
	Descripcion varchar(50) 
	constraint TPCUniqueCodigo unique(idnegocio, idlocalidadNegocio,Codigo)
	--Constraint TPCAvoidCodigos check (Codigo<>'I01' and Codigo<>'I02' and Codigo<>'C01' and Codigo<>'GC01' and Codigo<>'NTA01' and codigo<>'PCL01')
)
