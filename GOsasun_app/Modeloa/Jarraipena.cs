using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Osasun jarraipen baten datuak biltzen dituen klasea.
    /// 'jarraipenak' taulari dagokio.
    /// </summary>
    public class Jarraipena
    {
        public int Id { get; set; }
        public int PazienteId { get; set; }
        public string PazienteNan { get; set; } = string.Empty;
        public string PazienteIzena { get; set; } = string.Empty;
        public string PazienteAbizenak { get; set; } = string.Empty;
        public int? OsasunLangileId { get; set; }
        public int? TentsioSistolikoa { get; set; }
        public int? TentsioDiastolikoa { get; set; }
        public decimal? PisuaKg { get; set; }
        public decimal? Altuera { get; set; }
        public int? PultsuaPpm { get; set; }
        public string? Oharrak { get; set; }
        public string? BideaZerbitzarian { get; set; }
        public DateTime ErregistroData { get; set; } = DateTime.Now;
        public int DokumentuKopurua { get; set; }
        public string EkintzakTestua => "Ikusi | Editatu | Gehitu | Dokumentuak | Ezabatu";
        public string PazienteIzenOsoa => $"{PazienteAbizenak}, {PazienteIzena}";

        public Jarraipena() { }

        public Jarraipena(int id, int pazienteId, int? osasunLangileId, int? tentsioSistolikoa,
                        int? tentsioDiastolikoa, decimal? pisuaKg, decimal? altuera, int? pultsuaPpm,
                        string? oharrak, string? bideaZerbitzarian, DateTime erregistroData)
        {
            Id = id;
            PazienteId = pazienteId;
            OsasunLangileId = osasunLangileId;
            TentsioSistolikoa = tentsioSistolikoa;
            TentsioDiastolikoa = tentsioDiastolikoa;
            PisuaKg = pisuaKg;
            Altuera = altuera;
            PultsuaPpm = pultsuaPpm;
            Oharrak = oharrak;
            BideaZerbitzarian = bideaZerbitzarian;
            ErregistroData = erregistroData;
        }
    }
}
