using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Osasun langile baten datuak biltzen dituen klasea.
    /// 'osasun_langileak' taulari dagokio.
    /// </summary>
    public class OsasunLangilea : Erabiltzailea
    {
        public string ElkargokideZenbakia { get; set; } = string.Empty;
        public string Espezialitatea { get; set; } = string.Empty;
        public string? Kontsulta { get; set; }
        public string Lanaldia { get; set; } = "Osoa";
        
        public System.Collections.ArrayList Pazienteak { get; set; } = new System.Collections.ArrayList();

        public override string Rola => "OsasunLangilea";
        public override bool DaOsasunLangilea() => true;

        public OsasunLangilea() : base() { }

        public OsasunLangilea(int id, string emaila, string pasahitza, int rolId, string nan, string izena, string abizenak,
                             DateTime jaiotze_data, string? telefonoa, string? helbidea, string? herria, string? posta_kodea,
                             string elkargokideZenbakia, string espezialitatea, string? kontsulta, string lanaldia, string irudia,
                             string hizkuntza = "Euskara", bool aktibo = true, DateTime? sortzeData = null)
            : base(id, emaila, pasahitza, rolId, nan, izena, abizenak, jaiotze_data, telefonoa, helbidea, herria, posta_kodea, irudia, aktibo, sortzeData ?? DateTime.Now, hizkuntza)
        {
            ElkargokideZenbakia = elkargokideZenbakia;
            Espezialitatea = espezialitatea;
            Kontsulta = kontsulta;
            Lanaldia = lanaldia;
        }

        public override string IzenOsoa => $"{Izena} {Abizenak}";
    }
}
