using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;

namespace GOsasun_app.Kontrola
{
    public class HitzorduKontrolatzailea
    {
        // ---------------------------SORTU OBJETUA------------------------------------------------------
        private readonly HitzorduDB _db = new HitzorduDB();

        // ---------------------------LORTU------------------------------------------------------

        public List<Hitzordua> LortuHitzorduGuztiak()
        {
            return _db.LortuHitzorduGuztiak();
        }

        public List<Hitzordua> LortuPazientearenHitzorduak(int pazienteId)
        {
            return _db.LortuPazientearenHitzorduak(pazienteId);
        }

        public List<Hitzordua> LortuOsasunLangilearenHitzorduak(int osasunLangileId)
        {
            return _db.LortuOsasunLangilearenHitzorduak(osasunLangileId);
        }

        // ---------------------------GEHITU------------------------------------------------------

        public void GehituHitzordua(Hitzordua h)
        {
            _db.GehituHitzordua(h);
        }

        // ---------------------------EGUNERATU------------------------------------------------------

        public void EguneratuHitzordua(Hitzordua h)
        {
            _db.EguneratuHitzordua(h);
        }

        // ---------------------------EZABATU------------------------------------------------------

        public void EzabatuHitzordua(int hitzorduId)
        {
            _db.EzabatuHitzordua(hitzorduId);
        }
    }
}
