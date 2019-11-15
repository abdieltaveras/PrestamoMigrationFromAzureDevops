namespace PrestamoEntidades
{
    public class TasaInteres
    {
        public int IdTasaInteres { get; set; } = 0;
        public string Codigo { get; set; } = string.Empty;
        // el valor numerico del interes 10%, 4%, etc
        public decimal InteresMensual { get; set; } = 0;
        public bool Activo { get; set; } = true;
        public bool RequiereAutorizacion { get; set; } = false;

    }
    public class TasaInteresGetParams
    {
        public int IdTasaInteres { get; set; } = 0;
        // el valor numerico del interes 10%, 4%, etc
        public string Codigo { get; set; } = string.Empty;
        public decimal InteresMensualMenorOIgualA { get; set; } = -1;
        public int Activo { get; set; } = -1;
        public int RequiereAutorizacion { get; set; } = -1; 
    }
}
