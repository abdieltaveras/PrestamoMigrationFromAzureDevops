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
	declare @usuario varchar(10)= 'SisInit'


	--Script para datos de Negocios
	insert into dbo.tblNegocios
			(Codigo,NombreComercial,NombreJuridico,InsertadoPor,FechaInsertado)
			VALUES
			('N01','Mi Empresa','Mi Empresa',@usuario,getdate()),
			('N02','Empresa de Abdiel','PC-PROG',@usuario,getdate())

	declare @idNegocio int = (select top 1 idNegocio from tblNegocios)	

	--Script para datos de Interes

	INSERT INTO [dbo].[tblTasasInteres]
           ([idNegocio]
           ,[Codigo]
		   ,[Descripcion]
           ,[InteresMensual]
           ,[InsertadoPor]
           ,[FechaInsertado])
     VALUES
		   (@idNegocio,'A00', '1% de interes' ,1.0,@usuario,getdate()),
		   (@idNegocio,'B00', '2% de interes' ,2.0,@usuario,getdate()),
		   (@idNegocio,'C00', '3% de interes' ,3.0,@usuario,getdate())

	--Script para datos de Moras
	insert into tblTiposMora
			(idNegocio, Codigo, Descripcion, DiasDeGracia, CalcularCargoPor, AplicarA,TipoCargo,MontoOPorCientoACargar,
			InsertadoPor, FechaInsertado)
		VALUES
			(@idNegocio,'P10','Porcentual 10% por cada dia por cada cuota',3,1,1,1,10.00,@usuario,getdate())


	--Script para datos de Tipo de localidad
	insert into tblTipoLocalidades
		(HijoDe, IdNegocio, Descripcion, PermiteCalle)
	VALUES
		(null ,@idNegocio, 'Division territorial', 0)
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
