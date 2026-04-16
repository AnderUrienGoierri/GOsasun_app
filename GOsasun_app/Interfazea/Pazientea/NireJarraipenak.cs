using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GOsasun_app.Kontrola;
using GOsasun_app.Modeloa;

namespace GOsasun_app.Interfazea
{
    public partial class NireJarraipenak : OinarriPantaila
    {
        private const string DokumentuKarpetarenBidea = @"C:\Apache24-64\htdocs\GOsasun_web\dokumentuak";
        private const int EkintzaZutabeZabalera = 210;
        private const int EkintzaBotoiTamaina = 40;
        private const int EkintzaIkonoTamaina = 18;
        private const int EkintzaBotoienTartea = 12;

        private readonly JarraipenaKontrolatzailea _jarraipenaKontrolatzailea = new JarraipenaKontrolatzailea();
        private readonly DokumentuaKontrolatzailea _dokumentuaKontrolatzailea = new DokumentuaKontrolatzailea();
        private readonly List<Jarraipena> _jarraipenak = new List<Jarraipena>();
        private readonly BindingSource _bindingSource = new BindingSource();
        private Image? _ikusiIkonoa;
        private Image? _gehituDokumentuaIkonoa;
        private Image? _ikusiDokumentuakIkonoa;

        private enum EkintzaMota
        {
            Ikusi,
            GehituDokumentua,
            IkusiDokumentuak
        }

        public NireJarraipenak() : base()
        {
            InitializeComponent();
        }

        public NireJarraipenak(Erabiltzailea erabiltzailea) : base(erabiltzailea)
        {
            InitializeComponent();
            HasieratuDatuak();
        }

        private void HasieratuDatuak()
        {
            Text = "GOsasun - Nire Jarraipenak";
            labelTitulua.Text = "NIRE JARRAIPENAK";

            KargatuEkintzaIkonoak();
            KonfiguratuTaula();
            KonfiguratuGertaerak();
            KargatuIragazkiekin();
        }

        private void KargatuEkintzaIkonoak()
        {
            _ikusiIkonoa = KargatuIkonoIrudia("eye.svg", Color.White, EkintzaIkonoTamaina);
            _gehituDokumentuaIkonoa = KargatuIkonoIrudia("plus-circle.svg", Color.White, EkintzaIkonoTamaina);
            _ikusiDokumentuakIkonoa = KargatuIkonoIrudia("file-text.svg", Color.White, EkintzaIkonoTamaina);
        }

