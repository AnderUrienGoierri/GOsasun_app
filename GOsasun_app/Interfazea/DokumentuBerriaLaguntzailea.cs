using System.ComponentModel;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class DokumentuBerriaLaguntzailea : GOsasunForm
    {
        private readonly List<Pazientea> _hasierakoPazienteak = new List<Pazientea>();
        private List<Pazientea> _unekoPazienteak = new List<Pazientea>();
        private Func<string?, List<Pazientea>>? _pazienteBilaketaHornitzailea;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public DokumentuBerriaLaguntzailea()
        {
            InitializeComponent();
            KonfiguratuElkarrizketa();
        }

        public int? PazienteId { get; private set; }

        public string DokumentuIzena => txtDokumentuIzena.Text.Trim();

        public string Deskribapena => txtDeskribapena.Text.Trim();

        public string PdfFitxategiBidea => txtPdfFitxategia.Text.Trim();

        private void KonfiguratuElkarrizketa()
        {
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = btnGorde;
            CancelButton = btnUtzi;

            txtPazienteBilaketa.TextChanged += TxtPazienteBilaketa_TextChanged;
            lstPazienteak.SelectedIndexChanged += LstPazienteak_SelectedIndexChanged;
            btnPdfHautatu.Click += BtnPdfHautatu_Click;
            btnGorde.Click += BtnGorde_Click;
        }

        public void Hasieratu(
            IEnumerable<Pazientea> pazienteak,
            bool erakutsiPazienteAukeraketa,
            Func<string?, List<Pazientea>>? pazienteBilaketaHornitzailea)
        {
            _pazienteBilaketaHornitzailea = pazienteBilaketaHornitzailea;
            _hasierakoPazienteak.Clear();
            _hasierakoPazienteak.AddRange(pazienteak.OrderBy(p => p.IzenOsoa));
            _unekoPazienteak = _hasierakoPazienteak.ToList();

            PazienteId = null;
            txtPazienteBilaketa.Clear();
            txtDokumentuIzena.Clear();
            txtDeskribapena.Clear();
            txtPdfFitxategia.Clear();

            EzarriPazienteAukeraketaIkusgai(erakutsiPazienteAukeraketa);

            if (erakutsiPazienteAukeraketa)
            {
                KargatuPazienteakBilaketarekin(null);
                ActiveControl = txtPazienteBilaketa;
                return;
            }

            Pazientea? pazientea = _hasierakoPazienteak.FirstOrDefault();
            PazienteId = pazientea?.Id;
            ActiveControl = txtDokumentuIzena;
        }

        private void EzarriPazienteAukeraketaIkusgai(bool ikusgai)
        {
            lblPazienteBilaketa.Visible = ikusgai;
            txtPazienteBilaketa.Visible = ikusgai;
            lstPazienteak.Visible = ikusgai;
            lblPazienteakEgoera.Visible = ikusgai;

            if (ikusgai)
            {
                ClientSize = new Size(720, 742);
                lblDokumentuIzena.Location = new Point(24, 318);
                txtDokumentuIzena.Location = new Point(24, 348);
                lblPdfFitxategia.Location = new Point(24, 400);
                txtPdfFitxategia.Location = new Point(24, 430);
                btnPdfHautatu.Location = new Point(522, 428);
                lblDeskribapena.Location = new Point(24, 482);
                txtDeskribapena.Location = new Point(24, 512);
                btnGorde.Location = new Point(464, 676);
                btnUtzi.Location = new Point(564, 676);
                return;
            }

            ClientSize = new Size(720, 520);
            lblDokumentuIzena.Location = new Point(24, 24);
            txtDokumentuIzena.Location = new Point(24, 54);
            lblPdfFitxategia.Location = new Point(24, 106);
            txtPdfFitxategia.Location = new Point(24, 136);
            btnPdfHautatu.Location = new Point(522, 134);
            lblDeskribapena.Location = new Point(24, 188);
            txtDeskribapena.Location = new Point(24, 218);
            btnGorde.Location = new Point(464, 352);
            btnUtzi.Location = new Point(564, 352);
        }

        private void TxtPazienteBilaketa_TextChanged(object? sender, EventArgs e)
        {
            int? hautatutakoPazienteId = lstPazienteak.SelectedIndex >= 0 && lstPazienteak.SelectedIndex < _unekoPazienteak.Count
                ? _unekoPazienteak[lstPazienteak.SelectedIndex].Id
                : PazienteId;

            KargatuPazienteakBilaketarekin(txtPazienteBilaketa.Text, hautatutakoPazienteId);
        }

        private void LstPazienteak_SelectedIndexChanged(object? sender, EventArgs e)
        {
            PazienteId = lstPazienteak.SelectedIndex >= 0 && lstPazienteak.SelectedIndex < _unekoPazienteak.Count
                ? _unekoPazienteak[lstPazienteak.SelectedIndex].Id
                : null;
        }

        private void BtnPdfHautatu_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Hautatu PDF dokumentua";
            dialog.Filter = "PDF dokumentuak|*.pdf";
            dialog.Multiselect = false;

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            txtPdfFitxategia.Text = dialog.FileName;
            if (string.IsNullOrWhiteSpace(txtDokumentuIzena.Text))
            {
                txtDokumentuIzena.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
            }
        }

        private void BtnGorde_Click(object? sender, EventArgs e)
        {
            if (lstPazienteak.Visible && !PazienteId.HasValue)
            {
                MessageBox.Show(this, "Paziente bat hautatu behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lstPazienteak.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDokumentuIzena.Text))
            {
                MessageBox.Show(this, "Dokumentuaren izena bete behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDokumentuIzena.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPdfFitxategia.Text) || !File.Exists(txtPdfFitxategia.Text))
            {
                MessageBox.Show(this, "PDF fitxategi baliozko bat hautatu behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnPdfHautatu.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void KargatuPazienteakBilaketarekin(string? bilaketa, int? hautatutakoPazienteId = null)
        {
            _unekoPazienteak = string.IsNullOrWhiteSpace(bilaketa)
                ? _hasierakoPazienteak.OrderBy(p => p.IzenOsoa).ToList()
                : (_pazienteBilaketaHornitzailea?.Invoke(bilaketa.Trim()) ?? new List<Pazientea>())
                    .OrderBy(p => p.IzenOsoa)
                    .ToList();

            lstPazienteak.BeginUpdate();
            lstPazienteak.Items.Clear();
            foreach (Pazientea pazientea in _unekoPazienteak)
            {
                lstPazienteak.Items.Add(FormateatuPazienteAukera(pazientea));
            }
            lstPazienteak.EndUpdate();

            if (hautatutakoPazienteId.HasValue)
            {
                int indizea = _unekoPazienteak.FindIndex(p => p.Id == hautatutakoPazienteId.Value);
                lstPazienteak.SelectedIndex = indizea >= 0 ? indizea : -1;
            }
            else if (_unekoPazienteak.Count == 1)
            {
                lstPazienteak.SelectedIndex = 0;
            }
            else
            {
                lstPazienteak.SelectedIndex = -1;
                PazienteId = null;
            }

            lblPazienteakEgoera.Text = _unekoPazienteak.Count switch
            {
                0 => "Ez da pazienterik aurkitu.",
                1 => "Paziente 1 aurkitu da.",
                _ => $"{_unekoPazienteak.Count} paziente aurkitu dira."
            };
        }

        private static string FormateatuPazienteAukera(Pazientea pazientea)
        {
            return $"{pazientea.Abizenak}, {pazientea.Izena} - {pazientea.Nan}";
        }
    }
}