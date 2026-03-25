using System;

namespace GOsasun_app.Modeloak
{
    /// <summary>
    /// Harrerako langile baten datuak biltzen dituen klasea.
    /// 'harrerako_Langileak' taulari dagokio.
    /// </summary>
    public class HarrerakoLangilea : Erabiltzailea
    {
        public override string Izena { get; set; } = string.Empty;
        public override string Abizenak { get; set; } = string.Empty;
        public string Irudia { get; set; } = "img/lehenetsia_harrera.png";

        public override string Rola => "Harrera";

        public HarrerakoLangilea() : base() { }

        public HarrerakoLangilea(int id, string emaila, string pasahitza, int rolId, bool aktibo, DateTime sortzeData,
                                string izena, string abizenak, string irudia)
            : base(id, emaila, pasahitza, rolId, aktibo, sortzeData)
        {
            Izena = izena;
            Abizenak = abizenak;
            Irudia = irudia;
        }

        public override string IzenOsoa => $"{Izena} {Abizenak}";
    }
}
