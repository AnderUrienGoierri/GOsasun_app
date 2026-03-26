using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public partial class PazienteMenua : OinarriFormularioa
    {
        public PazienteMenua() : base() { InitializeComponent(); }
        public PazienteMenua(Erabiltzailea u) : base(u) { InitializeComponent(); GehituAtzeraBotoia(); }
    }
}
