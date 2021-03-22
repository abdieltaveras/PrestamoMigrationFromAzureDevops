/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*
	***************NOTE******************
	DO NOT USE GO in next sentences, because is going to remove from memory any variable declared 
	as @usuario, idNegocio, etc variable
*/
	declare @usuario varchar(50)= 'SeedDBUser'
	declare @idNegocio int = 1
	
	--Script para datos de Negocios
	--Negocio Matriz y otras truncales no permite realizar transacciones
	insert into dbo.tblNegocios
			(Codigo,NombreJuridico,NombreComercial,InsertadoPor,FechaInsertado, TaxIdNo, OtrosDetalles, logo)
			VALUES
			('INT-M','Intagsa SRL','Intagsa',@usuario,getdate(), '1','','Papito.png')

	INSERT INTO dbo.tblLocalidadesNegocio (Codigo, Nombre,Activo, idNegocio, PrefijoPrestamo, InsertadoPor, FechaInsertado, MunicipioOCiudad, Calle, Telefono)
	VALUES ('IRO', 'Intagsa Romana', 1,1,null, @usuario,getdate(), 'La Romana','Prolongacion Gregorio Luperon no 12','809-813-1719')

--tblDivisas
	INSERT INTO [dbo].[tblDivisas]
           ([idNegocio]
           ,[Codigo]
           ,[Nombre]
           ,[InsertadoPor]
           ,[FechaInsertado])
     VALUES
           (@idNegocio
           ,'RD$'
           ,'Pesos Dominicano'
           ,@Usuario
		   ,Getdate())

	--Script para datos de Interes

	INSERT INTO [dbo].[tblTasasInteres] ([idNegocio],[Codigo],[Nombre],[InteresMensual],[InsertadoPor],[FechaInsertado])
     VALUES
		   (@idNegocio,'A00', '1% de interes' ,1.0,@usuario,getdate()),
		   (@idNegocio,'B00', '2% de interes' ,2.0,@usuario,getdate()),
		   (@idNegocio,'C00', '3% de interes' ,3.0,@usuario,getdate()),
		   (@idNegocio,'D00', '4% de interes' ,4.0,@usuario,getdate()),
		   (@idNegocio,'E00', '5% de interes' ,5.0,@usuario,getdate()),
		   (@idNegocio,'F00', '6% de interes' ,6.0,@usuario,getdate()),
		   (@idNegocio,'G00', '7% de interes' ,7.0,@usuario,getdate()),
		   (@idNegocio,'H00', '8% de interes' ,8.0,@usuario,getdate()),
		   (@idNegocio,'I00', '9% de interes' ,9.0,@usuario,getdate()),
		   (@idNegocio,'J00', '10% de interes' ,10.0,@usuario,getdate()),
		   (@idNegocio,'K00', '11% de interes' ,11.0,@usuario,getdate()),
		   (@idNegocio,'L00', '12% de interes' ,12.0,@usuario,getdate()),
		   (@idNegocio,'M00', '13% de interes' ,13.0,@usuario,getdate()),
		   (@idNegocio,'N00', '143% de interes' ,14.0,@usuario,getdate()),
		   (@idNegocio,'O00', '15% de interes' ,15.0,@usuario,getdate()),
		   (@idNegocio,'P00', '16% de interes' ,16.0,@usuario,getdate()),
		   (@idNegocio,'Q00', '17% de interes' ,17.0,@usuario,getdate()),
		   (@idNegocio,'R00', '18% de interes' ,18.0,@usuario,getdate()),
		   (@idNegocio,'S00', '19% de interes' ,19.0,@usuario,getdate()),
		   (@idNegocio,'T00', '20% de interes' ,20.0,@usuario,getdate())
		   
	-- tblClasificaciones
		   INSERT INTO [dbo].[tblClasificaciones]
           ([Nombre]
           ,[IdNegocio]
           ,[IdClasificacionFinanciera]
		   ,RequiereAutorizacion
		   ,RequiereGarantia
           ,[Codigo]
           ,[InsertadoPor]
           ,[FechaInsertado]
		   )
		VALUES
           ('Con Vehiculos' ,@idNegocio ,1 ,0,1,'Vehic' ,@usuario,getDate()),
		   ('Con Motocicletas' ,@idNegocio ,1,0,1,'Mot' ,@usuario,getDate()),
		   ('Con Casa' ,@idNegocio ,2,0,1 ,'Casa',@usuario,getDate()),
		   ('Personal' ,@idNegocio ,2 ,1,0,'Personal' ,@usuario,getDate())
    --Script para periodos
	insert into tblPeriodos (IdNegocio, IdPeriodoBase,Codigo, MultiploPeriodoBase,Nombre, insertadoPor, fechaInsertado) values (1,1,'DIA',1,'Cuotas Diarias','seed', getdate()) 
	insert into tblPeriodos (IdNegocio, IdPeriodoBase,Codigo, MultiploPeriodoBase,Nombre, insertadoPor, fechaInsertado) values (1,2,'SEM',1,'Cuotas Semanales', 'seed', getdate()) 
	insert into tblPeriodos (IdNegocio, IdPeriodoBase,Codigo, MultiploPeriodoBase,Nombre, insertadoPor, fechaInsertado) values (1,3,'QUI',1,'Cuotas Quincenales','seed', getdate()) 
	insert into tblPeriodos (IdNegocio, IdPeriodoBase,Codigo, MultiploPeriodoBase,Nombre, insertadoPor, fechaInsertado) values (1,4,'MES',1,'Cuotas Mensuales','seed', getdate()) 
	insert into tblPeriodos (IdNegocio, IdPeriodoBase,Codigo, MultiploPeriodoBase,Nombre, insertadoPor, fechaInsertado) values (1,1,'INTD',2,'Cuotas Inter Diario','seed', getdate()) 
	insert into tblPeriodos (IdNegocio, IdPeriodoBase,Codigo, MultiploPeriodoBase,Nombre, insertadoPor, fechaInsertado) values (1,4,'BIMES',2,'Cuotas BiMensuales','seed', getdate()) 
	--Script para datos de Moras
	insert into tblTiposMora
			(Nombre,
			idNegocio,Codigo,DiasDeGracia, CalcularCargoPor, AplicarA,TipoCargo,MontoOPorCientoACargar,InsertadoPor, FechaInsertado)
		VALUES

			('Porcentual 10% al interes y capital atrasado por cada dia por cada cuota',
			@idNegocio,'P10IC',3,1,1,1,10.00,@usuario,getdate()),
			('Porcentual 5% al interes y capital atrasado por cada dia por cada cuota',
			@idNegocio,'P05IC',3,1,1,1,10.00,@usuario,getdate()),
			('Porcentual 10% al interes atrasado por cada dia por cada cuota',
			@idNegocio,'P10I',3,2,1,1,10.00,@usuario,getdate()),
			('Porcentual 5% al interes  atrasado por cada dia por cada cuota',
			@idNegocio,'P05I',3,2,1,1,10.00,@usuario,getdate()),
			('Acumulada 10% por total atrasado',
			@idNegocio,'A10IC',3,3,1,1,10.00,@usuario,getdate()),
			('Acumulada 10% por total atrasado',
			@idNegocio,'A05IC',3,3,1,1,5.00,@usuario,getdate()),
			('Lineal 10 pesos por cada dia atrasado',
			@idNegocio,'L05',3,3,1,1,5.00,@usuario,getdate())
