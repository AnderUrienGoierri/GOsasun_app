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

namespace GOsasun_app.Formularioak
{
    /// <summary>
    /// Medikuak hautatu dezakeen neurketa moten formularioa.
    /// (Tentsioa, Glukosa, Pisua, Altuera)
    /// </summary>
    public partial class NeurketaMotakFormularioa : OinarriFormularioa
    {
        private readonly BM58Driver _driver = new BM58Driver();
        private string? _portuIzena;
        private bool _isHid;
        private CancellationTokenSource? _searchCts;
        private List<Pazientea> _pazienteak = new List<Pazientea>();
        private readonly ErabiltzaileKontrolatzailea _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();

        // -----------------------------------------------------------
        // Eraikitzailea
        // -----------------------------------------------------------
        public NeurketaMotakFormularioa() : base()
        {
            InitializeComponent();
        }

        public NeurketaMotakFormularioa(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            InitializeComponent();
            KonfiguratuGertaerak();
        }

        private void KonfiguratuGertaerak()
        {
            btnTentsiometroa.Click += (s, e) => HasieraEstatuaEzarri();
            
            _btnUtzi.Click += (s, e) => PantailaNagusiaErakutsi();
            _btnInportatu.Click += (s, e) => DatuakInportatu();
            _txtPazienteBilatu.TextChanged += (s, e) => PazienteakBatu();
            _dgvPazienteak.SelectionChanged += (s, e) => _btnInportatu.Enabled = _dgvPazienteak.SelectedRows.Count > 0;
            _dgvPazienteak.CellDoubleClick += (s, e) => { if (_btnInportatu.Enabled) DatuakInportatu(); };

            btnGlukometroa.Click += (s, e) => MessageBox.Show("Glukometroa kargatzen...");
            btnPisua.Click += (s, e) => MessageBox.Show("Pisua kargatzen...");
            btnAltuera.Click += (s, e) => MessageBox.Show("Altuera kargatzen...");
        }

        private void HasieraEstatuaEzarri()
        {
            _pnlImport.Visible = true;
            _lblStatus.Text = "Konektatu Beurer BM58 USB bidez,\neta sakatu 'MEM' gailuan...";
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
                                // Konekzio berria
                                wasConnected = true;
                                _portuIzena = aurkitutakoPortua;
                                _isHid = isHidOrain;
                                this.Invoke(new Action(() => GailuaAurkitua()));
                            }
                        }
                        else
                        {
                            if (wasConnected)
                            {
                                // Konexioa galdu da
                                wasConnected = false;
                                _portuIzena = null;
                                this.Invoke(new Action(() => GailuaGalduta()));
                            }

                            // 10 segundo pasatu badira egoera zehatza erakutsi (eta ez badago gailurik)
                            if ((DateTime.Now - startTime).TotalSeconds > 10)
                            {
                                this.Invoke(new Action(() => {
                                    string[] portuakList = SerialPort.GetPortNames();
                                    string msg = "Oraindik bilatzen... (Saiakera: " + counter + ")\n\n";
                                    
                                    if (portuakList.Length == 0)
                                    {
                                        msg += "ERROREA: Ez da serie-porturik (COM) aurkitu.\n" +
                                               "Gailua USB-HID gisa ere ez da azaltzen.\n\n" +
                                               "1. Ziurtatu PC agertzen dela pantailan.\n" +
                                               "2. Aldatu USB kablea (datu-kablea dela ziurtatuz).";
                                    }
                                    else
                                    {
                                        msg += "Serie-portu batzuk aurkitu dira baina ez Beurer gailua.\n" +
                                               "Portuak: " + string.Join(", ", portuakList) + "\n\n" +
                                               "Ziurtatu 'MEM' sakatu duzula PC moduan sartzeko.";
                                    }
                                    
                                    _lblStatus.Text = msg;
                                    _lblStatus.ForeColor = Color.DarkOrange;
                                }));
                            }
                            else
                            {
                                this.Invoke(new Action(() => {
                                    _lblStatus.Text = "Bilatzen... (Saiakera: " + counter + ")\nKonektatu gailua eta sakatu 'MEM'...";
                                    _lblStatus.ForeColor = Color.FromArgb(44, 62, 80);
                                }));
                            }
                        }

