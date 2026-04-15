using System;

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Dokumentu baten metadata biltzen duen klasea.
    /// 'dokumentuak' taulari dagokio.
    /// </summary>
    public class Dokumentua
    {
        public int Id { get; set; }
        public int JarraipenaId { get; set; }
        public string FitxategiIzena { get; set; } = string.Empty;
        public string BideaZerbitzarian { get; set; } = string.Empty;
        public string? DokumentuIzena { get; set; }
        public string? Deskribapena { get; set; }
        public DateTime IgotzeData { get; set; } = DateTime.Now;

        public Dokumentua() { }

        public Dokumentua(int id, int jarraipenaId, string fitxategiIzena,
                          string bideaZerbitzarian, string? dokumentuIzena, string? deskribapena, DateTime igotzeData)
        {
            Id = id;
            JarraipenaId = jarraipenaId;
            FitxategiIzena = fitxategiIzena;
            BideaZerbitzarian = bideaZerbitzarian;
            DokumentuIzena = dokumentuIzena;
            Deskribapena = deskribapena;
            IgotzeData = igotzeData;
        }
    }
}
