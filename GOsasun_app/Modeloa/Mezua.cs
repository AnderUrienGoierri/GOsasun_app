using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Mezularitza sistema barruko mezu baten datuak biltzen dituen klasea.
    /// 'mezuak' taulari dagokio.
    /// </summary>
    public class Mezua
    {
        public int MezuId { get; set; }
        public int BidaltzaileId { get; set; }
        public int HartzaileId { get; set; }
        public string Gaia { get; set; } = string.Empty;
        public string Testua { get; set; } = string.Empty;
        public bool Irakurrita { get; set; } = false;
        public DateTime BidalketaData { get; set; } = DateTime.Now;

        public Mezua() { }

        public Mezua(int mezuId, int bidaltzaileId, int hartzaileId, string gaia, string testua,
                     bool irakurrita, DateTime bidalketaData)
        {
            MezuId = mezuId;
            BidaltzaileId = bidaltzaileId;
            HartzaileId = hartzaileId;
            Gaia = gaia;
            Testua = testua;
            Irakurrita = irakurrita;
            BidalketaData = bidalketaData;
        }
    }
}
