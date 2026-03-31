using GOsasun_app.Modeloak;
using GOsasun_app.Zerbitzuak;
using GOsasun_app.Kontrolatzaileak;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Medikuak hautatu dezakeen neurketa moten formularioa.
    /// (Tentsioa, Glukosa, Pisua, Altuera)
    /// </summary>
    public partial class NeurketaMotakFormularioa : OinarriFormularioa
    {
        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public NeurketaMotakFormularioa() : base()
        {
            InitializeComponent();
        }

        public NeurketaMotakFormularioa(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            InitializeComponent();
            KonfiguratuGertaerak();
        }

        private void KonfiguratuGertaerak()
        {
            // Tentsiometroa: Pantaila berria ireki
            btnTentsiometroa.Click += (s, e) => IrekiFormularioa(new TentsiometroNeurketaFormularioa(_erabiltzailea!));

            // Pisua: eskuzko sarrera
            btnPisua.Click += (s, e) => IrekiFormularioa(new EskuzkoNeurketaFormularioa(_erabiltzailea!, true));

            // Altuera: eskuzko sarrera
            btnAltuera.Click += (s, e) => IrekiFormularioa(new EskuzkoNeurketaFormularioa(_erabiltzailea!, false));
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
