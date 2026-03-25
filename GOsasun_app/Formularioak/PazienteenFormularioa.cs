using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public partial class PazienteenFormularioa : OinarriFormularioa
    {
        public PazienteenFormularioa() : base() { InitializeComponent(); }
        public PazienteenFormularioa(Erabiltzailea u) : base(u) { InitializeComponent(); GehituAtzeraBotoia(); }
    }
}
