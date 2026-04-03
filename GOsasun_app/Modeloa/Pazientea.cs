using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Paziente baten datuak biltzen dituen klasea.
    /// 'pazienteak' taulari dagokio.
    /// </summary>
    public class Pazientea : Erabiltzailea
    {
        public string Nan { get; set; } = string.Empty;
        public override string Izena { get; set; } = string.Empty;
        public override string Abizenak { get; set; } = string.Empty;
        public string Sexua { get; set; } = "Gizona";
        public DateTime JaiotzeData { get; set; }
        public string? Telefonoa { get; set; }
        public string? Helbidea { get; set; }
        public string? Herria { get; set; }
        public string? PostaKodea { get; set; }
        public string? OdolTaldea { get; set; }
        public decimal? AzkenAltuera { get; set; }
        public decimal? AzkenPisua { get; set; }
        public string EgoeraKlinikoa { get; set; } = "Alta";
        public string Irudia { get; set; } = "img/lehenetsia_pazientea.png";

        public override string Rola => "Pazientea";
        public override bool DaPazientea() => true;

        public Pazientea() : base() { }

        public Pazientea(int id, string emaila, string pasahitza, int rolId, bool aktibo, DateTime sortzeData,
                        string nan, string izena, string abizenak, string sexua, DateTime jaiotzeData, string? telefonoa,
                        string? helbidea, string? herria, string? postaKodea,
                        string? odolTaldea, decimal? azkenAltuera, decimal? azkenPisua, string egoeraKlinikoa, string irudia,
                        string hizkuntza = "Euskara", string? ezarpenak = null)
            : base(id, emaila, pasahitza, rolId, aktibo, sortzeData, hizkuntza, ezarpenak)
        {
            Nan = nan;
            Izena = izena;
            Abizenak = abizenak;
            Sexua = sexua;
            JaiotzeData = jaiotzeData;
            Telefonoa = telefonoa;
            Helbidea = helbidea;
            Herria = herria;
            PostaKodea = postaKodea;
            OdolTaldea = odolTaldea;
            AzkenAltuera = azkenAltuera;
            AzkenPisua = azkenPisua;
            EgoeraKlinikoa = egoeraKlinikoa;
            Irudia = irudia;
        }

        public override string IzenOsoa => $"{Izena} {Abizenak}";
    }
}
