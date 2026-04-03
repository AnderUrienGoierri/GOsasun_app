using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Mediku baten datuak biltzen dituen klasea.
    /// 'medikuak' taulari dagokio.
    /// </summary>
    public class Medikua : Erabiltzailea
    {
        public override string Izena { get; set; } = string.Empty;
        public override string Abizenak { get; set; } = string.Empty;
        public DateTime JaiotzeData { get; set; }
        public string ElkargokideZenbakia { get; set; } = string.Empty;
        public string Espezialitatea { get; set; } = string.Empty;
        public string? Kontsulta { get; set; }
        public string Lanaldia { get; set; } = "Osoa";
        public string? Telefonoa { get; set; }
        public string Irudia { get; set; } = "img/lehenetsia_medikua.png";

        public override string Rola => "Medikua";
        public override bool DaMedikua() => true;

        public Medikua() : base() { }

        public Medikua(int id, string emaila, string pasahitza, int rolId, bool aktibo, DateTime sortzeData,
                        string izena, string abizenak, DateTime jaiotzeData, string elkargokideZenbakia,
                        string espezialitatea, string? kontsulta, string lanaldia, string? telefonoa, string irudia,
                        string hizkuntza = "Euskara", string? ezarpenak = null)
            : base(id, emaila, pasahitza, rolId, aktibo, sortzeData, hizkuntza, ezarpenak)
        {
            Izena = izena;
            Abizenak = abizenak;
            JaiotzeData = jaiotzeData;
            ElkargokideZenbakia = elkargokideZenbakia;
            Espezialitatea = espezialitatea;
            Kontsulta = kontsulta;
            Lanaldia = lanaldia;
            Telefonoa = telefonoa;
            Irudia = irudia;
        }

        public override string IzenOsoa => $"{Izena} {Abizenak}";
    }
}
