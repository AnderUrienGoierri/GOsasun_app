using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;
using System.Diagnostics;

namespace GOsasun_app.Interfazea
{
    public partial class Dokumentuak : OinarriPantaila
    {
        private const int EkintzaIkonoTamaina = 20;
        private const int EkintzaBotoiTamaina = 40;
        private const int EkintzaBotoienTartea = 12;

        private readonly DokumentuaKontrolatzailea _dokumentuaKontrolatzailea = new DokumentuaKontrolatzailea();
        private readonly ErabiltzaileKontrolatzailea _erabiltzaileKontrolatzailea = new ErabiltzaileKontrolatzailea();
        private readonly BindingSource _bindingSource = new BindingSource();
        private readonly List<Dokumentua> _dokumentuak = new List<Dokumentua>();
        private Image? _dokumentuBerriaIkonoa;
        private Image? _ikusiIkonoa;
        private Image? _editatuIkonoa;
        private Image? _ezabatuIkonoa;
        private string _azkenOrdenazioZutabea = "IgotzeData";
        private bool _ordenazioGorakorra;

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
            KargatuDokumentuak();
        }

        private void HasieratuPantaila()
        {
            Text = "GOsasun - Dokumentuak";

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
            _dokumentuBerriaBotoia.Click += (s, e) => SortuDokumentuBerria();
            _dokumentuakGrid.DataSource = _bindingSource;

            KargatuEkintzaIkonoak();
            _dokumentuBerriaBotoia.Image = _dokumentuBerriaIkonoa;

            KonfiguratuZutabeak();
        }

