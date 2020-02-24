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
	declare @usuario varchar(50)= 'SeedDBUser'
	
	
	--Script para datos de Negocios
	insert into dbo.tblNegocios
			(Codigo,NombreComercial,NombreJuridico,InsertadoPor,FechaInsertado, TaxIdNo, OtrosDetalles)
			VALUES
			('N01','Empresa no 1','Empresa 1 SRL',@usuario,getdate(), '1',''),
			('N02','Empresa no 2','Empresa 2 Srl',@usuario,getdate(), '2','')

	declare @idNegocio int = (select top 1 idNegocio from tblNegocios)	

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
VALUES (1,'Usuario','Usuario por defecto','slT1cIeEDhgXsnLcD0w/ng==',0,null,'809-550-8455',null,1,0,null,0,null,null,null,-1,null,null,
			@usuario,getdate(),null,null,null,null)


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

INSERT INTO tblClientes (Activo, AnuladoPor, Apodo, Apellidos, Codigo, EstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, Sexo, TelefonoCasa, TelefonoMovil, CorreoElectronico) VALUES (1, NULL, 'yeyo', 'Taveras', '0D75CFAB-FE5D-4745-A488-982F64A3B87D', 2, '1969-07-31', '2020-02-13 05:13:26.13', '2020-02-13 05:11:00.03', NULL, 1, 1, 2, '{"Nombres":"YOCASTA","Apodo":"Yami","Apellidos":"RODRIGUEZ","NoTelefono1":"8299619140","LugarTrabajo":"Glipsy Novias","TelefonoTrabajo":"8098339140","DireccionLugarTrabajo":"CALLE SERAPIA NO 3","IdTipoIdentificacion":1,"NoIdentificacion":"02600667543","Notas":null}', '{"Nombre":"Pc Prog","Puesto":"Ingeniero de Sistemas","FechaInicio":"2020-02-13T00:00:00","NoTelefono1":"8095508455","NoTelefono2":"8098131251","Direccion":"General Grerio Luperon no 12","Notas":null}', '{"IdDireccion":0,"IdLocalidad":0,"Calle":"calle serapia no 3 las Orquideas","CodigoPostal":"22000","CoordenadasGPS":null,"Detalles":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, @usuario, '02600679191', 'Abdiel', 1, '8098131438', '8299619141', '')

-- Server: ABDIELALIENWARE\SQLEXPRESS2016
-- Date:   13-Feb-20 5:53 AM
-- Table:  tblTipoLocalidades

INSERT INTO tblTipoLocalidades (IdLocalidadPadre, IdDivisionTerritorial, IdNegocio, Nombre, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) 
VALUES (NULL, NULL, 1, 'Division Territorial Raiz (Nunca Borrar)',  1, 0, @usuario, '2020-02-13 04:15:54.953', NULL, NULL, NULL, NULL),
		(1, 0, 1, 'Division Territorial tipo Republica Dominicana',  1, 0, @usuario, '2020-02-13 04:16:17.46', NULL, NULL, NULL, NULL),
		(2, 2, 1, 'Pais',  1, 0, @usuario, '2020-02-13 04:16:30.96', NULL, NULL, NULL, NULL),
		(3, 2, 1, 'Provincia', 1, 0, @usuario, '2020-02-13 04:17:36.637', NULL, NULL, NULL, NULL),
		(4, 2, 1, 'Municipio',  1, 0, @usuario, '2020-02-13 04:17:50.617', NULL, NULL, NULL, NULL),
		(5, 2, 1, 'Sector',  1, 1, @usuario, '2020-02-13 04:25:33.913', NULL, NULL, NULL, NULL),
		(5, 2, 1, 'Distrito Municipal', 1, 0, @usuario, '2020-02-13 04:31:51.437', NULL, NULL, NULL, NULL),
		(7, 2, 1, 'Sector',  1, 1, @usuario, '2020-02-13 04:32:11.25', NULL, NULL, NULL, NULL)
go