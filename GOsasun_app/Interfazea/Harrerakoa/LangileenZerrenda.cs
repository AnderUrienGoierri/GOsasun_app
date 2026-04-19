using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class LangileenZerrenda : OinarriPantaila
    {
        private enum LangileMota
        {
            OsasunLangilea,
            HarrerakoLangilea
        }

        private enum EkintzaMota
        {
            Ikusi,
            Editatu,
            Ezabatu
        }

        private sealed class LangileZerrendaItem
        {
            public int Id { get; init; }
            public string Nan { get; init; } = string.Empty;
            public string Izena { get; init; } = string.Empty;
            public string Abizenak { get; init; } = string.Empty;
            public string Emaila { get; init; } = string.Empty;
            public string Telefonoa { get; init; } = string.Empty;
            public DateTime? JaiotzeData { get; init; }
            public string Helbidea { get; init; } = string.Empty;
            public string Herria { get; init; } = string.Empty;
            public string PostaKodea { get; init; } = string.Empty;
            public string Hizkuntza { get; init; } = string.Empty;
            public string ElkargokideZenbakia { get; init; } = string.Empty;
            public string Espezialitatea { get; init; } = string.Empty;
            public string Kontsulta { get; init; } = string.Empty;
            public string Lanaldia { get; init; } = string.Empty;
            public string Txanda { get; init; } = string.Empty;
            public DateTime? SortzeData { get; init; }
            public string Aktibo { get; init; } = string.Empty;
        }

        private const int EkintzaIkonoTamaina = 18;
        private const int EkintzaBotoiTamaina = 34;
        private const int OrrikoErregistroKopurua = 10;
        private const int PaginazioPanelAltuera = 64;

        private readonly LangileMota _mota;
        private readonly ErabiltzaileKontrolatzailea _kontrolatzailea = new ErabiltzaileKontrolatzailea();
        private readonly Dictionary<EkintzaMota, Bitmap> _ekintzaIkonoak = new Dictionary<EkintzaMota, Bitmap>();

        private List<LangileZerrendaItem> _langileak = new List<LangileZerrendaItem>();
        private string _azkenOrdenazioZutabea = string.Empty;
        private bool _ordenazioGorakorra = true;
        private int _unekoOrria = 1;
        private bool _interfazeaHasieratuta;

        public LangileenZerrenda() : this("Osasun Langilea", SortuDiseinukoErabiltzailea())
        {
        }

        public LangileenZerrenda(string rolIzena, Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            _mota = string.Equals(rolIzena, "Harrerako Langilea", StringComparison.OrdinalIgnoreCase)
                ? LangileMota.HarrerakoLangilea
                : LangileMota.OsasunLangilea;

            InitializeComponent();
            HasieratuInterfazea();
            KonfiguratuInterfazea();
            KonfiguratuTaula();
            KonfiguratuGertaerak();
            KargatuEkintzaIkonoak();
            _interfazeaHasieratuta = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!_interfazeaHasieratuta)
            {
                return;
            }

            ClientSize = new Size(1680, 980);
            ZentratuPantailaLanEremuan();
            EguneratuDiseinua();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (DiseinuModuan())
            {
                return;
            }

            KargatuLangileak();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_interfazeaHasieratuta && !DiseinuModuan())
            {
                EguneratuDiseinua();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();

                foreach (Bitmap bitmap in _ekintzaIkonoak.Values)
                {
                    bitmap.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        private void HasieratuInterfazea()
        {
            _pnlBilatzailea.BackColor = Color.White;
            _pnlBilatzailea.BorderStyle = BorderStyle.FixedSingle;

            _pnlPaginazioa.BackColor = Color.Transparent;
            _lblPaginazioa.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            _lblPaginazioa.ForeColor = Color.White;
            _lblPaginazioa.TextAlign = ContentAlignment.MiddleCenter;
            _btnAurrekoOrria.FlatStyle = FlatStyle.Flat;
            _btnAurrekoOrria.FlatAppearance.BorderSize = 0;
            _btnAurrekoOrria.BackColor = Color.FromArgb(52, 73, 94);
            _btnAurrekoOrria.ForeColor = Color.White;
            _btnAurrekoOrria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnHurrengoOrria.FlatStyle = FlatStyle.Flat;
            _btnHurrengoOrria.FlatAppearance.BorderSize = 0;
            _btnHurrengoOrria.BackColor = Color.FromArgb(41, 128, 185);
            _btnHurrengoOrria.ForeColor = Color.White;
            _btnHurrengoOrria.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            _dgvLangileak.AutoGenerateColumns = false;
            _dgvLangileak.BackgroundColor = Color.White;
            _dgvLangileak.BorderStyle = BorderStyle.None;
            _dgvLangileak.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            _dgvLangileak.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            _dgvLangileak.EnableHeadersVisualStyles = false;
            _dgvLangileak.MultiSelect = false;
            _dgvLangileak.ReadOnly = true;
            _dgvLangileak.RowHeadersVisible = false;
            _dgvLangileak.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvLangileak.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            _dgvLangileak.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(33, 150, 243);
            _dgvLangileak.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _dgvLangileak.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            _dgvLangileak.ColumnHeadersHeight = 64;
            _dgvLangileak.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            _dgvLangileak.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            _dgvLangileak.DefaultCellStyle.SelectionBackColor = Color.FromArgb(227, 242, 253);
            _dgvLangileak.DefaultCellStyle.SelectionForeColor = Color.Black;
            _dgvLangileak.RowTemplate.Height = 42;
            _dgvLangileak.ScrollBars = ScrollBars.Both;
        }

        private void KonfiguratuInterfazea()
        {
            Text = _mota == LangileMota.OsasunLangilea
                ? "GOsasun - Osasun langileen zerrenda"
                : "GOsasun - Harrerako langileen zerrenda";

            _lblIzenburua.Text = _mota == LangileMota.OsasunLangilea
                ? "OSASUN LANGILEEN ZERRENDA"
                : "HARRERAKO LANGILEEN ZERRENDA";
        }

        private void KonfiguratuTaula()
        {
            _dgvLangileak.Columns.Clear();
            DataGridViewTextBoxColumn ekintzak = new DataGridViewTextBoxColumn
            {
                Name = "Ekintzak",
                HeaderText = "EKINTZAK",
                Width = 180,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true
            };
            ekintzak.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _dgvLangileak.Columns.Add(ekintzak);

            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Nan), "NAN", 120));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Izena), "IZENA", 140));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Abizenak), "ABIZENAK", 170));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Emaila), "EMAILA", 220, DataGridViewContentAlignment.MiddleLeft));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Telefonoa), "TELEFONOA", 120));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.JaiotzeData), "JAIOTZE DATA", 120, DataGridViewContentAlignment.MiddleCenter, "yyyy/MM/dd"));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Helbidea), "HELBIDEA", 180, DataGridViewContentAlignment.MiddleLeft));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Herria), "HERRIA", 120));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.PostaKodea), "POSTA KODEA", 110));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Hizkuntza), "HIZKUNTZA", 110));

            if (_mota == LangileMota.OsasunLangilea)
            {
                _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.ElkargokideZenbakia), "ELKARGOKIDE", 130));
                _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Espezialitatea), "ESPEZIALITATEA", 150));
                _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Kontsulta), "KONTSULTA", 120));
                _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Lanaldia), "LANALDIA", 110));
            }
            else
            {
                _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Txanda), "TXANDA", 110));
            }

            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.SortzeData), "SORTZE DATA", 120, DataGridViewContentAlignment.MiddleCenter, "yyyy/MM/dd"));
            _dgvLangileak.Columns.Add(SortuTestuZutabea(nameof(LangileZerrendaItem.Aktibo), "AKTIBO", 90));
        }

        private static DataGridViewTextBoxColumn SortuTestuZutabea(string propertyName, string headerText, int width, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleCenter, string? format = null)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                Name = propertyName,
                Width = width,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Programmatic
            };

            column.DefaultCellStyle.Alignment = alignment;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.HeaderCell.Style.WrapMode = DataGridViewTriState.True;
            column.DefaultCellStyle.NullValue = "-";
            if (!string.IsNullOrWhiteSpace(format))
            {
                column.DefaultCellStyle.Format = format;
            }

            return column;
        }

        private void KonfiguratuGertaerak()
        {
            _txtBilatu.TextChanged += TxtBilatu_TextChanged;
            _dgvLangileak.CellPainting += DgvLangileak_CellPainting;
            _dgvLangileak.CellMouseClick += DgvLangileak_CellMouseClick;
            _dgvLangileak.CellMouseEnter += DgvLangileak_CellMouseEnter;
            _dgvLangileak.ColumnHeaderMouseClick += DgvLangileak_ColumnHeaderMouseClick;
            _btnAurrekoOrria.Click += BtnAurrekoOrria_Click;
            _btnHurrengoOrria.Click += BtnHurrengoOrria_Click;
        }

        private void DgvLangileak_CellMouseEnter(object? sender, DataGridViewCellEventArgs e)
        {
            _dgvLangileak.Cursor = e.RowIndex >= 0 && EkintzakZutabeaDa(e.ColumnIndex)
                ? Cursors.Hand
                : Cursors.Default;
        }

        private void KargatuEkintzaIkonoak()
        {
            Bitmap? ikusi = KargatuIkonoBitmapa("eye.svg", Color.White, EkintzaIkonoTamaina);
            Bitmap? editatu = KargatuIkonoBitmapa("pencil.svg", Color.White, EkintzaIkonoTamaina);
            Bitmap? ezabatu = KargatuIkonoBitmapa("trash-2.svg", Color.White, EkintzaIkonoTamaina);

            if (ikusi != null)
            {
                _ekintzaIkonoak[EkintzaMota.Ikusi] = ikusi;
            }

            if (editatu != null)
            {
                _ekintzaIkonoak[EkintzaMota.Editatu] = editatu;
            }

            if (ezabatu != null)
            {
                _ekintzaIkonoak[EkintzaMota.Ezabatu] = ezabatu;
            }
        }

        private void EguneratuDiseinua()
        {
            if (!_interfazeaHasieratuta
                || _edukiPanela == null
                || _lblIzenburua == null
                || _pnlBilatzailea == null
                || _lblBilatu == null
                || _txtBilatu == null
                || _dgvLangileak == null
                || _pnlPaginazioa == null
                || _btnAurrekoOrria == null
                || _btnHurrengoOrria == null
                || _lblPaginazioa == null
                || _edukiPanela.ClientSize.Width <= 0)
            {
                return;
            }

            int zabalera = _edukiPanela.ClientSize.Width;
            int altuera = _edukiPanela.ClientSize.Height;
            int marjina = zabalera < 1300 ? 28 : 36;
            int zabaleraErabilgarria = Math.Max(900, zabalera - (marjina * 2));
            int bilatzaileZabalera = Math.Min(620, Math.Max(360, zabaleraErabilgarria / 2));

            _lblIzenburua.Location = new Point(marjina, 22);
            _pnlBilatzailea.Bounds = new Rectangle(marjina, 92, bilatzaileZabalera, 78);
            _lblBilatu.Location = new Point(24, 26);
            _txtBilatu.Bounds = new Rectangle(110, 18, Math.Max(180, _pnlBilatzailea.Width - 142), 40);

            int taulaGoia = _pnlBilatzailea.Bottom + 22;
            int taulaAltueraErabilgarria = Math.Max(240, altuera - taulaGoia - PaginazioPanelAltuera - 24);
            int taulaAltuera = Math.Min(taulaAltueraErabilgarria, LortuTaularenHelburuAltuera());
            int taulaZabalera = Math.Min(zabaleraErabilgarria, LortuTaularenEdukiZabalera());
            _dgvLangileak.Bounds = new Rectangle(marjina, taulaGoia, taulaZabalera, taulaAltuera);
            _pnlPaginazioa.Bounds = new Rectangle(marjina, _dgvLangileak.Bottom + 12, zabaleraErabilgarria, PaginazioPanelAltuera);
            EguneratuPaginazioKontrolak();
        }

        private int LortuTaularenEdukiZabalera()
        {
            int zutabeZabalera = _dgvLangileak.Columns
                .Cast<DataGridViewColumn>()
                .Where(column => column.Visible)
                .Sum(column => column.Width);

            int scrollEtaMarjina = SystemInformation.VerticalScrollBarWidth + 8;
            return Math.Max(760, zutabeZabalera + scrollEtaMarjina);
        }

        private int LortuTaularenHelburuAltuera()
        {
            int goiburuAltuera = Math.Max(40, _dgvLangileak.ColumnHeadersHeight);
            int errenkadaAltuera = Math.Max(32, _dgvLangileak.RowTemplate.Height);
            int scrollAltuera = SystemInformation.HorizontalScrollBarHeight;
            int ertzak = 6;

            return goiburuAltuera + (errenkadaAltuera * OrrikoErregistroKopurua) + scrollAltuera + ertzak;
        }

        private void TxtBilatu_TextChanged(object? sender, EventArgs e)
        {
            KargatuLangileak();
        }

        private void KargatuLangileak()
        {
            try
            {
                string? bilatzailea = string.IsNullOrWhiteSpace(_txtBilatu.Text) ? null : _txtBilatu.Text.Trim();
                _langileak = _mota == LangileMota.OsasunLangilea
                    ? _kontrolatzailea.LortuGuztiakOsasunLangileak(bilatzailea).Select(SortuItem).ToList()
                    : _kontrolatzailea.LortuGuztiakHarrerakoak(bilatzailea).Select(SortuItem).ToList();

                BistaratuUnekoOrria(lehenOrriraJoan: true);
                GarbituOrdenazioIkurrak();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Errorea gertatu da langileen zerrenda kargatzean: " + ex.Message,
                    "Errorea",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private LangileZerrendaItem SortuItem(OsasunLangilea langilea)
        {
            return new LangileZerrendaItem
            {
                Id = langilea.Id,
                Nan = FormateatuTestua(langilea.Nan),
                Izena = FormateatuTestua(langilea.Izena),
                Abizenak = FormateatuTestua(langilea.Abizenak),
                Emaila = FormateatuTestua(langilea.Emaila),
                Telefonoa = FormateatuTestua(langilea.Telefonoa),
                JaiotzeData = langilea.JaiotzeData == DateTime.MinValue ? null : langilea.JaiotzeData,
                Helbidea = FormateatuTestua(langilea.Helbidea),
                Herria = FormateatuTestua(langilea.Herria),
                PostaKodea = FormateatuTestua(langilea.PostaKodea),
                Hizkuntza = FormateatuTestua(langilea.Hizkuntza),
                ElkargokideZenbakia = FormateatuTestua(langilea.ElkargokideZenbakia),
                Espezialitatea = FormateatuTestua(langilea.Espezialitatea),
                Kontsulta = FormateatuTestua(langilea.Kontsulta),
                Lanaldia = FormateatuTestua(langilea.Lanaldia),
                SortzeData = langilea.SortzeData == DateTime.MinValue ? null : langilea.SortzeData,
                Aktibo = langilea.Aktibo ? "Bai" : "Ez"
            };
        }

        private LangileZerrendaItem SortuItem(HarrerakoLangilea langilea)
        {
            return new LangileZerrendaItem
            {
                Id = langilea.Id,
                Nan = FormateatuTestua(langilea.Nan),
                Izena = FormateatuTestua(langilea.Izena),
                Abizenak = FormateatuTestua(langilea.Abizenak),
                Emaila = FormateatuTestua(langilea.Emaila),
                Telefonoa = FormateatuTestua(langilea.Telefonoa),
                JaiotzeData = langilea.JaiotzeData == DateTime.MinValue ? null : langilea.JaiotzeData,
                Helbidea = FormateatuTestua(langilea.Helbidea),
                Herria = FormateatuTestua(langilea.Herria),
                PostaKodea = FormateatuTestua(langilea.PostaKodea),
                Hizkuntza = FormateatuTestua(langilea.Hizkuntza),
                Txanda = FormateatuTestua(langilea.Txanda),
                SortzeData = langilea.SortzeData == DateTime.MinValue ? null : langilea.SortzeData,
                Aktibo = langilea.Aktibo ? "Bai" : "Ez"
            };
        }

        private static string FormateatuTestua(string? balioa)
        {
            return string.IsNullOrWhiteSpace(balioa) ? "-" : balioa.Trim();
        }

        private static Erabiltzailea SortuDiseinukoErabiltzailea()
        {
            return new HarrerakoLangilea
            {
                Id = 1,
                Izena = "Diseinu",
                Abizenak = "Erabiltzailea",
                Emaila = "designer@gosasun.local",
                RolId = 3,
                Nan = "00000000A",
                Aktibo = true,
                Hizkuntza = "Euskara"
            };
        }

        private bool EkintzakZutabeaDa(int columnIndex)
        {
            return columnIndex >= 0
                && columnIndex < _dgvLangileak.Columns.Count
                && string.Equals(_dgvLangileak.Columns[columnIndex].Name, "Ekintzak", StringComparison.OrdinalIgnoreCase);
        }

        private int LortuOrrialdeKopurua()
        {
            return Math.Max(1, (int)Math.Ceiling(_langileak.Count / (double)OrrikoErregistroKopurua));
        }

        private List<LangileZerrendaItem> LortuUnekoOrrikoLangileak()
        {
            return _langileak
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

            _dgvLangileak.DataSource = null;
            _dgvLangileak.DataSource = LortuUnekoOrrikoLangileak();
            EguneratuPaginazioKontrolak();
        }

        private void EguneratuPaginazioKontrolak()
        {
            if (_pnlPaginazioa.ClientSize.Width <= 0)
            {
                return;
            }

            int orrialdeKopurua = LortuOrrialdeKopurua();
            int hasiera = _langileak.Count == 0 ? 0 : ((_unekoOrria - 1) * OrrikoErregistroKopurua) + 1;
            int amaiera = _langileak.Count == 0 ? 0 : Math.Min(_unekoOrria * OrrikoErregistroKopurua, _langileak.Count);

            _btnAurrekoOrria.Enabled = _unekoOrria > 1;
            _btnHurrengoOrria.Enabled = _unekoOrria < orrialdeKopurua;
            _btnAurrekoOrria.Location = new Point(0, 12);
            _btnHurrengoOrria.Location = new Point(_pnlPaginazioa.Width - _btnHurrengoOrria.Width, 12);
            _lblPaginazioa.Bounds = new Rectangle(_btnAurrekoOrria.Right + 18, 10, Math.Max(260, _pnlPaginazioa.Width - 336), 42);
            _lblPaginazioa.Text = _langileak.Count == 0
                ? "0 erregistro"
                : $"{hasiera}-{amaiera} / {_langileak.Count}   |   {_unekoOrria}. orria / {orrialdeKopurua}";
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

        private static Dictionary<EkintzaMota, Rectangle> LortuEkintzaBotoiak(Rectangle cellBounds)
        {
            int spacing = 12;
            int totalWidth = (EkintzaBotoiTamaina * 3) + (spacing * 2);
            int left = cellBounds.Left + Math.Max(10, (cellBounds.Width - totalWidth) / 2);
            int top = cellBounds.Top + Math.Max(4, (cellBounds.Height - EkintzaBotoiTamaina) / 2);

            return new Dictionary<EkintzaMota, Rectangle>
            {
                [EkintzaMota.Ikusi] = new Rectangle(left, top, EkintzaBotoiTamaina, EkintzaBotoiTamaina),
                [EkintzaMota.Editatu] = new Rectangle(left + EkintzaBotoiTamaina + spacing, top, EkintzaBotoiTamaina, EkintzaBotoiTamaina),
                [EkintzaMota.Ezabatu] = new Rectangle(left + ((EkintzaBotoiTamaina + spacing) * 2), top, EkintzaBotoiTamaina, EkintzaBotoiTamaina)
            };
        }

        private void DgvLangileak_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (!EkintzakZutabeaDa(e.ColumnIndex) || e.RowIndex < 0 || e.Graphics == null)
            {
                return;
            }

            e.PaintBackground(e.CellBounds, true);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);

            foreach (KeyValuePair<EkintzaMota, Rectangle> botoia in LortuEkintzaBotoiak(e.CellBounds))
            {
                MarraztuEkintzaBotoia(e.Graphics, botoia.Key, botoia.Value);
            }

            e.Handled = true;
        }

        private void MarraztuEkintzaBotoia(Graphics graphics, EkintzaMota mota, Rectangle bounds)
        {
            Color kolorea = mota == EkintzaMota.Ikusi
                ? Color.FromArgb(41, 128, 185)
                : mota == EkintzaMota.Editatu
                    ? Color.FromArgb(39, 174, 96)
                    : Color.FromArgb(192, 57, 43);

            using GraphicsPath path = SortuBiribildua(bounds, 12);
            using SolidBrush brush = new SolidBrush(kolorea);
            using Pen pen = new Pen(Color.White, 1);

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillPath(brush, path);
            graphics.DrawPath(pen, path);

            if (_ekintzaIkonoak.TryGetValue(mota, out Bitmap? ikonoa))
            {
                int left = bounds.Left + ((bounds.Width - ikonoa.Width) / 2);
                int top = bounds.Top + ((bounds.Height - ikonoa.Height) / 2);
                graphics.DrawImage(ikonoa, new Rectangle(left, top, ikonoa.Width, ikonoa.Height));
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

        private void DgvLangileak_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (!EkintzakZutabeaDa(e.ColumnIndex) || e.RowIndex < 0)
            {
                return;
            }

            if (_dgvLangileak.Rows[e.RowIndex].DataBoundItem is not LangileZerrendaItem item)
            {
                return;
            }

            Rectangle cellBounds = _dgvLangileak.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(cellBounds.Left + e.X, cellBounds.Top + e.Y);

            foreach (KeyValuePair<EkintzaMota, Rectangle> botoia in LortuEkintzaBotoiak(cellBounds))
            {
                if (!botoia.Value.Contains(posizioa))
                {
                    continue;
                }

                if (botoia.Key == EkintzaMota.Ikusi)
                {
                    IkusiLangilea(item);
                }
                else if (botoia.Key == EkintzaMota.Editatu)
                {
                    EditatuLangilea(item);
                }
                else
                {
                    EzabatuLangilea(item);
                }

                break;
            }
        }

        private void IkusiLangilea(LangileZerrendaItem item)
        {
            if (_mota == LangileMota.OsasunLangilea)
            {
                OsasunLangilea? langilea = _kontrolatzailea.LortuOsasunLangilea(item.Id);
                if (langilea != null)
                {
                    IrekiAzpiPantaila(() => new NireErabiltzaileFitxa(langilea));
                }

                return;
            }

            HarrerakoLangilea? harrerakoa = _kontrolatzailea.LortuHarrerakoa(item.Id);
            if (harrerakoa != null)
            {
                IrekiAzpiPantaila(() => new NireErabiltzaileFitxa(harrerakoa));
            }
        }

        private void EditatuLangilea(LangileZerrendaItem item)
        {
            if (_erabiltzailea == null)
            {
                return;
            }

            if (_mota == LangileMota.OsasunLangilea)
            {
                OsasunLangilea? langilea = _kontrolatzailea.LortuOsasunLangilea(item.Id);
                if (langilea == null)
                {
                    MessageBox.Show("Ezin izan da osasun langilearen informazioa kargatu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                IrekiAzpiPantaila(() => new ErabiltzaileaSortu(langilea, _erabiltzailea), KargatuLangileak);
                return;
            }

            HarrerakoLangilea? harrerakoa = _kontrolatzailea.LortuHarrerakoa(item.Id);
            if (harrerakoa == null)
            {
                MessageBox.Show("Ezin izan da harrerako langilearen informazioa kargatu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IrekiAzpiPantaila(() => new ErabiltzaileaSortu(harrerakoa, _erabiltzailea), KargatuLangileak);
        }

        private void EzabatuLangilea(LangileZerrendaItem item)
        {
            string izenOsoa = $"{item.Izena} {item.Abizenak}".Trim();
            string rola = _mota == LangileMota.OsasunLangilea ? "osasun langilea" : "harrerako langilea";
            DialogResult emaitza = MessageBox.Show(
                $"Ziur zaude {izenOsoa} {rola} ezabatu nahi duzula?",
                "Berretsi ezabatzea",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (emaitza != DialogResult.Yes)
            {
                return;
            }

            bool ondo = _mota == LangileMota.OsasunLangilea
                ? _kontrolatzailea.EzabatuOsasunLangilea(item.Id)
                : _kontrolatzailea.EzabatuHarrerakoa(item.Id);

            if (!ondo)
            {
                MessageBox.Show("Errorea gertatu da erabiltzailea desaktibatzean.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Erabiltzailea ondo desaktibatu da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            KargatuLangileak();
        }

        private void DgvLangileak_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (_langileak.Count == 0 || e.ColumnIndex < 0 || EkintzakZutabeaDa(e.ColumnIndex))
            {
                return;
            }

            string? propertyName = _dgvLangileak.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return;
            }

            if (_azkenOrdenazioZutabea == propertyName)
            {
                _ordenazioGorakorra = !_ordenazioGorakorra;
            }
            else
            {
                _azkenOrdenazioZutabea = propertyName;
                _ordenazioGorakorra = true;
            }

            System.Reflection.PropertyInfo? pi = typeof(LangileZerrendaItem).GetProperty(propertyName);
            if (pi == null)
            {
                return;
            }

            _langileak = _ordenazioGorakorra
                ? _langileak.OrderBy(item => pi.GetValue(item, null)).ToList()
                : _langileak.OrderByDescending(item => pi.GetValue(item, null)).ToList();

            BistaratuUnekoOrria();
            GarbituOrdenazioIkurrak();
            _dgvLangileak.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = _ordenazioGorakorra ? SortOrder.Ascending : SortOrder.Descending;
        }

        private void GarbituOrdenazioIkurrak()
        {
            foreach (DataGridViewColumn column in _dgvLangileak.Columns)
            {
                column.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }
    }
}