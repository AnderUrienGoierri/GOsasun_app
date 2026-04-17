using System.ComponentModel;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class OharBidezkoJarraipenaLaguntzailea : Form
    {
        private readonly List<Pazientea> _hasierakoPazienteak = new List<Pazientea>();
        private List<Pazientea> _unekoPazienteak = new List<Pazientea>();
        private Func<string?, List<Pazientea>>? _bilaketaHornitzailea;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public OharBidezkoJarraipenaLaguntzailea()
        {
            InitializeComponent();
            KonfiguratuElkarrizketa();
        }

        public int? HautatutakoPazienteId { get; private set; }

        public string Oharra => txtOharrak.Text.Trim();

        private void KonfiguratuElkarrizketa()
        {
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = btnGorde;
            CancelButton = btnUtzi;

            txtPazienteBilatu.TextChanged += TxtPazienteBilatu_TextChanged;
            cmbPazienteak.SelectedIndexChanged += CmbPazienteak_SelectedIndexChanged;
            btnGorde.Click += BtnGorde_Click;
        }

        public void Hasieratu(
            string izenburua,
            bool erakutsiPazienteAukeraketa,
            int? aurrehautatutakoPazienteId,
            IEnumerable<Pazientea>? hasierakoPazienteak,
            Func<string?, List<Pazientea>>? bilaketaHornitzailea)
        {
            Text = izenburua;
            _bilaketaHornitzailea = bilaketaHornitzailea;
            _hasierakoPazienteak.Clear();
            _hasierakoPazienteak.AddRange((hasierakoPazienteak ?? Enumerable.Empty<Pazientea>()).OrderBy(p => p.IzenOsoa));
            txtPazienteBilatu.Clear();
            txtOharrak.Clear();
            EzarriPazienteAukeraketaIkusgai(erakutsiPazienteAukeraketa);

            if (erakutsiPazienteAukeraketa)
            {
                KargatuPazienteakBilaketarekin(null, aurrehautatutakoPazienteId);
            }
            else
            {
                HautatutakoPazienteId = aurrehautatutakoPazienteId;
                lblBilaketaEmaitza.Text = string.Empty;
            }
        }

        private void EzarriPazienteAukeraketaIkusgai(bool ikusgai)
        {
            lblBilatuPazientea.Visible = ikusgai;
            txtPazienteBilatu.Visible = ikusgai;
            lblPazientea.Visible = ikusgai;
            cmbPazienteak.Visible = ikusgai;
            lblBilaketaEmaitza.Visible = ikusgai;

            if (ikusgai)
            {
                ClientSize = new Size(760, 540);
                lblOharrak.Location = new Point(24, 250);
                txtOharrak.Location = new Point(24, 290);
            }
            else
            {
                ClientSize = new Size(760, 360);
                lblOharrak.Location = new Point(24, 24);
                txtOharrak.Location = new Point(24, 64);
            }

            btnUtzi.Location = new Point(ClientSize.Width - 24 - btnGorde.Width - 16 - btnUtzi.Width, ClientSize.Height - 70);
            btnGorde.Location = new Point(ClientSize.Width - 24 - btnGorde.Width, ClientSize.Height - 70);
        }

        private void TxtPazienteBilatu_TextChanged(object? sender, EventArgs e)
        {
            int? aurrehautatutakoId = cmbPazienteak.SelectedItem is Pazientea pazientea
                ? pazientea.Id
                : HautatutakoPazienteId;

            KargatuPazienteakBilaketarekin(txtPazienteBilatu.Text, aurrehautatutakoId);
        }

        private void CmbPazienteak_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbPazienteak.SelectedItem is Pazientea pazientea)
            {
                HautatutakoPazienteId = pazientea.Id;
            }
            else if (cmbPazienteak.SelectedValue is int pazienteId)
            {
                HautatutakoPazienteId = pazienteId;
            }
        }

        private void KargatuPazienteakBilaketarekin(string? bilaketa, int? hautatutakoPazienteId = null)
        {
            _unekoPazienteak = string.IsNullOrWhiteSpace(bilaketa)
                ? _hasierakoPazienteak.OrderBy(p => p.IzenOsoa).ToList()
                : (_bilaketaHornitzailea?.Invoke(bilaketa.Trim()) ?? _hasierakoPazienteak)
                    .OrderBy(p => p.IzenOsoa)
                    .ToList();

            cmbPazienteak.DataSource = null;
            cmbPazienteak.DisplayMember = nameof(Erabiltzailea.IzenOsoa);
            cmbPazienteak.ValueMember = nameof(Erabiltzailea.Id);
            cmbPazienteak.DataSource = _unekoPazienteak;
            cmbPazienteak.Enabled = _unekoPazienteak.Count > 0;

            if (hautatutakoPazienteId.HasValue)
            {
                int indizea = _unekoPazienteak.FindIndex(p => p.Id == hautatutakoPazienteId.Value);
                cmbPazienteak.SelectedIndex = indizea >= 0 ? indizea : -1;
            }
            else if (_unekoPazienteak.Count == 1)
            {
                cmbPazienteak.SelectedIndex = 0;
            }
            else
            {
                cmbPazienteak.SelectedIndex = _unekoPazienteak.Count > 0 ? 0 : -1;
            }

            lblBilaketaEmaitza.Text = _unekoPazienteak.Count switch
            {
                0 => "Ez da pazienterik aurkitu.",
                1 => "Paziente 1 aurkitu da.",
                _ => $"{_unekoPazienteak.Count} paziente aurkitu dira."
            };

            if (cmbPazienteak.SelectedItem is Pazientea pazientea)
            {
                HautatutakoPazienteId = pazientea.Id;
            }
        }

        private void BtnGorde_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOharrak.Text))
            {
                MessageBox.Show(this, "Oharra idatzi behar da ohar bidezko jarraipena sortzeko.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOharrak.Focus();
                return;
            }

            if (cmbPazienteak.Visible && !HautatutakoPazienteId.HasValue)
            {
                MessageBox.Show(this, "Paziente bat aukeratu behar da.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPazienteak.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}