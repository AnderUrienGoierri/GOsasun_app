using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Pazientearentzako abisu baten datuak biltzen dituen klasea.
    /// 'abisuak' taulari dagokio.
    /// </summary>
    public class Abisua
    {
        public int AbisuId { get; set; }
        public int PazienteId { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public string? Mota { get; set; }
        public string? Testua { get; set; }
        public bool Irakurrita { get; set; } = false;

        public Abisua() { }

        public Abisua(int abisuId, int pazienteId, DateTime data, string? mota,
                      string? testua, bool irakurrita)
        {
            AbisuId = abisuId;
            PazienteId = pazienteId;
            Data = data;
            Mota = mota;
            Testua = testua;
            Irakurrita = irakurrita;
        }
    }
}
