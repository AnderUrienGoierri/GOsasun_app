using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    public partial class AbisuenFormularioa : OinarriFormularioa
    {
        public AbisuenFormularioa() : base() { InitializeComponent(); }
        public AbisuenFormularioa(Erabiltzailea u) : base(u) { InitializeComponent(); GehituAtzeraBotoia(); }
    }
}
