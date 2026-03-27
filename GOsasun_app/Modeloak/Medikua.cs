using System;

namespace GOsasun_app.Modeloak
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
        public string? Telefonoa { get; set; }
        public string Irudia { get; set; } = "img/lehenetsia_medikua.png";

        public override string Rola => "Medikua";
        public override bool DaMedikua() => true;

        public Medikua() : base() { }

        public Medikua(int id, string emaila, string pasahitza, int rolId, bool aktibo, DateTime sortzeData,
                        string izena, string abizenak, DateTime jaiotzeData, string elkargokideZenbakia,
                        string espezialitatea, string? telefonoa, string irudia)
            : base(id, emaila, pasahitza, rolId, aktibo, sortzeData)
        {
            Izena = izena;
            Abizenak = abizenak;
            JaiotzeData = jaiotzeData;
            ElkargokideZenbakia = elkargokideZenbakia;
            Espezialitatea = espezialitatea;
            Telefonoa = telefonoa;
            Irudia = irudia;
        }

        public override string IzenOsoa => $"{Izena} {Abizenak}";
    }
}
