using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Mediku eta paziente baten arteko lotura biltzen duen klasea.
    /// 'mediku_Paziente' taulari dagokio.
    /// </summary>
    public class MedikuPaziente
    {
        public int LoturaId { get; set; }
        public int MedikuId { get; set; }
        public int PazienteId { get; set; }
        public DateTime EsleipenData { get; set; } = DateTime.Now;

        public MedikuPaziente() { }

        public MedikuPaziente(int loturaId, int medikuId, int pazienteId, DateTime esleipenData)
        {
            LoturaId = loturaId;
            MedikuId = medikuId;
            PazienteId = pazienteId;
            EsleipenData = esleipenData;
        }
    }
}
