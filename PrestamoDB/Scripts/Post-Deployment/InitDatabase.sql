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
	declare @idLocalidadNegocio int = 1
	
	--Script para datos de Negocios
	--Negocio Matriz y otras truncales no permite realizar transacciones
	insert into dbo.tblNegocios
			(Codigo,NombreJuridico,NombreComercial,InsertadoPor,FechaInsertado, TaxIdNo, OtrosDetalles, logo)
			VALUES
			('INT-M','Intagsa SRL','Intagsa',@usuario,getdate(), '1','','Papito.png')

	
	INSERT INTO dbo.tblLocalidadesNegocio (Codigo, NombreComercial, NombreJuridico, Activo, InsertadoPor, FechaInsertado )
	VALUES ('001','Intagsa Romana','Intagsa SRL', 1, 'sdsd',getdate())


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

	INSERT INTO [dbo].[tblTasasInteres] ([idNegocio], idLocalidadNegocio, [Codigo],[Nombre],[InteresMensual],[InsertadoPor],[FechaInsertado])
     VALUES
		   (@idNegocio,1,'A00', '1% de interes' ,1.0,@usuario,getdate()),
		   (@idNegocio,1,'B00', '2% de interes' ,2.0,@usuario,getdate()),
		   (@idNegocio,1,'C00', '3% de interes' ,3.0,@usuario,getdate()),
		   (@idNegocio,1,'D00', '4% de interes' ,4.0,@usuario,getdate()),
		   (@idNegocio,1,'E00', '5% de interes' ,5.0,@usuario,getdate()),
		   (@idNegocio,1,'F00', '6% de interes' ,6.0,@usuario,getdate()),
		   (@idNegocio,1,'G00', '7% de interes' ,7.0,@usuario,getdate()),
		   (@idNegocio,1,'H00', '8% de interes' ,8.0,@usuario,getdate()),
		   (@idNegocio,1,'I00', '9% de interes' ,9.0,@usuario,getdate()),
		   (@idNegocio,1,'J00', '10% de interes' ,10.0,@usuario,getdate()),
		   (@idNegocio,1,'K00', '11% de interes' ,11.0,@usuario,getdate()),
		   (@idNegocio,1,'L00', '12% de interes' ,12.0,@usuario,getdate()),
		   (@idNegocio,1,'M00', '13% de interes' ,13.0,@usuario,getdate()),
		   (@idNegocio,1,'N00', '143% de interes' ,14.0,@usuario,getdate()),
		   (@idNegocio,1,'O00', '15% de interes' ,15.0,@usuario,getdate()),
		   (@idNegocio,1,'P00', '16% de interes' ,16.0,@usuario,getdate()),
		   (@idNegocio,1,'Q00', '17% de interes' ,17.0,@usuario,getdate()),
		   (@idNegocio,1,'R00', '18% de interes' ,18.0,@usuario,getdate()),
		   (@idNegocio,1,'S00', '19% de interes' ,19.0,@usuario,getdate()),
		   (@idNegocio,1,'T00', '20% de interes' ,20.0,@usuario,getdate())
		   
	-- tblClasificaciones
		   INSERT INTO [dbo].[tblClasificaciones]
           ([Nombre]
           ,[IdNegocio]
		   ,IdLocalidadNegocio
           ,[IdClasificacionFinanciera]
		   ,RequiereAutorizacion
		   ,RequiereGarantia
           ,[Codigo]
           ,[InsertadoPor]
           ,[FechaInsertado]
		   )
		VALUES
           ('Con Vehiculos' ,@idNegocio,1 ,1 ,0,1,'Vehic' ,@usuario,getDate()),
		   ('Con Motocicletas' ,@idNegocio,1 ,1,0,1,'Mot' ,@usuario,getDate()),
		   ('Con Casa' ,@idNegocio,1 ,2,0,1 ,'Casa',@usuario,getDate()),
		   ('Personal' ,@idNegocio,1 ,2 ,1,0,'Personal' ,@usuario,getDate())
    --Script para periodos
	insert into tblPeriodos (IdNegocio, IdLocalidadNegocio, IdPeriodoBase,Codigo, MultiploPeriodoBase,Nombre, insertadoPor, fechaInsertado) values
		(1,1,1,'DIA',1,'Cuotas Diarias','seed', getdate()), 
		(1,1,2,'SEM',1,'Cuotas Semanales', 'seed', getdate()),
		(1,1,3,'QUI',1,'Cuotas Quincenales','seed', getdate()), 
		(1,1,4,'MES',1,'Cuotas Mensuales','seed', getdate()), 
		(1,1,1,'INTD',2,'Cuotas Inter Diario','seed', getdate()), 
		(1,1,4,'BIMES',2,'Cuotas BiMensuales','seed', getdate()) 
	--Script para datos de Moras
	insert into tblTiposMora
			(Nombre,
			idNegocio, IdLocalidadNegocio,Codigo,DiasDeGracia, CalcularCargoPor, AplicarA,TipoCargo,MontoOPorCientoACargar,InsertadoPor, FechaInsertado)
		VALUES
			('Porcentual 10% al interes y capital atrasado por cada dia por cada cuota',
			@idNegocio,@IdLocalidadNegocio,'P10IC',3,1,1,1,10.00,@usuario,getdate()),
			('Porcentual 5% al interes y capital atrasado por cada dia por cada cuota',
			@idNegocio,@IdLocalidadNegocio,'P05IC',3,1,1,1,10.00,@usuario,getdate()),
			('Porcentual 10% al interes atrasado por cada dia por cada cuota',
			@idNegocio,@IdLocalidadNegocio,'P10I',3,2,1,1,10.00,@usuario,getdate()),
			('Porcentual 5% al interes  atrasado por cada dia por cada cuota',
			@idNegocio,@IdLocalidadNegocio,'P05I',3,2,1,1,10.00,@usuario,getdate()),
			('Acumulada 10% por total atrasado',
			@idNegocio,@IdLocalidadNegocio,'A10IC',3,3,1,1,10.00,@usuario,getdate()),
			('Acumulada 10% por total atrasado',
			@idNegocio,@IdLocalidadNegocio,'A05IC',3,3,1,1,5.00,@usuario,getdate()),
			('Lineal 10 pesos por cada dia atrasado',
			@idNegocio,@IdLocalidadNegocio,'L05',3,3,1,1,5.00,@usuario,getdate())

