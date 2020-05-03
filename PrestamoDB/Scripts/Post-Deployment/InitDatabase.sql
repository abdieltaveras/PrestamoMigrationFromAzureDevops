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
	DO NOT USE GO, because is going to remove from memory @usuario variable
*/
	declare @usuario varchar(50)= 'SeedDBUser'
	
	
	--Script para datos de Negocios
	--Negocio Matriz y otras truncales no permite realizar transacciones
	insert into dbo.tblNegocios
			(Codigo,NombreJuridico,NombreComercial,InsertadoPor,FechaInsertado, TaxIdNo, OtrosDetalles, logo, PermitirOperaciones, IdNegocioPadre)
			VALUES
			('INT-M','Intagsa SRL','Intagsa',@usuario,getdate(), '1','','Papito.png',0,null),
			('INTR','Intagsa SRL','Romana ',@usuario,getdate(), '2','','Papito.png',0,1),
			('INTS','Intagsa SRL','Sosua ',@usuario,getdate(), '2','','Papito.png',0,1),
			('PAPITO-M','Inversiones Bancola','Papito Prestamo',@usuario,getdate(), '1','','Papito.png',0,null),
			('FYG','Consorcio Empresarial FYG','FYG',@usuario,getdate(), '2','','Papito.png',0,null)

	--Negocios para realizar transacciones
	INSERT INTO tblNegocios (Codigo, NombreJuridico, NombreComercial, CorreoElectronico, Activo, Bloqueado, idNegocioPadre, TaxIdNo, OtrosDetalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, logo, PermitirOperaciones) 
			VALUES 
			('INTRP','Intagsa SRL', 'Romana Prestamo', NULL, 1, 0, 2, '1', '', 'SeedDBUser', '2020-04-05 14:59:21.693', NULL, NULL, NULL, NULL,'Papito.png',1),
			('INTRV', 'Intagsa SRL', 'Romana ventas', NULL, 1, 0, 2, '2', '', 'SeedDBUser', '2020-04-05 14:59:21.693', NULL, NULL, NULL, NULL,'Papito.png',1),
			('INTSP', 'Intagsa SRL', 'Sosua Prestamo', NULL, 1, 0, 3, '2', '', 'SeedDBUser', '2020-04-05 14:59:21.693', NULL, NULL, NULL, NULL,'Papito.png',1),
			('INTSV', 'Intagsa SRL', 'Sosua Prestamo', NULL, 1, 0, 3, '2', '', 'SeedDBUser', '2020-04-05 14:59:21.693', NULL, NULL, NULL, NULL,'Papito.png',1),
			('PAPITO-1', 'Inversiones Bancola', 'Papito Prestamo', NULL, 1, 0, 4, '2', '', 'SeedDBUser', '2020-04-05 14:59:21.693', NULL, NULL, NULL, NULL,'Papito.png',1),
			('FYG-1', 'Consorcio FYG', 'Consorcio FYG', NULL, 1, 0, 5, '2', '', 'SeedDBUser', '2020-04-05 14:59:21.693', NULL, NULL, NULL, NULL,'Papito.png',1),
			('AN', 'Aires del Norte', 'Aires del norte', NULL, 1, 0, 5, '2', '', 'SeedDBUser', '2020-04-05 14:59:21.693', NULL, NULL, NULL, NULL,'Papito.png',1)

