using GOsasun_app.Modeloa;
using GOsasun_app.Kontrola.Zerbitzuak;
using GOsasun_app.Kontrola;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Beurer BM58 gailutik tentsio neurketak inportatzeko formularioa.
    /// </summary>
    public partial class TentsiometroNeurketak : OinarriPantaila
    {
        private readonly BM58Driver _driver = new BM58Driver();
        private string? _portuIzena;
        private bool _isHid;
        private CancellationTokenSource? _searchCts;
        private List<Pazientea> _pazienteak = new List<Pazientea>();
        private readonly ErabiltzaileKontrolatzailea _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();
        private readonly NeurketaKontrolatzailea _neurketaKontrolatzailea = new NeurketaKontrolatzailea();

        public TentsiometroNeurketak(Erabiltzailea medikua)
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
                    _pazienteak = _erabiltzaileKontrolatzailea.LortuLangilearenPazienteak(_erabiltzailea.Id);
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

            List<BM58RawRecord> guztiak = null;
            try
            {
                // 1. PRE-SCAN MODAL: Konexioa egiaztatu
                using (var waitForm = new Form())
                {
                    waitForm.Text = "Beurer BM58";
                    waitForm.Size = new Size(500, 200); // Handiagoa oraindik testua erabat kabitzeko
                    waitForm.StartPosition = FormStartPosition.CenterParent;
                    waitForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    waitForm.ControlBox = false;

                    var lbl = new Label { 
                        Text = "Gailua aztertzen eta datuak deskargatzen...\nItxaron une bat mesedez.", 
                        Dock = DockStyle.Top, 
                        Padding = new Padding(10), 
                        TextAlign = ContentAlignment.MiddleCenter, 
                        Height = 110, // Altuera gehiago eman lerro guztietarako
                        Font = new Font(this.Font.FontFamily, 11, FontStyle.Bold) // Letra piska bat handiagoa eta lodia
                    };
                    var pb = new ProgressBar { Style = ProgressBarStyle.Marquee, Dock = DockStyle.Bottom, Height = 25 };
                    waitForm.Controls.Add(lbl);
                    waitForm.Controls.Add(pb);

                    waitForm.Shown += async (s, e) => {
                        try {
                            await Task.Run(() => {
                                guztiak = _driver.IrakurriErrekordGuztiak(_portuIzena, _isHid);
                            });
                            waitForm.DialogResult = DialogResult.OK;
                        } catch (Exception ex) {
                            MessageBox.Show("Errorea konektatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            waitForm.DialogResult = DialogResult.Cancel;
                        } finally {
                            waitForm.Close();
                        }
                    };
                    waitForm.ShowDialog(this);
                }

                if (guztiak == null || guztiak.Count == 0) return;

                // 2. Analizatu memoriak
                var info = _driver.AnalizatuErrekordak(guztiak);

                // 3. HAUTAKETA BERRIA (Form dinamikoa Messagebox ordez)
                int aukeratutakoMemoria = 1;
                using (var selectForm = new Form())
                {
                    selectForm.Text = "Hautatu Memoria";
                    selectForm.Size = new Size(600, 420); // Askok handituta dena ondo ikusteko
                    selectForm.StartPosition = FormStartPosition.CenterParent;
                    selectForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                    selectForm.MaximizeBox = false; selectForm.MinimizeBox = false;

                    string u1Indizeak = string.Join(", ", guztiak.Where(r => r.UserId == 1).Select(r => r.Index));
                    string u2Indizeak = string.Join(", ", guztiak.Where(r => r.UserId == 2).Select(r => r.Index));
                    
                    string indizeakTestua = "";
                    if (info.U1Kopurua > 0 && info.U2Kopurua > 0) indizeakTestua = $"U1: {u1Indizeak} eta U2: {u2Indizeak}";
                    else if (info.U1Kopurua > 0) indizeakTestua = $"U1: {u1Indizeak}";
                    else if (info.U2Kopurua > 0) indizeakTestua = $"U2: {u2Indizeak}";

                    var lblMsg = new Label { 
                        Text = $"Gailuan {info.Denetara} neurketa aurkitu dira.\n({indizeakTestua})\nZein memoria inportatu nahi duzu?", 
                        Dock = DockStyle.Top, 
                        Height = 150, // Altuera askoz gehiago testuarentzat
                        TextAlign = ContentAlignment.MiddleCenter, 
                        Font = new Font(this.Font.FontFamily, 11, FontStyle.Bold) 
                    };
                    
                    var btnU1 = new Button { Text = $"U1 ({info.U1Kopurua} neurketa)", Size = new Size(250, 80), Location = new Point(30, 170), BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, Font = new Font(this.Font.FontFamily, 9, FontStyle.Bold) };
                    var btnU2 = new Button { Text = $"U2 ({info.U2Kopurua} neurketa)", Size = new Size(250, 80), Location = new Point(310, 170), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, Font = new Font(this.Font.FontFamily, 9, FontStyle.Bold) };
                    var btnUtzi = new Button { Text = "Utzi", DialogResult = DialogResult.Cancel, Dock = DockStyle.Bottom, Height = 50 };

                    btnU1.Click += (s, e) => { aukeratutakoMemoria = 1; selectForm.DialogResult = DialogResult.OK; };
                    btnU2.Click += (s, e) => { aukeratutakoMemoria = 2; selectForm.DialogResult = DialogResult.OK; };

                    if (info.U1Kopurua == 0) btnU1.Enabled = false;
                    if (info.U2Kopurua == 0) btnU2.Enabled = false;

                    selectForm.Controls.Add(lblMsg);
                    selectForm.Controls.Add(btnU1);
                    selectForm.Controls.Add(btnU2);
                    selectForm.Controls.Add(btnUtzi);

                    if (selectForm.ShowDialog(this) != DialogResult.OK) return;
                }

                // 4. Kalkulatu eta Gorde
                var neurria = _driver.KalkulatuBatezbestekoa(guztiak, pazienteId, aukeratutakoMemoria);
                if (neurria != null)
                {
                    bool gordeta = _neurketaKontrolatzailea.GordeNeurketa(neurria);
                    
                    if (gordeta) {
                        _neurketaKontrolatzailea.EsportatuXML(neurria);
                        MessageBox.Show($"U{aukeratutakoMemoria} neurketa inportatu da:\nSistole: {neurria.TentsioSistolikoa}\nDiastole: {neurria.TentsioDiastolikoa}\nPultsua: {neurria.PultsuaPpm}", "Inportazio Arrakastatsua", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    } else {
                        MessageBox.Show("Ezin izan da datu-basean gorde.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea inportatzean:\n" + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _pnlMainCard_Paint(object sender, PaintEventArgs e)
        {
            // Visual Studio Designerrak behar duen gertaera-kudeatzailea.
        }
    }
}
