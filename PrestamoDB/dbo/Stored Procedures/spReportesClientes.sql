CREATE PROCEDURE [dbo].[spReportesClientes]
@Opcion int,
@FechaDesde datetime = null,
@FechaHasta datetime = null
as
SET DATEFORMAT ymd;
IF(@Opcion= 1)
	BEGIN
		SELECT * from tblClientes with(nolock)
		WHERE FORMAT(FechaNacimiento,'MM-dd') >= FORMAT(@FechaDesde,'MM-dd') and
		FORMAT(FechaNacimiento,'MM-dd') <= FORMAT(@FechaHasta,'MM-dd')
	END