                        Thread.Sleep(1000); // 1 segundoro egiazatu
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
            _lblStatus.Text = _isHid ? "Gailua aurkituta! (USB-HID modua)" : "Gailua aurkituta! (COM modua)";
            _lblStatus.ForeColor = Color.FromArgb(46, 204, 113);
            
            _lblBilatu.Visible = true;
            _txtPazienteBilatu.Visible = true;
            _dgvPazienteak.Visible = true;
            _btnInportatu.Visible = true;
            _btnInportatu.Enabled = false;
            
            PazienteakKargatu();
            _txtPazienteBilatu.Focus();
        }

        private void PazienteakKargatu()
        {
            try
            {
                // Erabiltzailea medikua dela ziurtatu (Logikatik dator)
                if (_erabiltzaileKontrolatzailea != null && _erabiltzailea != null)
                {
                    _pazienteak = _erabiltzaileKontrolatzailea.LortuMedikuarenPazienteak(_erabiltzailea.Id);
                    GordeDgvDatuak(_pazienteak);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea pazienteak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                p.Id // Ezkutuan egongo da nahiz eta DataSource-n egon (edo eskuz zutabeak definitu)
            }).ToList();

            var idColumn = _dgvPazienteak.Columns["Id"];
            if (idColumn != null)
                idColumn.Visible = false;
        }

        private void DatuakInportatu()
        {
            if (_portuIzena == null || _dgvPazienteak.SelectedRows.Count == 0) return;

            var hautatutakoa = _dgvPazienteak.SelectedRows[0].DataBoundItem;
            if (hautatutakoa == null) return;

            // Dinamikoki lortu IDa (anonimous type denez)
            string pazienteId = hautatutakoa.GetType().GetProperty("Id")?.GetValue(hautatutakoa)?.ToString() ?? "";

            if (string.IsNullOrEmpty(pazienteId)) return;

            // Argibideak gailua prestatzeko (PC modua)
            DialogResult dr = MessageBox.Show(
                "Ziurtatu 'PC' agertzen dela tentsiometroaren pantailan.\n\n" +
                "Konektatu USBa eta itxaron gailuak 'PC' letra horiek erakutsi arte.\n\n" +
                "Behin 'PC' ikusten duzunean, sakatu 'Aceptar' jarraitzeko.",
                "Gailua Prestatu", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr != DialogResult.OK) return;

            try
            {
                var neurria = _driver.IrakurriAzkenNeurria(_portuIzena, _isHid, int.Parse(pazienteId));
                _driver.GordeXML(neurria);

                string izena = hautatutakoa.GetType().GetProperty("Izena")?.GetValue(hautatutakoa)?.ToString() ?? "";
                string abizenak = hautatutakoa.GetType().GetProperty("Abizenak")?.GetValue(hautatutakoa)?.ToString() ?? "";

                MessageBox.Show($"Neurria ondo inportatu da!\n\nPazientea: {izena} {abizenak} (ID: {pazienteId})\nSistole: {neurria.TentsioSistolikoa}\nDiastole: {neurria.TentsioDiastolikoa}\nPultsua: {neurria.Pultsua}", 
                    "Inportazio Arrakastatsua", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PantailaNagusiaErakutsi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea neurketa irakurtzean:\n" + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HasieraEstatuaEzarri(); // Berriz saiatu
            }
        }

        private void PantailaNagusiaErakutsi()
        {
            _searchCts?.Cancel();
            _pnlImport.Visible = false;
            _txtPazienteBilatu.Clear();
            _dgvPazienteak.DataSource = null;
        }
    }
}
