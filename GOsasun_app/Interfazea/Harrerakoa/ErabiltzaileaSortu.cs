using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Windows.Forms;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class ErabiltzaileaSortu : OinarriPantaila
    {
        private const string IrudiLehenetsia = "img/png/irudi_lehenetsia.png";
        private readonly string _rolIzena;
        private readonly ErabiltzaileKontrolatzailea _kontrolatzailea;
        private readonly int? _esleitutakoLangileId;
        private readonly List<OsasunLangilea> _langileGuztiak = new List<OsasunLangilea>();
        private readonly List<OsasunLangilea> _hautatutakoLangileak = new List<OsasunLangilea>();
        private string? _hautatutakoIrudiBidea;

        public ErabiltzaileaSortu(string rolIzena, Erabiltzailea unekoLangilea, int? esleitutakoLangileId = null) : base(unekoLangilea)
        {
            _rolIzena = rolIzena;
            _kontrolatzailea = new ErabiltzaileKontrolatzailea();
            _esleitutakoLangileId = esleitutakoLangileId;

            InitializeComponent();

            // Gehitu kontrol berriak base klaseko eduki-panelera
            if (_edukiPanela != null)
            {
                _edukiPanela.Controls.Add(this.lblIntegrazioa);
                _edukiPanela.Controls.Add(this.pnlForm);
            }

            KonfiguratuIkuspegia();
            KargatuIrudiLehenetsia();
            KargatuOsasunLangileak();
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
            lblOdolTaldea.Visible = cmbOdolTaldea.Visible = false;
            lblPisua.Visible = txtPisua.Visible = false;
            lblAltuera.Visible = txtAltuera.Visible = false;
            lblElkargokide.Visible = txtElkargokide.Visible = txtEspezialitatea.Visible = false;
            lblKontsulta.Visible = txtKontsulta.Visible = cmbLanaldia.Visible = false;
            lblTxanda.Visible = cmbTxanda.Visible = false;
            lblOsasunLangilea.Visible = cmbOsasunLangileak.Visible = btnLangileaGehitu.Visible = false;
            lblEsleitutakoLangileak.Visible = lstEsleitutakoLangileak.Visible = btnLangileaKendu.Visible = false;

            if (RolPazienteaDa())
            {
                lblSexua.Visible = cmbSexua.Visible = true;
                lblJaiotzeData.Visible = dtpJaiotzeData.Visible = true;
                lblTelefonoa.Visible = txtTelefonoa.Visible = true;
                lblHelbidea.Visible = txtHelbidea.Visible = true;
                lblHerria.Visible = txtHerria.Visible = txtPostaKodea.Visible = true;
                lblOdolTaldea.Visible = cmbOdolTaldea.Visible = true;
                lblPisua.Visible = txtPisua.Visible = true;
                lblAltuera.Visible = txtAltuera.Visible = true;
                lblOsasunLangilea.Visible = cmbOsasunLangileak.Visible = btnLangileaGehitu.Visible = true;
                lblEsleitutakoLangileak.Visible = lstEsleitutakoLangileak.Visible = btnLangileaKendu.Visible = true;
            }
            else if (RolOsasunLangileaDa())
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

            cmbHizkuntza.SelectedIndex = 0;
            cmbSexua.SelectedIndex = 0;
            cmbOdolTaldea.SelectedIndex = 0;
            cmbLanaldia.SelectedIndex = 0;
            cmbTxanda.SelectedIndex = 0;

            cmbOsasunLangileak.DropDownStyle = ComboBoxStyle.DropDown;
            cmbOsasunLangileak.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbOsasunLangileak.AutoCompleteSource = AutoCompleteSource.ListItems;

            btnIrudiaAukeratu.Click += BtnIrudiaAukeratu_Click;
            btnLangileaGehitu.Click += BtnLangileaGehitu_Click;
            btnLangileaKendu.Click += BtnLangileaKendu_Click;
        }

        private bool RolPazienteaDa()
        {
            return string.Equals(_rolIzena, "Pazientea", StringComparison.OrdinalIgnoreCase);
        }

        private bool RolOsasunLangileaDa()
        {
            return string.Equals(_rolIzena, "Osasun Langilea", StringComparison.OrdinalIgnoreCase)
                || string.Equals(_rolIzena, "OsasunLangilea", StringComparison.OrdinalIgnoreCase)
                || string.Equals(_rolIzena, "Medikua", StringComparison.OrdinalIgnoreCase);
        }

        private void KargatuOsasunLangileak()
        {
            if (!RolPazienteaDa())
            {
                return;
            }

            _langileGuztiak.Clear();
            _langileGuztiak.AddRange(_kontrolatzailea.LortuGuztiakOsasunLangileak()
                .OrderBy(langilea => langilea.IzenOsoa));

            if (_esleitutakoLangileId.HasValue)
            {
                OsasunLangilea? aurkitutakoa = _langileGuztiak.FirstOrDefault(langilea => langilea.Id == _esleitutakoLangileId.Value);
                if (aurkitutakoa != null)
                {
                    _hautatutakoLangileak.Add(aurkitutakoa);
                }
            }

            EguneratuLangileenAukerak();
            EguneratuHautatutakoLangileenZerrenda();
        }

        private void EguneratuLangileenAukerak()
        {
            List<OsasunLangilea> aukerak = _langileGuztiak
                .Where(langilea => _hautatutakoLangileak.All(hautatua => hautatua.Id != langilea.Id))
                .OrderBy(langilea => langilea.IzenOsoa)
                .ToList();

            cmbOsasunLangileak.DataSource = null;
            cmbOsasunLangileak.DisplayMember = nameof(Erabiltzailea.IzenOsoa);
            cmbOsasunLangileak.ValueMember = nameof(Erabiltzailea.Id);
            cmbOsasunLangileak.DataSource = aukerak;

            if (aukerak.Count > 0)
            {
                cmbOsasunLangileak.SelectedIndex = 0;
            }
            else
            {
                cmbOsasunLangileak.Text = string.Empty;
            }
        }

        private void EguneratuHautatutakoLangileenZerrenda()
        {
            lstEsleitutakoLangileak.DataSource = null;
            lstEsleitutakoLangileak.DisplayMember = nameof(Erabiltzailea.IzenOsoa);
            lstEsleitutakoLangileak.ValueMember = nameof(Erabiltzailea.Id);
            lstEsleitutakoLangileak.DataSource = _hautatutakoLangileak
                .OrderBy(langilea => langilea.IzenOsoa)
                .ToList();
        }

        private void BtnLangileaGehitu_Click(object? sender, EventArgs e)
        {
            if (cmbOsasunLangileak.SelectedItem is not OsasunLangilea hautatutakoa)
            {
                MessageBox.Show("Hautatu osasun langile bat zerrendatik.", "Kontuz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_hautatutakoLangileak.Any(langilea => langilea.Id == hautatutakoa.Id))
            {
                return;
            }

            _hautatutakoLangileak.Add(hautatutakoa);
            EguneratuLangileenAukerak();
            EguneratuHautatutakoLangileenZerrenda();
        }

        private void BtnLangileaKendu_Click(object? sender, EventArgs e)
        {
            if (lstEsleitutakoLangileak.SelectedItem is not OsasunLangilea hautatutakoa)
            {
                return;
            }

            _hautatutakoLangileak.RemoveAll(langilea => langilea.Id == hautatutakoa.Id);
            EguneratuLangileenAukerak();
            EguneratuHautatutakoLangileenZerrenda();
        }

        private void BtnIrudiaAukeratu_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialogoa = new OpenFileDialog
            {
                Title = "Hautatu erabiltzailearen irudia",
                Filter = "Irudiak|*.png;*.jpg;*.jpeg;*.bmp",
                Multiselect = false
            };

            if (dialogoa.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            _hautatutakoIrudiBidea = dialogoa.FileName;
            lblIrudiFitxategia.Text = Path.GetFileName(dialogoa.FileName);
            KargatuIrudiaAurrebistan(dialogoa.FileName);
        }

        private void KargatuIrudiLehenetsia()
        {
            lblIrudiFitxategia.Text = "Irudi lehenetsia";
            string? lehenetsia = BilatuFitxategia(IrudiLehenetsia);
            if (!string.IsNullOrWhiteSpace(lehenetsia))
            {
                KargatuIrudiaAurrebistan(lehenetsia);
            }
        }

        private string? BilatuFitxategia(string erlatiboa)
        {
            string bideNormala = erlatiboa.Replace('/', Path.DirectorySeparatorChar);
            string[] hautagaiak =
            {
                Path.Combine(Application.StartupPath, bideNormala),
                Path.Combine(Directory.GetCurrentDirectory(), bideNormala),
                Path.Combine(Directory.GetCurrentDirectory(), "GOsasun_app", bideNormala),
                Path.Combine(AppContext.BaseDirectory, bideNormala),
                Path.Combine(AppContext.BaseDirectory, "..", "..", "..", bideNormala)
            };

            return hautagaiak
                .Select(Path.GetFullPath)
                .FirstOrDefault(File.Exists);
        }

        private void KargatuIrudiaAurrebistan(string bidea)
        {
            using Image jatorrizkoa = Image.FromFile(bidea);
            Image? aurrekoa = pbIrudia.Image;
            pbIrudia.Image = new Bitmap(jatorrizkoa);
            aurrekoa?.Dispose();
        }

        private void btnGorde_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIzena.Text) || 
                string.IsNullOrWhiteSpace(txtAbizenak.Text) || 
                string.IsNullOrWhiteSpace(txtEmaila.Text) || 
                string.IsNullOrWhiteSpace(txtPasahitza.Text) ||
                (RolPazienteaDa() && string.IsNullOrWhiteSpace(txtNan.Text)))
            {
                MessageBox.Show("(*) markatutako eremuak nahitaezkoak dira.", "Kontuz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (RolPazienteaDa() && _hautatutakoLangileak.Count == 0)
            {
                MessageBox.Show("Gutxienez osasun langile bat hautatu behar duzu pazientearentzat.", "Kontuz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ondoGordeta = false;
            string hizkuntzaSelected = cmbHizkuntza.SelectedItem?.ToString() ?? "Euskara";

            if (RolPazienteaDa())
            {
                if (!SaiatuLortuDecimala(txtPisua.Text, "Pisua", out decimal? pisua)
                    || !SaiatuLortuDecimala(txtAltuera.Text, "Altuera", out decimal? altuera))
                {
                    return;
                }

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
                    PostaKodea = txtPostaKodea.Text,
                    OdolTaldea = string.IsNullOrWhiteSpace(cmbOdolTaldea.Text) ? null : cmbOdolTaldea.Text,
                    AzkenPisua = pisua,
                    AzkenAltuera = altuera
                };
                ondoGordeta = _kontrolatzailea.SortuPazientea(
                    p,
                    _hautatutakoLangileak.Select(langilea => langilea.Id).ToArray(),
                    _hautatutakoIrudiBidea);
            }
            else if (RolOsasunLangileaDa())
            {
                OsasunLangilea m = new OsasunLangilea
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
                ondoGordeta = _kontrolatzailea.SortuOsasunLangilea(m, _hautatutakoIrudiBidea);
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
                ondoGordeta = _kontrolatzailea.SortuHarrerakoa(h, _hautatutakoIrudiBidea);
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

        private bool SaiatuLortuDecimala(string testua, string eremuIzena, out decimal? balioa)
        {
            balioa = null;

            if (string.IsNullOrWhiteSpace(testua))
            {
                return true;
            }

            string balioGarbitua = testua.Trim();
            if (decimal.TryParse(balioGarbitua, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal emaitza)
                || decimal.TryParse(balioGarbitua, NumberStyles.Number, CultureInfo.InvariantCulture, out emaitza))
            {
                balioa = emaitza;
                return true;
            }

            MessageBox.Show($"{eremuIzena} zenbakiz sartu behar da.", "Kontuz", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

    }
}
