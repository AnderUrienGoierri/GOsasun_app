using System.ComponentModel;
using System.Globalization;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class JarraipenaEditatuLaguntzailea : GOsasunForm
    {
        private readonly List<Dokumentua> _dokumentuak = new List<Dokumentua>();
        private Action<Dokumentua>? _irekiDokumentua;
        private Func<Dokumentua, bool>? _ezabatuDokumentua;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public JarraipenaEditatuLaguntzailea()
        {
            InitializeComponent();
            KonfiguratuElkarrizketa();
        }

        public int? TentsioSistolikoa { get; private set; }
        public int? TentsioDiastolikoa { get; private set; }
        public int? PultsuaPpm { get; private set; }
        public decimal? PisuaKg { get; private set; }
        public decimal? Altuera { get; private set; }
        public string? Oharrak => string.IsNullOrWhiteSpace(txtOharrak.Text) ? null : txtOharrak.Text.Trim();

        private void KonfiguratuElkarrizketa()
        {
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = btnGorde;
            CancelButton = btnUtzi;

            dgvDokumentuak.AutoGenerateColumns = false;
            dgvDokumentuak.CellContentClick += DgvDokumentuak_CellContentClick;
            btnGorde.Click += BtnGorde_Click;
        }

        public void Hasieratu(
            Jarraipena jarraipena,
            Jarraipena xehetasuna,
            IEnumerable<Dokumentua> dokumentuak,
            Action<Dokumentua>? irekiDokumentua,
            Func<Dokumentua, bool>? ezabatuDokumentua)
        {
            lblGoiburua.Text = $"{jarraipena.PazienteIzenOsoa} - {xehetasuna.ErregistroData:g}";
            txtSistolikoa.Text = FormatBalioa(xehetasuna.TentsioSistolikoa);
            txtDiastolikoa.Text = FormatBalioa(xehetasuna.TentsioDiastolikoa);
            txtPultsua.Text = FormatBalioa(xehetasuna.PultsuaPpm);
            txtPisua.Text = FormatBalioa(xehetasuna.PisuaKg, "N2");
            txtAltuera.Text = FormatBalioa(xehetasuna.Altuera, "N2");
            txtOharrak.Text = xehetasuna.Oharrak ?? string.Empty;

            _irekiDokumentua = irekiDokumentua;
            _ezabatuDokumentua = ezabatuDokumentua;
            EzarriDokumentuak(dokumentuak);
        }

        private void EzarriDokumentuak(IEnumerable<Dokumentua> dokumentuak)
        {
            _dokumentuak.Clear();
            _dokumentuak.AddRange(dokumentuak);
            BerrikargatuGrid();
        }

        private void BerrikargatuGrid()
        {
            dgvDokumentuak.DataSource = null;
            dgvDokumentuak.DataSource = _dokumentuak.Select(d => new DokumentuGridRow(d)).ToList();
        }

        private void DgvDokumentuak_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (dgvDokumentuak.Rows[e.RowIndex].DataBoundItem is not DokumentuGridRow row)
            {
                return;
            }

            if (dgvDokumentuak.Columns[e.ColumnIndex].Name == colIreki.Name)
            {
                _irekiDokumentua?.Invoke(row.Dokumentua);
                return;
            }

            if (dgvDokumentuak.Columns[e.ColumnIndex].Name != colEzabatu.Name)
            {
                return;
            }

            if (!DokumentuaEzabatuLaguntzailea.Baieztatu(this, row.Dokumentua))
            {
                return;
            }

            if (_ezabatuDokumentua?.Invoke(row.Dokumentua) == true)
            {
                _dokumentuak.RemoveAll(d => d.Id == row.Dokumentua.Id);
                BerrikargatuGrid();
                return;
            }

            MessageBox.Show(this, "Ezin izan da dokumentua ezabatu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnGorde_Click(object? sender, EventArgs e)
        {
            if (!SaiatuLortuIntBalioa(txtSistolikoa.Text, "Tentsio sistolikoa", out int? tentsioSistolikoa)
                || !SaiatuLortuIntBalioa(txtDiastolikoa.Text, "Tentsio diastolikoa", out int? tentsioDiastolikoa)
                || !SaiatuLortuIntBalioa(txtPultsua.Text, "Pultsua", out int? pultsua)
                || !SaiatuLortuDecimalBalioa(txtPisua.Text, "Pisua", out decimal? pisua)
                || !SaiatuLortuDecimalBalioa(txtAltuera.Text, "Altuera", out decimal? altuera))
            {
                return;
            }

            TentsioSistolikoa = tentsioSistolikoa;
            TentsioDiastolikoa = tentsioDiastolikoa;
            PultsuaPpm = pultsua;
            PisuaKg = pisua;
            Altuera = altuera;
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool SaiatuLortuIntBalioa(string testua, string etiketa, out int? balioa)
        {
            balioa = null;
            if (string.IsNullOrWhiteSpace(testua))
            {
                return true;
            }

            if (int.TryParse(testua.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int zenbakia)
                || int.TryParse(testua.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out zenbakia))
            {
                balioa = zenbakia;
                return true;
            }

            MessageBox.Show(this, $"'{etiketa}' eremuan zenbaki oso baliozkoa sartu behar da.", "Balio baliogabea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private bool SaiatuLortuDecimalBalioa(string testua, string etiketa, out decimal? balioa)
        {
            balioa = null;
            if (string.IsNullOrWhiteSpace(testua))
            {
                return true;
            }

            if (decimal.TryParse(testua.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out decimal zenbakia)
                || decimal.TryParse(testua.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out zenbakia))
            {
                balioa = zenbakia;
                return true;
            }

            MessageBox.Show(this, $"'{etiketa}' eremuan zenbaki hamartar baliozkoa sartu behar da.", "Balio baliogabea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private static string FormatBalioa<T>(T? balioa, string? formatua = null) where T : struct, IFormattable
        {
            return balioa.HasValue ? balioa.Value.ToString(formatua, null) : string.Empty;
        }

        private sealed class DokumentuGridRow
        {
            public DokumentuGridRow(Dokumentua dokumentua)
            {
                Dokumentua = dokumentua;
                DokumentuIzena = dokumentua.DokumentuIzena;
                FitxategiIzena = dokumentua.FitxategiIzena;
                IgotzeData = dokumentua.IgotzeData;
            }

            public Dokumentua Dokumentua { get; }
            public string? DokumentuIzena { get; }
            public string? FitxategiIzena { get; }
            public DateTime IgotzeData { get; }
        }
    }
}