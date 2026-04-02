using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Dokumentu baten metadata biltzen duen klasea.
    /// 'dokumentuak' taulari dagokio.
    /// </summary>
    public class Dokumentua
    {
        public int DokumentuId { get; set; }
        public int PazienteId { get; set; }
        public int IgotzaileId { get; set; }
        public string FitxategiIzena { get; set; } = string.Empty;
        public string BideaZerbitzarian { get; set; } = string.Empty;
        public string? Mota { get; set; }
        public DateTime IgotzeData { get; set; } = DateTime.Now;
        public string? Deskribapena { get; set; }

        public Dokumentua() { }

        public Dokumentua(int dokumentuId, int pazienteId, int igotzaileId, string fitxategiIzena,
                          string bideaZerbitzarian, string? mota, DateTime igotzeData, string? deskribapena)
        {
            DokumentuId = dokumentuId;
            PazienteId = pazienteId;
            IgotzaileId = igotzaileId;
            FitxategiIzena = fitxategiIzena;
            BideaZerbitzarian = bideaZerbitzarian;
            Mota = mota;
            IgotzeData = igotzeData;
            Deskribapena = deskribapena;
        }
    }
}
