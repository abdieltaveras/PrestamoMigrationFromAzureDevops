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

	INSERT INTO [dbo].[tblTasaInteres]
           ([idNegocio]
           ,[Codigo]
           ,[InteresMensual]
           ,[InsertadoPor]
           ,[FechaInsertado])
     VALUES
		   (1,'A00',1.0,'Sis2',getdate()),
		   (1,'B00',1.0,'Sis2',getdate()),
		   (1,'C00',1.0,'Sis2',getdate())


--INSERT INTO [dbo].[tblTasaInteres]
--           ([idNegocio]
--           ,[Codigo]
--           ,[InteresMensual]
--           ,[InsertadoPor]
--           ,[FechaInsertado])
--     VALUES
--		   (1,'B00',1.0,'Sis2',getdate())

--INSERT INTO [dbo].[tblTasaInteres]
--           ([idNegocio]
--           ,[Codigo]
--           ,[InteresMensual]
--           ,[InsertadoPor]
--           ,[FechaInsertado])
--     VALUES
--		   (1,'C00',1.0,'Sis2',getdate())
	

--EXECUTE [dbo].[spInsUpdTasaInteres] @idNegocio=1, @idTasaInteres=0,@codigo='A00',@InteresMensual=1.0,@Usuario='Sis'
--EXECUTE [dbo].[spInsUpdTasaInteres] @idNegocio=1, @idTasaInteres=0,@codigo='B00',@InteresMensual=2.0,@Usuario='Sis'
--EXECUTE [dbo].[spInsUpdTasaInteres] @idNegocio=1, @idTasaInteres=0,@codigo='C00',@InteresMensual=3.0,@Usuario='Sis'

