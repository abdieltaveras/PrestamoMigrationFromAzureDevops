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
		   ,[Descripcion]
           ,[InteresMensual]
           ,[InsertadoPor]
           ,[FechaInsertado])
     VALUES
		   (1,'A00', '1% de interes' ,1.0,'Sis2',getdate()),
		   (1,'B00', '2% de interes' ,2.0,'Sis2',getdate()),
		   (1,'C00', '3% de interes' ,3.0,'Sis2',getdate())

