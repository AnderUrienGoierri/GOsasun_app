using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Harrerako langile baten datuak biltzen dituen klasea.
    /// 'harrerako_Langileak' taulari dagokio.
    /// </summary>
    public class HarrerakoLangilea : Erabiltzailea
    {
        public override string Izena { get; set; } = string.Empty;
        public override string Abizenak { get; set; } = string.Empty;
        public string Txanda { get; set; } = "Goizez";
        public DateTime? JaiotzeData { get; set; }
        public string? Telefonoa { get; set; }
        public string Irudia { get; set; } = "img/lehenetsia_harrera.png";

        public override string Rola => "Harrera";

        public HarrerakoLangilea() : base() { }

        public HarrerakoLangilea(int id, string emaila, string pasahitza, int rolId, bool aktibo, DateTime sortzeData,
                                string izena, string abizenak, string txanda, DateTime? jaiotzeData, string? telefonoa, string irudia,
                                string hizkuntza = "Euskara", string? ezarpenak = null)
            : base(id, emaila, pasahitza, rolId, aktibo, sortzeData, hizkuntza, ezarpenak)
        {
            Izena = izena;
            Abizenak = abizenak;
            Txanda = txanda;
            JaiotzeData = jaiotzeData;
            Telefonoa = telefonoa;
            Irudia = irudia;
        }

        public override string IzenOsoa => $"{Izena} {Abizenak}";
    }
}
