// ============================================================
// Kontaktua.cs - Kontaktua Formularioa
// ============================================================
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class Kontaktua : OinarriPantaila
    {
        public Kontaktua() : base() { InitializeComponent(); }
        public Kontaktua(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            InitializeComponent();
        }
    }
}
