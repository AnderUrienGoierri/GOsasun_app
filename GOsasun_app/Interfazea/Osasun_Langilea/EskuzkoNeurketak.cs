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
        private readonly ErabiltzaileKontrolatzailea _erabiltzaileKontrolatzailea;
        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea;
        private readonly bool _isPisua;
        private Pazientea? _hautatutakoPazientea;

        public EskuzkoNeurketak(Erabiltzailea erabiltzailea, bool isPisua) : base(erabiltzailea)
        {
            InitializeComponent();
            _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();
            _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
            _isPisua = isPisua;

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

        private void PazienteakBatu()
        {
            try
            {
                string bilaketa = _txtPazienteBilatu.Text.Trim();
                var list = _erabiltzaileKontrolatzailea.LortuLangilearenPazienteak(_erabiltzailea!.Id, bilaketa);
                
                _dgvPazienteak.DataSource = null;
                _dgvPazienteak.DataSource = list;

                // Zutabeak konfiguratu
                if (_dgvPazienteak.Columns.Count > 0)
                {
                    foreach (DataGridViewColumn col in _dgvPazienteak.Columns) col.Visible = false;
                    
                    _dgvPazienteak.Columns["Izena"].Visible = true;
                    _dgvPazienteak.Columns["Abizenak"].Visible = true;
                    _dgvPazienteak.Columns["Nan"].Visible = true;
                    _dgvPazienteak.Columns["Nan"].HeaderText = "NAN";
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
                    _dgvHistoriala.Columns["Data"].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm";
                    if (_isPisua) _dgvHistoriala.Columns["Pisua"].HeaderText = "Pisua (kg)";
                    else _dgvHistoriala.Columns["Altuera"].HeaderText = "Altuera (m)";
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
                    Oharrak = "Eskuzko sarrera"
                };

                if (_isPisua) berria.PisuaKg = _numBalioa.Value;
                else berria.Altuera = _numBalioa.Value;

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
