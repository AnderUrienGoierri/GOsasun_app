// ============================================================
// KontaktuaFormularioa.cs - Kontaktua Formularioa
// ============================================================
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public class KontaktuaFormularioa : OinarriFormularioa
    {
        public KontaktuaFormularioa() : base() { }
        public KontaktuaFormularioa(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            GehituAtzeraBotoia();
        }
    }
}
