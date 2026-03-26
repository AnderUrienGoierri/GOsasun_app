using GOsasun_app.Modeloak;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Harrerako langileentzako menu nagusia.
    /// </summary>
    public partial class HarreraMenua : OinarriFormularioa
    {
        public HarreraMenua() : base()
        {
            InitializeComponent();
        }

        public HarreraMenua(Erabiltzailea u) : base(u)
        {
            InitializeComponent();
        }
    }
}
