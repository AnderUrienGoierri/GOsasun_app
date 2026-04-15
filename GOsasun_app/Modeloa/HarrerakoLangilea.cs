using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Harrerako langile baten datuak biltzen dituen klasea.
    /// 'harrerako_Langileak' taulari dagokio.
    /// </summary>
    public class HarrerakoLangilea : Erabiltzailea
    {
        public string Txanda { get; set; } = "Goizez";

        public override string Rola => "Harrera";

        public HarrerakoLangilea() : base() { }

        public HarrerakoLangilea(int id, string emaila, string pasahitza, int rolId, string nan, string izena, string abizenak, 
                                string txanda, DateTime jaiotzeData, string? telefonoa, string? helbidea, string? herria, string? postaKodea,
                                string irudia, string hizkuntza = "Euskara", bool aktibo = true, DateTime? sortzeData = null)
            : base(id, emaila, pasahitza, rolId, nan, izena, abizenak, jaiotzeData, telefonoa, helbidea, herria, postaKodea, irudia, aktibo, sortzeData ?? DateTime.Now, hizkuntza)
        {
            Txanda = txanda;
        }

        public override string IzenOsoa => $"{Izena} {Abizenak}";
    }
}
