using System.ComponentModel;
using GOsasun_app.Kontrola.Zerbitzuak;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class OsasunTxostenaSortuLaguntzailea : Form
    {
        private readonly List<Pazientea> _hasierakoPazienteak = new List<Pazientea>();
        private List<Pazientea> _unekoPazienteak = new List<Pazientea>();
        private Func<string?, List<Pazientea>>? _pazienteBilaketa;
        private Func<int, List<Jarraipena>>? _jarraipenBilaketa;
        private bool _dataTarteaEguneratzen;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public OsasunTxostenaSortuLaguntzailea()
        {
            InitializeComponent();
            KonfiguratuElkarrizketa();
        }

        public int? PazienteId { get; private set; }

        public string DokumentuIzena => txtTxostenIzena.Text.Trim();

        public string Deskribapena => txtDeskribapena.Text.Trim();

        public List<TxostenGrafikaMota> GrafikaMotak => clbGrafikak.CheckedItems
            .OfType<TxostenGrafikaAukeraItem>()
            .Select(item => item.Mota)
            .Distinct()
            .ToList();

        public DateTime? GrafikaHasieraData => chkGrafikaDatuGuztiak.Checked ? null : dtpGrafikaHasiera.Value.Date;

        public DateTime? GrafikaAmaieraData => chkGrafikaDatuGuztiak.Checked ? null : dtpGrafikaAmaiera.Value.Date;

        private void KonfiguratuElkarrizketa()
        {
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = btnSortu;
            CancelButton = btnUtzi;

            txtPazienteBilaketa.TextChanged += TxtPazienteBilaketa_TextChanged;
            lstPazienteak.SelectedIndexChanged += LstPazienteak_SelectedIndexChanged;
            chkGrafikaDatuGuztiak.CheckedChanged += ChkGrafikaDatuGuztiak_CheckedChanged;
            dtpGrafikaHasiera.ValueChanged += DtpGrafikaHasiera_ValueChanged;
            dtpGrafikaAmaiera.ValueChanged += DtpGrafikaAmaiera_ValueChanged;
            btnSortu.Click += BtnSortu_Click;

            foreach (TxostenGrafikaMota mota in Enum.GetValues(typeof(TxostenGrafikaMota)).Cast<TxostenGrafikaMota>())
            {
                clbGrafikak.Items.Add(new TxostenGrafikaAukeraItem
                {
                    Mota = mota,
                    Testua = TxostenGrafikaZerbitzua.LortuGrafikaTestua(mota)
                }, true);
            }

            lblGrafikaDataTartea.Text = "Aukeratu paziente bat datu tartea ikusteko.";
        }

        public void Hasieratu(
            IEnumerable<Pazientea> pazienteak,
            Func<string?, List<Pazientea>> pazienteBilaketa,
            Func<int, List<Jarraipena>> jarraipenBilaketa)
        {
            _pazienteBilaketa = pazienteBilaketa;
            _jarraipenBilaketa = jarraipenBilaketa;

            _hasierakoPazienteak.Clear();
            _hasierakoPazienteak.AddRange(pazienteak.OrderBy(p => p.IzenOsoa));
            _unekoPazienteak = _hasierakoPazienteak.ToList();

            PazienteId = null;
            txtPazienteBilaketa.Clear();
            txtTxostenIzena.Text = "Osasun txostena";
            txtDeskribapena.Text = "Txosten mediko automatikoa";
            chkGrafikaDatuGuztiak.Checked = true;

            KargatuPazienteakBilaketarekin(null);
            ActiveControl = txtPazienteBilaketa;
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

            EguneratuGrafikaDataTartea();
        }

        private void ChkGrafikaDatuGuztiak_CheckedChanged(object? sender, EventArgs e)
        {
            bool dataTarteMurriztua = !chkGrafikaDatuGuztiak.Checked;
            dtpGrafikaHasiera.Enabled = dataTarteMurriztua;
            dtpGrafikaAmaiera.Enabled = dataTarteMurriztua;
        }

        private void DtpGrafikaHasiera_ValueChanged(object? sender, EventArgs e)
        {
            if (!_dataTarteaEguneratzen && dtpGrafikaHasiera.Value.Date > dtpGrafikaAmaiera.Value.Date)
            {
                dtpGrafikaAmaiera.Value = dtpGrafikaHasiera.Value.Date;
            }
        }

        private void DtpGrafikaAmaiera_ValueChanged(object? sender, EventArgs e)
        {
            if (!_dataTarteaEguneratzen && dtpGrafikaAmaiera.Value.Date < dtpGrafikaHasiera.Value.Date)
            {
                dtpGrafikaHasiera.Value = dtpGrafikaAmaiera.Value.Date;
            }
        }

        private void BtnSortu_Click(object? sender, EventArgs e)
        {
            if (!PazienteId.HasValue)
            {
                MessageBox.Show(this, "Paziente bat hautatu behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTxostenIzena.Text))
            {
                MessageBox.Show(this, "Txostenaren izena bete behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTxostenIzena.Focus();
                return;
            }

            if (clbGrafikak.CheckedItems.Count == 0)
            {
                MessageBox.Show(this, "Gutxienez grafika mediko bat hautatu behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void KargatuPazienteakBilaketarekin(string? bilaketa, int? hautatutakoPazienteId = null)
        {
            _unekoPazienteak = string.IsNullOrWhiteSpace(bilaketa)
                ? _hasierakoPazienteak.OrderBy(p => p.IzenOsoa).ToList()
                : (_pazienteBilaketa?.Invoke(bilaketa.Trim()) ?? new List<Pazientea>())
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

            EguneratuGrafikaDataTartea();
        }

        private void EguneratuGrafikaDataTartea()
        {
            if (!PazienteId.HasValue)
            {
                lblGrafikaDataTartea.Text = "Aukeratu paziente bat datu tartea ikusteko.";
                return;
            }

            List<Jarraipena> jarraipenak = (_jarraipenBilaketa?.Invoke(PazienteId.Value) ?? new List<Jarraipena>())
                .OrderBy(j => j.ErregistroData)
                .ToList();

            DateTime minData = jarraipenak.Count == 0 ? DateTime.Today : jarraipenak.First().ErregistroData.Date;
            DateTime maxData = jarraipenak.Count == 0 ? DateTime.Today : jarraipenak.Last().ErregistroData.Date;

            _dataTarteaEguneratzen = true;
            try
            {
                dtpGrafikaHasiera.MinDate = minData;
                dtpGrafikaAmaiera.MinDate = minData;
                dtpGrafikaHasiera.MaxDate = maxData;
                dtpGrafikaAmaiera.MaxDate = maxData;
                dtpGrafikaHasiera.Value = minData;
                dtpGrafikaAmaiera.Value = maxData;
            }
            finally
            {
                _dataTarteaEguneratzen = false;
            }

            lblGrafikaDataTartea.Text = jarraipenak.Count == 0
                ? "Paziente honek ez du neurketa erregistrorik; grafika atala hutsik geratuko da."
                : $"Grafikek {minData:yyyy/MM/dd} - {maxData:yyyy/MM/dd} bitarteko neurketak erabiliko dituzte.";
        }

        private static string FormateatuPazienteAukera(Pazientea pazientea)
        {
            return $"{pazientea.Abizenak}, {pazientea.Izena} - {pazientea.Nan}";
        }

        private sealed class TxostenGrafikaAukeraItem
        {
            public TxostenGrafikaMota Mota { get; init; }
            public string Testua { get; init; } = string.Empty;

            public override string ToString() => Testua;
        }
    }
}