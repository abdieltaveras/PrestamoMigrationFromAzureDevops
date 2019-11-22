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

	insert into dbo.tblNegocios
			(Codigo,NombreComercial,NombreJuridico,InsertadoPor,FechaInsertado)
			VALUES
			('N01','Mi Empresa','Mi Empresa',@usuario,getdate())
	declare @idNegocio int = (select top 1 idNegocio from tblNegocios)
	
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

	insert into tblTiposMora
			(idNegocio, Codigo, Descripcion,DiasDeGracia, CalcularCargoPor, AplicarA,TipoCargo,MontoOPorCientoACargar,
			InsertadoPor, FechaInsertado)
		VALUES
			(@idNegocio,'P10','Porcentual 10% por cada dia por cada cuota',3,1,1,1,10.00,@usuario,getdate())

