// ============================================================
// Osasuna.cs - Osasuna Formularioa
// ============================================================
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class Osasuna : GoiburuPanela
    {
        public Osasuna() : base() { InitializeComponent(); }
        public Osasuna(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            InitializeComponent();
        }
    }
}
