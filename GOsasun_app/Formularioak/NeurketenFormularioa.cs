using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public partial class NeurketenFormularioa : OinarriFormularioa
    {
        public NeurketenFormularioa() : base() { InitializeComponent(); }
        public NeurketenFormularioa(Erabiltzailea u) : base(u) { InitializeComponent(); GehituAtzeraBotoia(); }
    }
}
