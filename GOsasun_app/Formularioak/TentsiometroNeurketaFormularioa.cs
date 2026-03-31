using GOsasun_app.Modeloak;
using GOsasun_app.Zerbitzuak;
using GOsasun_app.Kontrolatzaileak;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Beurer BM58 gailutik tentsio neurketak inportatzeko formularioa.
    /// </summary>
    public partial class TentsiometroNeurketaFormularioa : OinarriFormularioa
    {
        private readonly BM58Driver _driver = new BM58Driver();
        private string? _portuIzena;
        private bool _isHid;
        private CancellationTokenSource? _searchCts;
        private List<Pazientea> _pazienteak = new List<Pazientea>();
        private readonly ErabiltzaileKontrolatzailea _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();
        private readonly NeurketaKontrolatzailea _neurketaKontrolatzailea = new NeurketaKontrolatzailea();

        public TentsiometroNeurketaFormularioa(Erabiltzailea medikua)
            : base(medikua)
        {
            InitializeComponent();
            
            // UI Egokitzapena: _lblStatus-i espazio gehiago eman
            _lblStatus.Location = new Point(3, 50);
            _lblStatus.Height = 220;

            // Gainontzeko osagaiak behera mugitu espazioa uzteko
            _lblBilatu.Top = 280;
            _txtPazienteBilatu.Top = 335;
            _dgvPazienteak.Top = 405;
            _lblHistoriala.Top = 600;
            _dgvHistoriala.Top = 660;
            
            KonfiguratuGertaerak();
            HasieraEstatuaEzarri();
        }

        private void KonfiguratuGertaerak()
        {
            _btnUtzi.Click += (s, e) => { _searchCts?.Cancel(); this.Close(); };
            _btnInportatu.Click += (s, e) => DatuakInportatu();
            _txtPazienteBilatu.TextChanged += (s, e) => PazienteakBatu();
            
            _dgvPazienteak.SelectionChanged += (s, e) => {
                bool selected = _dgvPazienteak.SelectedRows.Count > 0;
                _btnInportatu.Enabled = selected;
                
                if (selected)
                {
                    var hautatutakoa = _dgvPazienteak.SelectedRows[0].DataBoundItem;
                    if (hautatutakoa != null)
                    {
                        string pIdStr = hautatutakoa.GetType().GetProperty("Id")?.GetValue(hautatutakoa)?.ToString() ?? "";
                        if (int.TryParse(pIdStr, out int pId))
                        {
                            KargatuPazientearenHistoriala(pId);
                        }
                    }
                }
                else
                {
                    _lblHistoriala.Visible = false;
                    _dgvHistoriala.Visible = false;
                }
            };
            
            _dgvPazienteak.CellDoubleClick += (s, e) => { if (_btnInportatu.Enabled) DatuakInportatu(); };
        }

        private void KargatuPazientearenHistoriala(int pazienteId)
        {
            try
            {
                var neurketak = _neurketaKontrolatzailea.LortuPazientearenNeurketak(pazienteId);
                
                DataTable dt = new DataTable();
                dt.Columns.Add("Data", typeof(DateTime));
                dt.Columns.Add("Sistole", typeof(int));
                dt.Columns.Add("Diastole", typeof(int));
                dt.Columns.Add("Pultsua", typeof(int));
                dt.Columns.Add("Pisua", typeof(decimal));
                dt.Columns.Add("Altuera", typeof(decimal));
                dt.Columns.Add("Sintomak", typeof(string));

                foreach (var n in neurketak)
                {
                    dt.Rows.Add(
                        n.ErregistroData,
                        (object?)n.TentsioSistolikoa ?? DBNull.Value,
                        (object?)n.TentsioDiastolikoa ?? DBNull.Value,
                        (object?)n.PultsuaPpm ?? DBNull.Value,
                        (object?)n.PisuaKg ?? DBNull.Value,
                        (object?)n.Altuera ?? DBNull.Value,
                        n.Sintomak ?? ""
                    );
                }

                _dgvHistoriala.DataSource = dt;

                if (_dgvHistoriala.Columns.Count > 0)
                {
                    if (_dgvHistoriala.Columns["Data"] != null) _dgvHistoriala.Columns["Data"]!.DefaultCellStyle.Format = "yyyy/MM/dd HH:mm";
                    if (_dgvHistoriala.Columns["Sistole"] != null) _dgvHistoriala.Columns["Sistole"]!.HeaderText = "Sist. (mmHg)";
                    if (_dgvHistoriala.Columns["Diastole"] != null) _dgvHistoriala.Columns["Diastole"]!.HeaderText = "Diast. (mmHg)";
                    if (_dgvHistoriala.Columns["Pultsua"] != null) _dgvHistoriala.Columns["Pultsua"]!.HeaderText = "Pult. (ppm)";
                    if (_dgvHistoriala.Columns["Pisua"] != null) _dgvHistoriala.Columns["Pisua"]!.HeaderText = "Pisua (kg)";
                    if (_dgvHistoriala.Columns["Altuera"] != null) _dgvHistoriala.Columns["Altuera"]!.HeaderText = "Alt. (cm)";
                    
                    foreach (DataGridViewColumn col in _dgvHistoriala.Columns)
                    {
                        col.DefaultCellStyle.NullValue = "-";
                    }
                }

                _lblHistoriala.Visible = true;
                _dgvHistoriala.Visible = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Errorea historiala kargatzean: " + ex.Message);
            }
        }

        private void HasieraEstatuaEzarri()
        {
            _lblStatus.Text = "Konektatu Beurer BM58 USB bidez,\neta itxaron 'PC' agertu arte...";
            _lblStatus.ForeColor = Color.FromArgb(44, 62, 80);
            
            _lblBilatu.Visible = false;
            _txtPazienteBilatu.Visible = false;
            _dgvPazienteak.Visible = false;
            _btnInportatu.Visible = false;
            
            HasiBilaketaAsync();
        }

        private async void HasiBilaketaAsync()
        {
            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();
            var token = _searchCts.Token;

            DateTime startTime = DateTime.Now;
            int counter = 0;
            bool wasConnected = false;

            try
            {
                await Task.Run(() =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        counter++;
                        string? aurkitutakoPortua = _driver.BilatuGailua(out bool isHidOrain);
                        
                        bool isConnectedNow = (aurkitutakoPortua != null);

                        if (isConnectedNow)
                        {
                            if (!wasConnected)
                            {
                                wasConnected = true;
                                _portuIzena = aurkitutakoPortua;
                                _isHid = isHidOrain;
                                if (this.IsHandleCreated) this.Invoke(new Action(() => GailuaAurkitua()));
                            }
                        }
                        else
                        {
                            if (wasConnected)
                            {
                                wasConnected = false;
                                _portuIzena = null;
                                if (this.IsHandleCreated) this.Invoke(new Action(() => GailuaGalduta()));
                            }

                            if ((DateTime.Now - startTime).TotalSeconds > 10)
                            {
                                if (this.IsHandleCreated) this.Invoke(new Action(() => {
                                    _lblStatus.Text = "Oraindik bilatzen... (Saiakera: " + counter + ")\nZiurtatu 'PC' ikusten dela pantailan.";
                                    _lblStatus.ForeColor = Color.DarkOrange;
                                }));
                            }
                        }
                        Thread.Sleep(1000);
                    }
                }, token);
            }
            catch (OperationCanceledException) { }
        }

        private void GailuaGalduta()
        {
            _lblBilatu.Visible = false;
            _txtPazienteBilatu.Visible = false;
            _dgvPazienteak.Visible = false;
            _btnInportatu.Visible = false;
            _lblStatus.Text = "Konexioa galdu da! Berriz konektatzen...";
            _lblStatus.ForeColor = Color.Red;
        }

        private void GailuaAurkitua()
        {
            _lblStatus.Text = _isHid ? "Gailua prest! (USB-HID)" : "Gailua prest! (COM)";
            _lblStatus.ForeColor = Color.FromArgb(46, 204, 113);

            if (_erabiltzailea != null && _erabiltzailea.DaPazientea())
            {
                // Pazientea bada, ezkutatu bilaketa eta erakutsi inportatu botoia zuzenean
                _lblBilatu.Visible = false;
                _txtPazienteBilatu.Visible = false;
                _dgvPazienteak.Visible = false;
                _btnInportatu.Visible = true;
                _btnInportatu.Enabled = true;
                _lblStatus.Text += "\nInportatu zure neurketa orain.";
            }
            else
            {
                _lblBilatu.Visible = true;
                _txtPazienteBilatu.Visible = true;
                _dgvPazienteak.Visible = true;
                _btnInportatu.Visible = true;
                _btnInportatu.Enabled = false;

                PazienteakKargatu();
                _txtPazienteBilatu.Focus();
            }
        }

        private void PazienteakKargatu()
        {
            try
            {
                if (_erabiltzaileKontrolatzailea != null && _erabiltzailea != null)
                {
                    _pazienteak = _erabiltzaileKontrolatzailea.LortuMedikuarenPazienteak(_erabiltzailea.Id);
                    GordeDgvDatuak(_pazienteak);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea pazienteak kargatzean: " + ex.Message);
            }
        }

        private void PazienteakBatu()
        {
            string testua = _txtPazienteBilatu.Text.ToLower();
            var iragazita = _pazienteak.Where(p => 
                p.Izena.ToLower().Contains(testua) || 
                p.Abizenak.ToLower().Contains(testua) || 
                p.Nan.ToLower().Contains(testua)
            ).ToList();
            GordeDgvDatuak(iragazita);
        }

        private void GordeDgvDatuak(List<Pazientea> zerrenda)
        {
            _dgvPazienteak.DataSource = zerrenda.Select(p => new {
                p.Nan,
                p.Izena,
                p.Abizenak,
                p.Id 
            }).ToList();

            if (_dgvPazienteak.Columns["Id"] != null)
                _dgvPazienteak.Columns["Id"].Visible = false;
        }

        private void DatuakInportatu()
        {
            if (_portuIzena == null) return;

            int pazienteId;

            if (_erabiltzailea != null && _erabiltzailea.DaPazientea())
            {
                pazienteId = _erabiltzailea.Id;
            }
            else
            {
                if (_dgvPazienteak.SelectedRows.Count == 0) return;
                var hautatutakoa = _dgvPazienteak.SelectedRows[0].DataBoundItem;
                if (hautatutakoa == null) return;
                string pIdStr = hautatutakoa.GetType().GetProperty("Id")?.GetValue(hautatutakoa)?.ToString() ?? "";
                if (string.IsNullOrEmpty(pIdStr)) return;
                pazienteId = int.Parse(pIdStr);
            }

            try
            {
                var neurria = _driver.IrakurriAzkenNeurria(_portuIzena, _isHid, pazienteId);
                
                // 1. Gorde XML (Lehendik zegoen bezala backup gisa)
                _driver.GordeXML(neurria);

                // 2. Gorde Datu-Basean (Berria: Pazienteak historia ikusi dezan)
                bool gordeta = _neurketaKontrolatzailea.GordeNeurketa(neurria);

                string msg = gordeta ? "Neurria ondo inportatu eta gordeta!" : "Neurria inportatu da baina datu-basean errorea egon da.";
                MessageBox.Show($"{msg}\n\nSistole: {neurria.TentsioSistolikoa}\nDiastole: {neurria.TentsioDiastolikoa}\nPultsua: {neurria.PultsuaPpm}", 
                    "Inportazio Arrakastatsua", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _searchCts?.Cancel();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea neurketa irakurtzean:\n" + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _pnlMainCard_Paint(object sender, PaintEventArgs e)
        {
            // Visual Studio Designerrak behar duen gertaera-kudeatzailea.
        }
    }
}
