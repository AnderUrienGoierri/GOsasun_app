using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.DatuBasea;

namespace GOsasun_app.Kontrola
{
    public class HitzorduKontrolatzailea
    {
        private readonly HitzorduDB _db = new HitzorduDB();

        public List<Hitzordua> LortuHitzorduGuztiak()
        {
            return _db.LortuHitzorduGuztiak();
        }

        public List<Hitzordua> LortuPazientearenHitzorduak(int pazienteId)
        {
            return _db.LortuPazientearenHitzorduak(pazienteId);
        }

        public List<Hitzordua> LortuMedikuarenHitzorduak(int medikuId)
        {
            return _db.LortuMedikuarenHitzorduak(medikuId);
        }

        public void GehituHitzordua(Hitzordua h)
        {
            _db.GehituHitzordua(h);
        }

        public void EguneratuHitzordua(Hitzordua h)
        {
            _db.EguneratuHitzordua(h);
        }

        public void EzabatuHitzordua(int hitzorduId)
        {
            _db.EzabatuHitzordua(hitzorduId);
        }
    }
}
