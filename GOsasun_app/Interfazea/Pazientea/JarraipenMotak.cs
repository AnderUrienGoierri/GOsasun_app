using GOsasun_app.Modeloa;
using GOsasun_app.Kontrola.Zerbitzuak;
using GOsasun_app.Kontrola;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
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
    public partial class JarraipenMotak : OinarriPantaila
    {
        private readonly ErabiltzaileKontrolatzailea _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();
        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
        private readonly int? _pazienteIdAurrehautatu;
        private readonly string? _pazienteIzenburua;

        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public JarraipenMotak() : base()
        {
            _pazienteIdAurrehautatu = null;
            InitializeComponent();
            KargatuIkonoak();
        }

        public JarraipenMotak(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            _pazienteIdAurrehautatu = null;
            InitializeComponent();
            KargatuIkonoak();
            KonfiguratuGertaerak();
        }

        public JarraipenMotak(Erabiltzailea erabiltzailea, int pazienteId, string? pazienteIzenburua = null)
            : base(erabiltzailea)
        {
            _pazienteIdAurrehautatu = pazienteId;
            _pazienteIzenburua = pazienteIzenburua;
            InitializeComponent();
            KargatuIkonoak();
            KonfiguratuGertaerak();
            EguneratuIzenburua();
        }

        private void EguneratuIzenburua()
        {
            if (_pazienteIdAurrehautatu.HasValue && !string.IsNullOrWhiteSpace(_pazienteIzenburua))
            {
                Text = $"GOsasun - Jarraipen Motak - {_pazienteIzenburua}";
            }
        }

        private void KargatuIkonoak()
        {
            btnTentsiometroa.Ikonoa = KargatuTxartelIrudia("tentsiometroa.png");
            btnPisua.Ikonoa = KargatuTxartelIrudia("pisua.png");
            btnAltuera.Ikonoa = KargatuTxartelIrudia("altuera.png");
            btnOharra.Ikonoa = KargatuTxartelIrudia("abisua.png");
        }

        private void KonfiguratuGertaerak()
        {
            // Tentsiometroa: Pantaila berria ireki
            btnTentsiometroa.Click += (s, e) =>
            {
                Form formularioa = _pazienteIdAurrehautatu.HasValue
                    ? new TentsiometroNeurketak(_erabiltzailea!, _pazienteIdAurrehautatu.Value, _pazienteIzenburua)
                    : new TentsiometroNeurketak(_erabiltzailea!);
                IrekiFormularioa(formularioa);
            };

            // Pisua: eskuzko sarrera
            btnPisua.Click += (s, e) =>
            {
                Form formularioa = _pazienteIdAurrehautatu.HasValue
                    ? new EskuzkoNeurketak(_erabiltzailea!, true, _pazienteIdAurrehautatu.Value, _pazienteIzenburua)
                    : new EskuzkoNeurketak(_erabiltzailea!, true);
                IrekiFormularioa(formularioa);
            };

            // Altuera: eskuzko sarrera
            btnAltuera.Click += (s, e) =>
            {
                Form formularioa = _pazienteIdAurrehautatu.HasValue
                    ? new EskuzkoNeurketak(_erabiltzailea!, false, _pazienteIdAurrehautatu.Value, _pazienteIzenburua)
                    : new EskuzkoNeurketak(_erabiltzailea!, false);
                IrekiFormularioa(formularioa);
            };

            btnOharra.Click += (s, e) => SortuOharJarraipena();
        }

        private Image? KargatuTxartelIrudia(string fitxategiIzena)
        {
            foreach (string root in LortuBilaketaErroakTxartelentzat())
            {
                string[] aukerak =
                {
                    Path.Combine(root, "img", "icons", fitxategiIzena),
                    Path.Combine(root, "GOsasun_app", "img", "icons", fitxategiIzena),
                    Path.Combine(root, "img", "png", fitxategiIzena),
                    Path.Combine(root, "GOsasun_app", "img", "png", fitxategiIzena)
                };

                string? aurkitua = aukerak.FirstOrDefault(File.Exists);
                if (!string.IsNullOrWhiteSpace(aurkitua))
                {
                    using Image jatorrizkoa = Image.FromFile(aurkitua);
                    return new Bitmap(jatorrizkoa);
                }
            }

            return null;
        }

        private static IEnumerable<string> LortuBilaketaErroakTxartelentzat()
        {
            HashSet<string> erroak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string?[] hasierakoak =
            {
                Application.StartupPath,
                AppContext.BaseDirectory,
                Directory.GetCurrentDirectory(),
                Environment.CurrentDirectory,
                Path.GetDirectoryName(typeof(JarraipenMotak).Assembly.Location)
            };

            foreach (string? hasiera in hasierakoak)
            {
                if (string.IsNullOrWhiteSpace(hasiera) || !Directory.Exists(hasiera)) continue;

                DirectoryInfo? karpeta = new DirectoryInfo(hasiera);
                while (karpeta != null)
                {
                    erroak.Add(karpeta.FullName);
                    karpeta = karpeta.Parent;
                }
            }

            return erroak;
        }

        private void SortuOharJarraipena()
        {
            if (_erabiltzailea == null) return;

            List<Pazientea> pazienteak = new List<Pazientea>();
            int? pazienteId = _erabiltzailea.DaPazientea() ? _erabiltzailea.Id : _pazienteIdAurrehautatu;

            if (!_erabiltzailea.DaPazientea() && !_pazienteIdAurrehautatu.HasValue)
            {
                pazienteak = _erabiltzaileKontrolatzailea.LortuGuztiakPazienteak()
                    .OrderBy(p => p.IzenOsoa)
                    .ToList();

                if (pazienteak.Count == 0)
                {
                    MessageBox.Show("Ez dago pazienterik ohar bidezko jarraipena sortzeko.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            using Form elkarrizketa = new Form
            {
                Text = "Ohar bidezko jarraipena",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(760, (_erabiltzailea.DaPazientea() || _pazienteIdAurrehautatu.HasValue) ? 360 : 540)
            };

            int hurrengoY = 22;
            ComboBox? cmbPazienteak = null;
            TextBox? txtPazienteBilatu = null;
            Label? lblBilaketaEmaitza = null;
            List<Pazientea> unekoPazienteak = pazienteak;

            if (!_erabiltzailea.DaPazientea() && !_pazienteIdAurrehautatu.HasValue)
            {
                Label lblBilatuPazientea = new Label
                {
                    Text = "Bilatu pazientea (abizena, izena edo NAN/DNI)",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(44, 62, 80),
                    Location = new Point(24, hurrengoY),
                    Size = new Size(460, 34)
                };
                elkarrizketa.Controls.Add(lblBilatuPazientea);

                txtPazienteBilatu = new TextBox
                {
                    Font = new Font("Segoe UI", 11F),
                    Location = new Point(24, hurrengoY + 38),
                    Size = new Size(712, 38),
                    PlaceholderText = "Idatzi abizena, izena edo NAN/DNI..."
                };
                elkarrizketa.Controls.Add(txtPazienteBilatu);

                hurrengoY += 92;

                Label lblPazientea = new Label
                {
                    Text = "Pazientea",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(44, 62, 80),
                    Location = new Point(24, hurrengoY),
                    Size = new Size(140, 34)
                };
                elkarrizketa.Controls.Add(lblPazientea);

                cmbPazienteak = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 11F),
                    Location = new Point(24, hurrengoY + 38),
                    Size = new Size(712, 38),
                    DataSource = pazienteak,
                    DisplayMember = "IzenOsoa",
                    ValueMember = "Id"
                };
                elkarrizketa.Controls.Add(cmbPazienteak);

                lblBilaketaEmaitza = new Label
                {
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(90, 90, 90),
                    Location = new Point(24, hurrengoY + 80),
                    Size = new Size(712, 24)
                };
                elkarrizketa.Controls.Add(lblBilaketaEmaitza);

                void KargatuPazienteakBilaketarekin(string? bilaketa, int? hautatutakoPazienteId = null)
                {
                    unekoPazienteak = string.IsNullOrWhiteSpace(bilaketa)
                        ? pazienteak.OrderBy(p => p.IzenOsoa).ToList()
                        : _erabiltzaileKontrolatzailea.LortuGuztiakPazienteak(bilaketa.Trim()).OrderBy(p => p.IzenOsoa).ToList();

                    cmbPazienteak.DataSource = null;
                    cmbPazienteak.DisplayMember = "IzenOsoa";
                    cmbPazienteak.ValueMember = "Id";
                    cmbPazienteak.DataSource = unekoPazienteak;
                    cmbPazienteak.Enabled = unekoPazienteak.Count > 0;

                    if (hautatutakoPazienteId.HasValue)
                    {
                        int indizea = unekoPazienteak.FindIndex(p => p.Id == hautatutakoPazienteId.Value);
                        cmbPazienteak.SelectedIndex = indizea >= 0 ? indizea : -1;
                    }
                    else if (unekoPazienteak.Count == 1)
                    {
                        cmbPazienteak.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbPazienteak.SelectedIndex = unekoPazienteak.Count > 0 ? 0 : -1;
                    }

                    lblBilaketaEmaitza!.Text = unekoPazienteak.Count switch
                    {
                        0 => "Ez da pazienterik aurkitu.",
                        1 => "Paziente 1 aurkitu da.",
                        _ => $"{unekoPazienteak.Count} paziente aurkitu dira."
                    };
                }

                txtPazienteBilatu.TextChanged += (s, e) =>
                {
                    int? hautatutakoPazienteId = cmbPazienteak.SelectedItem is Pazientea hautatutakoPazientea
                        ? hautatutakoPazientea.Id
                        : null;
                    KargatuPazienteakBilaketarekin(txtPazienteBilatu.Text, hautatutakoPazienteId);
                };

                KargatuPazienteakBilaketarekin(null);
                hurrengoY += 126;
            }

            Label lblOharrak = new Label
            {
                Text = "Idatzi oharra",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(24, hurrengoY),
                Size = new Size(200, 34)
            };
            elkarrizketa.Controls.Add(lblOharrak);

            TextBox txtOharrak = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 11F),
                Location = new Point(24, hurrengoY + 40),
                Size = new Size(712, 180),
                PlaceholderText = "Jarraipen honen oharra idatzi hemen..."
            };
            elkarrizketa.Controls.Add(txtOharrak);

            const int botoiAltuera = 50;
            const int btnUtziZabalera = 170;
            const int btnGordeZabalera = 310;
            const int botoiArtekoTartea = 16;
            const int eskuinekoMarjina = 24;
            int botoienGoikoa = elkarrizketa.ClientSize.Height - 70;

            Button btnUtzi = new Button
            {
                Text = "Utzi",
                DialogResult = DialogResult.Cancel,
                BackColor = Color.FromArgb(127, 140, 141),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(elkarrizketa.ClientSize.Width - eskuinekoMarjina - btnGordeZabalera - botoiArtekoTartea - btnUtziZabalera, botoienGoikoa),
                Size = new Size(btnUtziZabalera, botoiAltuera)
            };
            btnUtzi.FlatAppearance.BorderSize = 0;

            Button btnGorde = new Button
            {
                Text = "Jarraipena sortu",
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                Location = new Point(elkarrizketa.ClientSize.Width - eskuinekoMarjina - btnGordeZabalera, botoienGoikoa),
                Size = new Size(btnGordeZabalera, botoiAltuera)
            };
            btnGorde.FlatAppearance.BorderSize = 0;
            btnGorde.Click += (s, e) =>
            {
                string oharra = txtOharrak.Text.Trim();
                if (string.IsNullOrWhiteSpace(oharra))
                {
                    MessageBox.Show("Oharra idatzi behar da ohar bidezko jarraipena sortzeko.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbPazienteak != null)
                {
                    pazienteId = cmbPazienteak.SelectedValue as int?;
                    if (!pazienteId.HasValue && cmbPazienteak.SelectedItem is Pazientea hautatutakoPazientea)
                    {
                        pazienteId = hautatutakoPazientea.Id;
                    }
                }

                if (!pazienteId.HasValue)
                {
                    MessageBox.Show("Paziente bat aukeratu behar da.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Jarraipena berria = new Jarraipena
                {
                    PazienteId = pazienteId.Value,
                    OsasunLangileId = _erabiltzailea.DaOsasunLangilea() ? _erabiltzailea.Id : null,
                    ErregistroData = DateTime.Now,
                    Oharrak = oharra
                };

                if (_jarraipenaKontrolatzailea.GordeJarraipena(berria))
                {
                    _jarraipenaKontrolatzailea.EsportatuXML(berria);
                    MessageBox.Show("Ohar bidezko jarraipena ondo gorde da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    elkarrizketa.DialogResult = DialogResult.OK;
                    elkarrizketa.Close();
                    return;
                }

                MessageBox.Show("Errorea gertatu da ohar bidezko jarraipena gordetzean.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            elkarrizketa.AcceptButton = btnGorde;
            elkarrizketa.CancelButton = btnUtzi;
            elkarrizketa.Controls.Add(btnUtzi);
            elkarrizketa.Controls.Add(btnGorde);
            elkarrizketa.ShowDialog(this);
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
