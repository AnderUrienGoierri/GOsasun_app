using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GOsasun_app.Interfazea.Kontrolak;
using GOsasun_app.Modeloa;
using GOsasun_app.Kontrola;

namespace GOsasun_app.Interfazea
{
    /// <summary>
    /// Medikuari esleitutako pazienteen zerrenda taula bidez erakusten duen formularioa.
    /// </summary>
    public partial class PazienteenZerrenda : OinarriPantaila
    {
        private const int EkintzaZutabeZabalera = 330;
        private const int EkintzaIkonoTamaina = 30;
        private const int EkintzaBotoiTamaina = 52;
        private const int PazienteErrenkadaAltuera = 64;
        private const int OrrikoErregistroKopurua = 10;
        private readonly PazienteKontrolatzailea _pazienteKontrolatzailea;
        private readonly OsasunLangileKontrolatzailea _osasunLangileKontrolatzailea;
        private List<Pazientea> _pazienteak = new List<Pazientea>();
        private readonly Dictionary<string, Bitmap?> _ekintzaIkonoak = new Dictionary<string, Bitmap?>();
        private readonly Panel _pnlPaginazioa = new Panel();
        private readonly Label _lblPaginazioa = new Label();
        private readonly Button _btnAurrekoOrria = new Button();
        private readonly Button _btnHurrengoOrria = new Button();
        private bool _hasierakoPazienteakKargatuta;
        private bool _hasierakoPazienteakKargatzen;
        private int _unekoOrria = 1;

        public PazienteenZerrenda(Erabiltzailea medikua)
            : base(medikua)
        {
            InitializeComponent();
            _pazienteKontrolatzailea = new PazienteKontrolatzailea();
            _osasunLangileKontrolatzailea = new OsasunLangileKontrolatzailea();
            EzarriFormularioZabalera();

            // Izenburua aldatu rolaran arabera
            if (_erabiltzailea is HarrerakoLangilea)
            {
                lblIzenburua.Text = "PAZIENTEEN KUDEAKETA";
                chkPazienteGuztiak.Checked = true;
                chkPazienteGuztiak.Enabled = false;
                btnPazienteBerria.Visible = true;
                btnOsasunLangileaSortu.Visible = true;
            }
            else
            {
                chkPazienteGuztiak.Checked = false;
                btnPazienteBerria.Visible = true;
                btnOsasunLangileaSortu.Visible = true;
            }

            KonfiguratuTaula();
            HasieratuPaginazioa();
            KargatuEkintzaIkonoak();

            // Gertaerak
            txtBilatu.TextChanged += TxtBilatu_TextChanged;
            chkPazienteGuztiak.CheckedChanged += PazienteMotaFiltroa_CheckedChanged;
            chkAltan.CheckedChanged += EgoeraFiltroa_CheckedChanged;
            chkBajan.CheckedChanged += EgoeraFiltroa_CheckedChanged;
            btnPazienteBerria.Click += BtnPazienteBerria_Click;
            btnOsasunLangileaSortu.Click += BtnOsasunLangileaSortu_Click;
        }

        private void HasieratuPaginazioa()
        {
            _pnlPaginazioa.BackColor = Color.Transparent;
            _pnlPaginazioa.Height = 56;

            _btnAurrekoOrria.Text = "Aurreko 10ak";
            _btnAurrekoOrria.Size = new Size(150, 40);
            _btnAurrekoOrria.FlatStyle = FlatStyle.Flat;
            _btnAurrekoOrria.FlatAppearance.BorderSize = 0;
            _btnAurrekoOrria.BackColor = Color.FromArgb(52, 73, 94);
            _btnAurrekoOrria.ForeColor = Color.White;
            _btnAurrekoOrria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnAurrekoOrria.Click += BtnAurrekoOrria_Click;

            _btnHurrengoOrria.Text = "Hurrengo 10ak";
            _btnHurrengoOrria.Size = new Size(150, 40);
            _btnHurrengoOrria.FlatStyle = FlatStyle.Flat;
            _btnHurrengoOrria.FlatAppearance.BorderSize = 0;
            _btnHurrengoOrria.BackColor = Color.FromArgb(41, 128, 185);
            _btnHurrengoOrria.ForeColor = Color.White;
            _btnHurrengoOrria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnHurrengoOrria.Click += BtnHurrengoOrria_Click;

            _lblPaginazioa.AutoSize = false;
            _lblPaginazioa.TextAlign = ContentAlignment.MiddleCenter;
            _lblPaginazioa.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            _lblPaginazioa.ForeColor = Color.White;

            _pnlPaginazioa.Controls.Add(_btnAurrekoOrria);
            _pnlPaginazioa.Controls.Add(_lblPaginazioa);
            _pnlPaginazioa.Controls.Add(_btnHurrengoOrria);
            pnlBilatzailea.Controls.Add(_pnlPaginazioa);
            _pnlPaginazioa.BringToFront();

            EguneratuPaginazioKontrolak();
        }