        private void KonfiguratuTaula()
        {
            dgvHistoriala.AutoGenerateColumns = false;
            dgvHistoriala.Columns.Clear();
            dgvHistoriala.DataSource = _bindingSource;

            dgvHistoriala.Columns.AddRange(
                SortuTextuZutabea("ErregistroData", "Data eta ordua", 220, "yyyy/MM/dd HH:mm"),
                SortuTextuZutabea("TentsioSistolikoa", "Sistole (mmHg)", 150),
                SortuTextuZutabea("TentsioDiastolikoa", "Diastole (mmHg)", 150),
                SortuTextuZutabea("PultsuaPpm", "Pultsua (ppm)", 135),
                SortuTextuZutabea("PisuaKg", "Pisua (kg)", 130, "N2"),
                SortuTextuZutabea("Altuera", "Altuera (m)", 130, "N2"),
                SortuTextuZutabea("Oharrak", "Oharrak", 470),
                SortuEkintzaZutabea());

            foreach (DataGridViewColumn zutabea in dgvHistoriala.Columns)
            {
                zutabea.ReadOnly = true;
                zutabea.DefaultCellStyle.NullValue = "-";
                zutabea.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                zutabea.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DataGridViewColumn? oharrakZutabea = BilatuZutabea("Oharrak");
            if (oharrakZutabea != null)
            {
                oharrakZutabea.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                oharrakZutabea.MinimumWidth = 260;
                oharrakZutabea.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private static DataGridViewTextBoxColumn SortuTextuZutabea(string dataPropertyName, string headerText, int width, string? formatua = null)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataPropertyName,
                Name = dataPropertyName,
                HeaderText = headerText,
                Width = width,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = formatua ?? string.Empty,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
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
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
        }

        private void KonfiguratuGertaerak()
        {
            btnNeurketaBerria.Click += (s, e) => IrekiFormularioa(new JarraipenMotak(_erabiltzailea!));
            txtBilatuOharrak.TextChanged += (s, e) => KargatuIragazkiekin();
            dtpHasieraData.ValueChanged += (s, e) => KargatuIragazkiekin();
            dtpAmaieraData.ValueChanged += (s, e) => KargatuIragazkiekin();
            btnGarbituFiltroa.Click += (s, e) => GarbituFiltroak();
            dgvHistoriala.CellFormatting += DgvHistoriala_CellFormatting;
            dgvHistoriala.CellPainting += DgvHistoriala_CellPainting;
            dgvHistoriala.CellMouseMove += DgvHistoriala_CellMouseMove;
            dgvHistoriala.CellMouseClick += DgvHistoriala_CellMouseClick;
            dgvHistoriala.DataBindingComplete += (s, e) => dgvHistoriala.ClearSelection();
        }

        private void GarbituFiltroak()
        {
            txtBilatuOharrak.Text = string.Empty;
            dtpHasieraData.Checked = false;
            dtpAmaieraData.Checked = false;
            KargatuIragazkiekin();
        }

        private void KargatuIragazkiekin()
        {
            if (_erabiltzailea == null) return;

            (DateTime? hasieraData, DateTime? amaieraData) = LortuDataTartea();
            string? bilaketa = string.IsNullOrWhiteSpace(txtBilatuOharrak.Text) ? null : txtBilatuOharrak.Text.Trim();

            KargatuHistoriala(bilaketa, hasieraData, amaieraData);
        }

        private (DateTime? HasieraData, DateTime? AmaieraData) LortuDataTartea()
        {
            DateTime? hasieraData = dtpHasieraData.Checked ? dtpHasieraData.Value.Date : null;
            DateTime? amaieraData = dtpAmaieraData.Checked ? dtpAmaieraData.Value.Date : null;

            if (hasieraData.HasValue && amaieraData.HasValue && hasieraData.Value > amaieraData.Value)
            {
                (hasieraData, amaieraData) = (amaieraData, hasieraData);
            }

            return (hasieraData, amaieraData);
        }

        private void KargatuHistoriala(string? bilaketa = null, DateTime? hasieraData = null, DateTime? amaieraData = null)
        {
            if (_erabiltzailea == null) return;

            try
            {
                _jarraipenak.Clear();
                _jarraipenak.AddRange(_jarraipenaKontrolatzailea.LortuJarraipenGuztiak(bilaketa, hasieraData, amaieraData, _erabiltzailea.Id));
                _bindingSource.DataSource = null;
                _bindingSource.DataSource = _jarraipenak.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errorea historiala kargatzean: " + ex.Message, "Errorea", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvHistoriala_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = ZutabeGakoa(dgvHistoriala.Columns[e.ColumnIndex]);
            bool arriskutsua = columnName switch
            {
                "TentsioSistolikoa" => BalioOsasungaitza(e.Value, 90, 140),
                "TentsioDiastolikoa" => BalioOsasungaitza(e.Value, 60, 90),
                "PultsuaPpm" => BalioOsasungaitza(e.Value, 50, 100),
                _ => false
            };

            if (!arriskutsua) return;

            e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
            e.CellStyle.Font = new Font(dgvHistoriala.Font, FontStyle.Bold);
        }

        private static bool BalioOsasungaitza(object? value, int minimoNormala, int maximoNormala)
        {
            if (value == null || value == DBNull.Value) return false;
            if (!int.TryParse(value.ToString(), out int zenbakia)) return false;
            return zenbakia < minimoNormala || zenbakia >= maximoNormala;
        }

        private void DgvHistoriala_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || !EkintzakZutabeaDa(e.ColumnIndex) || e.Graphics == null) return;

            e.PaintBackground(e.CellBounds, true);
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);

            foreach (KeyValuePair<EkintzaMota, Rectangle> botoia in LortuEkintzaBotoiak(e.CellBounds))
            {
                MarraztuEkintzaBotoia(e.Graphics, botoia.Key, botoia.Value);
            }

            e.Handled = true;
        }

