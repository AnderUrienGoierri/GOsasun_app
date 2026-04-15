using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Paziente baten datuak biltzen dituen klasea.
    /// 'pazienteak' taulari dagokio.
    /// </summary>
    public class Pazientea : Erabiltzailea
    {
        public string Sexua { get; set; } = "Gizona";
        public string? OdolTaldea { get; set; }
        public decimal? AzkenAltuera { get; set; }
        public decimal? AzkenPisua { get; set; }
        public string EgoeraKlinikoa { get; set; } = "Alta";
        
        public System.Collections.ArrayList OsasunLangileak { get; set; } = new System.Collections.ArrayList();

        public override string Rola => "Pazientea";
        public override bool DaPazientea() => true;

        public Pazientea() : base() { }

        public Pazientea(int id, string emaila, string pasahitza, int rolId, string nan, string izena, string abizenak,
                        string sexua, DateTime jaiotzeData, string? telefonoa, string? helbidea, string? herria, string? postaKodea,
                        string? odolTaldea, decimal? azkenAltuera, decimal? azkenPisua, string egoeraKlinikoa, string irudia,
                        string hizkuntza = "Euskara", bool aktibo = true, DateTime? sortzeData = null)
            : base(id, emaila, pasahitza, rolId, nan, izena, abizenak, jaiotzeData, telefonoa, helbidea, herria, postaKodea, irudia, aktibo, sortzeData ?? DateTime.Now, hizkuntza)
        {
            Sexua = sexua;
            OdolTaldea = odolTaldea;
            AzkenAltuera = azkenAltuera;
            AzkenPisua = azkenPisua;
            EgoeraKlinikoa = egoeraKlinikoa;
        }

        public override string IzenOsoa => $"{Izena} {Abizenak}";
    }
}