        private void KonfiguratuTaula()
        {
            dgvPazienteak.AutoGenerateColumns = false;
            dgvPazienteak.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvPazienteak.ScrollBars = ScrollBars.Both;
            dgvPazienteak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPazienteak.Columns.Clear();
            dgvPazienteak.RowTemplate.Height = PazienteErrenkadaAltuera;

            dgvPazienteak.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IzenOsoa",
                HeaderText = "EKINTZAK",
                Name = "Ekintzak",
                Width = LortuDoitutakoZabalera(EkintzaZutabeZabalera),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            dgvPazienteak.Columns.Add(SortuTestuZutabea("Nan", "NAN", LortuDoitutakoZabalera(185)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Izena", "Izena", LortuDoitutakoZabalera(150)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Abizenak", "Abizenak", LortuDoitutakoZabalera(220)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Emaila", "Emaila", LortuDoitutakoZabalera(380)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Telefonoa", "Telefonoa", LortuDoitutakoZabalera(180)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("JaiotzeData", "Jaiotze data", LortuDoitutakoZabalera(195), "yyyy/MM/dd"));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("Sexua", "Sexua", LortuDoitutakoZabalera(110)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("OdolTaldea", "Odol taldea", LortuDoitutakoZabalera(120)));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("AzkenAltuera", "Altuera", LortuDoitutakoZabalera(110), "N2"));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("AzkenPisua", "Pisua", LortuDoitutakoZabalera(110), "N2"));
            dgvPazienteak.Columns.Add(SortuTestuZutabea("EgoeraKlinikoa", "Egoera", LortuDoitutakoZabalera(120)));

