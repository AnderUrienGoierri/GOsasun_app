using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Globalization;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;
using Svg;

namespace GOsasun_app.Interfazea
{
    public partial class Jarraipenak : OinarriPantaila
    {
        private const string DokumentuKarpetarenBidea = @"C:\Apache24-64\htdocs\GOsasun_web\dokumentuak";
        private static readonly Size JarraipenPantailaTamaina = new Size(2700, 1394);
        private const int KanpokoMarjina = 70;
        private const int TaulaGoikoPosizioa = 420;
        private const int JarraipenFilaAltuera = 128;
        private const int EkintzaZutabeZabalera = 280;
        private const int OharrakZutabeZabalera = 430;
        private const int GehienezkoIkusgaiJarraipenak = 10;
        private const int GoiburuAltuera = 181;
        private const int TaularenGoiburuAltuera = 96;
        private const int EkintzaIkonoTamaina = 26;
        private const int OharIkonoTamaina = 22;
        private const int DataFiltroZabalera = 520;
        private const int FiltroakGarbituZabalera = 230;

        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
        private readonly DokumentuaKontrolatzailea _dokumentuaKontrolatzailea = new DokumentuaKontrolatzailea();
        private readonly List<Jarraipena> _jarraipenak = new List<Jarraipena>();
        private readonly Dictionary<EkintzaMota, Bitmap?> _ekintzaIkonoak = new Dictionary<EkintzaMota, Bitmap?>();
        private readonly int? _pazienteIdFiltroa;
        private readonly string? _pazienteIzenburua;
        private Bitmap? _oharraIkonoa;

        private string _azkenOrdenazioZutabea = string.Empty;
        private bool _ordenazioGorakorra = true;

        private enum EkintzaMota
        {
            Ikusi,
            Editatu,
            GehituDokumentua,
            IkusiDokumentuak,
            Ezabatu
        }

        public Jarraipenak() : base()
        {
            _pazienteIdFiltroa = null;
            InitializeComponent();
            HasieratuPantaila();
        }

        public Jarraipenak(Erabiltzailea u) : base(u)
        {
            _pazienteIdFiltroa = null;
            InitializeComponent();
            HasieratuPantaila();
        }

        public Jarraipenak(Erabiltzailea u, int pazienteId, string? pazienteIzenburua = null) : base(u)
        {
            _pazienteIdFiltroa = pazienteId;
            _pazienteIzenburua = pazienteIzenburua;
            InitializeComponent();
            HasieratuPantaila();
        }

        private void HasieratuPantaila()
        {
            EraikiInterfazea();
            EguneratuIzenburua();
            KargatuEkintzaIkonoak();
            KonfiguratuTaula();
            KonfiguratuGertaerak();
            EguneratuPantailaDiseinua();

            if (DiseinuModuan())
            {
                KargatuDiseinuDatuak();
                return;
            }

            KargatuJarraipenak();
        }

        private void EguneratuIzenburua()
        {
            if (_pazienteIdFiltroa.HasValue && !string.IsNullOrWhiteSpace(_pazienteIzenburua))
            {
                _lblIzenburua.Text = $"JARRAIPENAK - {_pazienteIzenburua!.ToUpperInvariant()}";
            }
        }

        private void EraikiInterfazea()
        {
            _btnJarraipenBerria.BackColor = Color.FromArgb(41, 128, 185);
            _btnJarraipenBerria.FlatAppearance.BorderSize = 0;
            _btnFiltroakGarbitu.FlatAppearance.BorderSize = 0;
            _txtBilatu.BackColor = Color.White;
            _txtBilatu.ForeColor = Color.FromArgb(44, 62, 80);
            _txtBilatu.BorderStyle = BorderStyle.FixedSingle;
            _dtpHasieraData.CalendarMonthBackground = Color.White;
            _dtpAmaieraData.CalendarMonthBackground = Color.White;
            _chkJarraipenGuztiakIkusi.BackColor = Color.Transparent;
            _chkJarraipenGuztiakIkusi.ForeColor = Color.FromArgb(44, 62, 80);
            _chkJarraipenGuztiakIkusi.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);

