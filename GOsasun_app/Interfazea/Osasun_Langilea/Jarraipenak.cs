using System.Diagnostics;
using System.Drawing.Drawing2D;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class Jarraipenak : OinarriPantaila
    {
        private const string DokumentuKarpetarenBidea = @"C:\Apache24-64\htdocs\GOsasun_web\dokumentuak";
        private static readonly Size JarraipenPantailaTamaina = new Size(2700, 1394);
        private const int KanpokoMarjina = 70;
        private const int TaulaGoikoPosizioa = 285;
        private const int JarraipenFilaAltuera = 128;
        private const int EkintzaZutabeZabalera = 700;
        private const int GoiburuAltuera = 181;

        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
        private readonly List<JarraipenZerrendaItem> _jarraipenak = new List<JarraipenZerrendaItem>();

        private string _azkenOrdenazioZutabea = string.Empty;
        private bool _ordenazioGorakorra = true;

        private enum EkintzaMota
        {
            Ikusi,
            GehituDokumentua,
            IkusiDokumentuak,
            Ezabatu
        }

        public Jarraipenak() : base()
        {
            InitializeComponent();
            HasieratuPantaila();
        }

        public Jarraipenak(Erabiltzailea u) : base(u)
        {
            InitializeComponent();
            HasieratuPantaila();
        }

        private void HasieratuPantaila()
        {
            EraikiInterfazea();
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

        private void EraikiInterfazea()
        {
            _btnJarraipenBerria.FlatAppearance.BorderSize = 0;
            _txtBilatu.BackColor = Color.White;
            _txtBilatu.ForeColor = Color.FromArgb(44, 62, 80);
            _txtBilatu.BorderStyle = BorderStyle.FixedSingle;

            _dgvJarraipenak.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            _dgvJarraipenak.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _dgvJarraipenak.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            _dgvJarraipenak.ColumnHeadersHeight = 68;
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
            _dgvJarraipenak.Columns.Clear();
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("PazienteId", "Paziente ID", 115));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("PazienteNan", "NAN/DNI", 160));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("PazienteIzena", "Izena", 170));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("PazienteAbizenak", "Abizenak", 240));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("ErregistroData", "Data", 180, "g"));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("TentsioSistolikoa", "Sistolikoa", 125));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("TentsioDiastolikoa", "Diastolikoa", 125));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("PultsuaPpm", "Pultsua", 105));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("PisuaKg", "Pisua (kg)", 115, "N2"));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("Altuera", "Altuera (m)", 115, "N2"));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("DokumentuKopurua", "Dok.", 80));
            _dgvJarraipenak.Columns.Add(SortuTestuZutabea("Oharrak", "Oharrak", 260));
            _dgvJarraipenak.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Ekintzak",
                HeaderText = "EKINTZAK",
                Width = EkintzaZutabeZabalera,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true
            });
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
                DefaultCellStyle = new DataGridViewCellStyle { Format = format ?? string.Empty }
            };
        }

        private void KonfiguratuGertaerak()
        {
            _txtBilatu.TextChanged += (s, e) => KargatuJarraipenak(_txtBilatu.Text.Trim());
            _btnJarraipenBerria.Click += (s, e) => IrekiFormularioa(new NeurketaMotak(_erabiltzailea!));
            _dgvJarraipenak.ColumnHeaderMouseClick += DgvJarraipenak_ColumnHeaderMouseClick;
            _dgvJarraipenak.CellFormatting += DgvJarraipenak_CellFormatting;
            _dgvJarraipenak.CellPainting += DgvJarraipenak_CellPainting;
            _dgvJarraipenak.CellMouseClick += DgvJarraipenak_CellMouseClick;
            _dgvJarraipenak.CellMouseMove += DgvJarraipenak_CellMouseMove;
            _dgvJarraipenak.DataBindingComplete += (s, e) => _dgvJarraipenak.ClearSelection();
        }

        private void KargatuJarraipenak(string? bilaketa = null)
        {
            try
            {
                _jarraipenak.Clear();
                _jarraipenak.AddRange(_jarraipenaKontrolatzailea.LortuJarraipenGuztiak(bilaketa));
                _dgvJarraipenak.DataSource = null;
                _dgvJarraipenak.DataSource = _jarraipenak.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea jarraipenak kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void KargatuDiseinuDatuak()
        {
            _jarraipenak.Clear();
            _jarraipenak.AddRange(new[]
            {
                new JarraipenZerrendaItem
                {
                    Id = 1,
                    PazienteId = 51,
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
                new JarraipenZerrendaItem
                {
                    Id = 2,
                    PazienteId = 1,
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
                }
            });

            _dgvJarraipenak.DataSource = null;
            _dgvJarraipenak.DataSource = _jarraipenak.ToList();
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

            _dgvJarraipenak.Location = new Point(KanpokoMarjina, TaulaGoikoPosizioa);
            _dgvJarraipenak.Size = new Size(_edukiPanela.ClientSize.Width - (KanpokoMarjina * 2), _edukiPanela.ClientSize.Height - TaulaGoikoPosizioa - 40);
            _dgvJarraipenak.RowTemplate.Height = JarraipenFilaAltuera;

            if (_dgvJarraipenak.Columns.Contains("Ekintzak"))
            {
                DataGridViewColumn? ekintzakZutabea = _dgvJarraipenak.Columns["Ekintzak"];
                if (ekintzakZutabea != null)
                {
                    ekintzakZutabea.Width = EkintzaZutabeZabalera;
                }
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
            if (e.ColumnIndex < 0 || _dgvJarraipenak.Columns[e.ColumnIndex].Name == "Ekintzak") return;

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

            var pi = typeof(JarraipenZerrendaItem).GetProperty(dataPropertyName);
            if (pi == null) return;

            List<JarraipenZerrendaItem> ordenatua = _ordenazioGorakorra
                ? _jarraipenak.OrderBy(x => pi.GetValue(x, null)).ToList()
                : _jarraipenak.OrderByDescending(x => pi.GetValue(x, null)).ToList();

            _jarraipenak.Clear();
            _jarraipenak.AddRange(ordenatua);

            _dgvJarraipenak.DataSource = null;
            _dgvJarraipenak.DataSource = _jarraipenak.ToList();

            foreach (DataGridViewColumn col in _dgvJarraipenak.Columns)
            {
                col.HeaderCell.SortGlyphDirection = SortOrder.None;
            }

            _dgvJarraipenak.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = _ordenazioGorakorra ? SortOrder.Ascending : SortOrder.Descending;
        }

        private void DgvJarraipenak_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = _dgvJarraipenak.Columns[e.ColumnIndex].Name;
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
            if (e.RowIndex < 0 || _dgvJarraipenak.Columns[e.ColumnIndex].Name != "Ekintzak") return;
            if (e.Graphics == null) return;

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
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                _dgvJarraipenak.Cursor = Cursors.Default;
                return;
            }

            if (_dgvJarraipenak.Columns[e.ColumnIndex].Name != "Ekintzak")
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
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || _dgvJarraipenak.Columns[e.ColumnIndex].Name != "Ekintzak") return;

            if (!(_dgvJarraipenak.Rows[e.RowIndex].DataBoundItem is JarraipenZerrendaItem jarraipena)) return;

            Rectangle cellBounds = _dgvJarraipenak.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(cellBounds.Left + e.X, cellBounds.Top + e.Y);

            foreach (var botoia in LortuEkintzaBotoiak(cellBounds))
            {
                if (!botoia.Value.Contains(posizioa)) continue;

                switch (botoia.Key)
                {
                    case EkintzaMota.Ikusi:
                        IkusiJarraipena(jarraipena);
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
            int paddingX = 18;
            int paddingY = 14;
            int spacing = 14;
            int buttonWidth = (cellBounds.Width - (paddingX * 2) - spacing) / 2;
            int buttonHeight = (cellBounds.Height - (paddingY * 2) - spacing) / 2;

            int left = cellBounds.Left + paddingX;
            int top = cellBounds.Top + paddingY;
            int secondColumnX = left + buttonWidth + spacing;
            int secondRowY = top + buttonHeight + spacing;

            return new Dictionary<EkintzaMota, Rectangle>
            {
                [EkintzaMota.Ikusi] = new Rectangle(left, top, buttonWidth, buttonHeight),
                [EkintzaMota.GehituDokumentua] = new Rectangle(secondColumnX, top, buttonWidth, buttonHeight),
                [EkintzaMota.IkusiDokumentuak] = new Rectangle(left, secondRowY, buttonWidth, buttonHeight),
                [EkintzaMota.Ezabatu] = new Rectangle(secondColumnX, secondRowY, buttonWidth, buttonHeight)
            };
        }

        private static void MarraztuEkintzaBotoia(Graphics graphics, EkintzaMota ekintza, Rectangle rectangle)
        {
            Color kolorea = ekintza == EkintzaMota.Ezabatu ? Color.FromArgb(176, 33, 22) : Color.FromArgb(192, 57, 43);
            string testua = ekintza switch
            {
                EkintzaMota.Ikusi => "Ikusi",
                EkintzaMota.GehituDokumentua => "Gehitu",
                EkintzaMota.IkusiDokumentuak => "Dok. ikusi",
                EkintzaMota.Ezabatu => "Ezabatu",
                _ => string.Empty
            };

            using (GraphicsPath path = SortuBiribildua(rectangle, 12))
            using (SolidBrush brush = new SolidBrush(kolorea))
            using (Pen pen = new Pen(Color.White, 1))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
                graphics.DrawPath(pen, path);
                TextRenderer.DrawText(graphics, testua, new Font("Segoe UI", 10F, FontStyle.Bold), rectangle, Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
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

        private void IkusiJarraipena(JarraipenZerrendaItem jarraipena)
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
                form.Size = new Size(700, 620);
                form.StartPosition = FormStartPosition.CenterParent;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MaximizeBox = false;
                form.MinimizeBox = false;

                taula.Dock = DockStyle.Fill;
                taula.Padding = new Padding(20);
                taula.ColumnCount = 2;
                taula.RowCount = 11;
                taula.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
                taula.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

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

        private void GehituDokumentua(JarraipenZerrendaItem jarraipena)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Hautatu gehitu nahi duzun dokumentua";
                dialog.Filter = "Dokumentuak|*.pdf;*.doc;*.docx;*.png;*.jpg;*.jpeg;*.txt|Fitxategi guztiak|*.*";

                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                if (!EskatuDokumentuDatuak(Path.GetFileNameWithoutExtension(dialog.FileName), out string dokumentuIzena, out string deskribapena))
                {
                    return;
                }

                try
                {
                    Directory.CreateDirectory(DokumentuKarpetarenBidea);
                    string helmugaIzena = $"jarraipena_{jarraipena.Id}_{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(dialog.FileName)}";
                    string helmugaBidea = Path.Combine(DokumentuKarpetarenBidea, helmugaIzena);
                    File.Copy(dialog.FileName, helmugaBidea, false);

                    Dokumentua dokumentua = new Dokumentua
                    {
                        JarraipenaId = jarraipena.Id,
                        FitxategiIzena = Path.GetFileName(dialog.FileName),
                        BideaZerbitzarian = helmugaBidea,
                        DokumentuIzena = dokumentuIzena,
                        Deskribapena = string.IsNullOrWhiteSpace(deskribapena) ? null : deskribapena,
                        IgotzeData = DateTime.Now
                    };

                    if (_jarraipenaKontrolatzailea.GordeDokumentua(dokumentua))
                    {
                        MessageBox.Show("Dokumentua ondo gehitu da.", "Arrakasta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        KargatuJarraipenak(_txtBilatu.Text.Trim());
                    }
                    else
                    {
                        if (File.Exists(helmugaBidea)) File.Delete(helmugaBidea);
                        MessageBox.Show("Ezin izan da dokumentua datu-basean gorde.", "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errorea dokumentua gehitzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        private void IkusiDokumentuak(JarraipenZerrendaItem jarraipena)
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

        private void EzabatuJarraipena(JarraipenZerrendaItem jarraipena)
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
                    KargatuJarraipenak(_txtBilatu.Text.Trim());
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
