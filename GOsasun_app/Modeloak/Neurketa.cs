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
        public int? TentsioSistolikoa { get; set; }
        public int? TentsioDiastolikoa { get; set; }
        public decimal? PisuaKg { get; set; }
        public decimal? Altuera { get; set; }
        public int? PultsuaPpm { get; set; }
        public string? Sintomak { get; set; }
        public DateTime ErregistroData { get; set; } = DateTime.Now;

        public Neurketa() { }

        public Neurketa(int neurketaId, int pazienteId, int? tentsioSistolikoa,
                        int? tentsioDiastolikoa, decimal? pisuaKg, decimal? altuera, int? pultsuaPpm,
                        string? sintomak, DateTime erregistroData)
        {
            NeurketaId = neurketaId;
            PazienteId = pazienteId;
            TentsioSistolikoa = tentsioSistolikoa;
            TentsioDiastolikoa = tentsioDiastolikoa;
            PisuaKg = pisuaKg;
            Altuera = altuera;
            PultsuaPpm = pultsuaPpm;
            Sintomak = sintomak;
            ErregistroData = erregistroData;
        }
    }
}
