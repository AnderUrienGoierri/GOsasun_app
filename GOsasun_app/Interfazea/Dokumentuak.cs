using GOsasun_app.Kontrola;
using GOsasun_app.Kontrola.Zerbitzuak;
using GOsasun_app.Modeloa;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GOsasun_app.Interfazea
{
    public partial class Dokumentuak : OinarriPantaila
    {
        private const int DokumentuakPantailaZabalera = 2150;
        private const int GehienezkoErregistroDefektuz = 10;
        private const int EkintzaIkonoTamaina = 20;
        private const int EkintzaBotoiTamaina = 40;
        private const int EkintzaBotoienTartea = 12;

        private readonly DokumentuaKontrolatzailea _dokumentuaKontrolatzailea = new DokumentuaKontrolatzailea();
        private readonly PazienteKontrolatzailea _pazienteKontrolatzailea = new PazienteKontrolatzailea();
        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
        private readonly BindingSource _bindingSource = new BindingSource();
        private readonly List<Dokumentua> _dokumentuak = new List<Dokumentua>();
        private Image? _osasunTxostenaIkonoa;
        private Image? _dokumentuBerriaIkonoa;
        private Image? _ikusiIkonoa;
        private Image? _editatuIkonoa;
        private Image? _ezabatuIkonoa;
        private bool _hasierakoDatuakKargatuta;
        private bool _hasierakoDatuakKargatzen;
        private string _azkenOrdenazioZutabea = "IgotzeData";
        private bool _ordenazioGorakorra;

        private sealed class DokumentuKargaEmaitza
        {
            public required List<Dokumentua> OrdenatutakoDokumentuak { get; init; }
            public required List<Dokumentua> BistaratzekoDokumentuak { get; init; }
            public required string EgoeraTestua { get; init; }
        }

        public Dokumentuak()
            : base()
        {
            InitializeComponent();
            HasieratuPantaila();
        }

        public Dokumentuak(Erabiltzailea erabiltzailea)
            : base(erabiltzailea)
        {
            InitializeComponent();
            HasieratuPantaila();
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_hasierakoDatuakKargatuta || _hasierakoDatuakKargatzen || DiseinuModuan())
            {
                return;
            }

            await KargatuHasierakoDatuakAsync();
        }

        private void HasieratuPantaila()
        {
            Text = "GOsasun - Dokumentuak";
            EzarriFormularioZabalera();

            _azalpenaLabel.Text = DaPazientea()
                ? "Zure dokumentu klinikoak kontsultatu eta ireki ditzakezu hemen."
                : "Dokumentu klinikoak kontsultatu eta ireki ditzakezu hemen.";

            _dokumentuakGrid.Columns.Clear();
            _dokumentuakGrid.AutoGenerateColumns = false;
            _dokumentuakGrid.ColumnHeaderMouseClick += DokumentuakGrid_ColumnHeaderMouseClick;
            _dokumentuakGrid.CellMouseClick += DokumentuakGrid_CellMouseClick;
            _dokumentuakGrid.CellPainting += DokumentuakGrid_CellPainting;
            _dokumentuakGrid.CellMouseMove += DokumentuakGrid_CellMouseMove;
            _bilaketaTextBox.KeyDown += BilaketaTextBox_KeyDown;
            _hasieraDataPicker.ValueChanged += (s, e) => KargatuDokumentuak();
            _amaieraDataPicker.ValueChanged += (s, e) => KargatuDokumentuak();
            _bilatuBotoia.Click += (s, e) => KargatuDokumentuak();
            _garbituBotoia.Click += (s, e) => GarbituIragazkia();
            _osasunTxostenaSortuBotoia.Click += (s, e) => SortuOsasunTxostena();
            _dokumentuBerriaBotoia.Click += (s, e) => SortuDokumentuBerria();
            _jarraipenGuztiakCheckBox.CheckedChanged += (s, e) => KargatuDokumentuak();
            _dokumentuakGrid.DataSource = _bindingSource;

            KargatuEkintzaIkonoak();
            _osasunTxostenaSortuBotoia.Image = _osasunTxostenaIkonoa;
            _dokumentuBerriaBotoia.Image = _dokumentuBerriaIkonoa;
            _osasunTxostenaSortuBotoia.Visible = _erabiltzailea?.DaOsasunLangilea() == true;
            _jarraipenGuztiakCheckBox.Visible = !DaPazientea();
            _jarraipenGuztiakCheckBox.Checked = false;

            KonfiguratuZutabeak();

            _jarraipenGuztiakCheckBox.BringToFront();
            iragazkiPanela.BringToFront();
            _osasunTxostenaSortuBotoia.BringToFront();
            _dokumentuBerriaBotoia.BringToFront();
            _egoeraLabel.Text = "Dokumentuak kargatzen...";
        }

        private async Task KargatuHasierakoDatuakAsync()
        {
            _hasierakoDatuakKargatzen = true;
            EzarriHasierakoKargaEgoera(true);

            try
            {
                await KargatuDokumentuakAsync();
                _hasierakoDatuakKargatuta = true;
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    MessageBox.Show(
                        "Ezin izan da dokumentuen hasierako informazioa kargatu: " + ex.Message,
                        "Errorea",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                _hasierakoDatuakKargatzen = false;
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
            _dokumentuakGrid.Visible = !kargatzen;
            _dokumentuakGrid.Enabled = !kargatzen;
            _bilaketaTextBox.Enabled = !kargatzen;
            _hasieraDataPicker.Enabled = !kargatzen;
            _amaieraDataPicker.Enabled = !kargatzen;
            _bilatuBotoia.Enabled = !kargatzen;
            _garbituBotoia.Enabled = !kargatzen;
            _jarraipenGuztiakCheckBox.Enabled = !kargatzen;
            _osasunTxostenaSortuBotoia.Enabled = !kargatzen;
            _dokumentuBerriaBotoia.Enabled = !kargatzen;

            if (kargatzen)
            {
                _egoeraLabel.Text = "Dokumentuak kargatzen...";
            }
        }

        private void EzarriFormularioZabalera()
        {
            int zabalera = LortuPantailaraEgokitutakoZabalera(DokumentuakPantailaZabalera);
            ClientSize = new Size(zabalera, ClientSize.Height);
            _goiburuBarra.Width = zabalera;
            _edukiPanela.Size = new Size(zabalera, _edukiPanela.Height);
        }

        private void KargatuEkintzaIkonoak()
        {
            _osasunTxostenaIkonoa = KargatuIkonoIrudia("file-text.svg", Color.White, 20);
            _dokumentuBerriaIkonoa = KargatuIkonoIrudia("plus-circle.svg", Color.White, 20);
            _ikusiIkonoa = KargatuIkonoIrudia("eye.svg", Color.White, EkintzaIkonoTamaina);
            _editatuIkonoa = KargatuIkonoIrudia("pencil.svg", Color.White, EkintzaIkonoTamaina);
            _ezabatuIkonoa = KargatuIkonoIrudia("trash-2.svg", Color.White, EkintzaIkonoTamaina);
        }

        private void KonfiguratuZutabeak()
        {
            _dokumentuakGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (!DaPazientea())
            {
                _dokumentuakGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PazienteIzenOsoa",
                    HeaderText = "Pazientea",
                    MinimumWidth = 140,
                    FillWeight = 16,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    SortMode = DataGridViewColumnSortMode.Programmatic,
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
                });

                _dokumentuakGrid.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PazienteNan",
                    HeaderText = "NAN",
                    MinimumWidth = 90,
                    FillWeight = 9,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    SortMode = DataGridViewColumnSortMode.Programmatic,
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
                });
            }

            _dokumentuakGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DokumentuIzena",
                HeaderText = "Dokumentua",
                MinimumWidth = 130,
                FillWeight = 13,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

            _dokumentuakGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FitxategiIzena",
                HeaderText = "Fitxategia",
                MinimumWidth = 220,
                FillWeight = 20,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

            _dokumentuakGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Deskribapena",
                HeaderText = "Deskribapena",
                MinimumWidth = 300,
                FillWeight = 28,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                SortMode = DataGridViewColumnSortMode.Programmatic,
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

            _dokumentuakGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IgotzeData",
                HeaderText = "Igotze data",
                MinimumWidth = 150,
                FillWeight = 15,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "g" },
                SortMode = DataGridViewColumnSortMode.Programmatic,
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

            _dokumentuakGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Ekintzak",
                Name = "Ekintzak",
                MinimumWidth = 170,
                FillWeight = 15,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

            _dokumentuakGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            _dokumentuakGrid.ColumnHeadersHeight = 58;
            _dokumentuakGrid.RowTemplate.Height = 52;
        }

        private void KargatuDokumentuak()
        {
            string? bilaketa = string.IsNullOrWhiteSpace(_bilaketaTextBox.Text) ? null : _bilaketaTextBox.Text.Trim();
            int? pazienteId = DaPazientea() ? _erabiltzailea?.Id : null;
            (DateTime? hasieraData, DateTime? amaieraData) = LortuDataTartea();
            bool dokumentuGuztiakErakutsi = DaPazientea() || _jarraipenGuztiakCheckBox.Checked;

            DokumentuKargaEmaitza emaitza = PrestatuDokumentuKarga(
                bilaketa,
                pazienteId,
                hasieraData,
                amaieraData,
                dokumentuGuztiakErakutsi);

            AplikatuDokumentuKargaEmaitza(emaitza);
        }

        private async Task KargatuDokumentuakAsync()
        {
            string? bilaketa = string.IsNullOrWhiteSpace(_bilaketaTextBox.Text) ? null : _bilaketaTextBox.Text.Trim();
            int? pazienteId = DaPazientea() ? _erabiltzailea?.Id : null;
            (DateTime? hasieraData, DateTime? amaieraData) = LortuDataTartea();
            bool dokumentuGuztiakErakutsi = DaPazientea() || _jarraipenGuztiakCheckBox.Checked;

            DokumentuKargaEmaitza emaitza = await Task.Run(() => PrestatuDokumentuKarga(
                bilaketa,
                pazienteId,
                hasieraData,
                amaieraData,
                dokumentuGuztiakErakutsi));

            if (IsDisposed)
            {
                return;
            }

            AplikatuDokumentuKargaEmaitza(emaitza);
        }

        private DokumentuKargaEmaitza PrestatuDokumentuKarga(
            string? bilaketa,
            int? pazienteId,
            DateTime? hasieraData,
            DateTime? amaieraData,
            bool dokumentuGuztiakErakutsi)
        {
            List<Dokumentua> dokumentuak = _dokumentuaKontrolatzailea
                .LortuDokumentuak(bilaketa, pazienteId: pazienteId)
                .Where(dokumentua => DataTarteanDago(dokumentua.IgotzeData, hasieraData, amaieraData))
                .ToList();

            List<Dokumentua> ordenatutakoDokumentuak = AplikatuOrdenazioa(dokumentuak);
            List<Dokumentua> bistaratzekoDokumentuak = dokumentuGuztiakErakutsi
                ? ordenatutakoDokumentuak
                : ordenatutakoDokumentuak.Take(GehienezkoErregistroDefektuz).ToList();

            int guztira = ordenatutakoDokumentuak.Count;
            string egoeraTestua = guztira == 0
                ? "Ez da dokumenturik aurkitu."
                : guztira == 1
                    ? "Dokumentu 1 aurkitu da."
                    : dokumentuGuztiakErakutsi || guztira <= GehienezkoErregistroDefektuz
                        ? $"{guztira} dokumentu aurkitu dira."
                        : $"{bistaratzekoDokumentuak.Count} dokumentu erakusten dira lehenetsita ({guztira} guztira).";

            return new DokumentuKargaEmaitza
            {
                OrdenatutakoDokumentuak = ordenatutakoDokumentuak,
                BistaratzekoDokumentuak = bistaratzekoDokumentuak,
                EgoeraTestua = egoeraTestua
            };
        }

        private void AplikatuDokumentuKargaEmaitza(DokumentuKargaEmaitza emaitza)
        {
            _dokumentuak.Clear();
            _dokumentuak.AddRange(emaitza.BistaratzekoDokumentuak);
            _bindingSource.DataSource = null;
            _bindingSource.DataSource = _dokumentuak.ToList();
            EzarriOrdenazioIkurra();
            _egoeraLabel.Text = emaitza.EgoeraTestua;
        }

        private void GarbituIragazkia()
        {
            _bilaketaTextBox.Clear();
            _hasieraDataPicker.Checked = false;
            _amaieraDataPicker.Checked = false;
            KargatuDokumentuak();
        }

        private (DateTime? HasieraData, DateTime? AmaieraData) LortuDataTartea()
        {
            DateTime? hasieraData = _hasieraDataPicker.Checked ? _hasieraDataPicker.Value.Date : null;
            DateTime? amaieraData = _amaieraDataPicker.Checked ? _amaieraDataPicker.Value.Date : null;

            if (hasieraData.HasValue && amaieraData.HasValue && hasieraData.Value > amaieraData.Value)
            {
                (hasieraData, amaieraData) = (amaieraData, hasieraData);
            }

            return (hasieraData, amaieraData);
        }

        private static bool DataTarteanDago(DateTime dokumentuData, DateTime? hasieraData, DateTime? amaieraData)
        {
            DateTime data = dokumentuData.Date;

            if (hasieraData.HasValue && data < hasieraData.Value)
            {
                return false;
            }

            if (amaieraData.HasValue && data > amaieraData.Value)
            {
                return false;
            }

            return true;
        }

        private void BilaketaTextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.SuppressKeyPress = true;
            KargatuDokumentuak();
        }

        private void DokumentuakGrid_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex < 0 || EkintzakZutabeaDa(e.ColumnIndex) || _dokumentuak.Count == 0)
            {
                return;
            }

            string dataPropertyName = _dokumentuakGrid.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrWhiteSpace(dataPropertyName))
            {
                return;
            }

            if (_azkenOrdenazioZutabea == dataPropertyName)
            {
                _ordenazioGorakorra = !_ordenazioGorakorra;
            }
            else
            {
                _azkenOrdenazioZutabea = dataPropertyName;
                _ordenazioGorakorra = true;
            }

            List<Dokumentua> ordenatua = AplikatuOrdenazioa(_dokumentuak);
            _dokumentuak.Clear();
            _dokumentuak.AddRange(ordenatua);
            _bindingSource.DataSource = null;
            _bindingSource.DataSource = _dokumentuak.ToList();
            EzarriOrdenazioIkurra();
        }

        private void DokumentuakGrid_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !EkintzakZutabeaDa(e.ColumnIndex))
            {
                return;
            }

            if (_dokumentuakGrid.Rows[e.RowIndex].DataBoundItem is not Dokumentua dokumentua)
            {
                return;
            }

            Rectangle gelaxka = _dokumentuakGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(gelaxka.Left + e.X, gelaxka.Top + e.Y);

            foreach (KeyValuePair<string, Rectangle> botoia in LortuEkintzaBotoiak(gelaxka))
            {
                if (!botoia.Value.Contains(posizioa))
                {
                    continue;
                }

                string zutabeIzena = botoia.Key;

                if (zutabeIzena == "btnIkusi")
                {
                    IrekiDokumentua(dokumentua);
                }
                else if (zutabeIzena == "btnEditatu")
                {
                    EditatuDokumentua(dokumentua);
                }
                else if (zutabeIzena == "btnEzabatu")
                {
                    EzabatuDokumentua(dokumentua);
                }

                break;
            }
        }

        private void DokumentuakGrid_CellMouseMove(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !EkintzakZutabeaDa(e.ColumnIndex))
            {
                _dokumentuakGrid.Cursor = Cursors.Default;
                return;
            }

            Rectangle gelaxka = _dokumentuakGrid.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(gelaxka.Left + e.X, gelaxka.Top + e.Y);
            bool botoiGaindean = LortuEkintzaBotoiak(gelaxka).Values.Any(botoiRect => botoiRect.Contains(posizioa));
            _dokumentuakGrid.Cursor = botoiGaindean ? Cursors.Hand : Cursors.Default;
        }

        private void DokumentuakGrid_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !EkintzakZutabeaDa(e.ColumnIndex))
            {
                return;
            }

            if (e.Graphics == null)
            {
                return;
            }

            e.PaintBackground(e.CellBounds, true);

            foreach (KeyValuePair<string, Rectangle> botoia in LortuEkintzaBotoiak(e.CellBounds))
            {
                Color botoiKolorea = botoia.Key switch
                {
                    "btnIkusi" => Color.FromArgb(52, 152, 219),
                    "btnEditatu" => Color.FromArgb(243, 156, 18),
                    _ => Color.FromArgb(231, 76, 60)
                };

                using (SolidBrush brotxa = new SolidBrush(botoiKolorea))
                {
                    e.Graphics.FillEllipse(brotxa, botoia.Value);
                }

                Image? ikonoa = botoia.Key switch
                {
                    "btnIkusi" => _ikusiIkonoa,
                    "btnEditatu" => _editatuIkonoa,
                    _ => _ezabatuIkonoa
                };

                if (ikonoa != null)
                {
                    Rectangle ikonoRect = new Rectangle(
                        botoia.Value.X + ((botoia.Value.Width - EkintzaIkonoTamaina) / 2),
                        botoia.Value.Y + ((botoia.Value.Height - EkintzaIkonoTamaina) / 2),
                        EkintzaIkonoTamaina,
                        EkintzaIkonoTamaina);
                    e.Graphics.DrawImage(ikonoa, ikonoRect);
                }
            }

            e.Paint(e.CellBounds, DataGridViewPaintParts.Border);
            e.Handled = true;
        }

        private bool EkintzakZutabeaDa(int columnIndex)
        {
            return columnIndex >= 0
                && columnIndex < _dokumentuakGrid.Columns.Count
                && string.Equals(_dokumentuakGrid.Columns[columnIndex].Name, "Ekintzak", StringComparison.OrdinalIgnoreCase);
        }

        private List<Dokumentua> AplikatuOrdenazioa(IEnumerable<Dokumentua> dokumentuak)
        {
            if (string.IsNullOrWhiteSpace(_azkenOrdenazioZutabea))
            {
                return dokumentuak.OrderByDescending(dokumentua => dokumentua.IgotzeData).ToList();
            }

            var propietatea = typeof(Dokumentua).GetProperty(_azkenOrdenazioZutabea);
            if (propietatea == null)
            {
                return dokumentuak.OrderByDescending(dokumentua => dokumentua.IgotzeData).ToList();
            }

            return _ordenazioGorakorra
                ? dokumentuak.OrderBy(dokumentua => propietatea.GetValue(dokumentua, null)).ToList()
                : dokumentuak.OrderByDescending(dokumentua => propietatea.GetValue(dokumentua, null)).ToList();
        }

        private void EzarriOrdenazioIkurra()
        {
            foreach (DataGridViewColumn zutabea in _dokumentuakGrid.Columns)
            {
                zutabea.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            if (string.IsNullOrWhiteSpace(_azkenOrdenazioZutabea))
            {
                return;
            }

            foreach (DataGridViewColumn zutabea in _dokumentuakGrid.Columns)
            {
                if (!string.Equals(zutabea.DataPropertyName, _azkenOrdenazioZutabea, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                zutabea.HeaderCell.SortGlyphDirection = _ordenazioGorakorra ? SortOrder.Ascending : SortOrder.Descending;
                break;
            }
        }

        private Dictionary<string, Rectangle> LortuEkintzaBotoiak(Rectangle gelaxka)
        {
            int zabaleraOsoa = (EkintzaBotoiTamaina * 3) + (EkintzaBotoienTartea * 2);
            int hasieraX = gelaxka.X + Math.Max(8, (gelaxka.Width - zabaleraOsoa) / 2);
            int hasieraY = gelaxka.Y + Math.Max(6, (gelaxka.Height - EkintzaBotoiTamaina) / 2);

            return new Dictionary<string, Rectangle>
            {
                ["btnIkusi"] = new Rectangle(hasieraX, hasieraY, EkintzaBotoiTamaina, EkintzaBotoiTamaina),
                ["btnEditatu"] = new Rectangle(hasieraX + EkintzaBotoiTamaina + EkintzaBotoienTartea, hasieraY, EkintzaBotoiTamaina, EkintzaBotoiTamaina),
                ["btnEzabatu"] = new Rectangle(hasieraX + ((EkintzaBotoiTamaina + EkintzaBotoienTartea) * 2), hasieraY, EkintzaBotoiTamaina, EkintzaBotoiTamaina)
            };
        }

        private void IrekiDokumentua(Dokumentua dokumentua)
        {
            if (!File.Exists(dokumentua.BideaZerbitzarian))
            {
                MessageBox.Show("Dokumentuaren fitxategia ez da aurkitu zerbitzarian.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo(dokumentua.BideaZerbitzarian) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea dokumentua irekitzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SortuDokumentuBerria()
        {
            if (_erabiltzailea == null)
            {
                MessageBox.Show("Erabiltzailearen datuak ez dira eskuragarri.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<Pazientea> pazienteak = LortuEskuragarriPazienteak();
            if (!DaPazientea() && pazienteak.Count == 0)
            {
                MessageBox.Show("Ez dago dokumentua lotzeko pazienterik eskuragarri.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EskatuDokumentuBerriarenDatuak(
                    pazienteak,
                    out int pazienteId,
                    out string dokumentuIzena,
                    out string deskribapena,
                    out string pdfFitxategiBidea))
            {
                return;
            }

            try
            {
                bool ondo = _dokumentuaKontrolatzailea.GehituDokumentuGenerikoa(
                    pdfFitxategiBidea,
                    pazienteId,
                    null,
                    LortuOsasunLangileId(),
                    dokumentuIzena,
                    deskribapena);

                if (ondo)
                {
                    KargatuDokumentuak();
                    MessageBox.Show("Dokumentu berria ondo gorde da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ezin izan da dokumentu berria gorde.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea dokumentu berria sortzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SortuOsasunTxostena()
        {
            if (_erabiltzailea == null)
            {
                MessageBox.Show("Erabiltzailearen datuak ez dira eskuragarri.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_erabiltzailea.DaOsasunLangilea() != true)
            {
                MessageBox.Show("Osasun langile batek bakarrik sor dezake txosten medikoa.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<Pazientea> pazienteak = LortuEskuragarriPazienteak();
            if (pazienteak.Count == 0)
            {
                MessageBox.Show("Ez dago txostena lotzeko pazienterik eskuragarri.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EskatuTxostenBerriarenDatuak(
                    pazienteak,
                    out int pazienteId,
                    out string dokumentuIzena,
                    out string deskribapena,
                    out List<TxostenGrafikaMota> grafikaMotak,
                    out DateTime? grafikaHasieraData,
                    out DateTime? grafikaAmaieraData))
            {
                return;
            }

            try
            {
                bool ondo = _dokumentuaKontrolatzailea.GehituTxostena(
                    pazienteId,
                    null,
                    LortuOsasunLangileId(),
                    dokumentuIzena,
                    deskribapena,
                    grafikaMotak,
                    grafikaHasieraData,
                    grafikaAmaieraData);

                if (ondo)
                {
                    KargatuDokumentuak();
                    MessageBox.Show("Osasun txostena ondo sortu da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ezin izan da osasun txostena sortu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea osasun txostena sortzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Pazientea> LortuEskuragarriPazienteak()
        {
            if (_erabiltzailea == null)
            {
                return new List<Pazientea>();
            }

            if (DaPazientea())
            {
                Pazientea? pazientea = _erabiltzailea as Pazientea ?? _pazienteKontrolatzailea.LortuPazientea(_erabiltzailea.Id);
                return pazientea == null ? new List<Pazientea>() : new List<Pazientea> { pazientea };
            }

            return _pazienteKontrolatzailea.LortuGuztiakPazienteak().OrderBy(p => p.IzenOsoa).ToList();
        }

        private bool EskatuDokumentuBerriarenDatuak(
            List<Pazientea> pazienteak,
            out int pazienteId,
            out string dokumentuIzena,
            out string deskribapena,
            out string pdfFitxategiBidea)
        {
            pazienteId = 0;
            dokumentuIzena = string.Empty;
            deskribapena = string.Empty;
            pdfFitxategiBidea = string.Empty;

            using DokumentuBerriaLaguntzailea formularioa = new DokumentuBerriaLaguntzailea();
            formularioa.Hasieratu(
                pazienteak,
                !DaPazientea(),
                bilaketa => string.IsNullOrWhiteSpace(bilaketa)
                    ? pazienteak.OrderBy(p => p.IzenOsoa).ToList()
                    : _pazienteKontrolatzailea.LortuGuztiakPazienteak(bilaketa.Trim()).OrderBy(p => p.IzenOsoa).ToList());

            if (formularioa.ShowDialog(this) != DialogResult.OK || !formularioa.PazienteId.HasValue)
            {
                return false;
            }

            pazienteId = formularioa.PazienteId.Value;
            dokumentuIzena = formularioa.DokumentuIzena;
            deskribapena = formularioa.Deskribapena;
            pdfFitxategiBidea = formularioa.PdfFitxategiBidea;
            return true;
        }

        private bool EskatuTxostenBerriarenDatuak(
            List<Pazientea> pazienteak,
            out int pazienteId,
            out string dokumentuIzena,
            out string deskribapena,
            out List<TxostenGrafikaMota> grafikaMotak,
            out DateTime? grafikaHasieraData,
            out DateTime? grafikaAmaieraData)
        {
            pazienteId = 0;
            dokumentuIzena = string.Empty;
            deskribapena = string.Empty;
            grafikaMotak = new List<TxostenGrafikaMota>();
            grafikaHasieraData = null;
            grafikaAmaieraData = null;
            using OsasunTxostenaSortuLaguntzailea formularioa = new OsasunTxostenaSortuLaguntzailea();
            formularioa.Hasieratu(
                pazienteak,
                bilaketa => string.IsNullOrWhiteSpace(bilaketa)
                    ? pazienteak.OrderBy(p => p.IzenOsoa).ToList()
                    : _pazienteKontrolatzailea.LortuGuztiakPazienteak(bilaketa.Trim()).OrderBy(p => p.IzenOsoa).ToList(),
                hautatutakoPazienteId => _jarraipenaKontrolatzailea.LortuJarraipenGuztiak(pazienteId: hautatutakoPazienteId));

            if (formularioa.ShowDialog(this) != DialogResult.OK || !formularioa.PazienteId.HasValue)
            {
                return false;
            }

            pazienteId = formularioa.PazienteId.Value;
            dokumentuIzena = formularioa.DokumentuIzena;
            deskribapena = formularioa.Deskribapena;
            grafikaMotak = formularioa.GrafikaMotak;
            grafikaHasieraData = formularioa.GrafikaHasieraData;
            grafikaAmaieraData = formularioa.GrafikaAmaieraData;
            return true;
        }

        private int? LortuOsasunLangileId()
        {
            return _erabiltzailea?.DaOsasunLangilea() == true ? _erabiltzailea.Id : null;
        }

        private void EditatuDokumentua(Dokumentua dokumentua)
        {
            using DokumentuaEditatuLaguntzailea formularioa = new DokumentuaEditatuLaguntzailea();
            formularioa.Hasieratu(dokumentua);

            if (formularioa.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            dokumentua.DokumentuIzena = formularioa.DokumentuIzena;
            dokumentua.Deskribapena = formularioa.Deskribapena;

            if (_dokumentuaKontrolatzailea.EguneratuDokumentua(dokumentua))
            {
                KargatuDokumentuak();
                MessageBox.Show("Dokumentua ondo eguneratu da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ezin izan da dokumentua eguneratu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EzabatuDokumentua(Dokumentua dokumentua)
        {
            if (!DokumentuaEzabatuLaguntzailea.Baieztatu(this, dokumentua))
            {
                return;
            }

            try
            {
                if (_dokumentuaKontrolatzailea.EzabatuDokumentua(dokumentua.Id))
                {
                    if (File.Exists(dokumentua.BideaZerbitzarian))
                    {
                        File.Delete(dokumentua.BideaZerbitzarian);
                    }

                    KargatuDokumentuak();
                    MessageBox.Show("Dokumentua ondo ezabatu da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ezin izan da dokumentua ezabatu.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea dokumentua ezabatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool DaPazientea()
        {
            return _erabiltzailea?.DaPazientea() == true;
        }

        private void _egoeraLabel_Click(object sender, EventArgs e)
        {

        }
    }
}