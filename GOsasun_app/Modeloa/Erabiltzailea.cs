// ============================================================
// Erabiltzailea.cs - Erabiltzaile modeloa (User Model)
// ============================================================
// Sistemako erabiltzaile baten informazioa biltzen duen klasea.
// OBP (OOP) printzipioak jarraitzeko sortua.
// ============================================================

namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Erabiltzaile baten datu orokorrak biltzen dituen klase abstraktua.
    /// 'erabiltzaileak' taulari dagokio.
    /// </summary>
    public abstract class Erabiltzailea
    {
        public int Id { get; set; }
        public string Emaila { get; set; } = string.Empty;
        public string Pasahitza { get; set; } = string.Empty;
        public int RolId { get; set; }
        public string Nan { get; set; } = string.Empty;
        public string Izena { get; set; } = string.Empty;
        public string Abizenak { get; set; } = string.Empty;
        public DateTime JaiotzeData { get; set; }
        public string? Telefonoa { get; set; }
        public string? Helbidea { get; set; }
        public string? Herria { get; set; }
        public string? PostaKodea { get; set; }
        public string Irudia { get; set; } = "img/lehenetsia.png";
        public bool Aktibo { get; set; } = true;
        public DateTime SortzeData { get; set; } = DateTime.Now;
        public string Hizkuntza { get; set; } = "Euskara";

        public virtual string IzenOsoa => $"{Izena} {Abizenak}";
        public virtual string Rola => "Erabiltzailea";

        public virtual bool DaPazientea() => false;
        public virtual bool DaOsasunLangilea() => false;

        protected Erabiltzailea() { }

        protected Erabiltzailea(int id, string emaila, string pasahitza, int rolId, string nan, string izena, string abizenak, 
                                DateTime jaiotzeData, string? telefonoa, string? helbidea, string? herria, string? postaKodea, 
                                string irudia, bool aktibo, DateTime sortzeData, string hizkuntza = "Euskara")
        {
            Id = id;
            Emaila = emaila;
            Pasahitza = pasahitza;
            RolId = rolId;
            Nan = nan;
            Izena = izena;
            Abizenak = abizenak;
            JaiotzeData = jaiotzeData;
            Telefonoa = telefonoa;
            Helbidea = helbidea;
            Herria = herria;
            PostaKodea = postaKodea;
            Irudia = irudia;
            Aktibo = aktibo;
            SortzeData = sortzeData;
            Hizkuntza = hizkuntza;
        }
    }
}