        private void KargatuEkintzaIkonoak()
        {
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

            List<Dokumentua> dokumentuak = _dokumentuaKontrolatzailea
                .LortuDokumentuak(bilaketa, pazienteId: pazienteId)
                .Where(dokumentua => DataTarteanDago(dokumentua.IgotzeData, hasieraData, amaieraData))
                .ToList();

            _dokumentuak.Clear();
            _dokumentuak.AddRange(AplikatuOrdenazioa(dokumentuak));
            _bindingSource.DataSource = null;
            _bindingSource.DataSource = _dokumentuak.ToList();
            EzarriOrdenazioIkurra();

            _egoeraLabel.Text = _dokumentuak.Count == 0
                ? "Ez da dokumenturik aurkitu."
                : _dokumentuak.Count == 1
                    ? "Dokumentu 1 aurkitu da."
                    : $"{_dokumentuak.Count} dokumentu aurkitu dira.";
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

        private List<Pazientea> LortuEskuragarriPazienteak()
        {
            if (_erabiltzailea == null)
            {
                return new List<Pazientea>();
            }

            if (DaPazientea())
            {
                Pazientea? pazientea = _erabiltzailea as Pazientea ?? _erabiltzaileKontrolatzailea.LortuPazientea(_erabiltzailea.Id);
                return pazientea == null ? new List<Pazientea>() : new List<Pazientea> { pazientea };
            }

            return _erabiltzailea.DaOsasunLangilea()
                ? _erabiltzaileKontrolatzailea.LortuLangilearenPazienteak(_erabiltzailea.Id).OrderBy(p => p.IzenOsoa).ToList()
                : _erabiltzaileKontrolatzailea.LortuGuztiakPazienteak().OrderBy(p => p.IzenOsoa).ToList();
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

            using Form formularioa = new Form();
            formularioa.Text = "Dokumentu berria";
            formularioa.StartPosition = FormStartPosition.CenterParent;
            formularioa.ClientSize = new Size(680, 430);
            formularioa.FormBorderStyle = FormBorderStyle.FixedDialog;
            formularioa.MaximizeBox = false;
            formularioa.MinimizeBox = false;

            int unekoY = 24;
            ComboBox? pazienteakComboBox = null;

            if (!DaPazientea())
            {
                if (pazienteak.Count == 0)
                {
                    MessageBox.Show(this, "Ez dago dokumentua lotzeko pazienterik eskuragarri.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                Label pazienteaLabel = new Label
                {
                    Text = "Pazientea",
                    Location = new Point(24, unekoY),
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                pazienteakComboBox = new ComboBox
                {
                    Location = new Point(24, unekoY + 30),
                    Size = new Size(632, 40),
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Font = new Font("Segoe UI", 10F),
                    DataSource = pazienteak,
                    DisplayMember = nameof(Pazientea.IzenOsoa)
                };

                formularioa.Controls.Add(pazienteaLabel);
                formularioa.Controls.Add(pazienteakComboBox);
                unekoY += 82;
            }

            Label izenaLabel = new Label
            {
                Text = "Dokumentu izena",
                Location = new Point(24, unekoY),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            TextBox izenaTextBox = new TextBox
            {
                Location = new Point(24, unekoY + 30),
                Width = 632,
                Font = new Font("Segoe UI", 10F)
            };

            unekoY += 82;

            Label pdfLabel = new Label
            {
                Text = "PDF fitxategia",
                Location = new Point(24, unekoY),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            TextBox pdfTextBox = new TextBox
            {
                Location = new Point(24, unekoY + 30),
                Width = 486,
                ReadOnly = true,
                Font = new Font("Segoe UI", 10F)
            };
            Button pdfHautatuBotoia = new Button
            {
                Text = "PDF hautatu",
                Location = new Point(522, unekoY + 28),
                Size = new Size(134, 42),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            pdfHautatuBotoia.FlatAppearance.BorderSize = 0;
            pdfHautatuBotoia.Click += (s, e) =>
            {
                using OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = "Hautatu PDF dokumentua";
                dialog.Filter = "PDF dokumentuak|*.pdf";
                dialog.Multiselect = false;

                if (dialog.ShowDialog(formularioa) != DialogResult.OK)
                {
                    return;
                }

                pdfTextBox.Text = dialog.FileName;
                if (string.IsNullOrWhiteSpace(izenaTextBox.Text))
                {
                    izenaTextBox.Text = Path.GetFileNameWithoutExtension(dialog.FileName);
                }
            };

            unekoY += 82;

            Label deskribapenaLabel = new Label
            {
                Text = "Deskribapena",
                Location = new Point(24, unekoY),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            TextBox deskribapenaTextBox = new TextBox
            {
                Location = new Point(24, unekoY + 30),
                Width = 632,
                Height = 110,
                Multiline = true,
                Font = new Font("Segoe UI", 10F)
            };

            Button btnGorde = new Button
            {
                Text = "Gorde",
                Location = new Point(464, 372),
                Size = new Size(92, 42),
                BackColor = Color.FromArgb(83, 148, 117),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnGorde.FlatAppearance.BorderSize = 0;

            Button btnUtzi = new Button
            {
                Text = "Utzi",
                Location = new Point(564, 372),
                Size = new Size(92, 42),
                DialogResult = DialogResult.Cancel
            };

            btnGorde.Click += (s, e) =>
            {
                if (!DaPazientea() && pazienteakComboBox?.SelectedItem is not Pazientea)
                {
                    MessageBox.Show(formularioa, "Paziente bat hautatu behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(izenaTextBox.Text))
                {
                    MessageBox.Show(formularioa, "Dokumentuaren izena bete behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    izenaTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(pdfTextBox.Text) || !File.Exists(pdfTextBox.Text))
                {
                    MessageBox.Show(formularioa, "PDF fitxategi baliozko bat hautatu behar duzu.", "Abisua", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                formularioa.DialogResult = DialogResult.OK;
                formularioa.Close();
            };

            formularioa.Controls.Add(izenaLabel);
            formularioa.Controls.Add(izenaTextBox);
            formularioa.Controls.Add(pdfLabel);
            formularioa.Controls.Add(pdfTextBox);
            formularioa.Controls.Add(pdfHautatuBotoia);
            formularioa.Controls.Add(deskribapenaLabel);
            formularioa.Controls.Add(deskribapenaTextBox);
            formularioa.Controls.Add(btnGorde);
            formularioa.Controls.Add(btnUtzi);
            formularioa.AcceptButton = btnGorde;
            formularioa.CancelButton = btnUtzi;

            if (formularioa.ShowDialog(this) != DialogResult.OK)
            {
                return false;
            }

            pazienteId = DaPazientea()
                ? _erabiltzailea!.Id
                : ((Pazientea)pazienteakComboBox!.SelectedItem!).Id;
            dokumentuIzena = izenaTextBox.Text.Trim();
            deskribapena = deskribapenaTextBox.Text.Trim();
            pdfFitxategiBidea = pdfTextBox.Text.Trim();
            return true;
        }

        private int? LortuOsasunLangileId()
        {
            return _erabiltzailea?.DaOsasunLangilea() == true ? _erabiltzailea.Id : null;
        }

        private void EditatuDokumentua(Dokumentua dokumentua)
        {
            using Form formularioa = new Form();
            formularioa.Text = "Dokumentua editatu";
            formularioa.StartPosition = FormStartPosition.CenterParent;
            formularioa.ClientSize = new Size(620, 330);
            formularioa.FormBorderStyle = FormBorderStyle.FixedDialog;
            formularioa.MaximizeBox = false;
            formularioa.MinimizeBox = false;

            Label lblDokumentuIzena = new Label { Text = "Dokumentu izena", Location = new Point(24, 24), AutoSize = true };
            TextBox txtDokumentuIzena = new TextBox { Location = new Point(24, 52), Width = 560, Text = dokumentua.DokumentuIzena ?? string.Empty };
            Label lblDeskribapena = new Label { Text = "Deskribapena", Location = new Point(24, 104), AutoSize = true };
            TextBox txtDeskribapena = new TextBox { Location = new Point(24, 132), Width = 560, Height = 88, Multiline = true, Text = dokumentua.Deskribapena ?? string.Empty };

            Button btnGorde = new Button
            {
                Text = "Gorde",
                Location = new Point(392, 250),
                Size = new Size(92, 42),
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(83, 148, 117),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnGorde.FlatAppearance.BorderSize = 0;

            Button btnUtzi = new Button
            {
                Text = "Utzi",
                Location = new Point(492, 250),
                Size = new Size(92, 42),
                DialogResult = DialogResult.Cancel
            };

            formularioa.Controls.Add(lblDokumentuIzena);
            formularioa.Controls.Add(txtDokumentuIzena);
            formularioa.Controls.Add(lblDeskribapena);
            formularioa.Controls.Add(txtDeskribapena);
            formularioa.Controls.Add(btnGorde);
            formularioa.Controls.Add(btnUtzi);
            formularioa.AcceptButton = btnGorde;
            formularioa.CancelButton = btnUtzi;

            if (formularioa.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            dokumentua.DokumentuIzena = string.IsNullOrWhiteSpace(txtDokumentuIzena.Text) ? null : txtDokumentuIzena.Text.Trim();
            dokumentua.Deskribapena = string.IsNullOrWhiteSpace(txtDeskribapena.Text) ? null : txtDeskribapena.Text.Trim();

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
            DialogResult erantzuna = MessageBox.Show(
                $"Ziur zaude '{dokumentua.DokumentuIzena ?? dokumentua.FitxategiIzena}' dokumentua ezabatu nahi duzula?",
                "Dokumentua ezabatu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (erantzuna != DialogResult.Yes)
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