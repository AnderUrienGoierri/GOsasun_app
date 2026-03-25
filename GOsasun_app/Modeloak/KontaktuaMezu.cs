using System;

namespace GOsasun_app.Modeloak
{
    /// <summary>
    /// Kanpoko kontakturako mezu baten datuak biltzen dituen klasea.
    /// 'kontaktua_Mezuak' taulari dagokio.
    /// </summary>
    public class KontaktuaMezu
    {
        public int MezuId { get; set; }
        public string Izena { get; set; } = string.Empty;
        public string Emaila { get; set; } = string.Empty;
        public string Mezua { get; set; } = string.Empty;
        public string? Erantzuna { get; set; }
        public DateTime? ErantzunData { get; set; }
        public bool Irakurrita { get; set; } = false;
        public DateTime BidalketaData { get; set; } = DateTime.Now;

        public KontaktuaMezu() { }

        public KontaktuaMezu(int mezuId, string izena, string emaila, string mezua,
                             string? erantzuna, DateTime? erantzunData, bool irakurrita,
                             DateTime bidalketaData)
        {
            MezuId = mezuId;
            Izena = izena;
            Emaila = emaila;
            Mezua = mezua;
            Erantzuna = erantzuna;
            ErantzunData = erantzunData;
            Irakurrita = irakurrita;
            BidalketaData = bidalketaData;
        }
    }
}
