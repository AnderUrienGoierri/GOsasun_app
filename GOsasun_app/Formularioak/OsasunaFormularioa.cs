// ============================================================
// OsasunaFormularioa.cs - Osasuna Formularioa
// ============================================================
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public class OsasunaFormularioa : OinarriFormularioa
    {
        public OsasunaFormularioa() : base() { }
        public OsasunaFormularioa(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            GehituAtzeraBotoia();
        }
    }
}
