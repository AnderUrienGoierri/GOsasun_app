using System;
using System.Windows.Forms;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class ErrezetakMenua : OinarriPantaila
    {
        public ErrezetakMenua() : base()
        {
            InitializeComponent();
            KargatuIkonoak();
        }

        public ErrezetakMenua(Erabiltzailea u) : base(u)
        {
            InitializeComponent();
            KargatuIkonoak();
            btnErrezetaSortu.Click += (s, e) => IrekiFormularioa(new ErrezetaSortu(_erabiltzailea!));
            btnErrezetakIkusi.Click += (s, e) => IrekiFormularioa(new ErrezetakIkusi(_erabiltzailea!));
        }

        private void KargatuIkonoak()
        {
            btnErrezetaSortu.Ikonoa = KargatuIkonoIrudia("clipboard-pen.svg");
            btnErrezetakIkusi.Ikonoa = KargatuIkonoIrudia("pill.svg");
        }

        private void IrekiFormularioa(Form form)
        {
            form.FormClosed += (s, e) => this.Show();
            this.Hide();
            form.Show();
        }

    }
}
