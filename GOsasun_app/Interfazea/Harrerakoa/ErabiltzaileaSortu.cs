using System;
using System.Windows.Forms;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class ErabiltzaileaSortu : OinarriPantaila
    {
        private string _rolIzena;
        private ErabiltzaileKontrolatzailea _kontrolatzailea;

        public ErabiltzaileaSortu(string rolIzena, Erabiltzailea unekoLangilea) : base(unekoLangilea)
        {
            _rolIzena = rolIzena;
            _kontrolatzailea = new ErabiltzaileKontrolatzailea();
            
            InitializeComponent();

            // Gehitu kontrol berriak base klaseko eduki-panelera
            if (_edukiPanela != null)
            {
                _edukiPanela.Controls.Add(this.lblIntegrazioa);
                _edukiPanela.Controls.Add(this.pnlForm);
            }

            KonfiguratuIkuspegia();
        }

        private void KonfiguratuIkuspegia()
        {
            lblIntegrazioa.Text = $"{_rolIzena.ToUpper()} SORTU";

            // Denak ezkutatu hasieran (Hizkuntza, Izena, Abizena, Emaila, Pasahitza eta NAN izan ezik)
            lblSexua.Visible = cmbSexua.Visible = false;
            lblJaiotzeData.Visible = dtpJaiotzeData.Visible = false;
            lblTelefonoa.Visible = txtTelefonoa.Visible = false;
            lblHelbidea.Visible = txtHelbidea.Visible = false;
            lblHerria.Visible = txtHerria.Visible = txtPostaKodea.Visible = false;
            lblElkargokide.Visible = txtElkargokide.Visible = txtEspezialitatea.Visible = false;
            lblKontsulta.Visible = txtKontsulta.Visible = cmbLanaldia.Visible = false;
            lblTxanda.Visible = cmbTxanda.Visible = false;

            if (_rolIzena == "Pazientea")
            {
                lblSexua.Visible = cmbSexua.Visible = true;
                lblJaiotzeData.Visible = dtpJaiotzeData.Visible = true;
                lblTelefonoa.Visible = txtTelefonoa.Visible = true;
                lblHelbidea.Visible = txtHelbidea.Visible = true;
                lblHerria.Visible = txtHerria.Visible = txtPostaKodea.Visible = true;
            }
            else if (_rolIzena == "Medikua")
            {
                lblJaiotzeData.Visible = dtpJaiotzeData.Visible = true;
                lblTelefonoa.Visible = txtTelefonoa.Visible = true;
                lblElkargokide.Visible = txtElkargokide.Visible = txtEspezialitatea.Visible = true;
                lblKontsulta.Visible = txtKontsulta.Visible = cmbLanaldia.Visible = true;
                // Medikuak ez du NAN behar sorkuntzan (baina erabiltzailea denez badu ID)
                lblNan.Visible = txtNan.Visible = false;
            }
            else if (_rolIzena == "Harrerako Langilea")
            {
                lblJaiotzeData.Visible = dtpJaiotzeData.Visible = true;
                lblTelefonoa.Visible = txtTelefonoa.Visible = true;
                lblTxanda.Visible = cmbTxanda.Visible = true;
                lblNan.Visible = txtNan.Visible = false;
            }
        }

        private void btnGorde_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIzena.Text) || 
                string.IsNullOrWhiteSpace(txtAbizenak.Text) || 
                string.IsNullOrWhiteSpace(txtEmaila.Text) || 
                string.IsNullOrWhiteSpace(txtPasahitza.Text) ||
                (_rolIzena == "Pazientea" && string.IsNullOrWhiteSpace(txtNan.Text)))
            {
                MessageBox.Show("(*) markatutako eremuak nahitaezkoak dira.", "Kontuz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ondoGordeta = false;
            string hizkuntzaSelected = cmbHizkuntza.SelectedItem?.ToString() ?? "Euskara";

            if (_rolIzena == "Pazientea")
            {
                Pazientea p = new Pazientea
                {
                    Emaila = txtEmaila.Text,
                    Pasahitza = txtPasahitza.Text,
                    Hizkuntza = hizkuntzaSelected,
                    Izena = txtIzena.Text,
                    Abizenak = txtAbizenak.Text,
                    Nan = txtNan.Text,
                    Sexua = cmbSexua.SelectedItem?.ToString() ?? "Gizona",
                    JaiotzeData = dtpJaiotzeData.Value,
                    Telefonoa = txtTelefonoa.Text,
                    Helbidea = txtHelbidea.Text,
                    Herria = txtHerria.Text,
                    PostaKodea = txtPostaKodea.Text
                };
                ondoGordeta = _kontrolatzailea.SortuPazientea(p);
            }
            else if (_rolIzena == "Medikua")
            {
                Medikua m = new Medikua
                {
                    Emaila = txtEmaila.Text,
                    Pasahitza = txtPasahitza.Text,
                    Hizkuntza = hizkuntzaSelected,
                    Izena = txtIzena.Text,
                    Abizenak = txtAbizenak.Text,
                    JaiotzeData = dtpJaiotzeData.Value,
                    ElkargokideZenbakia = txtElkargokide.Text,
                    Espezialitatea = txtEspezialitatea.Text,
                    Kontsulta = txtKontsulta.Text,
                    Lanaldia = cmbLanaldia.SelectedItem?.ToString() ?? "Osoa",
                    Telefonoa = txtTelefonoa.Text
                };
                ondoGordeta = _kontrolatzailea.SortuMedikua(m);
            }
            else if (_rolIzena == "Harrerako Langilea")
            {
                HarrerakoLangilea h = new HarrerakoLangilea
                {
                    Emaila = txtEmaila.Text,
                    Pasahitza = txtPasahitza.Text,
                    Hizkuntza = hizkuntzaSelected,
                    Izena = txtIzena.Text,
                    Abizenak = txtAbizenak.Text,
                    Txanda = cmbTxanda.SelectedItem?.ToString() ?? "Goizez",
                    JaiotzeData = dtpJaiotzeData.Value,
                    Telefonoa = txtTelefonoa.Text
                };
                ondoGordeta = _kontrolatzailea.SortuHarrerakoa(h);
            }

            if (ondoGordeta)
            {
                MessageBox.Show($"{_rolIzena} ondo gorde da sistemako datu-basean.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Errorea gertatu da gordetzean. Ziurtatu e-maila edota NAN-a ez direla errepikatzen ari.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
