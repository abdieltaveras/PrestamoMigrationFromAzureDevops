﻿/*
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
	--declare @usuario varchar(10)= 'SeedDBUser'
	:setvar usuario 'SeedDBUser'
	go
	--Script para datos de Negocios
	insert into dbo.tblNegocios
			(Codigo,NombreComercial,NombreJuridico,InsertadoPor,FechaInsertado, TaxIdNo, OtrosDetalles)
			VALUES
			('N01','Empresa no 1','Empresa 1 SRL',$(usuario),getdate(), '1',''),
			('N02','Empresa no 2','Empresa 2 Srl',$(usuario),getdate(), '2','')

	declare @idNegocio int = (select top 1 idNegocio from tblNegocios)	

	--Script para datos de Interes

	INSERT INTO [dbo].[tblTasasInteres]
           ([idNegocio]
           ,[Codigo]
		   ,[Nombre]
           ,[InteresMensual]
           ,[InsertadoPor]
           ,[FechaInsertado])
     VALUES
		   (@idNegocio,'A00', '1% de interes' ,1.0,$(usuario),getdate()),
		   (@idNegocio,'B00', '2% de interes' ,2.0,$(usuario),getdate()),
		   (@idNegocio,'C00', '3% de interes' ,3.0,$(usuario),getdate())

	--Script para datos de Moras
	insert into tblTiposMora
			(Nombre,
			idNegocio, Codigo,DiasDeGracia, CalcularCargoPor, AplicarA,TipoCargo,MontoOPorCientoACargar,InsertadoPor, FechaInsertado)
		VALUES
<<<<<<< HEAD
			('Porcentual 10% al interes y capital atrasado por cada dia por cada cuota',
			@idNegocio,'P10IC',3,1,1,1,10.00,$(usuario),getdate()),
			('Porcentual 5% al interes y capital atrasado por cada dia por cada cuota',
			@idNegocio,'P05IC',3,1,1,1,10.00,$(usuario),getdate()),

			('Porcentual 10% al interes atrasado por cada dia por cada cuota',
			@idNegocio,'P10I',3,2,1,1,10.00,$(usuario),getdate()),
			('Porcentual 5% al interes  atrasado por cada dia por cada cuota',
			@idNegocio,'P05I',3,2,1,1,10.00,$(usuario),getdate()),

			('Acumulada 10% por total atrasado',
			@idNegocio,'A10IC',3,3,1,1,10.00,$(usuario),getdate()),
			('Acumulada 10% por total atrasado',
			@idNegocio,'A05IC',3,3,1,1,5.00,$(usuario),getdate()),

			('Lineal 10 pesos por cada dia atrasado',
			@idNegocio,'L05',3,3,1,1,5.00,$(usuario),getdate())
	
	-- Table: Garantias
	INSERT INTO tblGarantias (IdClasificacion, IdTipo, IdModelo, IdMarca, NoIdentificacion, IdNegocio, Detalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (2, 1, 1, 1, '2626', 1, '{"Color":"2","NoMaquina":"nm2626","Año":"2001","Placa":"p2626","Matricula":"ma2626","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":"Vehiculo usado esta bien cuidado","InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', $(usuario), '2020-02-13 05:20:04.95', NULL, NULL, NULL, NULL)
	GO
	INSERT INTO tblGarantias (IdClasificacion, IdTipo, IdModelo, IdMarca, NoIdentificacion, IdNegocio, Detalles, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (2, 2, 6, 3, '2727', 1, '{"Color":"2","NoMaquina":"nm2727","Año":"2006","Placa":"p2727","Matricula":"ma2727","IdLocalidad":0,"DetallesDireccion":null,"Medida":null,"UsoExclusivo":false,"Descripcion":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', $(usuario), '2020-02-13 05:21:50.343', NULL, NULL, NULL, NULL)
	GO
	-- Table:  tblMarcas

	INSERT INTO tblMarcas (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Toyota', 1, '', 1, $(usuario), '2020-02-13 05:16:14.567', NULL, NULL, NULL, NULL)
	GO
	INSERT INTO tblMarcas (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Ford', 1, '', 1, $(usuario), '2020-02-13 05:16:21.607', NULL, NULL, NULL, NULL)
	GO
	INSERT INTO tblMarcas (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Suzuki', 1, '', 1, $(usuario), '2020-02-13 05:16:28.543', NULL, NULL, NULL, NULL)
	GO
	-- Table:  tblColores
-- Date:   13-Feb-20 5:55 AM

INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Blanco', 1, '', 1, $(usuario), '2020-02-13 05:17:52.627', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Negro', 1, '', 1, $(usuario), '2020-02-13 05:17:59.683', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Gris', 1, '', 1, $(usuario), '2020-02-13 05:18:09.43', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Azul', 1, '', 1, $(usuario), '2020-02-13 05:18:14.913', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Rojo', 1, '', 1, $(usuario), '2020-02-13 05:18:20.473', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Amarillo/Rojo', 1, '', 1, $(usuario), '2020-02-13 05:18:32.4', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Verde', 1, '', 1, $(usuario), '2020-02-13 05:18:37.657', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblColores (Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES ('Verde/Blanco', 1, '', 1, $(usuario), '2020-02-13 05:18:44.857', NULL, NULL, NULL, NULL)
GO

	
-- Server: ABDIELALIENWARE\SQLEXPRESS2016
-- Table:  tblTipos
-- Date:   13-Feb-20 5:46 AM

INSERT INTO tblTipos (IdClasificacion, Nombre, Codigo, Activo, IdNegocio, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (1, 'Vehiculos (Carros, Camionetas, Geepetas, etc)', '', 1, 1, $(usuario), '2020-02-13 05:15:50.703', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblTipos (IdClasificacion, Nombre, Codigo, Activo, IdNegocio, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (1, 'Motocicletas', '', 1, 1, $(usuario), '2020-02-13 05:16:02.82', NULL, NULL, NULL, NULL)
GO-- Server: ABDIELALIENWARE\SQLEXPRESS2016
-- Table:  tblClientes
-- Date:   13-Feb-20 5:52 AM

INSERT INTO tblClientes (Activo, AnuladoPor, Apodo, Apellidos, Codigo, EstadoCivil, FechaNacimiento, FechaModificado, FechaInsertado, FechaAnulado, IdNegocio, IdTipoIdentificacion, IdTipoProfesionUOcupacion, InfoConyuge, InfoLaboral, InfoDireccion, InsertadoPor, ModificadoPor, NoIdentificacion, Nombres, Sexo, TelefonoCasa, TelefonoMovil, CorreoElectronico) VALUES (1, NULL, 'yeyo', 'Taveras', '0D75CFAB-FE5D-4745-A488-982F64A3B87D', 2, '1969-07-31', '2020-02-13 05:13:26.13', '2020-02-13 05:11:00.03', NULL, 1, 1, 2, '{"Nombres":"YOCASTA","Apodo":"Yami","Apellidos":"RODRIGUEZ","NoTelefono1":"8299619140","LugarTrabajo":"Glipsy Novias","TelefonoTrabajo":"8098339140","DireccionLugarTrabajo":"CALLE SERAPIA NO 3","IdTipoIdentificacion":1,"NoIdentificacion":"02600667543","Notas":null}', '{"Nombre":"Pc Prog","Puesto":"Ingeniero de Sistemas","FechaInicio":"2020-02-13T00:00:00","NoTelefono1":"8095508455","NoTelefono2":"8098131251","Direccion":"General Gregorio Luperon no 12","Notas":null}', '{"IdDireccion":0,"IdLocalidad":0,"Calle":"calle serapia no 3 las Orquideas","CodigoPostal":"22000","CoordenadasGPS":null,"Detalles":null,"InsertadoPor":"","FechaInsertado":"1900-01-01T00:00:00","ModificadoPor":"","FechaModificado":"1900-01-01T00:00:00","AnuladoPor":"","FechaAnulado":"1900-01-01T00:00:00","IdNegocio":-1,"Usuario":""}', $(usuario), $(usuario), '02600679191', 'Abdiel', 1, '8098131438', '8299619141', '')
GO
-- Server: ABDIELALIENWARE\SQLEXPRESS2016

-- Server: ABDIELALIENWARE\SQLEXPRESS2016
-- Table:  tblModelos
-- Date:   13-Feb-20 5:53 AM

INSERT INTO tblModelos (IdMarca, Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (1, 'Camry', 1, '', 1, $(usuario), '2020-02-13 05:16:43.067', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblModelos (IdMarca, Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (1, 'Corolla', 1, '', 1, $(usuario), '2020-02-13 05:16:49.09', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblModelos (IdMarca, Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (2, 'Explorer', 1, '', 1, $(usuario), '2020-02-13 05:16:57.657', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblModelos (IdMarca, Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (2, 'Focus', 1, '', 1, $(usuario), '2020-02-13 05:17:06.52', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblModelos (IdMarca, Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (2, 'Escape', 1, '', 1, $(usuario), '2020-02-13 05:17:15.153', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblModelos (IdMarca, Nombre, IdNegocio, Codigo, Activo, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (3, 'ax-100', 1, '', 1, $(usuario), '2020-02-13 05:21:06.917', NULL, NULL, NULL, NULL)
GO



	


	-- Table:  tblTipoLocalidades


INSERT INTO tblTipoLocalidades (HijoDe, IdDivisionTerritorial, IdNegocio, Nombre, Codigo, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (NULL, NULL, 1, 'Division territorial', 'CA2DD137-7316-418B-AAB7-DB2DB9D3580B', 1, 0, $(usuario), '2020-02-13 04:15:54.953', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblTipoLocalidades (HijoDe, IdDivisionTerritorial, IdNegocio, Nombre, Codigo, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (1, 0, 1, 'Republica Dominicana', 'E19AFE88-8C8B-47C4-9978-01D3B22641E5', 1, 0, $(usuario), '2020-02-13 04:16:17.46', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblTipoLocalidades (HijoDe, IdDivisionTerritorial, IdNegocio, Nombre, Codigo, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (2, 2, 1, 'Pais', '7C288A95-D247-4CD7-A091-144859BBE923', 1, 0, $(usuario), '2020-02-13 04:16:30.96', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblTipoLocalidades (HijoDe, IdDivisionTerritorial, IdNegocio, Nombre, Codigo, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (3, 2, 1, 'Provincia', '77BB6F5B-54C8-43C9-89F9-F0239688B31B', 1, 0, $(usuario), '2020-02-13 04:17:36.637', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblTipoLocalidades (HijoDe, IdDivisionTerritorial, IdNegocio, Nombre, Codigo, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (4, 2, 1, 'Municipio', 'A53CE64B-C070-4446-BBE7-57E42EC26E02', 1, 0, $(usuario), '2020-02-13 04:17:50.617', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblTipoLocalidades (HijoDe, IdDivisionTerritorial, IdNegocio, Nombre, Codigo, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (5, 2, 1, 'Sector', '09451DA2-8A80-4699-B9C2-D85FA955BEEB', 1, 1, $(usuario), '2020-02-13 04:25:33.913', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblTipoLocalidades (HijoDe, IdDivisionTerritorial, IdNegocio, Nombre, Codigo, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (5, 2, 1, 'Distrito Municipal', '217C1CAA-0211-4401-BA6D-1A423EABFF2A', 1, 0, $(usuario), '2020-02-13 04:31:51.437', NULL, NULL, NULL, NULL)
GO
INSERT INTO tblTipoLocalidades (HijoDe, IdDivisionTerritorial, IdNegocio, Nombre, Codigo, Activo, PermiteCalle, InsertadoPor, FechaInsertado, ModificadoPor, FechaModificado, AnuladoPor, FechaAnulado) VALUES (7, 2, 1, 'Sector', '90A9E8F3-5FEB-45B9-98C7-80EB0CDACCEC', 1, 1, $(usuario), '2020-02-13 04:32:11.25', NULL, NULL, NULL, NULL)
GO

=======
			(@idNegocio,'P10','Porcentual 10% por cada dia por cada cuota',3,1,1,1,10.00,@usuario,getdate())


	--Script para datos de Tipo de localidad
	insert into tblTipoLocalidades
		(IdLocalidadPadre, IdNegocio, Nombre, PermiteCalle, InsertadoPor, FechaInsertado)
	VALUES
		(null ,@idNegocio, 'Division territorial', 0, '@usuario', getdate())
		--(1 , 2, 'Pais-Estado-Condado-Ciudad', 0),
		--(1 , @idNegocio, 'Pais-Provincia-Municipio-Sector', 0),
		--(2 , 2, 'Pais', 0),
		--(3 , 1, 'Pais', 0) --estados unidos ID 4


	--insert into tblLocalidades
	--	( IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre )
	--VALUES
	--	--Pais
	--	(null ,2, 4, 'Estados unidos'),
	--	(null ,@idNegocio, 5, 'Republica Dominicana')

	----Script para datos de Tipo de localidad
	--insert into tblTipoLocalidad
	--		(PadreDe, IdNegocio, Descripcion)
	--	VALUES
	--		(null ,@idNegocio, 'Pais'),
	--		(1 ,@idNegocio, 'Region'),
	--		(2 ,@idNegocio, 'Provincia'),
	--		(3 ,@idNegocio, 'Municipio')

	--Script para datos de localidad
	--insert into tblLocalidad
	--		( IdLocalidadPadre, IdNegocio, IdTipoLocalidad, Nombre, PermiteCalle )
	--	VALUES
	--		--Pais
	--		(null ,@idNegocio, 1, 'Republica Dominicana', 0),
	--		--Regiones
	--		(1 ,@idNegocio, 2, 'Region Este', 0),
	--		(1 ,@idNegocio, 2, 'Region Sur', 0),
	--		(1 ,@idNegocio, 2, 'Region Norte', 0),
	--		--Provincias
	--		(2 ,@idNegocio, 3, 'La Romana', 0),
	--		(3 ,@idNegocio, 3, 'San Juan', 0),
	--		(4 ,@idNegocio, 3, 'Monte Cristi', 0),

	--		--Municipio
	--		(5 ,@idNegocio, 4, 'La Romana', 0),
	--		(6 ,@idNegocio, 4, 'San Juan de la Maguana', 0),
	--		(7 ,@idNegocio, 4, 'Monte Cristi', 0)
>>>>>>> 83b30158c229d7ef555b37a14648f1b190dd3630
