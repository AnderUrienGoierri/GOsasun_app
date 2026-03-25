using System;

namespace GOsasun_app.Modeloak
{
    /// <summary>
    /// Hitzordu baten datuak biltzen dituen klasea.
    /// 'hitzorduak' taulari dagokio.
    /// </summary>
    public class Hitzordua
    {
        public int HitzorduId { get; set; }
        public int PazienteId { get; set; }
        public int MedikuId { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HasieraOrdua { get; set; }
        public TimeSpan? BukaeraOrdua { get; set; }
        public string? Arrazoia { get; set; }
        public string Egoera { get; set; } = "Zain";
        public DateTime SortzeData { get; set; } = DateTime.Now;

        public Hitzordua() { }

        public Hitzordua(int hitzorduId, int pazienteId, int medikuId, DateTime data,
                         TimeSpan hasieraOrdua, TimeSpan? bukaeraOrdua, string? arrazoia,
                         string egoera, DateTime sortzeData)
        {
            HitzorduId = hitzorduId;
            PazienteId = pazienteId;
            MedikuId = medikuId;
            Data = data;
            HasieraOrdua = hasieraOrdua;
            BukaeraOrdua = bukaeraOrdua;
            Arrazoia = arrazoia;
            Egoera = egoera;
            SortzeData = sortzeData;
        }
    }
}
