using System.ComponentModel;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class EsleituOsasunLangileakLaguntzailea : GOsasunForm
    {
        private readonly List<OsasunLangilea> _langileGuztiak = new List<OsasunLangilea>();
        private readonly HashSet<int> _jadaEsleitutaIds = new HashSet<int>();
        private readonly List<OsasunLangilea> _hautatutakoLangileak = new List<OsasunLangilea>();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public EsleituOsasunLangileakLaguntzailea()
        {
            InitializeComponent();
            KonfiguratuPopupa();
        }

        public IReadOnlyCollection<int> HautatutakoLangileIds => _hautatutakoLangileak.Select(langilea => langilea.Id).ToList();

        private void KonfiguratuPopupa()
        {
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = btnEsleitu;
            CancelButton = btnUtzi;

            dgvLangileak.AutoGenerateColumns = false;
            dgvLangileak.Columns.Clear();
            dgvLangileak.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Izena", DataPropertyName = nameof(OsasunLangilePopupRow.Izena), Width = 120 });
            dgvLangileak.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Abizenak", DataPropertyName = nameof(OsasunLangilePopupRow.Abizenak), Width = 150 });
            dgvLangileak.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "DNI", DataPropertyName = nameof(OsasunLangilePopupRow.Nan), Width = 120 });
            dgvLangileak.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Elkargokide", DataPropertyName = nameof(OsasunLangilePopupRow.ElkargokideZenbakia), Width = 130 });
            dgvLangileak.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Espezialitatea", DataPropertyName = nameof(OsasunLangilePopupRow.Espezialitatea), Width = 160 });
            dgvLangileak.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Kontsulta", DataPropertyName = nameof(OsasunLangilePopupRow.Kontsulta), Width = 100 });

            cmbEspezialitatea.SelectedIndexChanged += CmbEspezialitatea_SelectedIndexChanged;
            txtBilaketa.TextChanged += TxtBilaketa_TextChanged;
            dgvLangileak.SelectionChanged += DgvLangileak_SelectionChanged;
            btnGehitu.Click += BtnGehitu_Click;
            btnKendu.Click += BtnKendu_Click;
            btnEsleitu.Click += BtnEsleitu_Click;
        }

        public void Hasieratu(Pazientea pazientea, IEnumerable<OsasunLangilea> langileak, IEnumerable<int> jadaEsleitutaIds)
        {
            lblPazientea.Text = $"Pazientea: {pazientea.IzenOsoa} ({pazientea.Nan})";

            _langileGuztiak.Clear();
            _langileGuztiak.AddRange(langileak.OrderBy(langilea => langilea.Espezialitatea).ThenBy(langilea => langilea.Abizenak).ThenBy(langilea => langilea.Izena));

            _jadaEsleitutaIds.Clear();
            foreach (int id in jadaEsleitutaIds)
            {
                _jadaEsleitutaIds.Add(id);
            }

            _hautatutakoLangileak.Clear();
            txtBilaketa.Clear();
            KargatuEspezialitateak();
            EguneratuHautatutakoenZerrenda();
            EguneratuGrid();
            lblJadaEsleituta.Text = _jadaEsleitutaIds.Count == 0
                ? "Paziente honek ez du oraindik osasun langilerik esleituta."
                : $"Jada esleituta: {_jadaEsleitutaIds.Count}";
        }

        private void KargatuEspezialitateak()
        {
            cmbEspezialitatea.Items.Clear();
            cmbEspezialitatea.Items.Add("Hautatu espezialitatea...");

            foreach (string espezialitatea in _langileGuztiak
                         .Select(LortuEspezialitateBalioa)
                         .Distinct(StringComparer.OrdinalIgnoreCase)
                         .OrderBy(espezialitatea => espezialitatea))
            {
                cmbEspezialitatea.Items.Add(espezialitatea);
            }

            cmbEspezialitatea.SelectedIndex = 0;
            txtBilaketa.Enabled = false;
        }

        private void CmbEspezialitatea_SelectedIndexChanged(object? sender, EventArgs e)
        {
            txtBilaketa.Enabled = cmbEspezialitatea.SelectedIndex > 0;
            if (!txtBilaketa.Enabled)
            {
                txtBilaketa.Clear();
            }

            EguneratuGrid();
        }

        private void TxtBilaketa_TextChanged(object? sender, EventArgs e)
        {
            EguneratuGrid();
        }

        private void DgvLangileak_SelectionChanged(object? sender, EventArgs e)
        {
            btnGehitu.Enabled = dgvLangileak.CurrentRow?.DataBoundItem is OsasunLangilePopupRow;
        }

        private void BtnGehitu_Click(object? sender, EventArgs e)
        {
            if (dgvLangileak.CurrentRow?.DataBoundItem is not OsasunLangilePopupRow row)
            {
                return;
            }

            if (_hautatutakoLangileak.Any(langilea => langilea.Id == row.Id) || _jadaEsleitutaIds.Contains(row.Id))
            {
                return;
            }

            _hautatutakoLangileak.Add(row.Langilea);
            EguneratuHautatutakoenZerrenda();
            EguneratuGrid();
        }

        private void BtnKendu_Click(object? sender, EventArgs e)
        {
            if (lstHautatutakoak.SelectedItem is not OsasunLangilePopupRow row)
            {
                return;
            }

            _hautatutakoLangileak.RemoveAll(langilea => langilea.Id == row.Id);
            EguneratuHautatutakoenZerrenda();
            EguneratuGrid();
        }

        private void BtnEsleitu_Click(object? sender, EventArgs e)
        {
            if (_hautatutakoLangileak.Count == 0)
            {
                MessageBox.Show(this, "Gutxienez osasun langile bat hautatu behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void EguneratuGrid()
        {
            List<OsasunLangilePopupRow> aukerak = LortuUnekoAukerak();
            dgvLangileak.DataSource = null;
            dgvLangileak.DataSource = aukerak;

            lblEmaitzak.Text = cmbEspezialitatea.SelectedIndex <= 0
                ? "Espezialitate bat hautatu bilaketa hasteko."
                : aukerak.Count == 0
                    ? "Ez da osasun langilerik aurkitu irizpide horiekin."
                    : $"{aukerak.Count} osasun langile aurkitu dira.";

            btnGehitu.Enabled = dgvLangileak.CurrentRow?.DataBoundItem is OsasunLangilePopupRow;
        }

        private void EguneratuHautatutakoenZerrenda()
        {
            lstHautatutakoak.DataSource = null;
            lstHautatutakoak.DisplayMember = nameof(OsasunLangilePopupRow.Deskribapena);
            lstHautatutakoak.ValueMember = nameof(OsasunLangilePopupRow.Id);
            lstHautatutakoak.DataSource = _hautatutakoLangileak
                .OrderBy(langilea => langilea.Abizenak)
                .ThenBy(langilea => langilea.Izena)
                .Select(langilea => new OsasunLangilePopupRow(langilea))
                .ToList();
        }

        private List<OsasunLangilePopupRow> LortuUnekoAukerak()
        {
            if (cmbEspezialitatea.SelectedIndex <= 0)
            {
                return new List<OsasunLangilePopupRow>();
            }

            string hautatutakoEspezialitatea = cmbEspezialitatea.SelectedItem?.ToString() ?? string.Empty;
            string bilaketa = txtBilaketa.Text.Trim();
            HashSet<int> blokeatutakoIdak = _jadaEsleitutaIds
                .Concat(_hautatutakoLangileak.Select(langilea => langilea.Id))
                .ToHashSet();

            return _langileGuztiak
                .Where(langilea => !blokeatutakoIdak.Contains(langilea.Id))
                .Where(langilea => string.Equals(LortuEspezialitateBalioa(langilea), hautatutakoEspezialitatea, StringComparison.OrdinalIgnoreCase))
                .Where(langilea => string.IsNullOrWhiteSpace(bilaketa) || BatDatorBilaketarekin(langilea, bilaketa))
                .OrderBy(langilea => langilea.Abizenak)
                .ThenBy(langilea => langilea.Izena)
                .Select(langilea => new OsasunLangilePopupRow(langilea))
                .ToList();
        }

        private static string LortuEspezialitateBalioa(OsasunLangilea langilea)
        {
            return string.IsNullOrWhiteSpace(langilea.Espezialitatea) ? "Espezialitaterik gabe" : langilea.Espezialitatea.Trim();
        }

        private static bool BatDatorBilaketarekin(OsasunLangilea langilea, string bilaketa)
        {
            string termino = bilaketa.Trim();
            if (string.IsNullOrWhiteSpace(termino))
            {
                return true;
            }

            return new[]
            {
                langilea.Izena,
                langilea.Abizenak,
                langilea.Nan,
                langilea.ElkargokideZenbakia,
                langilea.Espezialitatea,
                langilea.Kontsulta,
                langilea.IzenOsoa
            }
            .Where(testua => !string.IsNullOrWhiteSpace(testua))
            .Any(testua => testua!.Contains(termino, StringComparison.OrdinalIgnoreCase));
        }

        private sealed class OsasunLangilePopupRow
        {
            public OsasunLangilePopupRow(OsasunLangilea langilea)
            {
                Langilea = langilea;
                Id = langilea.Id;
                Izena = langilea.Izena;
                Abizenak = langilea.Abizenak;
                Nan = langilea.Nan;
                ElkargokideZenbakia = langilea.ElkargokideZenbakia;
                Espezialitatea = LortuEspezialitateBalioa(langilea);
                Kontsulta = string.IsNullOrWhiteSpace(langilea.Kontsulta) ? "-" : langilea.Kontsulta;
                Deskribapena = $"{langilea.IzenOsoa} | {Nan} | {ElkargokideZenbakia} | {Kontsulta}";
            }

            public OsasunLangilea Langilea { get; }
            public int Id { get; }
            public string Izena { get; }
            public string Abizenak { get; }
            public string Nan { get; }
            public string ElkargokideZenbakia { get; }
            public string Espezialitatea { get; }
            public string Kontsulta { get; }
            public string Deskribapena { get; }
        }
    }
}