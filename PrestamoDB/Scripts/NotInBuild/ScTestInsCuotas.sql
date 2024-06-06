-- este escript revisa posibles fallas al insertar cuotas
declare  @MaestrosCxC tpMaestroCxCPrestamo
insert  into  @MaestrosCxC select * from tmpMaestrocxc
declare  @detallesCxC tpDetalleDrCxC
insert  into  @detallesCxC select * from tmpDetalleDrCxC
exec spInsMaestroDetalleDrCxCPrestamo 1, @MaestrosCxC, @DetallesCxC
-- nota las tablas tmp aqui usada se crean al ejecutar el store procedure
-- spInsMaestroDetalleDrCxCPrestamo y se le envia en el 4to parametro
-- el valor 1 indicando que se desean crear esta tablas con datos, para luego ser usadas 
-- en esta prueba