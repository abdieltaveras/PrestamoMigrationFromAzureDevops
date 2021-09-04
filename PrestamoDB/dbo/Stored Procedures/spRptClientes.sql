CREATE PROCEDURE [dbo].[spRptClientes]
@OrdenarPor varchar(50)= 'Nombres',
@Rango varchar(50)= 'Nombres',
@Desde varchar(50) = 'a',
@Hasta varchar(50) = 'z',
@ReportType int = -1
as
IF(@Rango = 'Nombres' )
BEGIN
	select * from tblClientes
	where Nombres between @Desde and  @Hasta +'Z'
	order by 
		CASE @OrdenarPor
		when 'Nombres' then Nombres
		when 'Apellidos' then Apellidos
		when 'NoIdentificacion' then NoIdentificacion
		END asc
		,CASE @OrdenarPor
		when 'FechaIngreso' then FechaInsertado
		when 'FechaNacimiento' then FechaNacimiento
		END asc
END
ELSE IF( @Rango ='Apellidos' )
	BEGIN
		select * from tblClientes
		where  Apellidos between @Desde and  @Hasta +'Z'
		order by 
			CASE @OrdenarPor
			when 'Nombres' then Nombres
			when 'Apellidos' then Apellidos
			when 'NoIdentificacion' then NoIdentificacion
			END asc
			,CASE @OrdenarPor
			when 'FechaIngreso' then FechaInsertado
			when 'FechaNacimiento' then FechaNacimiento
			END asc
	END
ELSE IF( @Rango ='NoIdentificacion' )
		BEGIN 
			select * from tblClientes
			where NoIdentificacion  between @Desde and  @Hasta
			order by 
				CASE @OrdenarPor
				when 'Nombres' then Nombres
				when 'Apellidos' then Apellidos
				when 'NoIdentificacion' then NoIdentificacion
				END asc
				,CASE @OrdenarPor
				when 'FechaIngreso' then FechaInsertado
				when 'FechaNacimiento' then FechaNacimiento
				END asc
		END
		ELSE IF( @Rango ='FechaIngreso' )
		BEGIN 
			select * from tblClientes
			where FechaInsertado  between @Desde and  DATEADD(day,1,@Hasta)
			order by 
				CASE @OrdenarPor
				when 'Nombres' then Nombres
				when 'Apellidos' then Apellidos
				when 'NoIdentificacion' then NoIdentificacion
				END asc
				,CASE @OrdenarPor
				when 'FechaIngreso' then FechaInsertado
				when 'FechaNacimiento' then FechaNacimiento
				END asc
		END
		ELSE IF( @Rango ='FechaNacimiento' )
		BEGIN 
			select * from tblClientes
			where FechaNacimiento  between @Desde and  DATEADD(day,1,@Hasta)
			order by 
				CASE @OrdenarPor
				when 'Nombres' then Nombres
				when 'Apellidos' then Apellidos
				when 'NoIdentificacion' then NoIdentificacion
				END asc
				,CASE @OrdenarPor
				when 'FechaIngreso' then FechaInsertado
				when 'FechaNacimiento' then FechaNacimiento
				END asc
		END
--Nombres between @Desde and  @Hasta
--or Apellidos between @Desde and  @Hasta
--or NoIdentificacion between @Desde and  @Hasta
