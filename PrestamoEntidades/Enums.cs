namespace PrestamoEntidades
{
    public enum Sexo { Masculino=1, Femenino, NoAplica }
    public enum EstadoCivil { Soltero=1, Casada, Union_Libre, Viudo, Divorciado }
    public enum TiposTelefono { Movil=1, Casa, Trabajo, Fax, Otros }
    public enum TiposIdentificacion { Cedula=1, Pasaporte,  RNC, Otro }

    public enum TipoLocalidad
    {
        Continente = 1,
        Pais,
        Provincia_Estado,
        Municipio,
        Sector
    }
}

