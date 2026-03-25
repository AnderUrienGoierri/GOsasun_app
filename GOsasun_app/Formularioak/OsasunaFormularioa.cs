// ============================================================
// OsasunaFormularioa.cs - Osasuna Formularioa
// ============================================================
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public partial class OsasunaFormularioa : OinarriFormularioa
    {
        public OsasunaFormularioa() : base() { InitializeComponent(); }
        public OsasunaFormularioa(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            InitializeComponent();
            GehituAtzeraBotoia();
        }
    }
}
