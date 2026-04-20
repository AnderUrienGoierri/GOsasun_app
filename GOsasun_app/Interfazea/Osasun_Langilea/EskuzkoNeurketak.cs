using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class EskuzkoNeurketak : OinarriPantaila
    {
        private readonly PazienteKontrolatzailea _pazienteKontrolatzailea;
        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea;
        private readonly bool _isPisua;
        private readonly int? _pazienteIdAurrehautatu;
        private readonly string? _pazienteIzenburua;
        private Pazientea? _hautatutakoPazientea;

        public EskuzkoNeurketak(Erabiltzailea erabiltzailea, bool isPisua) : base(erabiltzailea)
        {
            InitializeComponent();
            _pazienteKontrolatzailea = new PazienteKontrolatzailea();
            _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
            _isPisua = isPisua;
            _pazienteIdAurrehautatu = null;
            _pazienteIzenburua = null;

            KonfiguratuPantaila();
            KonfiguratuGertaerak();
            PazienteakBatu();
        }

        public EskuzkoNeurketak(Erabiltzailea erabiltzailea, bool isPisua, int pazienteId, string? pazienteIzenburua = null) : base(erabiltzailea)
        {
            InitializeComponent();
            _pazienteKontrolatzailea = new PazienteKontrolatzailea();
            _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
            _isPisua = isPisua;
            _pazienteIdAurrehautatu = pazienteId;
            _pazienteIzenburua = pazienteIzenburua;

            KonfiguratuPantaila();
            KonfiguratuGertaerak();
            PazienteakBatu();
        }

        private void KonfiguratuPantaila()
        {
            if (_isPisua)
            {
                _lblTitle.Text = "PISUA GEHITU";
                _lblBalioa.Text = "Sartu pisua (kg):";
                _lblUnitatea.Text = "kg";
                _numBalioa.DecimalPlaces = 1;
                _numBalioa.Value = 70.0m;
                _numBalioa.Maximum = 500;
            }
            else
            {
                _lblTitle.Text = "ALTUERA GEHITU";
                _lblBalioa.Text = "Sartu altuera (m):";
                _lblUnitatea.Text = "m";
                _numBalioa.DecimalPlaces = 2;
                _numBalioa.Value = 1.70m;
                _numBalioa.Maximum = 3;
            }
        }

        private void KonfiguratuGertaerak()
        {
            _btnItzuli.Click += (s, e) => this.Close();
            _txtPazienteBilatu.TextChanged += (s, e) => PazienteakBatu();

            _dgvPazienteak.SelectionChanged += (s, e) => {
                if (_dgvPazienteak.SelectedRows.Count > 0)
                {
                    _hautatutakoPazientea = _dgvPazienteak.SelectedRows[0].DataBoundItem as Pazientea;
                    if (_hautatutakoPazientea != null)
                    {
                        KargatuPazientearenHistoriala(_hautatutakoPazientea.Id);
                        _pnlSarrera.Visible = true;
                    }
                }
                else
                {
                    _hautatutakoPazientea = null;
                    _pnlSarrera.Visible = false;
                    _dgvHistoriala.Visible = false;
                    _lblHistoriala.Visible = false;
                }
            };

            _btnGorde.Click += (s, e) => GordeJarraipena();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_erabiltzailea?.DaPazientea() == true)
            {
                PrestatuPazienteModua();
            }
            else if (_pazienteIdAurrehautatu.HasValue)
            {
                PrestatuAurrehautatutakoPazientea();
            }
        }

        private void PrestatuAurrehautatutakoPazientea()
        {
            Pazientea? aurkitutakoa = _pazienteKontrolatzailea.LortuPazientea(_pazienteIdAurrehautatu!.Value);
            _hautatutakoPazientea = aurkitutakoa ?? new Pazientea
            {
                Id = _pazienteIdAurrehautatu.Value,
                Izena = _pazienteIzenburua ?? "Pazientea"
            };

            _lblBilatu.Text = string.IsNullOrWhiteSpace(_pazienteIzenburua)
                ? "1. Pazientearen aurreko jarraipenak:"
                : $"1. {_pazienteIzenburua} pazientearen aurreko jarraipenak:";
            _txtPazienteBilatu.Visible = false;
            _dgvPazienteak.Visible = false;
            _lblHistoriala.Location = new Point(50, 190);
            _lblHistoriala.Text = "2. Jarraipenen historiala:";
            _dgvHistoriala.Location = new Point(50, 245);
            _pnlSarrera.Location = new Point(50, 525);
            _pnlSarrera.Visible = true;

            KargatuPazientearenHistoriala(_hautatutakoPazientea.Id);
        }

        private void PrestatuPazienteModua()
        {
            _hautatutakoPazientea = _hautatutakoPazientea ?? new Pazientea
            {
                Id = _erabiltzailea!.Id,
                Izena = _erabiltzailea.Izena,
                Abizenak = _erabiltzailea.Abizenak,
                Nan = _erabiltzailea.Nan
            };

            _lblBilatu.Text = "1. Zure aurreko jarraipenak:";
            _txtPazienteBilatu.Visible = false;
            _dgvPazienteak.Visible = false;
            _lblHistoriala.Location = new Point(50, 190);
            _lblHistoriala.Text = "2. Jarraipenen historiala:";
            _dgvHistoriala.Location = new Point(50, 245);
            _pnlSarrera.Location = new Point(50, 525);
            _pnlSarrera.Visible = true;

            KargatuPazientearenHistoriala(_hautatutakoPazientea.Id);
        }

        private void PazienteakBatu()
        {
            try
            {
                if (_erabiltzailea?.DaPazientea() == true || _pazienteIdAurrehautatu.HasValue)
                {
                    return;
                }

                string bilaketa = _txtPazienteBilatu.Text.Trim();
                var list = _pazienteKontrolatzailea.LortuLangilearenPazienteak(_erabiltzailea!.Id, bilaketa);

                _dgvPazienteak.DataSource = null;
                _dgvPazienteak.DataSource = list;

                // Zutabeak konfiguratu
                if (_dgvPazienteak.Columns.Count > 0)
                {
                    foreach (DataGridViewColumn col in _dgvPazienteak.Columns) col.Visible = false;

                    DataGridViewColumn? izenaZutabea = _dgvPazienteak.Columns["Izena"];
                    DataGridViewColumn? abizenakZutabea = _dgvPazienteak.Columns["Abizenak"];
                    DataGridViewColumn? nanZutabea = _dgvPazienteak.Columns["Nan"];

                    if (izenaZutabea != null) izenaZutabea.Visible = true;
                    if (abizenakZutabea != null) abizenakZutabea.Visible = true;
                    if (nanZutabea != null)
                    {
                        nanZutabea.Visible = true;
                        nanZutabea.HeaderText = "NAN";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea pazienteak kargatzean: " + ex.Message);
            }
        }

        private void KargatuPazientearenHistoriala(int pazienteId)
        {
            try
            {
                var jarraipenak = _jarraipenaKontrolatzailea.LortuPazientearenJarraipenak(pazienteId);

                DataTable dt = new DataTable();
                dt.Columns.Add("Data", typeof(DateTime));
                if (_isPisua) dt.Columns.Add("Pisua", typeof(decimal));
                else dt.Columns.Add("Altuera", typeof(decimal));
                dt.Columns.Add("Oharrak", typeof(string));

                foreach (var n in jarraipenak)
                {
                    if (_isPisua && n.PisuaKg.HasValue)
                    {
                        dt.Rows.Add(n.ErregistroData, n.PisuaKg.Value, n.Oharrak ?? "-");
                    }
                    else if (!_isPisua && n.Altuera.HasValue)
                    {
                        dt.Rows.Add(n.ErregistroData, n.Altuera.Value, n.Oharrak ?? "-");
                    }
                }

                _dgvHistoriala.DataSource = dt;

                if (_dgvHistoriala.Columns.Count > 0)
                {
                    DataGridViewColumn? dataZutabea = _dgvHistoriala.Columns["Data"];
                    DataGridViewColumn? pisuaZutabea = _dgvHistoriala.Columns["Pisua"];
                    DataGridViewColumn? altueraZutabea = _dgvHistoriala.Columns["Altuera"];

                    if (dataZutabea != null) dataZutabea.DefaultCellStyle.Format = "yyyy/MM/dd HH:mm";
                    if (_isPisua && pisuaZutabea != null) pisuaZutabea.HeaderText = "Pisua (kg)";
                    else if (!_isPisua && altueraZutabea != null) altueraZutabea.HeaderText = "Altuera (m)";
                }

                _lblHistoriala.Visible = true;
                _dgvHistoriala.Visible = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errorea historiala kargatzean: " + ex.Message);
            }
        }

        private void GordeJarraipena()
        {
            if (_hautatutakoPazientea == null) return;

            try
            {
                Jarraipena berria = new Jarraipena
                {
                    PazienteId = _hautatutakoPazientea.Id,
                    ErregistroData = DateTime.Now,
                    OsasunLangileId = _erabiltzailea?.Id,
                    Oharrak = _isPisua ? "Pisua - eskuzko sarrera" : "Altuera - eskuzko sarrera"
                };

                if (_isPisua) berria.PisuaKg = _numBalioa.Value;
                else berria.Altuera = _numBalioa.Value;

                string? oharGehigarria = JarraipenOharLaguntzailea.EskatuAukerakoOharra(
                    this,
                    _isPisua ? "Pisua jarraipenaren oharra" : "Altuera jarraipenaren oharra",
                    _isPisua
                        ? "Pisua gorde aurretik, nahi baduzu ohar osagarria gehitu dezakezu."
                        : "Altuera gorde aurretik, nahi baduzu ohar osagarria gehitu dezakezu.");
                berria.Oharrak = JarraipenOharLaguntzailea.BatuOharrak(berria.Oharrak, oharGehigarria);

                if (_jarraipenaKontrolatzailea.GordeJarraipena(berria))
                {
                    _jarraipenaKontrolatzailea.EsportatuXML(berria);
                    MessageBox.Show("Jarraipena ondo gorde da.", "Kuztiz ondo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KargatuPazientearenHistoriala(_hautatutakoPazientea.Id);
                }
                else
                {
                    MessageBox.Show("Errorea jarraipena gordetzean.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea: " + ex.Message);
            }
        }
    }
}
