using System;
using System.Drawing;
using System.Windows.Forms;
using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class ErabiltzaileKudeaketaMenua : OinarriPantaila
    {
        private string _rolIzena;

        public ErabiltzaileKudeaketaMenua(string rolIzena, Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            _rolIzena = rolIzena;
            InitializeComponent();
            KonfiguratuInterfazea();
            KonfiguratuGertaerak();
        }

        private void KonfiguratuInterfazea()
        {
            this.Text = $"GOsasun - {_rolIzena} Kudeaketa";
            btnSortu.Testua = $"{_rolIzena.ToUpper()} SORTU";
            btnZerrendatu.Testua = $"{_rolIzena.ToUpper()}AK ZERRENDATU";
            btnSortu.Ikonoa = KargatuIkonoIrudia("plus-circle.svg");
            btnZerrendatu.Ikonoa = KargatuIkonoIrudia("list.svg");
        }

        private void KonfiguratuGertaerak()
        {
            btnSortu.Click += (s, e) => IrekiFormularioa(new ErabiltzaileaSortu(_rolIzena, _erabiltzailea!));
            btnZerrendatu.Click += (s, e) =>
            {
                if (_rolIzena == "Pazientea")
                    IrekiFormularioa(new PazienteenZerrenda(_erabiltzailea!));
                else
                    MessageBox.Show("Modulu hau garatzen ari da.", "Laster...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }
    }
}
