namespace GOsasun_app.Modeloa
{
    /// <summary>
    /// Erabiltzaile rol baten datuak biltzen dituen klasea.
    /// 'rolak' taulari dagokio.
    /// </summary>
    public class Rola
    {
        public int RolId { get; set; }
        public string Izena { get; set; } = string.Empty;

        public Rola() { }

        public Rola(int rolId, string izena)
        {
            RolId = rolId;
            Izena = izena;
        }
    }
}
