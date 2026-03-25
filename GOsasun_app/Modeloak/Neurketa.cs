using System;

namespace GOsasun_app.Modeloak
{
    /// <summary>
    /// Osasun neurketa baten datuak biltzen dituen klasea.
    /// 'neurketak' taulari dagokio.
    /// </summary>
    public class Neurketa
    {
        public int NeurketaId { get; set; }
        public int PazienteId { get; set; }
        public decimal? Glukosa { get; set; }
        public int? TentsioSistolikoa { get; set; }
        public int? TentsioDiastolikoa { get; set; }
        public decimal? Pisua { get; set; }
        public decimal? Altuera { get; set; }
        public int? Pultsua { get; set; }
        public string? Sintomak { get; set; }
        public DateTime ErregistroData { get; set; } = DateTime.Now;

        public Neurketa() { }

        public Neurketa(int neurketaId, int pazienteId, decimal? glukosa, int? tentsioSistolikoa,
                        int? tentsioDiastolikoa, decimal? pisua, decimal? altuera, int? pultsua,
                        string? sintomak, DateTime erregistroData)
        {
            NeurketaId = neurketaId;
            PazienteId = pazienteId;
            Glukosa = glukosa;
            TentsioSistolikoa = tentsioSistolikoa;
            TentsioDiastolikoa = tentsioDiastolikoa;
            Pisua = pisua;
            Altuera = altuera;
            Pultsua = pultsua;
            Sintomak = sintomak;
            ErregistroData = erregistroData;
        }
    }
}