--('N06', 'Hija 1 de Hija 1 Empresa 2 Srl', 'Hija 1 de Hija 1 Empresa no 2', NULL, 1, 0, 4, '2', '', 'SeedDBUser', '2020-04-05 14:59:21.693', NULL, NULL, NULL, NULL,'Papito.png')
	declare @idNegocio int = 1

	--Script para datos de Interes

	INSERT INTO [dbo].[tblTasasInteres] ([idNegocio],[Codigo],[Nombre],[InteresMensual],[InsertadoPor],[FechaInsertado])
     VALUES
		   (@idNegocio,'A00', '1% de interes' ,1.0,@usuario,getdate()),
		   (@idNegocio,'B00', '2% de interes' ,2.0,@usuario,getdate()),
		   (@idNegocio,'C00', '3% de interes' ,3.0,@usuario,getdate())

	--Script para datos de Moras
	insert into tblTiposMora
			(Nombre,
			idNegocio, Codigo,DiasDeGracia, CalcularCargoPor, AplicarA,TipoCargo,MontoOPorCientoACargar,InsertadoPor, FechaInsertado)
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
VALUES (2,'Admin','Usuario Administrador','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'809-550-8455',null,1,0,null,0,null,null,null,-1,null,null,@usuario,getdate(),null,null,null,null)

INSERT INTO dbo.tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado)
VALUES (2,'Usuario','Usuario por defecto','slT@idNegociocIeEDhgXsnLcD0w/ng==',0,null,'809-550-8455',null,1,0,null,0,null,null,null,-1,null,null,@usuario,getdate(),null,null,null,null)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (3, 'usrcc', 'Cambiar Contraseña', 'cu0+Y/VLEdwHc/4vxsazEQ==', 1, getdate(), '(809) 550-4678', '', 1, 0, '', 0, '', '2020-03-28', '1900-01-01', -1, -1, 0, 'admin', getdate(), NULL, NULL, NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (3, 'usrina', 'Usuario Inactivo', 'cu0+Y/VLEdwHc/4vxsazEQ==', 1, getdate(), '(809) 813-1719', '', 0, 0, '', 0, '', '2020-03-28', '1900-01-01', -1, -1, 0, 'admin', getdate(), NULL, NULL, NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (5, 'usrbl', 'usuario bloqueado', 'cu0+Y/VLEdwHc/4vxsazEQ==', 0, getdate(), '(809) 813-1719', '', 1, 1, '', 0, '', '2020-03-28', '1900-01-01', -1, -1, 0, 'admin', getdate(), NULL, NULL, NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (5, 'usrconex', 'usuario contrasena expira', 'cu0+Y/VLEdwHc/4vxsazEQ==', 0, getdate(), '(809) 550-2897', '', 1, 0, 'abdieltaveras@gmail.com', 0, '', '2020-03-28', '1900-01-01', 1, -1, 0, 'admin', getdate(), 'usrconex', '2020-03-30 08:35:59.4', NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (4, 'usrlivi', 'usuario limitar vigencia', 'cu0+Y/VLEdwHc/4vxsazEQ==', 0, getdate(), '(809) 550-4678', '', 1, 0, '', 0, '', '2020-03-28', '2020-03-29', -1, -1, 0, 'admin', getdate(), NULL, NULL, NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado, ImgFilePath, VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (4, 'success', 'Succes User', 'cu0+Y/VLEdwHc/4vxsazEQ==', 0, getdate(), '829-961-9141', '', 1, 0, 'abdieltaveras@hotmail.com', 0, '', '1900-01-01', '1900-01-01', 1, -1, 0, 'UsuarioTest', '2020-03-28 10:17:44.833', 'testUser29-Mar-20', '2020-03-29 22:15:53.987', NULL, NULL)

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, 
InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado,  VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath) 
values	(2,'bryan','Bryan Pouerie','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'829-973-4733',null,1,0,null,0,null,null,-1,null,null,
			@usuario,getdate(),null,null,null,null, 'Bryan01.png')

INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, 
InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado,  VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath) 
values	(10,'usrpapito','usr Papito-inv. Bancola','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'829-973-4733',null,1,0,null,0,null,null,-1,null,null,
			@usuario,getdate(),null,null,null,null, 'Bryan01.png')


INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, 
InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado,  VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath) 
values	(11,'usrfyg','usr fyg','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'829-973-4733',null,1,0,null,0,null,null,-1,null,null,
			@usuario,getdate(),null,null,null,null, 'Bryan01.png')


INSERT INTO tblUsuarios (IdNegocio, LoginName, NombreRealCompleto, Contraseña, DebeCambiarContraseñaAlIniciarSesion, 
InicioVigenciaContraseña, Telefono1, Telefono2, Activo, Bloqueado, CorreoElectronico, EsEmpleado,  VigenteDesde, VigenteHasta, ContraseñaExpiraCadaXMes, RazonBloqueo, IdPersonal, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado, ImgFilePath) 
values	(12,'usran','usr fyg','cu0+Y/VLEdwHc/4vxsazEQ==',0,null,'829-973-4733',null,1,0,null,0,null,null,-1,null,null,
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

	-- Table: Garantias
	INSERT INTO tblGarantias (IdClasificacion, IdTipoGarantia, IdModelo, IdMarca, NoIdentificacion, IdNegocio, Detalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
	VALUES 
	(2, 1, 1, 1, '2626', 1, '{"Color":"2","NoMaquina":"nm2626","Ano":"2001","Placa":"p2626","Matricula":"ma2626","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":"Vehiculo usado esta bien cuidado","InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:20:04.95', NULL, NULL, NULL, NULL),
	(2, 2, 6, 3, '2727', 1, '{"Color":"2","NoMaquina":"nm2727","Ano":"2006","Placa":"p2727","Matricula":"ma2727","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL)

-- Table:  tblTipos
-- Date:   13-Feb-20 5:46 AM

INSERT INTO tblTiposGarantia (IdClasificacion, Nombre, Codigo, Activo, IdNegocio, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado)
VALUES	(1, 'Vehiculos (Carros, Camionetas, Geepetas, etc)', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(1, 'Motocicletas', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(2, 'Casa', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(2, 'Solar con Edificacion', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(2, 'Solar sin Edificacion', '', 1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL)
		
-- Table:  tblClientes
-- Date:   13-Feb-20 5:52 AM

INSERT INTO tblClientes (Activo, AnuladoPor, Apodo, Apellidos, Codigo, EstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, Sexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, InfoReferencia) 
VALUES (1, NULL, 'yeyo', 'Taveras', '0D75CFAB-FE5D-4745-A488-982F64A3B87D', 2, '1969-07-31', '2020-02-13 05:13:26.13', '2020-02-13 05:11:00.03', NULL, 6, 1, 2, '{"Nombres":"YOCASTA","Apodo":"Yami","Apellidos":"RODRIGUEZ","NoTelefono1":"8299619140","LugarTrabajo":"Glipsy Novias","TelefonoTrabajo":"8098339140","DireccionLugarTrabajo":"CALLE SERAPIA NO 3","IdTipoIdentificacion":1,"NoIdentificacion":"02600667543","Notas":null}', '{"Nombre":"Pc Prog","Puesto":"Ingeniero de Sistemas","FechaInicio":"2020-02-13T00:00:00","NoTelefono1":"8095508455","NoTelefono2":"8098131251","Direccion":"General Grerio Luperon no 12","Notas":null}', '{"IdDireccion":0,"IdLocalidad":0,"Calle":"calle serapia no 3 las Orquideas","CodigoPostal":"22000","CoordenadasGPS":null,"Detalles":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, @usuario, '02600679191', 'Abdiel', 1, '8098131438', '8299619141','', '[{"Tipo":1, "Vinculo":3, "NombreCompleto":"Hola", "Telefono":"fg", "Direccion":"dfg", "Detalles":"Hola"}]')

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
