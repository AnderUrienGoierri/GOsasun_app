using GOsasun_app.Modeloa;
using GOsasun_app.Kontrola.Zerbitzuak;
using GOsasun_app.Kontrola;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Medikuak hautatu dezakeen neurketa moten formularioa.
    /// (Tentsioa, Glukosa, Pisua, Altuera)
    /// </summary>
    public partial class NeurketaMotak : OinarriPantaila
    {
        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public NeurketaMotak() : base()
        {
            InitializeComponent();
        }

        public NeurketaMotak(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            InitializeComponent();
            KonfiguratuGertaerak();
        }

        private void KonfiguratuGertaerak()
        {
            // Tentsiometroa: Pantaila berria ireki
            btnTentsiometroa.Click += (s, e) => IrekiFormularioa(new TentsiometroNeurketak(_erabiltzailea!));

            // Pisua: eskuzko sarrera
            btnPisua.Click += (s, e) => IrekiFormularioa(new EskuzkoNeurketak(_erabiltzailea!, true));

            // Altuera: eskuzko sarrera
            btnAltuera.Click += (s, e) => IrekiFormularioa(new EskuzkoNeurketak(_erabiltzailea!, false));
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }

        private void _edukiPanela_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
