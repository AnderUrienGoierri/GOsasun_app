namespace GOsasun_app.Modeloak
{
    /// <summary>
    /// Botika baten datuak biltzen dituen klasea.
    /// 'botikak' taulari dagokio.
    /// </summary>
    public class Botika
    {
        public int BotikaId { get; set; }
        public string Izena { get; set; } = string.Empty;
        public string? IzenKimikoa { get; set; }
        public string? NomenklaturaKimikoa { get; set; }
        public string? EraginFokoa { get; set; }
        public string? Aktibitatea { get; set; }

        public Botika() { }

        public Botika(int botikaId, string izena, string? izenKimikoa,
                      string? nomenklaturaKimikoa, string? eraginFokoa, string? aktibitatea)
        {
            BotikaId = botikaId;
            Izena = izena;
            IzenKimikoa = izenKimikoa;
            NomenklaturaKimikoa = nomenklaturaKimikoa;
            EraginFokoa = eraginFokoa;
            Aktibitatea = aktibitatea;
        }
    }
}
