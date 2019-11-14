namespace PrestamoEntidades
{
    public class TasaInteres
    {
        public int IdTasaInteres { get; set; } = 0;
        // el valor numerico del interes 10%, 4%, etc
        public decimal InteresMensual { get; set; } = 0;
        public bool Activo { get; set; } = true;
        public bool RequiereAutorizacion { get; set; } = false;

    }
}