            dgvPazienteak.Cursor = Cursors.Default;
            dgvPazienteak.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0 && EkintzakZutabeaDa(e.ColumnIndex))
                    dgvPazienteak.Cursor = Cursors.Hand;
                else
                    dgvPazienteak.Cursor = Cursors.Default;
            };

            dgvPazienteak.ColumnHeaderMouseClick += DgvPazienteak_ColumnHeaderMouseClick;
            dgvPazienteak.CellPainting += DgvPazienteak_CellPainting;
            dgvPazienteak.CellMouseClick += DgvPazienteak_CellMouseClick;
        }

        private void EzarriFormularioZabalera()
        {
            int zabalera = ClientSize.Width;

            ClientSize = new Size(zabalera, ClientSize.Height);
            _goiburuBarra.Width = zabalera;
            _edukiPanela.Size = new Size(zabalera, _edukiPanela.Height);
            EguneratuBilatzailearenDiseinua();
            ZentratuPantailaLanEremuan();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EzarriFormularioZabalera();
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            await Task.Yield();

            EzarriFormularioZabalera();
            EguneratuBilatzailearenDiseinua();
            EguneratuTaularenZabalerak();
            ZentratuPantailaLanEremuan();

            if (_hasierakoPazienteakKargatuta || _hasierakoPazienteakKargatzen || DiseinuModuan())
            {
                return;
            }

            await KargatuHasierakoPazienteakAsync();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (!DiseinuModuan())
            {
                EguneratuBilatzailearenDiseinua();
                EguneratuTaularenZabalerak();
                EguneratuPaginazioKontrolak();
            }
        }

        private async Task KargatuHasierakoPazienteakAsync()
        {
            _hasierakoPazienteakKargatzen = true;
            EzarriHasierakoKargaEgoera(true);

            try
            {
                string bilaketa = txtBilatu.Text.Trim();
                string? egoeraFiltroa = LortuEgoeraFiltroa();
                bool pazienteGuztiak = chkPazienteGuztiak.Checked;
                List<Pazientea> pazienteak = await Task.Run(() => LortuPazienteZerrenda(bilaketa, egoeraFiltroa, pazienteGuztiak));

                if (IsDisposed)
                {
                    return;
                }

                AplikatuPazienteZerrenda(pazienteak);
                _hasierakoPazienteakKargatuta = true;
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    MessageBox.Show("Errorea pazienteak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                _hasierakoPazienteakKargatzen = false;
                if (!IsDisposed)
                {
                    EzarriHasierakoKargaEgoera(false);
                }
            }
        }

        private void EzarriHasierakoKargaEgoera(bool kargatzen)
        {
            UseWaitCursor = kargatzen;
            Cursor = kargatzen ? Cursors.WaitCursor : Cursors.Default;
            txtBilatu.Enabled = !kargatzen;
            chkPazienteGuztiak.Enabled = !kargatzen && _erabiltzailea is not HarrerakoLangilea;
            chkAltan.Enabled = !kargatzen;
            chkBajan.Enabled = !kargatzen;
            btnPazienteBerria.Enabled = !kargatzen;
            btnOsasunLangileaSortu.Enabled = !kargatzen;
            dgvPazienteak.Enabled = !kargatzen;
            EguneratuPaginazioKontrolak();
        }

        private int LortuOrrialdeKopurua()
        {
            return Math.Max(1, (int)Math.Ceiling(_pazienteak.Count / (double)OrrikoErregistroKopurua));
        }

        private List<Pazientea> LortuUnekoOrrikoPazienteak()
        {
            return _pazienteak
                .Skip((_unekoOrria - 1) * OrrikoErregistroKopurua)
                .Take(OrrikoErregistroKopurua)
                .ToList();
        }

        private void BistaratuUnekoOrria(bool lehenOrriraJoan = false)
        {
            if (lehenOrriraJoan)
            {
                _unekoOrria = 1;
            }

            int orrialdeKopurua = LortuOrrialdeKopurua();
            _unekoOrria = Math.Max(1, Math.Min(_unekoOrria, orrialdeKopurua));

            dgvPazienteak.DataSource = null;
            dgvPazienteak.DataSource = LortuUnekoOrrikoPazienteak();
            EguneratuPaginazioKontrolak();
        }

        private void EguneratuPaginazioKontrolak()
        {
            int orrialdeKopurua = LortuOrrialdeKopurua();
            int hasiera = _pazienteak.Count == 0 ? 0 : ((_unekoOrria - 1) * OrrikoErregistroKopurua) + 1;
            int amaiera = _pazienteak.Count == 0 ? 0 : Math.Min(_unekoOrria * OrrikoErregistroKopurua, _pazienteak.Count);

            _btnAurrekoOrria.Enabled = !_hasierakoPazienteakKargatzen && _unekoOrria > 1;
            _btnHurrengoOrria.Enabled = !_hasierakoPazienteakKargatzen && _unekoOrria < orrialdeKopurua;
            _lblPaginazioa.Text = _pazienteak.Count == 0
                ? "0 erregistro"
                : $"{hasiera}-{amaiera} / {_pazienteak.Count}   |   {_unekoOrria}. orria / {orrialdeKopurua}";

            if (_pnlPaginazioa.ClientSize.Width <= 0)
            {
                return;
            }

            _btnAurrekoOrria.Location = new Point(0, 8);
            _btnHurrengoOrria.Location = new Point(_btnAurrekoOrria.Right + 14, 8);
            _lblPaginazioa.Bounds = new Rectangle(
                _btnHurrengoOrria.Right + 18,
                8,
                Math.Max(260, _pnlPaginazioa.ClientSize.Width - (_btnAurrekoOrria.Width + _btnHurrengoOrria.Width + 32)),
                40);
        }

        private void BtnAurrekoOrria_Click(object? sender, EventArgs e)
        {
            if (_unekoOrria <= 1)
            {
                return;
            }

            _unekoOrria--;
            BistaratuUnekoOrria();
        }

        private void BtnHurrengoOrria_Click(object? sender, EventArgs e)
        {
            if (_unekoOrria >= LortuOrrialdeKopurua())
            {
                return;
            }

            _unekoOrria++;
            BistaratuUnekoOrria();
        }

        private int LortuDoitutakoZabalera(int oinarrizkoZabalera)
        {
            return oinarrizkoZabalera;
        }

        private void EguneratuTaularenZabalerak()
        {
            if (dgvPazienteak == null || dgvPazienteak.Columns.Count == 0)
            {
                return;
            }

            Dictionary<string, int> zabalerak = new Dictionary<string, int>
            {
                ["Nan"] = 185,
                ["Izena"] = 150,
                ["Abizenak"] = 220,
                ["Emaila"] = 380,
                ["Telefonoa"] = 180,
                ["JaiotzeData"] = 195,
                ["Sexua"] = 110,
                ["OdolTaldea"] = 120,
                ["AzkenAltuera"] = 110,
                ["AzkenPisua"] = 110,
                ["EgoeraKlinikoa"] = 120,
                ["Ekintzak"] = EkintzaZutabeZabalera
            };

            foreach (DataGridViewColumn zutabea in dgvPazienteak.Columns)
            {
                if (zabalerak.TryGetValue(zutabea.Name, out int oinarrizkoZabalera))
                {
                    zutabea.Width = LortuDoitutakoZabalera(oinarrizkoZabalera);
                }
            }
        }

        private void EguneratuBilatzailearenDiseinua()
        {
            if (pnlBilatzailea == null
                || lblIzenburua == null
                || lblBilatu == null
                || txtBilatu == null
                || chkPazienteGuztiak == null
                || chkAltan == null
                || chkBajan == null
                || btnPazienteBerria == null
                || btnOsasunLangileaSortu == null)
            {
                return;
            }

            int panelZabalera = pnlBilatzailea.ClientSize.Width;
            if (panelZabalera <= 0)
            {
                return;
            }

            int ezkerMarjina = Math.Max(20, lblBilatu.Left);
            int azkenBehea = 0;

            azkenBehea = Math.Max(azkenBehea, lblBilatu.Bottom);
            azkenBehea = Math.Max(azkenBehea, txtBilatu.Bottom);

            if (chkPazienteGuztiak.Visible)
            {
                azkenBehea = Math.Max(azkenBehea, chkPazienteGuztiak.Bottom);
            }

            if (chkAltan.Visible)
            {
                azkenBehea = Math.Max(azkenBehea, chkAltan.Bottom);
            }

            if (chkBajan.Visible)
            {
                azkenBehea = Math.Max(azkenBehea, chkBajan.Bottom);
            }

            if (btnPazienteBerria.Visible)
            {
                azkenBehea = Math.Max(azkenBehea, btnPazienteBerria.Bottom);
            }

            if (btnOsasunLangileaSortu.Visible)
            {
                azkenBehea = Math.Max(azkenBehea, btnOsasunLangileaSortu.Bottom);
            }

            int paginazioGoia = azkenBehea + 18;
            int paginazioZabalera = Math.Min(700, Math.Max(420, panelZabalera - (ezkerMarjina * 2)));
            int paginazioX = Math.Max(ezkerMarjina, Math.Min(_pnlPaginazioa.Left <= 0 ? ezkerMarjina : _pnlPaginazioa.Left, panelZabalera - paginazioZabalera - ezkerMarjina));
            _pnlPaginazioa.Location = new Point(paginazioX, paginazioGoia);
            _pnlPaginazioa.Size = new Size(paginazioZabalera, 56);

            azkenBehea = Math.Max(azkenBehea, _pnlPaginazioa.Bottom);

            int panelAltuera = azkenBehea + 20;
            pnlBilatzailea.Height = panelAltuera;
            EguneratuPaginazioKontrolak();
        }

        private static DataGridViewTextBoxColumn SortuTestuZutabea(string propertyName, string headerText, int width, string? format = null)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                Name = propertyName,
                Width = width,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = format ?? string.Empty,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            };
        }

        private void KargatuEkintzaIkonoak()
        {
            _ekintzaIkonoak["ikusi"] = KargatuIkonoBitmapa("eye.svg", Color.White, EkintzaIkonoTamaina);
            _ekintzaIkonoak["editatu"] = KargatuIkonoBitmapa("pencil.svg", Color.White, EkintzaIkonoTamaina);
            _ekintzaIkonoak["jarraipena"] = KargatuIkonoBitmapa("plus-circle.svg", Color.White, EkintzaIkonoTamaina);
            _ekintzaIkonoak["jarraipenak"] = KargatuIkonoBitmapa("list.svg", Color.White, EkintzaIkonoTamaina);
            _ekintzaIkonoak["ezabatu"] = KargatuIkonoBitmapa("trash-2.svg", Color.White, EkintzaIkonoTamaina);
        }

        private bool EkintzakZutabeaDa(int columnIndex)
        {
            return columnIndex >= 0
                && columnIndex < dgvPazienteak.Columns.Count
                && string.Equals(dgvPazienteak.Columns[columnIndex].Name, "Ekintzak", StringComparison.OrdinalIgnoreCase);
        }

        private Dictionary<string, Rectangle> LortuEkintzaBotoiak(Rectangle cellBounds)
        {
            int buttonSize = EkintzaBotoiTamaina;
            int spacing = 10;
            bool harrerakoa = _erabiltzailea is HarrerakoLangilea;

            // Harrerakoak ez ditu jarraipena/jarraipenak botoiak ikusten
            List<string> ekintzak = harrerakoa
                ? new List<string> { "ikusi", "editatu", "ezabatu" }
                : new List<string> { "ikusi", "editatu", "jarraipena", "jarraipenak", "ezabatu" };

            int totalWidth = (buttonSize * ekintzak.Count) + (spacing * (ekintzak.Count - 1));
            int left = cellBounds.Left + Math.Max(10, (cellBounds.Width - totalWidth) / 2);
            int top = cellBounds.Top + Math.Max(6, (cellBounds.Height - buttonSize) / 2);

            Dictionary<string, Rectangle> botoiak = new Dictionary<string, Rectangle>();
            for (int i = 0; i < ekintzak.Count; i++)
            {
                botoiak[ekintzak[i]] = new Rectangle(left + ((buttonSize + spacing) * i), top, buttonSize, buttonSize);
            }

            return botoiak;
        }

        private void DgvPazienteak_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (!EkintzakZutabeaDa(e.ColumnIndex) || e.RowIndex < 0 || e.Graphics == null) return;

            e.PaintBackground(e.CellBounds, true);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);

            foreach (KeyValuePair<string, Rectangle> botoia in LortuEkintzaBotoiak(e.CellBounds))
            {
                MarraztuEkintzaBotoia(e.Graphics, botoia.Key, botoia.Value);
            }

            e.Handled = true;
        }

        private void MarraztuEkintzaBotoia(Graphics graphics, string ekintza, Rectangle rectangle)
        {
            Color kolorea = ekintza == "ikusi"
                ? Color.FromArgb(41, 128, 185)
                : ekintza == "editatu"
                    ? Color.FromArgb(39, 174, 96)
                    : ekintza == "jarraipena"
                        ? Color.FromArgb(243, 156, 18)
                        : ekintza == "jarraipenak"
                            ? Color.FromArgb(142, 68, 173)
                    : Color.FromArgb(192, 57, 43);
            string fallbackIkurra = ekintza == "ikusi" ? "I" : ekintza == "editatu" ? "E" : ekintza == "jarraipena" ? "+" : ekintza == "jarraipenak" ? "L" : "X";

            using GraphicsPath path = SortuBiribildua(rectangle, 12);
            using SolidBrush brush = new SolidBrush(kolorea);
            using Pen pen = new Pen(Color.White, 1);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillPath(brush, path);
            graphics.DrawPath(pen, path);

            if (_ekintzaIkonoak.TryGetValue(ekintza, out Bitmap? ikonoa) && ikonoa != null)
            {
                int left = rectangle.Left + ((rectangle.Width - ikonoa.Width) / 2);
                int top = rectangle.Top + ((rectangle.Height - ikonoa.Height) / 2);
                graphics.DrawImage(ikonoa, new Rectangle(left, top, ikonoa.Width, ikonoa.Height));
            }
            else
            {
                TextRenderer.DrawText(graphics, fallbackIkurra, new Font("Segoe UI", 12F, FontStyle.Bold), rectangle, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }

        private static GraphicsPath SortuBiribildua(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void DgvPazienteak_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (!EkintzakZutabeaDa(e.ColumnIndex) || e.RowIndex < 0) return;

            Pazientea? pazientea = dgvPazienteak.Rows[e.RowIndex].DataBoundItem as Pazientea;
            if (pazientea == null) return;

            Rectangle cellBounds = dgvPazienteak.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(cellBounds.Left + e.X, cellBounds.Top + e.Y);

            foreach (KeyValuePair<string, Rectangle> botoia in LortuEkintzaBotoiak(cellBounds))
            {
                if (!botoia.Value.Contains(posizioa)) continue;

                if (botoia.Key == "ikusi")
                {
                    IrekiFormularioa(new PazienteXehetasunak(pazientea));
                }
                else if (botoia.Key == "editatu")
                {
                    EditatuPazientea(pazientea.Id);
                }
                else if (botoia.Key == "jarraipena")
                {
                    SortuPazientearenJarraipena(pazientea);
                }
                else if (botoia.Key == "jarraipenak")
                {
                    IkusiPazientearenJarraipenak(pazientea);
                }
                else if (botoia.Key == "ezabatu")
                {
                    EzabatuPazientea(pazientea);
                }

                break;
            }
        }

        private void EzabatuPazientea(Pazientea pazientea)
        {
            DialogResult emaitza = MessageBox.Show($"Ziur zaude {pazientea.IzenOsoa} pazientea ezabatu (desaktibatu) nahi duzula?",
                "Berretsi ezabatzea", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (emaitza != DialogResult.Yes)
            {
                return;
            }

            if (_pazienteKontrolatzailea.EzabatuPazientea(pazientea.Id))
            {
                MessageBox.Show("Pazientea ondo desaktibatu da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KargatuPazienteak(txtBilatu.Text.Trim());
            }
            else
            {
                MessageBox.Show("Errorea gertatu da pazientea desaktibatzean.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditatuPazientea(int pazienteId)
        {
            if (_erabiltzailea == null)
            {
                return;
            }

            Pazientea? pazienteOsoa = _pazienteKontrolatzailea.LortuPazientea(pazienteId);
            if (pazienteOsoa == null)
            {
                MessageBox.Show("Ezin izan da pazientearen informazioa kargatu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int? esleitutakoLangileId = _erabiltzailea is OsasunLangilea ? _erabiltzailea.Id : null;
            IrekiAzpiPantaila(
                () => new ErabiltzaileaSortu(pazienteOsoa, _erabiltzailea, esleitutakoLangileId),
                () => KargatuPazienteak(txtBilatu.Text.Trim()));
        }

        private void SortuPazientearenJarraipena(Pazientea pazientea)
        {
            if (_erabiltzailea == null)
            {
                return;
            }

            IrekiAzpiPantaila(() => new JarraipenMotak(_erabiltzailea, pazientea.Id, pazientea.IzenOsoa));
        }

        private void IkusiPazientearenJarraipenak(Pazientea pazientea)
        {
            if (_erabiltzailea == null)
            {
                return;
            }

            IrekiAzpiPantaila(() => new Jarraipenak(_erabiltzailea, pazientea.Id, pazientea.IzenOsoa));
        }

        private void EsleituOsasunLangileak(Pazientea pazientea)
        {
            List<OsasunLangilea> langileGuztiak = _osasunLangileKontrolatzailea.LortuGuztiakOsasunLangileak()
                .OrderBy(langilea => langilea.Espezialitatea)
                .ThenBy(langilea => langilea.Abizenak)
                .ThenBy(langilea => langilea.Izena)
                .ToList();
            HashSet<int> jadaEsleitutaIds = _pazienteKontrolatzailea.LortuPazientearenOsasunLangileak(pazientea.Id)
                .Select(langilea => langilea.Id)
                .ToHashSet();
            using EsleituOsasunLangileakLaguntzailea popup = new EsleituOsasunLangileakLaguntzailea();
            popup.Hasieratu(pazientea, langileGuztiak, jadaEsleitutaIds);

            if (popup.ShowDialog(this) == DialogResult.OK)
            {
                bool ondo = _pazienteKontrolatzailea.EsleituOsasunLangileakPazienteari(
                    pazientea.Id,
                    popup.HautatutakoLangileIds.ToList());

                if (!ondo)
                {
                    MessageBox.Show(this, "Ezin izan dira osasun langileak pazienteari esleitu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Osasun langileak ondo esleitu dira.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KargatuPazienteak(txtBilatu.Text.Trim());
            }
        }

        // Ordenatzeko aldagaiak
        private string _azkenOrdenazioZutabea = "";
        private bool _ordenazioGorakorra = true;

        private void DgvPazienteak_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (_pazienteak == null || _pazienteak.Count == 0 || e.ColumnIndex < 0) return;

            string kolumnaIzena = dgvPazienteak.Columns[e.ColumnIndex].DataPropertyName;

            if (_azkenOrdenazioZutabea == kolumnaIzena)
            {
                _ordenazioGorakorra = !_ordenazioGorakorra; // Alderantzikatu
            }
            else
            {
                _azkenOrdenazioZutabea = kolumnaIzena;
                _ordenazioGorakorra = true;
            }

            var unekoPazienteak = _pazienteak.AsEnumerable();
            var pi = typeof(Pazientea).GetProperty(kolumnaIzena);

            if (pi != null)
            {
                if (_ordenazioGorakorra)
                    _pazienteak = unekoPazienteak.OrderBy(x => pi.GetValue(x, null)).ToList();
                else
                    _pazienteak = unekoPazienteak.OrderByDescending(x => pi.GetValue(x, null)).ToList();

                BistaratuUnekoOrria();

                // Sort glyphs (gezi txikiak) aktibatzeko
                foreach (DataGridViewColumn col in dgvPazienteak.Columns)
                    col.HeaderCell.SortGlyphDirection = SortOrder.None;

                dgvPazienteak.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = _ordenazioGorakorra ? SortOrder.Ascending : SortOrder.Descending;
            }
        }

        private void KargatuPazienteak(string? testua = null)
        {
            try
            {
                string? egoeraFiltroa = LortuEgoeraFiltroa();
                bool pazienteGuztiak = chkPazienteGuztiak.Checked;
                List<Pazientea> pazienteak = LortuPazienteZerrenda(testua, egoeraFiltroa, pazienteGuztiak);
                AplikatuPazienteZerrenda(pazienteak);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea pazienteak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Pazientea> LortuPazienteZerrenda(string? testua, string? egoeraFiltroa, bool pazienteGuztiak)
        {
            if (_erabiltzailea is HarrerakoLangilea || pazienteGuztiak)
            {
                return _pazienteKontrolatzailea.LortuGuztiakPazienteak(testua, egoeraFiltroa);
            }

            return _pazienteKontrolatzailea.LortuLangilearenPazienteak(_erabiltzailea!.Id, testua, egoeraFiltroa);
        }

        private void AplikatuPazienteZerrenda(List<Pazientea> pazienteak)
        {
            _pazienteak = pazienteak;
            BistaratuUnekoOrria(lehenOrriraJoan: true);
        }

        private void BtnPazienteBerria_Click(object? sender, EventArgs e)
        {
            if (_erabiltzailea == null) return;

            int? esleitutakoLangileId = _erabiltzailea is OsasunLangilea ? _erabiltzailea.Id : null;
            IrekiAzpiPantaila(
                () => new ErabiltzaileaSortu("Pazientea", _erabiltzailea, esleitutakoLangileId),
                () => KargatuPazienteak(txtBilatu.Text.Trim()));
        }

        private void BtnOsasunLangileaSortu_Click(object? sender, EventArgs e)
        {
            if (_erabiltzailea == null) return;

            IrekiAzpiPantaila(() => new ErabiltzaileaSortu("Osasun Langilea", _erabiltzailea));
        }

        private void TxtBilatu_TextChanged(object? sender, EventArgs e)
        {
            KargatuPazienteak(txtBilatu.Text.Trim());
        }

        private void PazienteMotaFiltroa_CheckedChanged(object? sender, EventArgs e)
        {
            if (_erabiltzailea is HarrerakoLangilea) return;
            KargatuPazienteak(txtBilatu.Text.Trim());
        }

        private void EgoeraFiltroa_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is not CheckBox aukeratutakoCheck) return;

            if (!aukeratutakoCheck.Checked)
            {
                return;
            }

            foreach (CheckBox check in new[] { chkAltan, chkBajan })
            {
                if (!ReferenceEquals(check, aukeratutakoCheck))
                {
                    check.Checked = false;
                }
            }

            KargatuPazienteak(txtBilatu.Text.Trim());
        }

        private string? LortuEgoeraFiltroa()
        {
            if (chkAltan.Checked) return "Alta";
            if (chkBajan.Checked) return "Baja";
            return null;
        }

        private void IrekiFormularioa(Func<Form> formularioSortzailea)
        {
            IrekiAzpiPantaila(formularioSortzailea);
        }

        private void IrekiFormularioa(Form formularioa)
        {
            IrekiAzpiPantaila(formularioa);
        }

        private void dgvPazienteak_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void lblIzenburua_Click(object sender, EventArgs e)
        {

        }
    }
}