--@idnegocio, @loginname, @nombrerealcompleto, @contraseña, @debecambiarcontraseñaaliniciarsesion,
--@iniciovigenciacontraseña, @telefono1, @telefono2, @activo, @bloqueado, @correoelectronico, 
--@esempleado, @imgfilepath, @vigentedesde, @vigentehasta, @contraseñaexpiracadaxmes, @razonbloqueo, @idpersonal, 
--@insertadopor, @fechainsertado, @modificadopor, @fechamodificado, @Borradopor, @fechaBorrado)
	
-- Table:  tblTipos
-- Date:   13-Feb-20 5:46 AM

	
	-- Table:  tblMarcas
	INSERT INTO tblMarcas (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
	VALUES	('Toyota', 1, '', 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
	   		('Ford', 1, '', 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
			('Suzuki', 1, '', 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

	-- Table:  tblModelos
INSERT INTO tblModelos (IdMarca, Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
VALUES	(1, 'Camry', 1, '', 1, @usuario, '2020-02-13 05:16:43.067', NULL, NULL, NULL, NULL),
		(1, 'Corolla', 1, '', 1, @usuario, '2020-02-13 05:16:49.09', NULL, NULL, NULL, NULL),
		(2, 'Explorer', 1, '', 1, @usuario, '2020-02-13 05:16:57.657', NULL, NULL, NULL, NULL),
		(2, 'Focus', 1, '', 1, @usuario, '2020-02-13 05:17:06.52', NULL, NULL, NULL, NULL),
		(2, 'Escape', 1, '', 1, @usuario, '2020-02-13 05:17:15.153', NULL, NULL, NULL, NULL),
		(3, 'ax-100', 1, '', 1, @usuario, '2020-02-13 05:21:06.917', NULL, NULL, NULL, NULL)

	
	-- Table:  tblColores
-- Date:   13-Feb-20 5:55 AM

INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
VALUES	('Blanco', 1, null, 1, @usuario, '2020-02-13 05:17:52.627', NULL, NULL, NULL, NULL),
		('Negro', 1, null, 1, @usuario, '2020-02-13 05:17:59.683', NULL, NULL, NULL, NULL),
		('Gris', 1, null, 1, @usuario, '2020-02-13 05:18:09.43', NULL, NULL, NULL, NULL),
		('Azul', 1, null, 1, @usuario, '2020-02-13 05:18:14.913', NULL, NULL, NULL, NULL),
		('Rojo', 1, null, 1, @usuario, '2020-02-13 05:18:20.473', NULL, NULL, NULL, NULL),
		('Amarillo/Rojo', 1, null, 1, @usuario, '2020-02-13 05:18:32.4', NULL, NULL, NULL, NULL),
		('Verde', 1, null, 1, @usuario, '2020-02-13 05:18:37.657', NULL, NULL, NULL, NULL),
		('Verde/Blanco', 1, null, 1, @usuario, '2020-02-13 05:18:44.857', NULL, NULL, NULL, NULL)


		-- Table: Garantias

-- Table:  tblTipos
-- Date:   13-Feb-20 5:46 AM

INSERT INTO tblTiposGarantia (IdClasificacion, Nombre, Codigo, Activo, IdNegocio, IdLocalidadNegocio, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado)
VALUES	(2, 'Vehiculos (Carros, Camionetas, Geepetas, etc)', '', 1, @IdNegocio,@IdLocalidadNegocio, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(2, 'Motocicletas', '', 1,@IdNegocio,@IdLocalidadNegocio, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(1, 'Casa', '', 1,@IdNegocio,@IdLocalidadNegocio, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(1, 'Solar con Edificacion', '', 1,@IdNegocio,@IdLocalidadNegocio, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(1, 'Solar sin Edificacion', '', 1, @IdNegocio,@IdLocalidadNegocio, @usuario, getdate(), NULL, NULL, NULL, NULL)
		

	-- Table: Garantias
	INSERT INTO tblGarantias (IdClasificacion, IdTipoGarantia, IdModelo, IdMarca, NoIdentificacion, IdNegocio, Detalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
	VALUES 
	(2, 1, 1, 1, '2626', 1, '{"Color":"2","NoMaquina":"nm2626","Ano":"2001","Placa":"p2626","Matricula":"ma2626","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":"Vehiculo usado esta bien cuidado","InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:20:04.95', NULL, NULL, NULL, NULL),
	(2, 2, 2, 3, '2727', 1, '{"Color":"2","NoMaquina":"nm2727","Ano":"2006","Placa":"p2727","Matricula":"ma2727","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 3, 3, '2929', 1, '{"Color":"2","NoMaquina":"nm2929","Ano":"2007","Placa":"p2929","Matricula":"ma2929","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 2, '3030', 1, '{"Color":"2","NoMaquina":"nm3030","Ano":"2007","Placa":"p3030","Matricula":"ma3030","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	--(1, 1, null, null, 'casaAbdiel', 1, '{"Color":"","NoMaquina":"","Ano":"","Placa":"","Matricula":"","IdLocalidad":1,"DetallesDireccion": calle serapia no 3 las","Medida":"380","UsoExclusivo":true,"Descripcion":null,"InsertadoPor":"seed","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 1, '3131', 1, '{"Color":"2","NoMaquina":"nm3131","Ano":"2007","Placa":"p3131","Matricula":"ma3131","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 2, '3232', 1, '{"Color":"2","NoMaquina":"nm3232","Ano":"2007","Placa":"p3232","Matricula":"ma3232","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 2, '3333', 1, '{"Color":"2","NoMaquina":"nm3333","Ano":"2007","Placa":"p3333","Matricula":"ma3333","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL),
	(2, 2, 4, 2, '6363', 1, '{"Color":"2","NoMaquina":"nm6363","Ano":"2007","Placa":"p6363","Matricula":"ma6363","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL)
	-- 9 garantias	


-- Table:  tblClientes
-- Date:   13-Feb-20 5:52 AM

declare @codigo varchar(20)
exec dbo.spGenerarSecuenciaString 'Codigo de Clientes',10,1, @codigo output
INSERT INTO tblClientes (Activo, BorradoPor, Apodo, Apellidos, Codigo, IdEstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaBorrado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, IdSexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, InfoReferencias, imagen1FileName, imagen2FileName, idLocalidadNegocio) 
VALUES (1, NULL, 'yeyo', 'Taveras', @codigo, 2, '1969-07-31', '2020-02-13 05:13:26.13', '2020-02-13 05:11:00.03', NULL, 1, 1, 2, '{"Nombres":"YOCASTA","Apodo":"Yami","Apellidos":"RODRIGUEZ","NoTelefono1":"8299619140","LugarTrabajo":"Glipsy Novias","TelefonoTrabajo":"8098339140","DireccionLugarTrabajo":"SERAPIA NO 3","IdTipoIdentificacion":1,"NoIdentificacion":"02600667543","Notas":null}', '{"Nombre":"Pc Prog","Puesto":"Ingeniero de Sistemas","FechaInicio":"2020-02-13T00:00:00","NoTelefono1":"8095508455","NoTelefono2":"8098131251","Direccion":"General Grerio Luperon no 12","Notas":null}', '{"IdDireccion":0,"IdLocalidad":5,"Calle":"Serapia no 3 las Orquideas","CodigoPostal":"22000","CoordenadasGPS":null,"Detalles":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, @usuario, '02600679191', 'Abdiel', 2, '8098131438', '8299619141','', '[{"Tipo":1, "Vinculo":3, "NombreCompleto":"Hola", "Telefono":"fg", "Direccion":"dfg", "Detalles":"Hola"}]','imagen1.jpeg','imagen2.jpeg',1)

exec dbo.spGenerarSecuenciaString 'Codigo de Clientes',10,1, @codigo output
INSERT INTO tblClientes (Activo, BorradoPor, Apodo, Apellidos, Codigo, IdEstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaBorrado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, IdSexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, InfoReferencias, imagen1FileName, imagen2FileName, IdLocalidadNegocio) 
VALUES (1, NULL, 'benja', 'Taveras', @codigo, 2, '1969-07-31', '2020-02-13 05:13:26.13', '2020-02-13 05:11:00.03', NULL, 1, 1, 2, '{"Nombres":"ANA MARIA","Apodo":"Ana","Apellidos":"RODRIGUEZ","NoTelefono1":"8299619142","LugarTrabajo":"Intagsa ","TelefonoTrabajo":"8098131719","DireccionLugarTrabajo":"SERAPIA NO 3","IdTipoIdentificacion":1,"NoIdentificacion":"02600667544","Notas":null}', '{"Nombre":"Pc Prog","Puesto":"Ingeniero de Sistemas","FechaInicio":"2020-02-13T00:00:00","NoTelefono1":"8095508455","NoTelefono2":"8098131251","Direccion":"General Grerio Luperon no 12","Notas":null}', '{"IdDireccion":0,"IdLocalidad":5,"Calle":"Serapia no 3 las Orquideas","CodigoPostal":"22000","CoordenadasGPS":null,"Detalles":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, @usuario, '02600679192', 'Benjamin', 1, '8098131438', '8299619141','', '[{"Tipo":1, "Vinculo":3, "NombreCompleto":"Hola", "Telefono":"fg", "Direccion":"dfg", "Detalles":"Hola"}]','imagen1.jpeg','imagen2.jpeg',1)

exec dbo.spGenerarSecuenciaString 'Codigo de Clientes',10,1, @codigo output
INSERT INTO tblClientes (Activo, BorradoPor, Apodo, Apellidos, Codigo, IdEstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaBorrado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, IdSexo, TelefonoCasa, TelefonoMovil, CorreoElectronico, InfoReferencias, imagen1FileName, imagen2FileName, IdLocalidadNegocio) 
VALUES (1, NULL, 'ernesto', 'Tejeda', @codigo, 2, '1969-07-31', '2020-02-13 05:13:26.13', '2020-02-13 05:11:00.03', NULL, 1, 1, 2, '{"Nombres":"Maria","Apodo":"Maria","Apellidos":"Guerrero","NoTelefono1":"8299619142","LugarTrabajo":"Intagsa ","TelefonoTrabajo":"8098131719","DireccionLugarTrabajo":"SERAPIA NO 3","IdTipoIdentificacion":1,"NoIdentificacion":"02600667548","Notas":null}', '{"Nombre":"Pc Prog","Puesto":"Ingeniero de Sistemas","FechaInicio":"2020-02-13T00:00:00","NoTelefono1":"8095508455","NoTelefono2":"8098131251","Direccion":"General Grerio Luperon no 12","Notas":null}', '{"IdDireccion":0,"IdLocalidad":5,"Calle":"serapia no 3 las Orquideas","CodigoPostal":"22000","CoordenadasGPS":null,"Detalles":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","BorradoPor":"","FechaBorrado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', @usuario, @usuario, '0260067914', 'Ernesto', 1, '8098131438', '8299619141','', '[{"Tipo":1, "Vinculo":3, "NombreCompleto":"Hola", "Telefono":"fg", "Direccion":"dfg", "Detalles":"Hola"}]','imagen1.jpeg','imagen2.jpeg',1)
-- Server: ABDIELALIENWARE\SQLEXPRESS2016
-- Date:   13-Feb-20 5:53 AM
-- Table:  tblDivisionTerritorial

INSERT INTO tblDivisionTerritorial(IdDivisionTerritorialPadre, IdNegocio, Nombre, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
VALUES ( null, 1, 'Division Territorial tipo Republica Dominicana',  1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(1, 1, 'Pais',  1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(2, 1, 'Provincia', 1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(3, 1, 'Municipio',  1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(4, 1, 'Sector',  1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(5, 1, 'Distrito Municipal', 1, 0, @usuario, getdate(), NULL, NULL, NULL, NULL),
		(6, 1, 'Sector',  1, 1, @usuario, getdate(), NULL, NULL, NULL, NULL)

--Localidades
INSERT INTO tblLocalidades (IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
VALUES (0, 1, 2, 'Republica Dominicana', '', 1, 'bryan', '2020-04-05 14:01:56.643', NULL, NULL, NULL, NULL),
	   (1, 1, 3, 'La Romana', '', 1, 'bryan', '2020-04-05 14:02:14.683', NULL, NULL, NULL, NULL),
	   (1, 1, 3, 'San Pedro', '', 1, 'bryan', '2020-04-05 14:02:32.96', NULL, NULL, NULL, NULL),
	   (2, 1, 4, 'La Romana', '', 1, 'bryan', '2020-04-05 14:02:46.863', NULL, NULL, NULL, NULL),
	   (4, 1, 5, 'Las Orquideas', '', 1, 'bryan', '2020-04-05 14:40:19.337', NULL, NULL, NULL, NULL),
	   (4, 1, 5, 'Quisquella', '', 1, 'bryan', '2020-04-05 14:40:19.337', NULL, NULL, NULL, NULL),
	   (4, 1, 5, 'Villa Pereira', '', 1, 'bryan', '2020-04-05 14:40:19.337', NULL, NULL, NULL, NULL),
	   (4, 1, 5, 'Villa España', '', 1, 'bryan', '2020-04-05 14:40:19.337', NULL, NULL, NULL, NULL),
	   (2, 1, 4, 'Villa Hermosa', '', 1, 'bryan', '2020-04-05 14:40:42.557',null,null,null,null),
	   (9, 1, 5, 'Pica Piedra', '', 1, 'bryan', '2020-04-05 14:41:06.373', NULL, NULL, NULL, NULL)

--Nuevos catalogos
	declare @codigo1 varchar(50)= 'cod1'
	declare @codigo2 varchar(50)= 'cod2'
	declare @codigo3 varchar(50)= 'cod3'

INSERT INTO tblOcupaciones(Nombre, IdNegocio,IdLocalidadNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
	VALUES	('Mecanico', 1,1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Programador', 1,1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Agricultor', 1,1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

--VERIFICADORES DE DIRECCIONES
INSERT INTO tblVerificadorDirecciones(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
	VALUES	('Pedro Pizarro', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Mario Hadut', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Lidia Perez', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

--Tipo de telefonos
INSERT INTO tblTipoTelefonos(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado, idLocalidadNegocio) 
	VALUES	('Casa', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL,1),
	   		('Oficina', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL,1),
			('Personal', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL,1)

-- Tipo de sexos
INSERT INTO tblTipoSexos (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado,IdlocalidadNegocio )
	VALUES	('Mujer', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL,1),
	   		('Hombre', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL,1),
			('Sin sexo', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL,1)

-- tasadores
INSERT INTO tblTasadores(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
	VALUES	('Michael', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Denzel', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Estarlin', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

INSERT INTO tblLocalizadores(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
	VALUES	('Jose', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Juan', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Pedro', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)

INSERT INTO tblEstadosCiviles(Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
	VALUES	('Soltero', 1, @codigo1, 1, @usuario, '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL),
	   		('Casado', 1, @codigo2, 1, @usuario, '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL),
			('Viudo\a', 1, @codigo3, 1, @usuario, '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)
--Fin de nuevos catalogos punto 66
declare @prestamoNumero1 varchar(20), @prestamoNumero2 varchar(20), @prestamoNumero3 varchar(20),
	@prestamoNumero4 varchar(20), @prestamoNumero5 varchar(20), @prestamoNumero6 varchar(20),
	@prestamoNumero7 varchar(20), @prestamoNumero8 varchar(20)

exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero1 output
exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero2 output
exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero3 output
exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero4 output
exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero5 output
exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero6 output
exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero7 output
exec dbo.spGenerarSecuenciaString 'Numero de Prestamo',10,1, @prestamoNumero8 output

-- tblPrestamos
INSERT INTO tblPrestamos (idNegocio,idlocalidadNegocio,  idCliente, prestamoNumero, IdPrestamoARenovar, DeudaRenovacion, idClasificacion, IdTipoAmortizacion, FechaEmisionReal, FechaEmisionParaCalculo, FechaVencimiento, IdTasaInteres, idTipoMora, idPeriodo, CantidadDePeriodos, MontoPrestado, IdDivisa, InteresGastoDeCierre, MontoGastoDeCierre, GastoDeCierreEsDeducible, CargarInteresAlGastoDeCierre, FinanciarGastoDeCierre, AcomodarFechaALasCuotas, FechaInicioPrimeraCuota, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
	VALUES (1,1, 1, @prestamoNumero1, NULL, 0, 1, 1, '2020-05-17', '2020-05-17', '2020-10-17', 3, 3, 4, 5, 10000, 1, 0, 0, 0, 0, 0, 0, '1900-01-01', '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL),


-- seccion de prestamos-cuotas-garantias
-- insertando prestamos

   (1, 1,1, @prestamoNumero2, NULL, 0, 1, 1, '2020-05-24', '2020-05-24', '2020-10-24', 1, 1, 3, 5, 10000, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-24 09:36:23.717', NULL, NULL, NULL, NULL),


	 (1,1, 2, @prestamoNumero3, NULL, 0, 1, 1, '2020-05-24', '2020-05-24', '2020-06-24', 1, 1, 4, 1, 10000, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-24 09:42:44.657', NULL, NULL, NULL, NULL),

	 (1,1, 3, @prestamoNumero4, NULL, 0, 1, 1, '2020-05-24', '2020-05-24', '2020-08-24', 3, 1, 3, 3, 3000,  1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-24 09:47:49.927', NULL, NULL, NULL, NULL),

	(1,1, 1, @prestamoNumero5, NULL, 0, 1, 1, '2020-05-25', '2020-05-25', '2020-09-25', 3, 1, 4, 4, 10000, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-25 06:30:30.257', NULL, NULL, NULL, NULL),

	(1, 1,2, @prestamoNumero6, NULL, 0, 3, 1, '2020-05-25', '2020-05-25', '2020-06-25', 1, 1, 1, 1, 500, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-25 06:36:24.1', NULL, NULL, NULL, NULL),

	 (1, 1,3, @prestamoNumero7, NULL, 0,3, 1, '2020-05-25', '2020-05-25', '2020-06-25', 1, 1, 1, 1, 500, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-25 06:37:54.643', NULL, NULL, NULL, NULL),


	(1, 1,1, @prestamoNumero8, NULL, 0, 1, 1, '2020-05-25', '2020-05-25', '2020-06-25', 1, 1, 1, 1,100, 1, 0, 0, 0, 1, 1, 0, '1900-01-01', '', '2020-05-25 06:39:30.46', NULL, NULL, NULL, NULL)

--insertando garantias a los prestamos
--INSERT INTO tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) VALUES (1, 1, '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL)
INSERT INTO tblPrestamoGarantias (IdPrestamo, IdGarantia, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, BorradoPor, FechaBorrado) 
VALUES (1, 1, '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL),
	   (1, 7, '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL),

		(2, 2, '', '2020-05-24 09:47:49.927', NULL, NULL, NULL, NULL),

		(3, 3, '', '2020-05-25 06:30:30.26', NULL, NULL, NULL, NULL),

	   (4, 4, '', '2020-05-25 06:36:24.11', NULL, NULL, NULL, NULL),
	   (4, 8, '', '2020-05-17 22:24:20', NULL, NULL, NULL, NULL),

		(5, 5, '', '2020-05-25 06:39:30.46', NULL, NULL, NULL, NULL),

	(6, 6, '', '2020-05-25 06:39:30.46', NULL, NULL, NULL, NULL)


--insertando cuotas a los prestamos

--tblCuotas

INSERT INTO tblCuotas (IdPrestamo, Numero, Fecha, Capital, BceCapital, Interes, BceInteres) VALUES 
(1, 1, '2020-06-17', 2000,2000, 300,300),
(1, 2, '2020-07-17', 2000,2000, 300, 300),
(1, 3, '2020-08-17', 2000,2000, 300, 300),
(1, 4, '2020-09-17', 2000,2000, 300,300),
(1, 5, '2020-10-17', 2000,2000, 300, 300),

(2, 1, '2020-06-17', 2000,2000, 300, 300),
(2, 2, '2020-07-17', 2000,2000, 300, 300),
(2, 3, '2020-08-17', 2000,2000, 300, 300),
(2, 4, '2020-09-17', 2000,2000, 300, 300),

(2, 5, '2020-10-17', 2000,2000, 300, 300),

(3, 1, '2020-06-24', 10000,10000, 100, 100 ),

(4, 1, '2020-06-24', 1000,1000, 90, 90),
(4, 2, '2020-07-24', 1000,1000, 90, 90),
(4, 3, '2020-08-24', 1000,1000, 90, 90),

(5, 1, '2020-06-25', 2500, 2500, 300,300),
(5, 2, '2020-07-25', 2500,2500, 300, 300),
(5, 3, '2020-08-25', 2500,2500, 300, 300),
(5, 4, '2020-09-25', 2500,2500, 300, 300),

(6, 1, '2020-06-25', 500,500, 5, 5),

(7, 1, '2020-06-25', 500,500, 5, 5)


--CREATE UNIQUE NONCLUSTERED INDEX [UniqueCodigoOcupacionExceptNulls]
--ON [TblOcupaciones] (Codigo)
--WHERE [Codigo] IS NOT NULL

--CREATE UNIQUE NONCLUSTERED INDEX [UniqueCodigoColorExceptNulls]
--ON [TblColores] (Codigo)
--WHERE [Codigo] IS NOT NULL