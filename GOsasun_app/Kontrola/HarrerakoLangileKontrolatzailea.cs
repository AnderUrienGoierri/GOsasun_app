using System.Collections.Generic;
using GOsasun_app.Modeloa;
using GOsasun_app.Repositorioa;

namespace GOsasun_app.Kontrola
{
    public class HarrerakoLangileKontrolatzailea
    {
        private readonly HarrerakoLangileaDB _harrerakoLangileaDb = new HarrerakoLangileaDB();

        public List<HarrerakoLangilea> LortuGuztiakHarrerakoak(string? bilatzailea = null)
        {
            return _harrerakoLangileaDb.LortuGuztiakHarrerakoak(bilatzailea);
        }

        public HarrerakoLangilea? LortuHarrerakoa(int harrerakoaId)
        {
            return _harrerakoLangileaDb.LortuHarrerakoa(harrerakoaId);
        }

        public bool SortuHarrerakoa(HarrerakoLangilea h)
        {
            return _harrerakoLangileaDb.SortuHarrerakoa(h);
        }

        public bool SortuHarrerakoa(HarrerakoLangilea h, string? irudiBidea)
        {
            return _harrerakoLangileaDb.SortuHarrerakoa(h, irudiBidea);
        }

        public bool EzabatuHarrerakoa(int id)
        {
            return _harrerakoLangileaDb.EzabatuHarrerakoa(id);
        }

        public bool EguneratuHarrerakoa(HarrerakoLangilea h)
        {
            return _harrerakoLangileaDb.EguneratuHarrerakoa(h);
        }
    }
}