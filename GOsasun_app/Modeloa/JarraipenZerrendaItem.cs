using System;

namespace GOsasun_app.Modeloa
{
    public class JarraipenZerrendaItem
    {
        public int Id { get; set; }
        public int PazienteId { get; set; }
        public string PazienteNan { get; set; } = string.Empty;
        public string PazienteIzena { get; set; } = string.Empty;
        public string PazienteAbizenak { get; set; } = string.Empty;
        public int? TentsioSistolikoa { get; set; }
        public int? TentsioDiastolikoa { get; set; }
        public decimal? PisuaKg { get; set; }
        public decimal? Altuera { get; set; }
        public int? PultsuaPpm { get; set; }
        public string? Oharrak { get; set; }
        public DateTime ErregistroData { get; set; }
        public int DokumentuKopurua { get; set; }

        public string PazienteIzenOsoa => $"{PazienteAbizenak}, {PazienteIzena}";
    }
}