-- Table:  tblUsuario
-- Date:   13-Feb-20 5:46 AM

INSERT INTO dbo.tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado)
VALUES (@idNegocio,'Admin','Usuario Administrador','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'809-550-8455',null,1,0,null,0,null,null,null,-1,null,null,@usuario,getdate(),null,null,null,null)

INSERT INTO dbo.tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado)
VALUES (@idNegocio,'Usuario','Usuario por defecto','slT@idNegociocIeEDhgXsnLcD0w/ng==',0,null,'809-550-8455',null,1,0,null,0,null,null,null,-1,null,null,@usuario,getdate(),null,null,null,null)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (@idNegocio, 'usrcc', 'Cambiar Contraseña', 'cu0+Y/VLEdwHc/4vxsazEQ==', 1, getdate(), '(809) 550-4678', '', 1, 0, '', 0, '', '2020-03-28', '1900-01-01', -1, -1, 0, 'admin', getdate(), NULL, NULL, NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (@idNegocio, 'usrina', 'Usuario Inactivo', 'cu0+Y/VLEdwHc/4vxsazEQ==', 1, getdate(), '(809) 813-1719', '', 0, 0, '', 0, '', '2020-03-28', '1900-01-01', -1, -1, 0, 'admin', getdate(), NULL, NULL, NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (@idNegocio, 'usrbl', 'usuario bloqueado', 'cu0+Y/VLEdwHc/4vxsazEQ==', 0, getdate(), '(809) 813-1719', '', 1, 1, '', 0, '', '2020-03-28', '1900-01-01', -1, -1, 0, 'admin', getdate(), NULL, NULL, NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (@idNegocio, 'usrconex', 'usuario contrasena expira', 'cu0+Y/VLEdwHc/4vxsazEQ==', 0, getdate(), '(809) 550-2897', '', 1, 0, 'abdieltaveras@gmail.com', 0, '', '2020-03-28', '1900-01-01', 1, -1, 0, 'admin', getdate(), 'usrconex', '2020-03-30 08:35:59.4', NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (@idNegocio, 'usrlivi', 'usuario limitar vigencia', 'cu0+Y/VLEdwHc/4vxsazEQ==', 0, getdate(), '(809) 550-4678', '', 1, 0, '', 0, '', '2020-03-28', '2020-03-29', -1, -1, 0, 'admin', getdate(), NULL, NULL, NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (@idNegocio, 'success', 'Succes User', 'cu0+Y/VLEdwHc/4vxsazEQ==', 0, getdate(), '829-961-9141', '', 1, 0, 'abdieltaveras@hotmail.com', 0, '', '1900-01-01', '1900-01-01', 1, -1, 0, 'UsuarioTest', '2020-03-28 10:17:44.833', 'testUser29-Mar-20', '2020-03-29 22:15:53.987', NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, 
InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado,  VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath) 
values	(@idNegocio,'bryan','Bryan Pouerie','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'829-973-4733',null,1,0,null,0,null,null,-1,null,null,
			@usuario,getdate(),null,null,null,null, 'Bryan01.png')

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, 
InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado,  VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath) 
values	(@idNegocio,'usrpapito','usr Papito-inv. Bancola','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'829-973-4733',null,1,0,null,0,null,null,-1,null,null,
			@usuario,getdate(),null,null,null,null, 'Bryan01.png')


INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, 
InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado,  VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath) 
values	(@idNegocio,'usrfyg','usr fyg','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'829-973-4733',null,1,0,null,0,null,null,-1,null,null,
			@usuario,getdate(),null,null,null,null, 'Bryan01.png')


INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, 
InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado,  VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath) 
values	(@idNegocio,'usran','usr fyg','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'829-973-4733',null,1,0,null,0,null,null,-1,null,null,
			@usuario,getdate(),null,null,null,null, 'Bryan01.png')


--@idnegocio, @loginname, @nombrerealcompleto, @contraseña, @debecambiarcontraseñaaliniciarsesion,
--@iniciovigenciacontraseña, @telefono1, @telefono2, @activo, @bloqueado, @correoelectronico, 
--@esempleado, @imgfilepath, @vigentedesde, @vigentehasta, @contraseñaexpiracadaxmes, @razonbloqueo, @idpersonal, 
--@insertadopor, @fechainsertado, @modificadopor, @fechamodificado, @anuladopor, @fechaanulado)
	
-- Table:  tblTipos
-- Date:   13-Feb-20 5:46 AM

	
	-- Table:  tblMarcas
	INSERT INTO tblMarcas (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
	VALUES	('Toyota', 1, '', 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
	   		('Ford', 1, '', 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
			('Suzuki', 1, '', 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

	-- Table:  tblModelos
INSERT INTO tblModelos (IdMarca, Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES	(1, 'Camry', 1, '', 1, @usuario, '2020-02-13 05:16:43.067', NULL, NULL, NULL, NULL),
		(1, 'Corolla', 1, '', 1, @usuario, '2020-02-13 05:16:49.09', NULL, NULL, NULL, NULL),
		(2, 'Explorer', 1, '', 1, @usuario, '2020-02-13 05:16:57.657', NULL, NULL, NULL, NULL),
		(2, 'Focus', 1, '', 1, @usuario, '2020-02-13 05:17:06.52', NULL, NULL, NULL, NULL),
		(2, 'Escape', 1, '', 1, @usuario, '2020-02-13 05:17:15.153', NULL, NULL, NULL, NULL),
		(3, 'ax-100', 1, '', 1, @usuario, '2020-02-13 05:21:06.917', NULL, NULL, NULL, NULL)

	
	-- Table:  tblColores
-- Date:   13-Feb-20 5:55 AM

INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES	('Blanco', 1, '', 1, @usuario, '2020-02-13 05:17:52.627', NULL, NULL, NULL, NULL),
		('Negro', 1, '', 1, @usuario, '2020-02-13 05:17:59.683', NULL, NULL, NULL, NULL),
		('Gris', 1, '', 1, @usuario, '2020-02-13 05:18:09.43', NULL, NULL, NULL, NULL),
		('Azul', 1, '', 1, @usuario, '2020-02-13 05:18:14.913', NULL, NULL, NULL, NULL),
		('Rojo', 1, '', 1, @usuario, '2020-02-13 05:18:20.473', NULL, NULL, NULL, NULL),
		('Amarillo/Rojo', 1, '', 1, @usuario, '2020-02-13 05:18:32.4', NULL, NULL, NULL, NULL),
		('Verde', 1, '', 1, @usuario, '2020-02-13 05:18:37.657', NULL, NULL, NULL, NULL),
		('Verde/Blanco', 1, '', 1, @usuario, '2020-02-13 05:18:44.857', NULL, NULL, NULL, NULL)


		-- Table: Garantias

-- Table:  tblTipos
-- Date:   13-Feb-20 5:46 AM

INSERT INTO tblTiposGarantia (IdClasificacion, Nombre, Codigo, Activo, IdNegocio, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado)
VALUES	(1, 'Vehiculos (Carros, Camionetas, Geepetas, etc)', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(1, 'Motocicletas', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(2, 'Casa', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(2, 'Solar con Edificacion', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(2, 'Solar sin Edificacion', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL)
		

	-- Table: Garantias
	INSERT INTO tblGarantias (IdClasificacion, IdTipoGarantia, IdModelo, IdMarca, NoIdentificacion, IdNegocio, Detalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
	VALUES 
	(2, 1, 1, 1, '2626', 1, '{"Color":"2","NoMaquina":"nm2626","Ano":"2001","Placa":"p2626","Matricula":"ma2626","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":"Vehiculo usado esta bien cuidado","InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:20:04.95', NULL, NULL, NULL, NULL),
	(2, 2, 2, 3, '2727', 1, '{"Color":"2","NoMaquina":"nm2727","Ano":"2006","Placa":"p2727","Matricula":"ma2727","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 3, 3, '2929', 1, '{"Color":"2","NoMaquina":"nm2929","Ano":"2007","Placa":"p2929","Matricula":"ma2929","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 2, '3030', 1, '{"Color":"2","NoMaquina":"nm3030","Ano":"2007","Placa":"p3030","Matricula":"ma3030","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	--(1, 1, null, null, 'casaAbdiel', 1, '{"Color":"","NoMaquina":"","Ano":"","Placa":"","Matricula":"","IdLocalidad":1,"DetallesDireccion": calle serapia no 3 las","Medida":"380","UsoExclusivo":true,"Descripcion":null,"InsertadoPor":"seed","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 1, '3131', 1, '{"Color":"2","NoMaquina":"nm3131","Ano":"2007","Placa":"p3131","Matricula":"ma3131","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 2, '3232', 1, '{"Color":"2","NoMaquina":"nm3232","Ano":"2007","Placa":"p3232","Matricula":"ma3232","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 2, '3333', 1, '{"Color":"2","NoMaquina":"nm3333","Ano":"2007","Placa":"p3333","Matricula":"ma3333","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 2, '6363', 1, '{"Color":"2","NoMaquina":"nm6363","Ano":"2007","Placa":"p6363","Matricula":"ma6363","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL)
	-- 9 garantias	


-- Table:  tblClientes
-- Date:   13-Feb-20 5:52 AM

declare @codigo varchar(20)
exec dbo.spGenerarSecuenciaString 'Codigo de Clientes',10,1, @codigo output
INSERT INTO tblClientes (Activo, AnuladoPor, Apodo, Apellidos, Codigo, IdEstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, IdSexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, InfoReferencias, imagen1FileName, imagen2FileName, idLocalidadNegocio) 
VALUES (1, NULL, 'yeyo', 'Taveras', @codigo, 2, '1969-07-31', '2020-02-13 05:13:26.13', '2020-02-13 05:11:00.03', NULL, 1, 1, 2, '{"Nombres":"YOCASTA","Apodo":"Yami","Apellidos":"RODRIGUEZ","NoTelefono1":"8299619140","LugarTrabajo":"Glipsy Novias","TelefonoTrabajo":"8098339140","DireccionLugarTrabajo":"CALLE SERAPIA NO 3","IdTipoIdentificacion":1,"NoIdentificacion":"02600667543","Notas":null}', '{"Nombre":"Pc Prog","Puesto":"Ingeniero de Sistemas","FechaInicio":"2020-02-13T00:00:00","NoTelefono1":"8095508455","NoTelefono2":"8098131251","Direccion":"General Grerio Luperon no 12","Notas":null}', '{"IdDireccion":0,"IdLocalidad":0,"Calle":"calle serapia no 3 las Orquideas","CodigoPostal":"22000","CoordenadasGPS":null,"Detalles":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, @usuario, '02600679191', 'Abdiel', 2, '8098131438', '8299619141','', '[{"Tipo":1, "Vinculo":3, "NombreCompleto":"Hola", "Telefono":"fg", "Direccion":"dfg", "Detalles":"Hola"}]','imagen1.jpeg','imagen2.jpeg',1)

exec dbo.spGenerarSecuenciaString 'Codigo de Clientes',10,1, @codigo output
INSERT INTO tblClientes (Activo, AnuladoPor, Apodo, Apellidos, Codigo, IdEstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, IdSexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, InfoReferencias, imagen1FileName, imagen2FileName, IdLocalidadNegocio) 
VALUES (1, NULL, 'benja', 'Taveras', @codigo, 2, '1969-07-31', '2020-02-13 05:13:26.13', '2020-02-13 05:11:00.03', NULL, 1, 1, 2, '{"Nombres":"ANA MARIA","Apodo":"Ana","Apellidos":"RODRIGUEZ","NoTelefono1":"8299619142","LugarTrabajo":"Intagsa ","TelefonoTrabajo":"8098131719","DireccionLugarTrabajo":"CALLE SERAPIA NO 3","IdTipoIdentificacion":1,"NoIdentificacion":"02600667544","Notas":null}', '{"Nombre":"Pc Prog","Puesto":"Ingeniero de Sistemas","FechaInicio":"2020-02-13T00:00:00","NoTelefono1":"8095508455","NoTelefono2":"8098131251","Direccion":"General Grerio Luperon no 12","Notas":null}', '{"IdDireccion":0,"IdLocalidad":0,"Calle":"calle serapia no 3 las Orquideas","CodigoPostal":"22000","CoordenadasGPS":null,"Detalles":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, @usuario, '02600679192', 'Benjamin', 1, '8098131438', '8299619141','', '[{"Tipo":1, "Vinculo":3, "NombreCompleto":"Hola", "Telefono":"fg", "Direccion":"dfg", "Detalles":"Hola"}]','imagen1.jpeg','imagen2.jpeg',1)

exec dbo.spGenerarSecuenciaString 'Codigo de Clientes',10,1, @codigo output
INSERT INTO tblClientes (Activo, AnuladoPor, Apodo, Apellidos, Codigo, IdEstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, IdSexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, InfoReferencias, imagen1FileName, imagen2FileName, IdLocalidadNegocio) 
VALUES (1, NULL, 'ernesto', 'Tejeda', @codigo, 2, '1969-07-31', '2020-02-13 05:13:26.13', '2020-02-13 05:11:00.03', NULL, 1, 1, 2, '{"Nombres":"Maria","Apodo":"Maria","Apellidos":"Guerrero","NoTelefono1":"8299619142","LugarTrabajo":"Intagsa ","TelefonoTrabajo":"8098131719","DireccionLugarTrabajo":"CALLE SERAPIA NO 3","IdTipoIdentificacion":1,"NoIdentificacion":"02600667548","Notas":null}', '{"Nombre":"Pc Prog","Puesto":"Ingeniero de Sistemas","FechaInicio":"2020-02-13T00:00:00","NoTelefono1":"8095508455","NoTelefono2":"8098131251","Direccion":"General Grerio Luperon no 12","Notas":null}', '{"IdDireccion":0,"IdLocalidad":0,"Calle":"calle serapia no 3 las Orquideas","CodigoPostal":"22000","CoordenadasGPS":null,"Detalles":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, @usuario, '0260067914', 'Ernesto', 1, '8098131438', '8299619141','', '[{"Tipo":1, "Vinculo":3, "NombreCompleto":"Hola", "Telefono":"fg", "Direccion":"dfg", "Detalles":"Hola"}]','imagen1.jpeg','imagen2.jpeg',1)
-- Server: ABDIELALIENWARE\SQLEXPRESS2016
-- Date:   13-Feb-20 5:53 AM
-- Table:  tblTipoLocalidades

INSERT INTO tblTipoLocalidades (IdLocalidadPadre, IdDivisionTerritorial, IdNegocio, Nombre, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (NULL, NULL, 1, 'Division Territorial Raiz (Nunca Borrar)',  1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(1, 0, 1, 'Division Territorial tipo Republica Dominicana',  1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(2, 2, 1, 'Pais',  1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(3, 2, 1, 'Provincia', 1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(4, 2, 1, 'Municipio',  1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(5, 2, 1, 'Sector',  1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(5, 2, 1, 'Distrito Municipal', 1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(7, 2, 1, 'Sector',  1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL)

--Localidades
INSERT INTO tblLocalidades (IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (0, 1, 3, 'Republica Dominicana', '', 1, 'bryan', '2020-04-05 14:01:56.643', NULL, NULL, NULL, NULL)
INSERT INTO tblLocalidades (IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (1, 1, 4, 'La Romana', '', 1, 'bryan', '2020-04-05 14:02:14.683', NULL, NULL, NULL, NULL)
INSERT INTO tblLocalidades (IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (1, 1, 4, 'San Pedro', '', 1, 'bryan', '2020-04-05 14:02:32.96', NULL, NULL, NULL, NULL)
INSERT INTO tblLocalidades (IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (2, 1, 5, 'La Romana', '', 1, 'bryan', '2020-04-05 14:02:46.863', NULL, NULL, NULL, NULL)
INSERT INTO tblLocalidades (IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (4, 1, 6, 'Las Orquideas', '', 1, 'bryan', '2020-04-05 14:40:19.337', NULL, NULL, NULL, NULL)
INSERT INTO tblLocalidades (IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (2, 1, 5, 'Villa Hermosa', '', 1, 'bryan', '2020-04-05 14:40:42.557', 'bryan', '2020-04-05 14:42:18.387', NULL, NULL)
INSERT INTO tblLocalidades (IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (6, 1, 6, 'Pica Piedra', '', 1, 'bryan', '2020-04-05 14:41:06.373', NULL, NULL, NULL, NULL)

--Nuevos catalogos
	declare @codigo1 varchar(50)= 'cod1'
	declare @codigo2 varchar(50)= 'cod2'
	declare @codigo3 varchar(50)= 'cod3'

INSERT INTO tblOcupaciones(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
	VALUES	('Mecanico', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Programador', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Agricultor', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

--VERIFICADORES DE DIRECCIONES
INSERT INTO tblVerificadorDirecciones(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
	VALUES	('Pedro Pizarro', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Mario Hadut', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Lidia Perez', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

--Tipo de telefonos
INSERT INTO tblTipoTelefonos(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
	VALUES	('Casa', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Oficina', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Personal', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

-- Tipo de sexos
INSERT INTO tblTipoSexos(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado)
	VALUES	('Mujer', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Hombre', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Sin sexo', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

-- Tipo de sexos
INSERT INTO tblTasadores(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
	VALUES	('Michael', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Denzel', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Estarlin', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

INSERT INTO tblLocalizadores(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
	VALUES	('Jose', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Juan', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Pedro', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

INSERT INTO tblEstadosCiviles(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
	VALUES	('Soltero', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Casado', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Viudo\a', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)
--Fin de nuevos catalogos punto 66

INSERT INTO tblRoles(Nombre, Descripcion)
	VALUES	('Contador', 'Role para contadores de PCP Prog')

INSERT INTO tblOperaciones(Nombre, Descripcion, Grupo, Codigo)
	VALUES	('Ver tasa interes', 'Con este permiso podra ver todas las tasas de interes', 1, 'tasainteres-ver'),
			('Crear tasa interes', 'Con este permiso podra crear tasas de interes', 1, 'tasainteres-crear'),
			('Editar tasa interes', 'Con este permiso podra editar todas las tasas de interes', 1, 'tasainteres-editar'),
			('Borrar tasa interes', 'Con este permiso podra borrar todas las tasas de interes', 1, 'tasainteres-anular'),
			('Desactivar tasa interes', 'Con este permiso podra desactivar todas las tasas de interes', 1, 'tasainteres-desactivar'),
			('Ver moras', 'Con este permiso podra ver todas las moras', 2, 'moras-ver'),
			('Crear moras', 'Con este permiso podra crear moras', 2, 'moras-crear'),
			('Editar moras', 'Con este permiso podra editar todas las moras', 2, 'moras-editar'),
			('Borrar moras', 'Con este permiso podra borrar todas las moras', 2, 'moras-anular'),
			('Desactivar moras', 'Con este permiso podra desactivar todas las moras', 2, 'moras-desactivar'),
			('Reporte de ventas', 'Con este permiso podra ver el reporte de ventas', 3, 'reporteventas-ver'),
			('Aplicar descuento', 'Con este permiso podra aplicar descuento a los prestamos', 4, 'aplicardescuento-crear')

INSERT INTO tblUsersRoles(IdUser, IdRole, InsertadoPor)
	VALUES	(2, 1, @usuario)

INSERT INTO tblRolesOperaciones(IdOperacion, IdRole, InsertadoPor)
	VALUES	(1, 1, @usuario)
declare @prestamoNumero varchar(20)
exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output

-- tblPrestamos
INSERT INTO tblPrestamos (idNegocio, idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (1, 1, @prestamoNumero, NULL, 0, 1, 1, '2020-05-17', '2020-05-17', '2020-10-17', 3, 3, 4, 5, 10000, 1, 0, 0, 0, 0, 0, 0, '1900-01-01', '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL)


-- seccion de prestamos-cuotas-garantias
-- insertando prestamos
exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output
  INSERT INTO tblPrestamos (idNegocio, idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
  VALUES (1, 1, @prestamoNumero, NULL, 0, 1, 1, '2020-05-24', '2020-05-24', '2020-10-24', 1, 1, 3, 5, 10000, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-24 09:36:23.717', NULL, NULL, NULL, NULL)

exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output
INSERT INTO tblPrestamos (idNegocio, idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (1, 2, @prestamoNumero, NULL, 0, 1, 1, '2020-05-24', '2020-05-24', '2020-06-24', 1, 1, 4, 1, 10000, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-24 09:42:44.657', NULL, NULL, NULL, NULL)

exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output
INSERT INTO tblPrestamos (idNegocio, idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, 
IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, 
idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, 
FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, 
ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (1, 3, @prestamoNumero, NULL, 0, 1, 1, '2020-05-24', '2020-05-24', '2020-08-24', 3, 1, 3, 3, 3000,  1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-24 09:47:49.927', NULL, NULL, NULL, NULL)

exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output
INSERT INTO tblPrestamos (idNegocio, idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (1, 1, @prestamoNumero, NULL, 0, 1, 1, '2020-05-25', '2020-05-25', '2020-09-25', 3, 1, 4, 4, 10000, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-25 06:30:30.257', NULL, NULL, NULL, NULL)

exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output
INSERT INTO tblPrestamos (idNegocio, idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (1, 2, @prestamoNumero, NULL, 0, 3, 1, '2020-05-25', '2020-05-25', '2020-06-25', 1, 1, 1, 1, 500, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-25 06:36:24.1', NULL, NULL, NULL, NULL)

exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output
INSERT INTO tblPrestamos (idNegocio, idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (1, 3, @prestamoNumero, NULL, 0,3, 1, '2020-05-25', '2020-05-25', '2020-06-25', 1, 1, 1, 1, 500, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-25 06:37:54.643', NULL, NULL, NULL, NULL)

exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero output
INSERT INTO tblPrestamos (idNegocio, idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, idDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado)
VALUES (1, 1, @prestamoNumero, NULL, 0, 1, 1, '2020-05-25', '2020-05-25', '2020-06-25', 1, 1, 1, 1,100, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-25 06:39:30.46', NULL, NULL, NULL, NULL)

--insertando garantias a los prestamos
--INSERT INTO tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (1, 1, '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL)
INSERT INTO tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (1, 1, '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL),
	   (1, 7, '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL)


INSERT INTO tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (2, 2, '', '2020-05-24 09:47:49.927', NULL, NULL, NULL, NULL)

INSERT INTO tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado)
VALUES (3, 3, '', '2020-05-25 06:30:30.26', NULL, NULL, NULL, NULL)

INSERT INTO tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (4, 4, '', '2020-05-25 06:36:24.11', NULL, NULL, NULL, NULL),
	   (4, 8, '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL)

INSERT INTO tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (5, 5, '', '2020-05-25 06:39:30.46', NULL, NULL, NULL, NULL)


INSERT INTO tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (6, 6, '', '2020-05-25 06:39:30.46', NULL, NULL, NULL, NULL)


--insertando cuotas a los prestamos

--tblCuotas
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (1, 1, '2020-06-17', 2000, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (1, 2, '2020-07-17', 2000, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (1, 3, '2020-08-17', 2000, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (1, 4, '2020-09-17', 2000, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (1, 5, '2020-10-17', 2000, 300)

INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (2, 1, '2020-06-17', 2000, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (2, 2, '2020-07-17', 2000, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (2, 3, '2020-08-17', 2000, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (2, 4, '2020-09-17', 2000, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (2, 5, '2020-10-17', 2000, 300)

INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (3, 1, '2020-06-24', 10000, 100)

INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (4, 1, '2020-06-24', 1000, 90)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (4, 2, '2020-07-24', 1000, 90)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (4, 3, '2020-08-24', 1000, 90)

INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (5, 1, '2020-06-25', 2500, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (5, 2, '2020-07-25', 2500, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (5, 3, '2020-08-25', 2500, 300)
INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (5, 4, '2020-09-25', 2500, 300)

INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (6, 1, '2020-06-25', 500, 5)

INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, Interes) VALUES (7, 1, '2020-06-25', 500, 5)