            _dgvJarraipenak.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            _dgvJarraipenak.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _dgvJarraipenak.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _dgvJarraipenak.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _dgvJarraipenak.ColumnHeadersHeight = TaularenGoiburuAltuera;
            _dgvJarraipenak.DefaultCellStyle.Font = new Font("Segoe UI", 10.5F);
            _dgvJarraipenak.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 240, 254);
            _dgvJarraipenak.DefaultCellStyle.SelectionForeColor = Color.FromArgb(44, 62, 80);
            _dgvJarraipenak.AutoGenerateColumns = false;
            _dgvJarraipenak.RowTemplate.Height = JarraipenFilaAltuera;
            _dgvJarraipenak.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            if (BackgroundImage != null)
            {
                _edukiPanela.BackgroundImage = BackgroundImage;
                _edukiPanela.BackgroundImageLayout = ImageLayout.Center;
            }
        }

        private void KonfiguratuTaula()
        {
            if (_dgvJarraipenak.Columns.Count == 0)
            {
                _dgvJarraipenak.Columns.AddRange(SortuJarraipenZutabeak());
            }

            KonfiguratuZutabea("PazienteNan", 140, null, DataGridViewColumnSortMode.Programmatic, null, DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("PazienteIzena", 125, null, DataGridViewColumnSortMode.Programmatic, null, DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("PazienteAbizenak", 170, null, DataGridViewColumnSortMode.Programmatic, null, DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("ErregistroData", 205, "g", DataGridViewColumnSortMode.Programmatic, null, DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("TentsioSistolikoa", 78, null, DataGridViewColumnSortMode.Programmatic, "Sist.", DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("TentsioDiastolikoa", 78, null, DataGridViewColumnSortMode.Programmatic, "Diast.", DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("PultsuaPpm", 95, null, DataGridViewColumnSortMode.Programmatic, null, DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("PisuaKg", 110, "N2", DataGridViewColumnSortMode.Programmatic, null, DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("Altuera", 110, "N2", DataGridViewColumnSortMode.Programmatic, null, DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("DokumentuKopurua", 70, null, DataGridViewColumnSortMode.Programmatic, null, DataGridViewAutoSizeColumnMode.AllCells);
            KonfiguratuZutabea("Oharrak", OharrakZutabeZabalera, null, DataGridViewColumnSortMode.NotSortable);
            KonfiguratuZutabea("Ekintzak", EkintzaZutabeZabalera, null, DataGridViewColumnSortMode.NotSortable, null, DataGridViewAutoSizeColumnMode.None);

            DataGridViewColumn? oharrakZutabea = BilatuZutabea("Oharrak");
            if (oharrakZutabea != null)
            {
                oharrakZutabea.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                oharrakZutabea.DefaultCellStyle.Padding = new Padding(12, 0, 52, 0);
            }
        }

        private static DataGridViewColumn[] SortuJarraipenZutabeak()
        {
            return new DataGridViewColumn[]
            {
                SortuTestuZutabea("PazienteNan", "NAN/DNI", 115),
                SortuTestuZutabea("PazienteIzena", "Izena", 100),
                SortuTestuZutabea("PazienteAbizenak", "Abizenak", 130),
                SortuTestuZutabea("ErregistroData", "Data", 188, "g"),
                SortuTestuZutabea("TentsioSistolikoa", "Sist.", 54),
                SortuTestuZutabea("TentsioDiastolikoa", "Diast.", 54),
                SortuTestuZutabea("PultsuaPpm", "Pultsua", 78),
                SortuTestuZutabea("PisuaKg", "Pisua (kg)", 80, "N2"),
                SortuTestuZutabea("Altuera", "Altuera (m)", 80, "N2"),
                SortuTestuZutabea("DokumentuKopurua", "Dok.", 52),
                SortuTestuZutabea("Oharrak", "Oharrak", OharrakZutabeZabalera),
                SortuEkintzaZutabea()
            };
        }

        private void KonfiguratuZutabea(
            string izena,
            int zabalera,
            string? formatua = null,
            DataGridViewColumnSortMode ordenazioa = DataGridViewColumnSortMode.Programmatic,
            string? headerText = null,
            DataGridViewAutoSizeColumnMode autoSizeMode = DataGridViewAutoSizeColumnMode.None)
        {
            DataGridViewColumn? zutabea = BilatuZutabea(izena);
            if (zutabea == null) return;

            zutabea.AutoSizeMode = autoSizeMode;
            zutabea.MinimumWidth = zabalera;
            zutabea.Width = zabalera;
            zutabea.ReadOnly = true;
            zutabea.SortMode = ordenazioa;
            if (!string.IsNullOrWhiteSpace(headerText))
            {
                zutabea.HeaderText = headerText;
            }
            zutabea.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            zutabea.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            zutabea.DefaultCellStyle.Format = formatua ?? string.Empty;
        }

        private DataGridViewColumn? BilatuZutabea(string gakoa)
        {
            foreach (DataGridViewColumn zutabea in _dgvJarraipenak.Columns)
            {
                string izena = ZutabeGakoa(zutabea);
                if (string.Equals(izena, gakoa, StringComparison.OrdinalIgnoreCase))
                {
                    return zutabea;
                }
            }

            return null;
        }

        private static string ZutabeGakoa(DataGridViewColumn zutabea)
        {
            if (!string.IsNullOrWhiteSpace(zutabea.DataPropertyName))
            {
                return zutabea.DataPropertyName;
            }

            if (!string.IsNullOrWhiteSpace(zutabea.Name))
            {
                return zutabea.Name.TrimStart('_').Replace("col", string.Empty, StringComparison.OrdinalIgnoreCase).TrimStart('_');
            }

            return zutabea.HeaderText;
        }

        private bool EkintzakZutabeaDa(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= _dgvJarraipenak.Columns.Count) return false;
            return string.Equals(ZutabeGakoa(_dgvJarraipenak.Columns[columnIndex]), "EkintzakTestua", StringComparison.OrdinalIgnoreCase)
                || string.Equals(ZutabeGakoa(_dgvJarraipenak.Columns[columnIndex]), "Ekintzak", StringComparison.OrdinalIgnoreCase)
                || string.Equals(_dgvJarraipenak.Columns[columnIndex].HeaderText, "EKINTZAK", StringComparison.OrdinalIgnoreCase);
        }

        private bool OharrakZutabeaDa(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= _dgvJarraipenak.Columns.Count) return false;
            return string.Equals(ZutabeGakoa(_dgvJarraipenak.Columns[columnIndex]), "Oharrak", StringComparison.OrdinalIgnoreCase)
                || string.Equals(_dgvJarraipenak.Columns[columnIndex].HeaderText, "Oharrak", StringComparison.OrdinalIgnoreCase);
        }

        private static DataGridViewTextBoxColumn SortuTestuZutabea(string dataPropertyName, string headerText, int width, string? format = null)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataPropertyName,
                HeaderText = headerText,
                Name = dataPropertyName,
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

        private static DataGridViewTextBoxColumn SortuEkintzaZutabea()
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EkintzakTestua",
                Name = "Ekintzak",
                HeaderText = "EKINTZAK",
                Width = EkintzaZutabeZabalera,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
        }

        private void KargatuEkintzaIkonoak()
        {
            _ekintzaIkonoak.Clear();
            _ekintzaIkonoak[EkintzaMota.Ikusi] = KargatuSvgIkonoa("eye.svg");
            _ekintzaIkonoak[EkintzaMota.Editatu] = KargatuSvgIkonoa("pencil.svg");
            _ekintzaIkonoak[EkintzaMota.GehituDokumentua] = KargatuSvgIkonoa("plus-circle.svg");
            _ekintzaIkonoak[EkintzaMota.IkusiDokumentuak] = KargatuSvgIkonoa("file-text.svg");
            _ekintzaIkonoak[EkintzaMota.Ezabatu] = KargatuSvgIkonoa("trash-2.svg");
            _oharraIkonoa = KargatuSvgIkonoa("search.svg");
        }

        private Bitmap? KargatuSvgIkonoa(string fileName)
        {
            string? bidea = BilatuSvgBidea(fileName);
            if (string.IsNullOrEmpty(bidea) || !File.Exists(bidea)) return null;

            try
            {
                string svgEdukia = File.ReadAllText(bidea);
                svgEdukia = svgEdukia.Replace("currentColor", "#FFFFFF", StringComparison.OrdinalIgnoreCase);

                using MemoryStream memoria = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(svgEdukia));
                SvgDocument svg = SvgDocument.Open<SvgDocument>(memoria);
                return svg.Draw(EkintzaIkonoTamaina, EkintzaIkonoTamaina);
            }
            catch
            {
                return null;
            }
        }

        private static string? BilatuSvgBidea(string fileName)
        {
            HashSet<string> erroak = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string?[] hasierakoak = {
                Application.StartupPath,
                AppContext.BaseDirectory,
                Directory.GetCurrentDirectory(),
                Environment.CurrentDirectory,
                Path.GetDirectoryName(typeof(Jarraipenak).Assembly.Location)
            };

            foreach (string? hasiera in hasierakoak)
            {
                if (string.IsNullOrWhiteSpace(hasiera) || !Directory.Exists(hasiera)) continue;

                DirectoryInfo? karpeta = new DirectoryInfo(hasiera);
                while (karpeta != null)
                {
                    erroak.Add(karpeta.FullName);
                    karpeta = karpeta.Parent;
                }
            }

            foreach (string root in erroak)
            {
                string[] aukerak = {
                    Path.Combine(root, "img", "svg", fileName),
                    Path.Combine(root, "GOsasun_app", "img", "svg", fileName)
                };

                string? aurkitua = aukerak.FirstOrDefault(File.Exists);
                if (!string.IsNullOrEmpty(aurkitua)) return aurkitua;
            }

            return null;
        }

        private void KonfiguratuGertaerak()
        {
            _txtBilatu.TextChanged += (s, e) => KargatuIragazkiekin();
            _dtpHasieraData.ValueChanged += (s, e) => KargatuIragazkiekin();
            _dtpAmaieraData.ValueChanged += (s, e) => KargatuIragazkiekin();
            _chkJarraipenGuztiakIkusi.CheckedChanged += (s, e) => BistaratuJarraipenak();
            _btnFiltroakGarbitu.Click += (s, e) => GarbituFiltroak();
            _btnJarraipenBerria.Click += (s, e) =>
            {
                Form formularioa = _pazienteIdFiltroa.HasValue
                    ? new JarraipenMotak(_erabiltzailea!, _pazienteIdFiltroa.Value, _pazienteIzenburua)
                    : new JarraipenMotak(_erabiltzailea!);
                IrekiFormularioa(formularioa);
            };
            _dgvJarraipenak.ColumnHeaderMouseClick += DgvJarraipenak_ColumnHeaderMouseClick;
            _dgvJarraipenak.CellFormatting += DgvJarraipenak_CellFormatting;
            _dgvJarraipenak.CellPainting += DgvJarraipenak_CellPainting;
            _dgvJarraipenak.DataBindingComplete += (s, e) => _dgvJarraipenak.ClearSelection();

            if (DiseinuModuan()) return;

            _dgvJarraipenak.CellMouseClick += DgvJarraipenak_CellMouseClick;
            _dgvJarraipenak.CellMouseMove += DgvJarraipenak_CellMouseMove;
        }

        private void KargatuIragazkiekin()
        {
            if (DiseinuModuan()) return;

            (DateTime? hasieraData, DateTime? amaieraData) = LortuDataTartea();
            KargatuJarraipenak(_txtBilatu.Text.Trim(), hasieraData, amaieraData);
        }

        private (DateTime? HasieraData, DateTime? AmaieraData) LortuDataTartea()
        {
            DateTime? hasieraData = _dtpHasieraData.Checked ? _dtpHasieraData.Value.Date : null;
            DateTime? amaieraData = _dtpAmaieraData.Checked ? _dtpAmaieraData.Value.Date : null;

            if (hasieraData.HasValue && amaieraData.HasValue && hasieraData.Value > amaieraData.Value)
            {
                (hasieraData, amaieraData) = (amaieraData, hasieraData);
            }

            return (hasieraData, amaieraData);
        }

        private void GarbituFiltroak()
        {
            _txtBilatu.Text = string.Empty;
            _dtpHasieraData.Checked = false;
            _dtpAmaieraData.Checked = false;

            if (!DiseinuModuan())
            {
                KargatuJarraipenak();
            }
        }

        private void KargatuJarraipenak(string? bilaketa = null, DateTime? hasieraData = null, DateTime? amaieraData = null)
        {
            try
            {
                _jarraipenak.Clear();
                _jarraipenak.AddRange(_jarraipenaKontrolatzailea.LortuJarraipenGuztiak(bilaketa, hasieraData, amaieraData, _pazienteIdFiltroa));
                BistaratuJarraipenak();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea jarraipenak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BistaratuJarraipenak()
        {
            IEnumerable<Jarraipena> erakutsiBeharrekoak = _chkJarraipenGuztiakIkusi.Checked
                ? _jarraipenak
                : _jarraipenak.Take(GehienezkoIkusgaiJarraipenak);

            _dgvJarraipenak.DataSource = null;
            _dgvJarraipenak.DataSource = erakutsiBeharrekoak.ToList();
        }

        private void KargatuDiseinuDatuak()
        {
            _dtpHasieraData.Checked = false;
            _dtpAmaieraData.Checked = false;
            _chkJarraipenGuztiakIkusi.Checked = false;
            _jarraipenak.Clear();
            _jarraipenak.AddRange(new[]
            {
                new Jarraipena
                {
                    Id = 1,
                    PazienteNan = "11111111A",
                    PazienteIzena = "Ander",
                    PazienteAbizenak = "Martinez",
                    TentsioSistolikoa = 128,
                    TentsioDiastolikoa = 84,
                    PultsuaPpm = 74,
                    PisuaKg = 78.50m,
                    Altuera = 1.82m,
                    DokumentuKopurua = 2,
                    Oharrak = "Kontrol arrunta",
                    ErregistroData = DateTime.Today.AddHours(9).AddMinutes(30)
                },
                new Jarraipena
                {
                    Id = 2,
                    PazienteNan = "000000001",
                    PazienteIzena = "Jon",
                    PazienteAbizenak = "Urrutia",
                    TentsioSistolikoa = 165,
                    TentsioDiastolikoa = 101,
                    PultsuaPpm = 108,
                    PisuaKg = 92.10m,
                    Altuera = 1.70m,
                    DokumentuKopurua = 1,
                    Oharrak = "Parametroren bat arriskutsua",
                    ErregistroData = DateTime.Today.AddDays(-1).AddHours(16)
                },
                new Jarraipena
                {
                    Id = 3,
                    PazienteNan = "22222222B",
                    PazienteIzena = "Miren",
                    PazienteAbizenak = "Etxeberria",
                    TentsioSistolikoa = 132,
                    TentsioDiastolikoa = 86,
                    PultsuaPpm = 77,
                    PisuaKg = 68.30m,
                    Altuera = 1.68m,
                    DokumentuKopurua = 0,
                    Oharrak = "Hilabeteko jarraipena",
                    ErregistroData = DateTime.Today.AddDays(-2).AddHours(10)
                },
                new Jarraipena
                {
                    Id = 4,
                    PazienteNan = "33333333C",
                    PazienteIzena = "Ane",
                    PazienteAbizenak = "Lopez",
                    TentsioSistolikoa = 118,
                    TentsioDiastolikoa = 79,
                    PultsuaPpm = 69,
                    PisuaKg = 61.20m,
                    Altuera = 1.64m,
                    DokumentuKopurua = 3,
                    Oharrak = "Kontrol egonkorra",
                    ErregistroData = DateTime.Today.AddDays(-3).AddHours(8)
                },
                new Jarraipena
                {
                    Id = 5,
                    PazienteNan = "44444444D",
                    PazienteIzena = "Iker",
                    PazienteAbizenak = "Garcia",
                    TentsioSistolikoa = 145,
                    TentsioDiastolikoa = 93,
                    PultsuaPpm = 88,
                    PisuaKg = 84.00m,
                    Altuera = 1.76m,
                    DokumentuKopurua = 1,
                    Oharrak = "Tentsio altua goizean",
                    ErregistroData = DateTime.Today.AddDays(-4).AddHours(11)
                },
                new Jarraipena
                {
                    Id = 6,
                    PazienteNan = "55555555E",
                    PazienteIzena = "Leire",
                    PazienteAbizenak = "Sanchez",
                    TentsioSistolikoa = 121,
                    TentsioDiastolikoa = 80,
                    PultsuaPpm = 72,
                    PisuaKg = 70.10m,
                    Altuera = 1.71m,
                    DokumentuKopurua = 2,
                    Oharrak = "Jarraipen normala",
                    ErregistroData = DateTime.Today.AddDays(-5).AddHours(15)
                },
                new Jarraipena
                {
                    Id = 7,
                    PazienteNan = "66666666F",
                    PazienteIzena = "Unai",
                    PazienteAbizenak = "Perez",
                    TentsioSistolikoa = 138,
                    TentsioDiastolikoa = 89,
                    PultsuaPpm = 81,
                    PisuaKg = 79.40m,
                    Altuera = 1.80m,
                    DokumentuKopurua = 0,
                    Oharrak = "Kirolaren ondorengo neurketa",
                    ErregistroData = DateTime.Today.AddDays(-6).AddHours(18)
                },
                new Jarraipena
                {
                    Id = 8,
                    PazienteNan = "77777777G",
                    PazienteIzena = "Nora",
                    PazienteAbizenak = "Agirre",
                    TentsioSistolikoa = 126,
                    TentsioDiastolikoa = 82,
                    PultsuaPpm = 75,
                    PisuaKg = 65.90m,
                    Altuera = 1.66m,
                    DokumentuKopurua = 1,
                    Oharrak = "Arratsaldeko kontrola",
                    ErregistroData = DateTime.Today.AddDays(-7).AddHours(14)
                },
                new Jarraipena
                {
                    Id = 9,
                    PazienteNan = "88888888H",
                    PazienteIzena = "Gorka",
                    PazienteAbizenak = "Ruiz",
                    TentsioSistolikoa = 152,
                    TentsioDiastolikoa = 95,
                    PultsuaPpm = 90,
                    PisuaKg = 88.60m,
                    Altuera = 1.78m,
                    DokumentuKopurua = 2,
                    Oharrak = "Tratamendu doikuntza",
                    ErregistroData = DateTime.Today.AddDays(-8).AddHours(9)
                },
                new Jarraipena
                {
                    Id = 10,
                    PazienteNan = "99999999I",
                    PazienteIzena = "Maialen",
                    PazienteAbizenak = "Diaz",
                    TentsioSistolikoa = 117,
                    TentsioDiastolikoa = 76,
                    PultsuaPpm = 68,
                    PisuaKg = 59.80m,
                    Altuera = 1.60m,
                    DokumentuKopurua = 1,
                    Oharrak = "Balio egokiak",
                    ErregistroData = DateTime.Today.AddDays(-9).AddHours(13)
                },
                new Jarraipena
                {
                    Id = 11,
                    PazienteNan = "12345678J",
                    PazienteIzena = "Oier",
                    PazienteAbizenak = "Mendizabal",
                    TentsioSistolikoa = 134,
                    TentsioDiastolikoa = 85,
                    PultsuaPpm = 73,
                    PisuaKg = 77.70m,
                    Altuera = 1.74m,
                    DokumentuKopurua = 0,
                    Oharrak = "Jarraipen gehigarria",
                    ErregistroData = DateTime.Today.AddDays(-10).AddHours(17)
                },
                new Jarraipena
                {
                    Id = 12,
                    PazienteNan = "87654321K",
                    PazienteIzena = "June",
                    PazienteAbizenak = "Arrieta",
                    TentsioSistolikoa = 129,
                    TentsioDiastolikoa = 83,
                    PultsuaPpm = 71,
                    PisuaKg = 64.40m,
                    Altuera = 1.69m,
                    DokumentuKopurua = 1,
                    Oharrak = "Azken berrikuspena",
                    ErregistroData = DateTime.Today.AddDays(-11).AddHours(12)
                }
            });

            BistaratuJarraipenak();
            _dgvJarraipenak.Invalidate();
            _dgvJarraipenak.Refresh();
        }

        private void EguneratuPantailaDiseinua()
        {
            if (ClientSize.Width < JarraipenPantailaTamaina.Width || ClientSize.Height < JarraipenPantailaTamaina.Height)
            {
                ClientSize = new Size(
                    Math.Max(ClientSize.Width, JarraipenPantailaTamaina.Width),
                    Math.Max(ClientSize.Height, JarraipenPantailaTamaina.Height));
            }

                    _goiburuBarra.Height = GoiburuAltuera;
            _goiburuBarra.Width = ClientSize.Width;
                    _atzeraBotoia.Location = new Point(40, 93);
                    _atzeraBotoia.Size = new Size(250, 59);
            _edukiPanela.Location = new Point(0, _goiburuBarra.Bottom);
            _edukiPanela.Size = new Size(ClientSize.Width, ClientSize.Height - _goiburuBarra.Height);

                    _lblIzenburua.Location = new Point(KanpokoMarjina, 35);
            _btnJarraipenBerria.Size = new Size(320, 64);
                    _btnJarraipenBerria.Location = new Point(_edukiPanela.ClientSize.Width - _btnJarraipenBerria.Width - KanpokoMarjina, 32);

                        _lblBilatu.Location = new Point(KanpokoMarjina, 115);
                        _txtBilatu.Location = new Point(KanpokoMarjina, 160);
            _txtBilatu.Size = new Size(Math.Max(900, _edukiPanela.ClientSize.Width - (KanpokoMarjina * 2)), 52);

                        _lblDataFiltroa.Location = new Point(KanpokoMarjina, 225);
                        _dtpHasieraData.Location = new Point(KanpokoMarjina, 274);
                        _dtpHasieraData.Size = new Size(DataFiltroZabalera, 47);
                        _dtpAmaieraData.Location = new Point(_dtpHasieraData.Right + 26, 274);
                        _dtpAmaieraData.Size = new Size(DataFiltroZabalera, 47);
                        _btnFiltroakGarbitu.Location = new Point(_dtpAmaieraData.Right + 30, 274);
                        _btnFiltroakGarbitu.Size = new Size(FiltroakGarbituZabalera, 47);
                        _chkJarraipenGuztiakIkusi.Location = new Point(KanpokoMarjina, 346);

            _dgvJarraipenak.Location = new Point(KanpokoMarjina, TaulaGoikoPosizioa);
            _dgvJarraipenak.Size = new Size(_edukiPanela.ClientSize.Width - (KanpokoMarjina * 2), _edukiPanela.ClientSize.Height - TaulaGoikoPosizioa - 40);
            _dgvJarraipenak.RowTemplate.Height = JarraipenFilaAltuera;

            DataGridViewColumn? ekintzakZutabea = BilatuZutabea("Ekintzak");
            if (ekintzakZutabea != null)
            {
                ekintzakZutabea.Width = EkintzaZutabeZabalera;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            EguneratuPantailaDiseinua();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (IsHandleCreated)
            {
                EguneratuPantailaDiseinua();
            }
        }

        private void DgvJarraipenak_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || EkintzakZutabeaDa(e.ColumnIndex) || OharrakZutabeaDa(e.ColumnIndex)) return;

            string dataPropertyName = _dgvJarraipenak.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrWhiteSpace(dataPropertyName)) return;

            if (_azkenOrdenazioZutabea == dataPropertyName)
            {
                _ordenazioGorakorra = !_ordenazioGorakorra;
            }
            else
            {
                _azkenOrdenazioZutabea = dataPropertyName;
                _ordenazioGorakorra = true;
            }

            var pi = typeof(Jarraipena).GetProperty(dataPropertyName);
            if (pi == null) return;

            List<Jarraipena> ordenatua = _ordenazioGorakorra
                ? _jarraipenak.OrderBy(x => pi.GetValue(x, null)).ToList()
                : _jarraipenak.OrderByDescending(x => pi.GetValue(x, null)).ToList();

            _jarraipenak.Clear();
            _jarraipenak.AddRange(ordenatua);

            _dgvJarraipenak.DataSource = null;
            BistaratuJarraipenak();

            foreach (DataGridViewColumn col in _dgvJarraipenak.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            _dgvJarraipenak.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = _ordenazioGorakorra ? SortOrder.Ascending : SortOrder.Descending;
        }

        private void DgvJarraipenak_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = ZutabeGakoa(_dgvJarraipenak.Columns[e.ColumnIndex]);
            bool arriskutsua = columnName switch
            {
                "TentsioSistolikoa" => BalioOsasungaitza(e.Value, 90, 140),
                "TentsioDiastolikoa" => BalioOsasungaitza(e.Value, 60, 90),
                "PultsuaPpm" => BalioOsasungaitza(e.Value, 50, 100),
                "PisuaKg" => BalioOsasungaitza(e.Value, 40m, 150m),
                "Altuera" => BalioOsasungaitza(e.Value, 1.40m, 2.10m),
                _ => false
            };

            if (arriskutsua)
            {
                e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                e.CellStyle.Font = new Font(_dgvJarraipenak.Font, FontStyle.Bold);
            }
        }

        private static bool BalioOsasungaitza(object? value, int minimoNormala, int maximoNormala)
        {
            if (value == null || value == DBNull.Value) return false;
            if (!int.TryParse(value.ToString(), out int zenbakia)) return false;
            return zenbakia < minimoNormala || zenbakia >= maximoNormala;
        }

        private static bool BalioOsasungaitza(object? value, decimal minimoNormala, decimal maximoNormala)
        {
            if (value == null || value == DBNull.Value) return false;
            if (!decimal.TryParse(value.ToString(), out decimal zenbakia)) return false;
            return zenbakia < minimoNormala || zenbakia > maximoNormala;
        }

        private void DgvJarraipenak_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.Graphics == null) return;

            if (OharrakZutabeaDa(e.ColumnIndex))
            {
                MarraztuOharGelaxka(e);
                return;
            }

            if (!EkintzakZutabeaDa(e.ColumnIndex)) return;

            e.PaintBackground(e.CellBounds, true);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);

            foreach (var botoia in LortuEkintzaBotoiak(e.CellBounds))
            {
                MarraztuEkintzaBotoia(e.Graphics, botoia.Key, botoia.Value);
            }

            e.Handled = true;
        }

        private void DgvJarraipenak_CellMouseMove(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (DiseinuModuan()) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                _dgvJarraipenak.Cursor = Cursors.Default;
                return;
            }

            if (OharrakZutabeaDa(e.ColumnIndex))
            {
                Rectangle oharCellBounds = _dgvJarraipenak.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                Point oharPosizioa = new Point(oharCellBounds.Left + e.X, oharCellBounds.Top + e.Y);
                _dgvJarraipenak.Cursor = LortuOharBotoia(oharCellBounds).Contains(oharPosizioa) ? Cursors.Hand : Cursors.Default;
                return;
            }

            if (!EkintzakZutabeaDa(e.ColumnIndex))
            {
                _dgvJarraipenak.Cursor = Cursors.Default;
                return;
            }

            Rectangle cellBounds = _dgvJarraipenak.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(cellBounds.Left + e.X, cellBounds.Top + e.Y);
            bool dago = LortuEkintzaBotoiak(cellBounds).Values.Any(r => r.Contains(posizioa));
            _dgvJarraipenak.Cursor = dago ? Cursors.Hand : Cursors.Default;
        }

        private void DgvJarraipenak_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (DiseinuModuan()) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (!(_dgvJarraipenak.Rows[e.RowIndex].DataBoundItem is Jarraipena jarraipena)) return;

            Rectangle cellBounds = _dgvJarraipenak.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(cellBounds.Left + e.X, cellBounds.Top + e.Y);

            if (OharrakZutabeaDa(e.ColumnIndex))
            {
                if (LortuOharBotoia(cellBounds).Contains(posizioa))
                {
                    IkusiOharra(jarraipena);
                }
                return;
            }

            if (!EkintzakZutabeaDa(e.ColumnIndex)) return;

            foreach (var botoia in LortuEkintzaBotoiak(cellBounds))
            {
                if (!botoia.Value.Contains(posizioa)) continue;

                switch (botoia.Key)
                {
                    case EkintzaMota.Ikusi:
                        IkusiJarraipena(jarraipena);
                        break;
                    case EkintzaMota.Editatu:
                        EditatuJarraipena(jarraipena);
                        break;
                    case EkintzaMota.GehituDokumentua:
                        GehituDokumentua(jarraipena);
                        break;
                    case EkintzaMota.IkusiDokumentuak:
                        IkusiDokumentuak(jarraipena);
                        break;
                    case EkintzaMota.Ezabatu:
                        EzabatuJarraipena(jarraipena);
                        break;
                }

                break;
            }
        }

        private static Dictionary<EkintzaMota, Rectangle> LortuEkintzaBotoiak(Rectangle cellBounds)
        {
            int paddingX = 14;
            int spacing = 10;
            int buttonSize = 42;
            int totalWidth = (buttonSize * 5) + (spacing * 4);
            int left = cellBounds.Left + Math.Max(paddingX, (cellBounds.Width - totalWidth) / 2);
            int top = cellBounds.Top + Math.Max(8, (cellBounds.Height - buttonSize) / 2);

            return new Dictionary<EkintzaMota, Rectangle>
            {
                [EkintzaMota.Ikusi] = new Rectangle(left, top, buttonSize, buttonSize),
                [EkintzaMota.Editatu] = new Rectangle(left + buttonSize + spacing, top, buttonSize, buttonSize),
                [EkintzaMota.GehituDokumentua] = new Rectangle(left + ((buttonSize + spacing) * 2), top, buttonSize, buttonSize),
                [EkintzaMota.IkusiDokumentuak] = new Rectangle(left + ((buttonSize + spacing) * 3), top, buttonSize, buttonSize),
                [EkintzaMota.Ezabatu] = new Rectangle(left + ((buttonSize + spacing) * 4), top, buttonSize, buttonSize)
            };
        }

        private static Rectangle LortuOharBotoia(Rectangle cellBounds)
        {
            const int buttonSize = 40;
            int left = cellBounds.Right - buttonSize - 12;
            int top = cellBounds.Top + Math.Max(8, (cellBounds.Height - buttonSize) / 2);
            return new Rectangle(left, top, buttonSize, buttonSize);
        }

        private void MarraztuOharGelaxka(DataGridViewCellPaintingEventArgs e)
        {
            Graphics graphics = e.Graphics!;
            DataGridViewCellStyle estiloa = e.CellStyle ?? new DataGridViewCellStyle();
            Font testuFont = estiloa.Font ?? _dgvJarraipenak.DefaultCellStyle.Font ?? _dgvJarraipenak.Font;

            e.PaintBackground(e.CellBounds, true);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);

            string testua = (_dgvJarraipenak.Rows[e.RowIndex].DataBoundItem as Jarraipena)?.Oharrak?.Trim() ?? string.Empty;
            string aurrebista = string.IsNullOrWhiteSpace(testua) ? "Oharrik ez" : testua;
            Rectangle botoia = LortuOharBotoia(e.CellBounds);
            Rectangle testuArea = new Rectangle(
                e.CellBounds.Left + 12,
                e.CellBounds.Top + 8,
                Math.Max(40, botoia.Left - e.CellBounds.Left - 22),
                e.CellBounds.Height - 16);

            Color testuKolorea = estiloa.ForeColor.IsEmpty ? Color.FromArgb(44, 62, 80) : estiloa.ForeColor;
            TextRenderer.DrawText(
                graphics,
                aurrebista,
                testuFont,
                testuArea,
                testuKolorea,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);

            MarraztuOharBotoia(graphics, botoia);
            e.Handled = true;
        }

        private void MarraztuOharBotoia(Graphics graphics, Rectangle rectangle)
        {
            using (GraphicsPath path = SortuBiribildua(rectangle, 12))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(52, 152, 219)))
            using (Pen pen = new Pen(Color.White, 1))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
                graphics.DrawPath(pen, path);

                if (_oharraIkonoa != null)
                {
                    int iconLeft = rectangle.Left + ((rectangle.Width - _oharraIkonoa.Width) / 2);
                    int iconTop = rectangle.Top + ((rectangle.Height - _oharraIkonoa.Height) / 2);
                    graphics.DrawImage(_oharraIkonoa, new Rectangle(iconLeft, iconTop, _oharraIkonoa.Width, _oharraIkonoa.Height));
                }
                else
                {
                    TextRenderer.DrawText(graphics, "?", new Font("Segoe UI", 10F, FontStyle.Bold), rectangle, Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                }
            }
        }

        private void MarraztuEkintzaBotoia(Graphics graphics, EkintzaMota ekintza, Rectangle rectangle)
        {
            Color kolorea = ekintza switch
            {
                EkintzaMota.Ikusi => Color.FromArgb(41, 128, 185),
                EkintzaMota.Editatu => Color.FromArgb(243, 156, 18),
                EkintzaMota.GehituDokumentua => Color.FromArgb(39, 174, 96),
                EkintzaMota.IkusiDokumentuak => Color.FromArgb(142, 68, 173),
                EkintzaMota.Ezabatu => Color.FromArgb(192, 57, 43),
                _ => Color.FromArgb(44, 62, 80)
            };
            string fallbackIkurra = ekintza switch
            {
                EkintzaMota.Ikusi => "I",
                EkintzaMota.Editatu => "E",
                EkintzaMota.GehituDokumentua => "+",
                EkintzaMota.IkusiDokumentuak => "D",
                EkintzaMota.Ezabatu => "X",
                _ => string.Empty
            };

            using (GraphicsPath path = SortuBiribildua(rectangle, 12))
            using (SolidBrush brush = new SolidBrush(kolorea))
            using (Pen pen = new Pen(Color.White, 1))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
                graphics.DrawPath(pen, path);

                if (_ekintzaIkonoak.TryGetValue(ekintza, out Bitmap? ikonoa) && ikonoa != null)
                {
                    int iconLeft = rectangle.Left + ((rectangle.Width - ikonoa.Width) / 2);
                    int iconTop = rectangle.Top + ((rectangle.Height - ikonoa.Height) / 2);
                    graphics.DrawImage(ikonoa, new Rectangle(iconLeft, iconTop, ikonoa.Width, ikonoa.Height));
                }
                else
                {
                    TextRenderer.DrawText(graphics, fallbackIkurra, new Font("Segoe UI", 10F, FontStyle.Bold), rectangle, Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                }
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

        private void IkusiJarraipena(Jarraipena jarraipena)
        {
            Jarraipena? xehetasuna = _jarraipenaKontrolatzailea.LortuJarraipena(jarraipena.Id);
            if (xehetasuna == null)
            {
                MessageBox.Show("Jarraipena ez da aurkitu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (Form form = new Form())
            using (TableLayoutPanel taula = new TableLayoutPanel())
            {
                form.Text = "Jarraipen xehetasunak";
                form.Size = new Size(860, 760);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                taula.Dock = DockStyle.Fill;
                taula.Padding = new Padding(28);
                taula.ColumnCount = 2;
                taula.RowCount = 11;
                taula.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
                taula.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68));

                GehituXehetasunLerroa(taula, 0, "Pazientea", jarraipena.PazienteIzenOsoa);
                GehituXehetasunLerroa(taula, 1, "NAN/DNI", jarraipena.PazienteNan);
                GehituXehetasunLerroa(taula, 2, "Erregistro data", xehetasuna.ErregistroData.ToString("g"));
                GehituXehetasunLerroa(taula, 3, "Tentsio sistolikoa", BalioaTestuan(xehetasuna.TentsioSistolikoa));
                GehituXehetasunLerroa(taula, 4, "Tentsio diastolikoa", BalioaTestuan(xehetasuna.TentsioDiastolikoa));
                GehituXehetasunLerroa(taula, 5, "Pultsua", BalioaTestuan(xehetasuna.PultsuaPpm));
                GehituXehetasunLerroa(taula, 6, "Pisua", BalioaTestuan(xehetasuna.PisuaKg, "N2", " kg"));
                GehituXehetasunLerroa(taula, 7, "Altuera", BalioaTestuan(xehetasuna.Altuera, "N2", " m"));
                GehituXehetasunLerroa(taula, 8, "XML bidea", xehetasuna.BideaZerbitzarian ?? "-" );
                GehituXehetasunLerroa(taula, 9, "Dokumentuak", jarraipena.DokumentuKopurua.ToString());

                Label lblOharrak = new Label
                {
                    Text = "Oharrak",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft
                };
                TextBox txtOharrak = new TextBox
                {
                    Text = xehetasuna.Oharrak ?? string.Empty,
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    Multiline = true,
                    Height = 100,
                    ScrollBars = ScrollBars.Vertical
                };
                taula.Controls.Add(lblOharrak, 0, 10);
                taula.Controls.Add(txtOharrak, 1, 10);

                Button btnItxi = new Button
                {
                    Text = "Itxi",
                    Dock = DockStyle.Bottom,
                    Height = 48,
                    BackColor = Color.FromArgb(44, 62, 80),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnItxi.FlatAppearance.BorderSize = 0;
                btnItxi.Click += (s, e) => form.Close();

                form.Controls.Add(btnItxi);
                form.Controls.Add(taula);
                form.ShowDialog(this);
            }
        }

        private void IkusiOharra(Jarraipena jarraipena)
        {
            Jarraipena? xehetasuna = _jarraipenaKontrolatzailea.LortuJarraipena(jarraipena.Id);
            if (xehetasuna == null)
            {
                MessageBox.Show("Jarraipena ez da aurkitu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string edukia = xehetasuna.Oharrak ?? string.Empty;
            bool aldaketakEginDira = false;

            using Form form = new Form();
            form.Text = $"Oharrak - {jarraipena.PazienteIzenOsoa}";
            form.ClientSize = new Size(900, 600);
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            Panel edukiaPanela = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(14, 14, 14, 14)
            };

            Panel botoiPanela = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 92,
                Padding = new Padding(14, 10, 14, 18)
            };

            FlowLayoutPanel botoiEdukiontzia = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            TextBox txtOharrak = new TextBox
            {
                Location = new Point(14, 14),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Size = new Size(852, 466),
                ReadOnly = false,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 11F),
                Text = edukia
            };

            Button btnUtzi = new Button
            {
                Text = "Utzi",
                Size = new Size(120, 46),
                Margin = new Padding(0, 0, 14, 0),
                DialogResult = DialogResult.Cancel
            };

            Button btnGorde = new Button
            {
                Text = "Gorde aldaketak",
                Size = new Size(200, 46),
                Margin = new Padding(0),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGorde.FlatAppearance.BorderSize = 0;
            btnGorde.Click += (s, e) =>
            {
                xehetasuna.Oharrak = string.IsNullOrWhiteSpace(txtOharrak.Text) ? null : txtOharrak.Text.Trim();

                if (_jarraipenaKontrolatzailea.EguneratuJarraipena(xehetasuna))
                {
                    aldaketakEginDira = true;
                    form.DialogResult = DialogResult.OK;
                    form.Close();
                }
                else
                {
                    MessageBox.Show(form, "Ezin izan dira oharrak eguneratu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            edukiaPanela.Controls.Add(txtOharrak);
            botoiEdukiontzia.Controls.Add(btnUtzi);
            botoiEdukiontzia.Controls.Add(btnGorde);
            botoiPanela.Controls.Add(botoiEdukiontzia);
            form.Controls.Add(edukiaPanela);
            form.Controls.Add(botoiPanela);
            form.AcceptButton = btnGorde;
            form.CancelButton = btnUtzi;
            form.ShowDialog(this);

            if (aldaketakEginDira)
            {
                KargatuIragazkiekin();
            }
        }

        private void EditatuJarraipena(Jarraipena jarraipena)
        {
            Jarraipena? xehetasuna = _jarraipenaKontrolatzailea.LortuJarraipena(jarraipena.Id);
            if (xehetasuna == null)
            {
                MessageBox.Show("Jarraipena ez da aurkitu editatzeko.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool aldaketakEginDira = false;
            List<Dokumentua> dokumentuak = _jarraipenaKontrolatzailea.LortuJarraipenarenDokumentuak(jarraipena.Id);

            using Form form = new Form();
            form.Text = "Jarraipena editatu";
            form.Size = new Size(1320, 1000);
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            Panel edukia = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(24)
            };

            Label lblGoiburua = new Label
            {
                Text = $"{jarraipena.PazienteIzenOsoa} - {xehetasuna.ErregistroData:g}",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                AutoSize = false,
                Location = new Point(24, 18),
                Size = new Size(1220, 40),
                AutoEllipsis = true
            };

            TableLayoutPanel taula = new TableLayoutPanel
            {
                Location = new Point(24, 76),
                Size = new Size(1080, 190),
                ColumnCount = 4,
                RowCount = 3
            };
            taula.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            taula.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350));
            taula.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            taula.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 350));
            taula.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            taula.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            taula.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));

            TextBox txtSistolikoa = SortuZenbakiTestua(xehetasuna.TentsioSistolikoa);
            TextBox txtDiastolikoa = SortuZenbakiTestua(xehetasuna.TentsioDiastolikoa);
            TextBox txtPultsua = SortuZenbakiTestua(xehetasuna.PultsuaPpm);
            TextBox txtPisua = SortuZenbakiTestua(xehetasuna.PisuaKg, "N2");
            TextBox txtAltuera = SortuZenbakiTestua(xehetasuna.Altuera, "N2");

            GehituEditatzekoEremua(taula, 0, 0, "Sistolikoa", txtSistolikoa);
            GehituEditatzekoEremua(taula, 0, 2, "Diastolikoa", txtDiastolikoa);
            GehituEditatzekoEremua(taula, 1, 0, "Pultsua", txtPultsua);
            GehituEditatzekoEremua(taula, 1, 2, "Pisua (kg)", txtPisua);
            GehituEditatzekoEremua(taula, 2, 0, "Altuera (m)", txtAltuera);

            Label lblOharrak = new Label
            {
                Text = "Oharrak",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(24, 286),
                Size = new Size(140, 30)
            };

            TextBox txtOharrak = new TextBox
            {
                Text = xehetasuna.Oharrak ?? string.Empty,
                Location = new Point(24, 320),
                Size = new Size(1080, 150),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Segoe UI", 10.5F)
            };

            Label lblDokumentuak = new Label
            {
                Text = "Esleitutako dokumentuak",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(24, 490),
                Size = new Size(240, 30)
            };

            DataGridView dgvDokumentuak = new DataGridView
            {
                Location = new Point(24, 528),
                Size = new Size(1080, 330),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            dgvDokumentuak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Dokumentua.DokumentuIzena), HeaderText = "Dokumentua", Width = 260 });
            dgvDokumentuak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Dokumentua.FitxategiIzena), HeaderText = "Fitxategia", Width = 260 });
            dgvDokumentuak.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Dokumentua.IgotzeData), HeaderText = "Igotze data", Width = 160, DefaultCellStyle = new DataGridViewCellStyle { Format = "g" } });
            dgvDokumentuak.Columns.Add(new DataGridViewButtonColumn { HeaderText = "", Name = "btnIreki", Text = "Ireki", UseColumnTextForButtonValue = true, Width = 90 });
            dgvDokumentuak.Columns.Add(new DataGridViewButtonColumn { HeaderText = "", Name = "btnEzabatu", Text = "Ezabatu", UseColumnTextForButtonValue = true, Width = 90 });

            void BerrikargatuDokumentuak()
            {
                dokumentuak = _jarraipenaKontrolatzailea.LortuJarraipenarenDokumentuak(jarraipena.Id);
                dgvDokumentuak.DataSource = null;
                dgvDokumentuak.DataSource = dokumentuak.ToList();
            }

            dgvDokumentuak.CellContentClick += (s, e) =>
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgvDokumentuak.Rows[e.RowIndex].DataBoundItem is not Dokumentua dokumentua) return;

                string zutabea = dgvDokumentuak.Columns[e.ColumnIndex].Name;
                if (zutabea == "btnIreki")
                {
                    IrekiDokumentua(dokumentua);
                    return;
                }

                if (zutabea != "btnEzabatu") return;

                DialogResult baieztapena = MessageBox.Show(
                    $"Ziur zaude '{dokumentua.DokumentuIzena ?? dokumentua.FitxategiIzena}' dokumentua ezabatu nahi duzula?",
                    "Dokumentua ezabatu",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (baieztapena != DialogResult.Yes) return;

                try
                {
                    if (File.Exists(dokumentua.BideaZerbitzarian))
                    {
                        File.Delete(dokumentua.BideaZerbitzarian);
                    }

                    if (_dokumentuaKontrolatzailea.EzabatuDokumentua(dokumentua.Id))
                    {
                        aldaketakEginDira = true;
                        BerrikargatuDokumentuak();
                    }
                    else
                    {
                        MessageBox.Show(form, "Ezin izan da dokumentua ezabatu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(form, "Errorea dokumentua ezabatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            BerrikargatuDokumentuak();

            Button btnUtzi = new Button
            {
                Text = "Utzi",
                Size = new Size(120, 46),
                Location = new Point(874, 884),
                DialogResult = DialogResult.Cancel
            };

            Button btnGorde = new Button
            {
                Text = "Gorde aldaketak",
                Size = new Size(230, 46),
                Location = new Point(1014, 884),
                BackColor = Color.FromArgb(41, 128, 185),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGorde.FlatAppearance.BorderSize = 0;
            btnGorde.Click += (s, e) =>
            {
                if (!SaiatuLortuIntBalioa(txtSistolikoa.Text, "Tentsio sistolikoa", form, out int? tentsioSistolikoa)
                    || !SaiatuLortuIntBalioa(txtDiastolikoa.Text, "Tentsio diastolikoa", form, out int? tentsioDiastolikoa)
                    || !SaiatuLortuIntBalioa(txtPultsua.Text, "Pultsua", form, out int? pultsua)
                    || !SaiatuLortuDecimalBalioa(txtPisua.Text, "Pisua", form, out decimal? pisua)
                    || !SaiatuLortuDecimalBalioa(txtAltuera.Text, "Altuera", form, out decimal? altuera))
                {
                    return;
                }

                xehetasuna.TentsioSistolikoa = tentsioSistolikoa;
                xehetasuna.TentsioDiastolikoa = tentsioDiastolikoa;
                xehetasuna.PultsuaPpm = pultsua;
                xehetasuna.PisuaKg = pisua;
                xehetasuna.Altuera = altuera;
                xehetasuna.Oharrak = string.IsNullOrWhiteSpace(txtOharrak.Text) ? null : txtOharrak.Text.Trim();

                if (_jarraipenaKontrolatzailea.EguneratuJarraipena(xehetasuna))
                {
                    aldaketakEginDira = true;
                    form.DialogResult = DialogResult.OK;
                    form.Close();
                }
                else
                {
                    MessageBox.Show(form, "Ezin izan dira jarraipenaren datuak eguneratu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            edukia.Controls.Add(lblGoiburua);
            edukia.Controls.Add(taula);
            edukia.Controls.Add(lblOharrak);
            edukia.Controls.Add(txtOharrak);
            edukia.Controls.Add(lblDokumentuak);
            edukia.Controls.Add(dgvDokumentuak);
            edukia.Controls.Add(btnUtzi);
            edukia.Controls.Add(btnGorde);
            form.Controls.Add(edukia);
            form.AcceptButton = btnGorde;
            form.CancelButton = btnUtzi;
            form.ShowDialog(this);

            if (aldaketakEginDira)
            {
                KargatuIragazkiekin();
            }
        }

        private static TextBox SortuZenbakiTestua<T>(T? balioa, string? formatua = null) where T : struct, IFormattable
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10.5F),
                Text = balioa.HasValue ? balioa.Value.ToString(formatua, null) : string.Empty
            };
        }

        private static void GehituEditatzekoEremua(TableLayoutPanel taula, int row, int column, string etiketa, Control kontrola)
        {
            Label lbl = new Label
            {
                Text = etiketa,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80)
            };

            taula.Controls.Add(lbl, column, row);
            taula.Controls.Add(kontrola, column + 1, row);
        }

        private static bool SaiatuLortuIntBalioa(string testua, string etiketa, IWin32Window jabea, out int? balioa)
        {
            balioa = null;
            if (string.IsNullOrWhiteSpace(testua)) return true;

            if (int.TryParse(testua.Trim(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int zenbakia)
                || int.TryParse(testua.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out zenbakia))
            {
                balioa = zenbakia;
                return true;
            }

            MessageBox.Show(jabea, $"'{etiketa}' eremuan zenbaki oso baliozkoa sartu behar da.", "Balio baliogabea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private static bool SaiatuLortuDecimalBalioa(string testua, string etiketa, IWin32Window jabea, out decimal? balioa)
        {
            balioa = null;
            if (string.IsNullOrWhiteSpace(testua)) return true;

            if (decimal.TryParse(testua.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out decimal zenbakia)
                || decimal.TryParse(testua.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out zenbakia))
            {
                balioa = zenbakia;
                return true;
            }

            MessageBox.Show(jabea, $"'{etiketa}' eremuan zenbaki hamartar baliozkoa sartu behar da.", "Balio baliogabea", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        private static void GehituXehetasunLerroa(TableLayoutPanel taula, int row, string etiketa, string balioa)
        {
            taula.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            taula.Controls.Add(new Label
            {
                Text = etiketa,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            }, 0, row);
            taula.Controls.Add(new Label
            {
                Text = balioa,
                Font = new Font("Segoe UI", 10F),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            }, 1, row);
        }

        private static string BalioaTestuan<T>(T? balioa, string? formatua = null, string? atzizkia = null) where T : struct, IFormattable
        {
            if (!balioa.HasValue) return "-";
            string testua = balioa.Value.ToString(formatua, null);
            return atzizkia == null ? testua : testua + atzizkia;
        }

        private void GehituDokumentua(Jarraipena jarraipena)
        {
            List<Dokumentua> dokumentuak = _dokumentuaKontrolatzailea.LortuPazientearenBesteDokumentuak(jarraipena.PazienteId, jarraipena.Id);
            if (dokumentuak.Count == 0)
            {
                MessageBox.Show("Paziente honek ez du beste dokumentu erregistraturik aukeratzeko.", "Informazioa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Dokumentua? dokumentua = HautatuDokumentua(jarraipena, dokumentuak);
            if (dokumentua == null)
            {
                return;
            }

            try
            {
                if (_dokumentuaKontrolatzailea.BerrlotuDokumentuaJarraipenera(dokumentua.Id, jarraipena.Id))
                {
                    MessageBox.Show("Dokumentua ondo esleitu da jarraipen honi.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KargatuIragazkiekin();
                }
                else
                {
                    MessageBox.Show("Ezin izan da dokumentua jarraipenari esleitu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea dokumentua esleitzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dokumentua? HautatuDokumentua(Jarraipena jarraipena, List<Dokumentua> dokumentuak)
        {
            using Form form = new Form();
            form.Text = "Dokumentua hautatu";
            form.Size = new Size(1180, 720);
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;

            Label azalpena = new Label
            {
                Dock = DockStyle.Top,
                Height = 78,
                Padding = new Padding(18, 14, 18, 8),
                Font = new Font("Segoe UI", 10F),
                Text = $"Aukeratu {jarraipena.PazienteIzenOsoa} pazienteari lotuta dagoen dokumentu bat jarraipen honetara ekartzeko."
            };

            DataGridView dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Dokumentua.DokumentuIzena), HeaderText = "Dokumentua", Width = 260 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Dokumentua.FitxategiIzena), HeaderText = "Fitxategia", Width = 300 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Dokumentua.Deskribapena), HeaderText = "Deskribapena", Width = 320 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(Dokumentua.IgotzeData),
                HeaderText = "Igotze data",
                Width = 180,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" }
            });
            dgv.DataSource = dokumentuak.ToList();

            Panel botoiPanela = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 82,
                Padding = new Padding(18, 10, 18, 14)
            };

            Button btnIreki = new Button
            {
                Text = "Ireki",
                Size = new Size(90, 40),
                Location = new Point(18, 12)
            };
            btnIreki.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is Dokumentua hautatutakoDokumentua)
                {
                    IrekiDokumentua(hautatutakoDokumentua);
                }
            };

            Dokumentua? dokumentuHautatua = null;

            Button btnLotu = new Button
            {
                Text = "Dokumentua lotu",
                Size = new Size(150, 40),
                Location = new Point(890, 18),
                BackColor = Color.FromArgb(192, 57, 43),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLotu.FlatAppearance.BorderSize = 0;
            btnLotu.Click += (s, e) =>
            {
                if (dgv.CurrentRow?.DataBoundItem is not Dokumentua hautatutakoDokumentua)
                {
                    MessageBox.Show(form, "Dokumentu bat hautatu behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dokumentuHautatua = hautatutakoDokumentua;
                form.DialogResult = DialogResult.OK;
                form.Close();
            };

            Button btnUtzi = new Button
            {
                Text = "Utzi",
                Size = new Size(90, 40),
                Location = new Point(1050, 18),
                DialogResult = DialogResult.Cancel
            };

            dgv.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex < 0 || dgv.Rows[e.RowIndex].DataBoundItem is not Dokumentua hautatutakoDokumentua)
                {
                    return;
                }

                dokumentuHautatua = hautatutakoDokumentua;
                form.DialogResult = DialogResult.OK;
                form.Close();
            };

            botoiPanela.Controls.Add(btnIreki);
            botoiPanela.Controls.Add(btnLotu);
            botoiPanela.Controls.Add(btnUtzi);

            form.Controls.Add(dgv);
            form.Controls.Add(botoiPanela);
            form.Controls.Add(azalpena);
            form.AcceptButton = btnLotu;
            form.CancelButton = btnUtzi;

            return form.ShowDialog(this) == DialogResult.OK ? dokumentuHautatua : null;
        }

        private bool EskatuDokumentuDatuak(string hasierakoIzena, out string dokumentuIzena, out string deskribapena)
        {
            dokumentuIzena = hasierakoIzena;
            deskribapena = string.Empty;

            using (Form form = new Form())
            {
                form.Text = "Dokumentuaren datuak";
                form.Size = new Size(520, 320);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                Label lblIzena = new Label { Text = "Dokumentu izena", Location = new Point(20, 24), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
                TextBox txtIzena = new TextBox { Location = new Point(20, 55), Width = 460, Text = hasierakoIzena, Font = new Font("Segoe UI", 10F) };
                Label lblDeskribapena = new Label { Text = "Deskribapena", Location = new Point(20, 100), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
                TextBox txtDeskribapena = new TextBox { Location = new Point(20, 130), Width = 460, Height = 90, Multiline = true, Font = new Font("Segoe UI", 10F) };
                Button btnGorde = new Button { Text = "Gorde", Location = new Point(290, 235), Size = new Size(90, 40), DialogResult = DialogResult.OK, BackColor = Color.FromArgb(192, 57, 43), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
                Button btnUtzi = new Button { Text = "Utzi", Location = new Point(390, 235), Size = new Size(90, 40), DialogResult = DialogResult.Cancel };
                btnGorde.FlatAppearance.BorderSize = 0;

                form.Controls.Add(lblIzena);
                form.Controls.Add(txtIzena);
                form.Controls.Add(lblDeskribapena);
                form.Controls.Add(txtDeskribapena);
                form.Controls.Add(btnGorde);
                form.Controls.Add(btnUtzi);
                form.AcceptButton = btnGorde;
                form.CancelButton = btnUtzi;

                if (form.ShowDialog(this) != DialogResult.OK) return false;

                dokumentuIzena = string.IsNullOrWhiteSpace(txtIzena.Text) ? hasierakoIzena : txtIzena.Text.Trim();
                deskribapena = txtDeskribapena.Text.Trim();
                return true;
            }
        }

        private void IkusiDokumentuak(Jarraipena jarraipena)
        {
            List<Dokumentua> dokumentuak = _jarraipenaKontrolatzailea.LortuJarraipenarenDokumentuak(jarraipena.Id);
            if (dokumentuak.Count == 0)
            {
                MessageBox.Show("Jarraipen honek ez du oraindik dokumenturik.", "Informazioa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (Form form = new Form())
            {
                form.Text = "Jarraipeneko dokumentuak";
                form.Size = new Size(950, 520);
                form.StartPosition = FormStartPosition.CenterParent;

                DataGridView dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    AutoGenerateColumns = false,
                    RowHeadersVisible = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    BackgroundColor = Color.White
                };

                dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DokumentuIzena", HeaderText = "Dokumentua", Width = 220 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FitxategiIzena", HeaderText = "Fitxategia", Width = 220 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Deskribapena", HeaderText = "Deskribapena", Width = 220 });
                dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "IgotzeData", HeaderText = "Igotze data", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "g" } });
                dgv.Columns.Add(new DataGridViewButtonColumn { HeaderText = "", Name = "btnIreki", Text = "Ireki", UseColumnTextForButtonValue = true, Width = 90 });
                dgv.DataSource = dokumentuak;

                dgv.CellContentClick += (s, e) =>
                {
                    if (e.RowIndex < 0 || dgv.Columns[e.ColumnIndex].Name != "btnIreki") return;
                    if (!(dgv.Rows[e.RowIndex].DataBoundItem is Dokumentua dokumentua)) return;
                    IrekiDokumentua(dokumentua);
                };

                form.Controls.Add(dgv);
                form.ShowDialog(this);
            }
        }

        private void IrekiDokumentua(Dokumentua dokumentua)
        {
            if (!File.Exists(dokumentua.BideaZerbitzarian))
            {
                MessageBox.Show("Dokumentuaren fitxategia ez da aurkitu zerbitzarian.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Process.Start(new ProcessStartInfo(dokumentua.BideaZerbitzarian) { UseShellExecute = true });
        }

        private void EzabatuJarraipena(Jarraipena jarraipena)
        {
            DialogResult emaitza = MessageBox.Show(
                $"Ziur zaude {jarraipena.PazienteIzenOsoa} pazientearen jarraipen hau ezabatu nahi duzula?",
                "Berretsi ezabatzea",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (emaitza != DialogResult.Yes) return;

            try
            {
                foreach (Dokumentua dokumentua in _jarraipenaKontrolatzailea.LortuJarraipenarenDokumentuak(jarraipena.Id))
                {
                    if (File.Exists(dokumentua.BideaZerbitzarian))
                    {
                        File.Delete(dokumentua.BideaZerbitzarian);
                    }
                }

                if (_jarraipenaKontrolatzailea.EzabatuJarraipena(jarraipena.Id))
                {
                    MessageBox.Show("Jarraipena ondo ezabatu da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KargatuIragazkiekin();
                }
                else
                {
                    MessageBox.Show("Ezin izan da jarraipena ezabatu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea jarraipena ezabatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => this.Show();
            this.Hide();
            formularioa.Show();
        }
    }
}
