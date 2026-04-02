using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.DatuBasea;

namespace GOsasun_app.Kontrola
{
    /// <summary>
    /// Neurketen kudeaketarako kontrolatzailea.
    /// </summary>
    public class NeurketaKontrolatzailea
    {
        private readonly NeurketaDB _db = new NeurketaDB();

        /// <summary>
        /// Paziente baten neurketa guztien zerrenda lortzen du.
        /// </summary>
        public List<Neurketa> LortuPazientearenNeurketak(int pazienteId)
        {
            return _db.LortuPazientearenNeurketak(pazienteId);
        }

        /// <summary>
        /// Neurketa berri bat gordetzen du datu-basean.
        /// </summary>
        public bool GordeNeurketa(Neurketa neurketa)
        {
            return _db.GordeNeurketa(neurketa);
        }
    }
}