        private void DgvHistoriala_CellMouseMove(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !EkintzakZutabeaDa(e.ColumnIndex))
            {
                dgvHistoriala.Cursor = Cursors.Default;
                return;
            }

            Rectangle cellBounds = dgvHistoriala.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(cellBounds.Left + e.X, cellBounds.Top + e.Y);
            bool botoiGaindian = LortuEkintzaBotoiak(cellBounds).Values.Any(rectangle => rectangle.Contains(posizioa));
            dgvHistoriala.Cursor = botoiGaindian ? Cursors.Hand : Cursors.Default;
        }

        private void DgvHistoriala_CellMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || !EkintzakZutabeaDa(e.ColumnIndex)) return;
            if (!(dgvHistoriala.Rows[e.RowIndex].DataBoundItem is Jarraipena jarraipena)) return;

            Rectangle cellBounds = dgvHistoriala.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            Point posizioa = new Point(cellBounds.Left + e.X, cellBounds.Top + e.Y);

            foreach (KeyValuePair<EkintzaMota, Rectangle> botoia in LortuEkintzaBotoiak(cellBounds))
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
                }

                break;
            }
        }

        private bool EkintzakZutabeaDa(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= dgvHistoriala.Columns.Count) return false;

            string gakoa = ZutabeGakoa(dgvHistoriala.Columns[columnIndex]);
            return string.Equals(gakoa, "Ekintzak", StringComparison.OrdinalIgnoreCase)
                || string.Equals(gakoa, "EkintzakTestua", StringComparison.OrdinalIgnoreCase);
        }

        private static string ZutabeGakoa(DataGridViewColumn zutabea)
        {
            if (!string.IsNullOrWhiteSpace(zutabea.DataPropertyName)) return zutabea.DataPropertyName;
            return zutabea.Name;
        }

        private DataGridViewColumn? BilatuZutabea(string gakoa)
        {
            foreach (DataGridViewColumn zutabea in dgvHistoriala.Columns)
            {
                if (string.Equals(ZutabeGakoa(zutabea), gakoa, StringComparison.OrdinalIgnoreCase))
                {
                    return zutabea;
                }
            }

            return null;
        }

        private static Dictionary<EkintzaMota, Rectangle> LortuEkintzaBotoiak(Rectangle cellBounds)
        {
            int totalWidth = (EkintzaBotoiTamaina * 3) + (EkintzaBotoienTartea * 2);
            int left = cellBounds.Left + Math.Max(10, (cellBounds.Width - totalWidth) / 2);
            int top = cellBounds.Top + Math.Max(6, (cellBounds.Height - EkintzaBotoiTamaina) / 2);

            return new Dictionary<EkintzaMota, Rectangle>
            {
                [EkintzaMota.Ikusi] = new Rectangle(left, top, EkintzaBotoiTamaina, EkintzaBotoiTamaina),
                [EkintzaMota.GehituDokumentua] = new Rectangle(left + EkintzaBotoiTamaina + EkintzaBotoienTartea, top, EkintzaBotoiTamaina, EkintzaBotoiTamaina),
                [EkintzaMota.IkusiDokumentuak] = new Rectangle(left + ((EkintzaBotoiTamaina + EkintzaBotoienTartea) * 2), top, EkintzaBotoiTamaina, EkintzaBotoiTamaina)
            };
        }

        private void MarraztuEkintzaBotoia(Graphics graphics, EkintzaMota ekintza, Rectangle rectangle)
        {
            Image? ikonoa = ekintza switch
            {
                EkintzaMota.Ikusi => _ikusiIkonoa,
                EkintzaMota.GehituDokumentua => _gehituDokumentuaIkonoa,
                EkintzaMota.IkusiDokumentuak => _ikusiDokumentuakIkonoa,
                _ => null
            };

            string fallback = ekintza switch
            {
                EkintzaMota.Ikusi => "I",
                EkintzaMota.GehituDokumentua => "+",
                EkintzaMota.IkusiDokumentuak => "D",
                _ => string.Empty
            };

            using (GraphicsPath path = SortuBiribildua(rectangle, 12))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(192, 57, 43)))
            using (Pen pen = new Pen(Color.White, 1))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
                graphics.DrawPath(pen, path);

                if (ikonoa != null)
                {
                    int iconLeft = rectangle.Left + ((rectangle.Width - ikonoa.Width) / 2);
                    int iconTop = rectangle.Top + ((rectangle.Height - ikonoa.Height) / 2);
                    graphics.DrawImage(ikonoa, new Rectangle(iconLeft, iconTop, ikonoa.Width, ikonoa.Height));
                }
                else
                {
                    TextRenderer.DrawText(graphics, fallback, new Font("Segoe UI", 10F, FontStyle.Bold), rectangle, Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
                }
            }
        }

        private static GraphicsPath SortuBiribildua(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diametroa = radius * 2;

            path.AddArc(bounds.Left, bounds.Top, diametroa, diametroa, 180, 90);
            path.AddArc(bounds.Right - diametroa, bounds.Top, diametroa, diametroa, 270, 90);
            path.AddArc(bounds.Right - diametroa, bounds.Bottom - diametroa, diametroa, diametroa, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - diametroa, diametroa, diametroa, 90, 90);
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
                taula.RowCount = 10;
                taula.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
                taula.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68));

                GehituXehetasunLerroa(taula, 0, "Pazientea", _erabiltzailea?.IzenOsoa ?? jarraipena.PazienteIzenOsoa);
                GehituXehetasunLerroa(taula, 1, "NAN/DNI", _erabiltzailea?.Nan ?? jarraipena.PazienteNan);
                GehituXehetasunLerroa(taula, 2, "Erregistro data", xehetasuna.ErregistroData.ToString("g"));
                GehituXehetasunLerroa(taula, 3, "Tentsio sistolikoa", BalioaTestuan(xehetasuna.TentsioSistolikoa));
                GehituXehetasunLerroa(taula, 4, "Tentsio diastolikoa", BalioaTestuan(xehetasuna.TentsioDiastolikoa));
                GehituXehetasunLerroa(taula, 5, "Pultsua", BalioaTestuan(xehetasuna.PultsuaPpm));
                GehituXehetasunLerroa(taula, 6, "Pisua", BalioaTestuan(xehetasuna.PisuaKg, "N2", " kg"));
                GehituXehetasunLerroa(taula, 7, "Altuera", BalioaTestuan(xehetasuna.Altuera, "N2", " m"));
                GehituXehetasunLerroa(taula, 8, "XML bidea", xehetasuna.BideaZerbitzarian ?? "-");

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
                taula.Controls.Add(lblOharrak, 0, 9);
                taula.Controls.Add(txtOharrak, 1, 9);

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

        private void GehituDokumentua(Jarraipena jarraipena)
        {
            List<Dokumentua> dokumentuak = _dokumentuaKontrolatzailea.LortuPazientearenBesteDokumentuak(jarraipena.PazienteId, jarraipena.Id);
            if (dokumentuak.Count == 0)
            {
                MessageBox.Show("Ez duzu beste dokumentu erregistraturik aukeratzeko.", "Informazioa", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                Text = "Aukeratu zure beste dokumentu erregistratuetako bat jarraipen honi lotzeko."
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
                BackColor = Color.FromArgb(41, 128, 185),
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
                dgv.Columns.Add(new DataGridViewButtonColumn { HeaderText = string.Empty, Name = "btnIreki", Text = "Ireki", UseColumnTextForButtonValue = true, Width = 90 });
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

        private void IrekiFormularioa(Form formularioa)
        {
            formularioa.FormClosed += (s, e) => {
                this.Show();
                KargatuIragazkiekin();
            };
            this.Hide();
            formularioa.Show();
        }
    }
}
