// ============================================================
// KontaktuaFormularioa.cs - Kontaktua Formularioa
// ============================================================
using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public partial class KontaktuaFormularioa : OinarriFormularioa
    {
        public KontaktuaFormularioa() : base() { InitializeComponent(); }
        public KontaktuaFormularioa(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            InitializeComponent();
        }
    }
}
