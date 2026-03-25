// ============================================================
// Erabiltzailea.cs - Erabiltzaile modeloa (User Model)
// ============================================================
// Sistemako erabiltzaile baten informazioa biltzen duen klasea.
// OBP (OOP) printzipioak jarraitzeko sortua.
// ============================================================

namespace GOsasun_app.Modeloak
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
        public bool Aktibo { get; set; } = true;
        public DateTime SortzeData { get; set; } = DateTime.Now;

        // API bateragarritasunerako kideak
        public abstract string Izena { get; set; }
        public abstract string Abizenak { get; set; }
        public virtual string IzenOsoa => $"{Izena} {Abizenak}";
        public abstract string Rola { get; }

        public virtual bool DaPazientea() => false;
        public virtual bool DaMedikua() => false;

        protected Erabiltzailea() { }

        protected Erabiltzailea(int id, string emaila, string pasahitza, int rolId, bool aktibo, DateTime sortzeData)
        {
            Id = id;
            Emaila = emaila;
            Pasahitza = pasahitza;
            RolId = rolId;
            Aktibo = aktibo;
            SortzeData = sortzeData;
        }
    }
}
