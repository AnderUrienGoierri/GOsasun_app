using System;
using System.Collections.Generic;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Errezeta edo diagnostiko baten datuak biltzen dituen klasea.
    /// 'errezetak' taulari dagokio.
    /// </summary>
    public class Errezeta
    {
        public int ErrezetaId { get; set; }
        public int? HitzorduId { get; set; }
        public int MedikuId { get; set; }
        public int PazienteId { get; set; }
        public DateTime IgorpenData { get; set; }
        public DateTime? IraungitzeData { get; set; }
        public string? XmlBidea { get; set; }
        public string? Diagnostikoa { get; set; }
        public bool Aktibo { get; set; } = true;
        public List<ErrezetaBotika> Botikak { get; set; } = new List<ErrezetaBotika>();
        
        // UI-rako eta Bilaketetarako ezaugarri gehigarriak
        public string? PazienteIzenOsoa { get; set; }
        public string? PazienteNan { get; set; }
        public DateTime? HitzorduData { get; set; }

        public Errezeta() { }

        public Errezeta(int errezetaId, int? hitzorduId, int medikuId, int pazienteId,
                        DateTime igorpenData, DateTime? iraungitzeData, string? xmlBidea,
                        string? diagnostikoa, bool aktibo)
        {
            ErrezetaId = errezetaId;
            HitzorduId = hitzorduId;
            MedikuId = medikuId;
            PazienteId = pazienteId;
            IgorpenData = igorpenData;
            IraungitzeData = iraungitzeData;
            XmlBidea = xmlBidea;
            Diagnostikoa = diagnostikoa;
            Aktibo = aktibo;
        }
    }
}
