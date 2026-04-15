using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Hitzordu baten datuak biltzen dituen klasea.
    /// 'hitzorduak' taulari dagokio.
    /// </summary>
    public class Hitzordua
    {
        public int HitzorduId { get; set; }
        public int PazienteId { get; set; }
        public int OsasunLangileId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HasieraOrdua { get; set; }
        public TimeSpan? BukaeraOrdua { get; set; }
        public string? Arrazoia { get; set; }
        public string Egoera { get; set; } = "Zain";
        public DateTime SortzeData { get; set; } = DateTime.Now;

        // Propietate gehigarriak Grid-erako (JOIN bidez beteak)
        public string? PazienteIzena { get; set; }
        public string? PazienteAbizenak { get; set; }
        public string? OsasunLangileIzena { get; set; }
        public string? OsasunLangileAbizenak { get; set; }

        public string PazienteIzenOsoa => $"{PazienteIzena} {PazienteAbizenak}".Trim();
        public string OsasunLangileIzenOsoa => $"{OsasunLangileIzena} {OsasunLangileAbizenak}".Trim();

        public Hitzordua() { }

        public Hitzordua(int hitzorduId, int pazienteId, int osasunLangileId, DateTime data,
                         TimeSpan hasieraOrdua, TimeSpan? bukaeraOrdua, string? arrazoia,
                         string egoera, DateTime sortzeData)
        {
            HitzorduId = hitzorduId;
            PazienteId = pazienteId;
            OsasunLangileId = osasunLangileId;
            Data = data;
            HasieraOrdua = hasieraOrdua;
            BukaeraOrdua = bukaeraOrdua;
            Arrazoia = arrazoia;
            Egoera = egoera;
            SortzeData = sortzeData;
        }
    }
}
