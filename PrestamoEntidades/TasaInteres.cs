﻿namespace PrestamoEntidades
{
    public class TasaInteres : BaseCatalogo
    {
        public int idTasaInteres { get; set; } = 0;
        // el valor numerico del interes 10%, 4%, etc
        public float InteresMensual { get; set; } = 0;
        public bool RequiereAutorizacion { get; set; } = false;
        public override int GetId() => this.idTasaInteres;
    }
    public class TasaInteresGetParams : BaseGetParams
    {
        public int idTasaInteres { get; set; } = -1;
        // el valor numerico del interes 10%, 4%, etc
        public string Codigo { get; set; } = string.Empty;
        public float InteresMensual { get; set; } = -1;
        public int Activo { get; set; } = -1;
        public int RequiereAutorizacion { get; set; } = -1; 
    }
    public class TasaInteresDelParams : BaseDelParams
    {

    }

}
