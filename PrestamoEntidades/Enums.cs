namespace PrestamoEntidades
{
    public enum Sexo { Masculino=1, Femenino, NoAplica }
   // public enum EstadoCivil { Soltero=1, Casado, Union_Libre, Viudo, Divorciado }
    public enum TiposTelefono { Movil=1, Casa, Trabajo, Fax, Otros }
    /// <summary>
    /// Los diferentes tipos de identificacion de los clientes 
    /// </summary>
    public enum TiposIdentificacionCliente { Cedula=1, Pasaporte,  RNC, Otro }
    
    public enum TiposIdentificacionPersona { Cedula = 1, Pasaporte, Otro }

    public enum Estado_Civil { Casado = 1, Soltero, Otro }

    public enum EnumTiposReferencia { Comercial = 1, Familiar, Personal}
    public enum EnumTiposVinculo { Madre = 1, Padre, Hermano, Primo, Tio, Abuelo, Sobrino, Esposa, Hijo, Nieto}


    //public enum TipoLocalidad
    //{
    //    Continente = 1,
    //    Pais,
    //    Provincia_Estado,
    //    Municipio,
    //    Sector
    //}
}

